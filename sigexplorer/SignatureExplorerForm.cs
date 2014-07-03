using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using BlobSync.Datatypes;
using BlobSync.Helpers;

namespace sigexplorer
{
    /// <summary>
    /// Hacky little util for displaying signatures.
    /// </summary>
    public partial class SignatureExplorerForm : Form
    {

        SizeBasedCompleteSignature sig1;
        SizeBasedCompleteSignature sig2;

        int sig1Total;
        int sig2Total;
        string sig1Filename;
        string sig2Filename;

        long file1Size;
        long file2Size;
        long bothFilesShared;

        public SignatureExplorerForm()
        {
            InitializeComponent();
            sigTreeView.AllowDrop = true;
            sigTreeView2.AllowDrop = true;
            sigTreeView.ShowNodeToolTips = true;
            sigTreeView2.ShowNodeToolTips = true;
            this.AllowDrop = true;
            sigTreeView.DoubleClick += new EventHandler(SigTreeViewClick);
            sigTreeView.DragDrop += new DragEventHandler(SignatureExplorerFormDragDrop);
            sigTreeView.DragEnter += new DragEventHandler(SignatureExplorerFormDragEnter);
            
            sigTreeView2.DoubleClick += new EventHandler(SigTreeViewClick2);
            sigTreeView2.DragDrop += new DragEventHandler(SignatureExplorerFormDragDrop2);
            sigTreeView2.DragEnter += new DragEventHandler(SignatureExplorerFormDragEnter);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.AllowDrop = true;
            //this.DragDrop += new DragEventHandler(SignatureExplorerFormDragDrop);
            //this.DragEnter += new DragEventHandler(SignatureExplorerFormDragEnter);
        }


        void SignatureExplorerFormDragDrop(object sender, DragEventArgs e)
        {
            
            string[] filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));

            if (filePaths.Length > 0)
            {
                file1Label.Text = filePaths[0];
                LoadSigFile(filePaths[0], ref sig1);
            }

        }

        void SignatureExplorerFormDragDrop2(object sender, DragEventArgs e)
        {

            string[] filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));

            if (filePaths.Length > 0)
            {
                file2Label.Text = filePaths[0];
                LoadSigFile(filePaths[0], ref sig2);
            }

        }

        private void SignatureExplorerFormDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void SigTreeViewClick(object sender, EventArgs e)
        {

            var selectedNode = sigTreeView.SelectedNode;
            ProcessDoubleClick(selectedNode, sig1);
        }

        private void SigTreeViewClick2(object sender, EventArgs e)
        {

            var selectedNode = sigTreeView2.SelectedNode;
            ProcessDoubleClick(selectedNode, sig2);
        }

        private void ProcessDoubleClick(TreeNode selectedNode, SizeBasedCompleteSignature sig)
        {

            if (selectedNode != null && selectedNode.Parent != null)
            {
                var sp = selectedNode.Parent.Text.Split();
                var sigSize = Convert.ToInt32(sp[0]);
                var offset = Convert.ToUInt32(selectedNode.Text.Split()[0]);

                var specificSig = (from s in sig.Signatures[sigSize].SignatureList where s.Offset == offset select s).First<BlockSignature>();

                var md5String = ByteArrayToString(specificSig.MD5Signature);

                var rollingSig = string.Format("{0}:{1}", specificSig.RollingSig.Sig1, specificSig.RollingSig.Sig2);

                var msg = string.Format("Offset: {0}\nSize: {1}\nRollingSig: {2}\nMD5: {3}", specificSig.Offset.ToString(),
                                        specificSig.Size.ToString(), rollingSig, md5String);

                var dialog = MessageBox.Show(msg);
            }
        }

        
        private List< Tuple<byte[], long>> GenerateMD5ListFromSig(SizeBasedCompleteSignature? sig)
        {
            
            var md5List = new List< Tuple<byte[], long>>();

            if (sig.HasValue && sig.Value.Signatures != null)
            {
                foreach (var size in sig.Value.Signatures.Keys)
                {
                    foreach (var sSig in sig.Value.Signatures[size].SignatureList)
                    {
                        var tuple = new Tuple<byte[], long>(sSig.MD5Signature, sSig.Offset);
                        md5List.Add(tuple);
                    }
                }
            }
            return md5List;
        }

        private void PopulateSignatureTreeBySize(TreeView treeView, SizeBasedCompleteSignature sig, SizeBasedCompleteSignature? otherSig)
        {

            var sigList = new List<BlockSignature>();
            if (sig.Signatures != null)
            {
                foreach (var size in sig.Signatures.Keys)
                {
                    foreach (var sSig in sig.Signatures[size].SignatureList)
                    {
                        sigList.Add(sSig);
                    }
                }

                var sortedSigList = (from s in sigList orderby s.Size select s).ToList<BlockSignature>();

                sortedSigList = sortedSigList.Reverse<BlockSignature>().ToList<BlockSignature>();

                var otherSigMD5List = GenerateMD5ListFromSig(otherSig);
                PopulateSignatureTreeFromList(treeView, sortedSigList, otherSigMD5List);
            }
        }

        private void PopulateSignatureTreeByOffset(TreeView treeView, SizeBasedCompleteSignature sig, SizeBasedCompleteSignature? otherSig)
        {
            var sigList = new List<BlockSignature>();

            if (sig.Signatures != null)
            {
                foreach (var size in sig.Signatures.Keys)
                {
                    foreach (var sSig in sig.Signatures[size].SignatureList)
                    {
                        sigList.Add(sSig);
                    }
                }

                var sortedSigList = (from s in sigList orderby s.Offset select s).ToList<BlockSignature>();

                var otherSigMD5List = GenerateMD5ListFromSig(otherSig);

                PopulateSignatureTreeFromList(treeView, sortedSigList, otherSigMD5List);
            }

        }

        private bool DoesMD5ExistInList(byte[] md5, List<Tuple<byte[], uint>> md5List)
        {
            var res = false;
            foreach (var m in md5List)
            {
                if (m.Item1.SequenceEqual(md5))
                {
                    res = true;
                    break;
                }
            }

            return res;
        }


        private void PopulateSignatureTreeFromList(TreeView treeView, List<BlockSignature> sortedSigList, List<Tuple<byte[], long>> otherSigMD5List)
        {
            treeView.Nodes.Clear();
            uint lastSize = 0;
            TreeNode currentNode = null;

            var md5Dict = GenerateMD5Dict(otherSigMD5List);
            long size = 0;
            foreach (var s in sortedSigList)
            {
                // if new size, then add new root node.
                if (s.Size != lastSize)
                {

                    if (currentNode != null)
                    {
                        currentNode.Text = string.Format("{0} ({1})", lastSize, currentNode.Nodes.Count);
                    }

                    currentNode = treeView.Nodes.Add(s.Size.ToString());
                    lastSize = s.Size;
                }

                TreeNode smallNode;
                if (otherSigMD5List != null && otherSigMD5List.Count > 0)
                {
                    file2Size += s.Size;
                   
                    var md5Str = ByteArrayToString(s.MD5Signature);
                    if (md5Dict.ContainsKey(md5Str))
                    {
                        smallNode = currentNode.Nodes.Add(s.Offset.ToString() + " : " + md5Dict[md5Str].ToString() );
                        bothFilesShared += s.Size;

                        smallNode.ForeColor = Color.Green;
                        //smallNode.Text = "foo";
                        // smallNode.Text + " : " + "foo";
                        //md5Dict[md5Str].ToString();
                    }
                    else
                    {
                        smallNode = currentNode.Nodes.Add(s.Offset.ToString());
                    }
                }
                else
                {
                    // othersig == null means this is first tree. So just add to file1Size
                    file1Size += s.Size;

                    smallNode = currentNode.Nodes.Add(s.Offset.ToString());
                }

                var md5String = ByteArrayToString(s.MD5Signature);
                var rollingSig = string.Format("{0}:{1}", s.RollingSig.Sig1, s.RollingSig.Sig2);
                var msg = string.Format("Offset: {0}\nSize: {1}\nRollingSig: {2}\nMD5: {3}", s.Offset.ToString(),
                                        s.Size.ToString(),rollingSig , md5String);


                smallNode.ToolTipText = msg;
               
                
            }
            currentNode.Text = string.Format("{0} ({1})", lastSize, currentNode.Nodes.Count);
            
        }

        private string ByteArrayToString(byte[] byteArray)
        {
            var sb = new StringBuilder();
            sb.Clear();
            foreach (var b in byteArray)
            {
                sb.AppendFormat("{0:X2}", b);
            }

            return sb.ToString();
        }

        private Dictionary<string, long> GenerateMD5Dict(List<Tuple<byte[], long>> otherSigMD5List)
        {
            var res = new Dictionary<string, long>();
            var sb = new StringBuilder();
            foreach (var i in otherSigMD5List)
            {
                var md5String = ByteArrayToString(i.Item1);
                res[md5String] = i.Item2;
            }
            return res;
        }

        private void PopulateSignatureTree( bool performOffsetSort = false )
        {
            sigTreeView.Nodes.Clear();
            sigTreeView2.Nodes.Clear();

            file1Size = 0;
            file2Size = 0;
            bothFilesShared = 0;

            if (!performOffsetSort)
            {
                PopulateSignatureTreeBySize( sigTreeView, sig1, null);
                PopulateSignatureTreeBySize( sigTreeView2, sig2, sig1);
            }
            else
            {
                PopulateSignatureTreeByOffset(sigTreeView, sig1, null);
                PopulateSignatureTreeByOffset( sigTreeView2, sig2, sig1);
            }

            file1TotalSize.Text = file1Size.ToString("N0");
            file2TotalSize.Text = file2Size.ToString("N0");
            sharedSize.Text = bothFilesShared.ToString("N0");
            newSize.Text = (file2Size - bothFilesShared).ToString("N0");
            sigTreeView.Update();
            sigTreeView2.Update();
        }


        /// <summary>
        /// Quick check to make sure we dont have any dupe md5's
        /// </summary>
        /// <param name="sig"></param>
        /// <returns></returns>
        private Boolean VerifySignature(SizeBasedCompleteSignature sig)
        {
            var myDict = new Dictionary<string, int>();
            var valid = true;
            var count = 0;
            foreach (var size in sig.Signatures.Keys)
            {
                foreach (var sSig in sig.Signatures[size].SignatureList)
                {
                    var md5 = sSig.MD5Signature;
                    var md5Str = ByteArrayToString(md5);
                    if (myDict.ContainsKey( md5Str) )
                    {
                        valid = false;
                        count++;
                    }
                    else
                    {
                        myDict[md5Str] = 1;
                    }



                }
            }

            return valid;
        }

        /// <summary>
        /// Loads the sig file.
        /// </summary>
        /// <param name="filename"></param>
        private void LoadSigFile(string filename, ref SizeBasedCompleteSignature sig)
        {
            using (var fs = new FileStream(filename, FileMode.Open))
            {
                sig = SerializationHelper.ReadSizeBasedBinarySignature( fs );

                VerifySignature(sig);
            }

            file1Size = 0;
            file2Size = 0;
            bothFilesShared = 0;
            PopulateSignatureTree();

        }

        /// <summary>
        /// Opens file picker. Then loads sig.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadSigButton_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.ShowDialog();
            var fn = dialog.FileName;

            //LoadSigFile(fn);

            
        }

        private void sigTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void OffsetSortButton_CheckedChanged(object sender, EventArgs e)
        {
            PopulateSignatureTree(true);
        }

        private void SizeSortButton_CheckedChanged(object sender, EventArgs e)
        {
            PopulateSignatureTree(false);
        }

        private void sigTreeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void file1TotalSize_Click(object sender, EventArgs e)
        {

        }

        private void file1Label_Click(object sender, EventArgs e)
        {

        }
    }
}
