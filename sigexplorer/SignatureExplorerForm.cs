using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AzureRsyncClient.MiscUtils;
using AzureRsyncClient.Common;
using System.IO;

namespace sigexplorer
{
    /// <summary>
    /// Hacky little util for displaying signatures.
    /// </summary>
    public partial class SignatureExplorerForm : Form
    {

        SizeBasedCompleteSignature sig;

        public SignatureExplorerForm()
        {
            InitializeComponent();
            sigTreeView.AllowDrop = true;
            this.AllowDrop = true;
            sigTreeView.DoubleClick += new EventHandler(SigTreeViewClick);
            sigTreeView.DragDrop += new DragEventHandler(SignatureExplorerFormDragDrop);
            sigTreeView.DragEnter += new DragEventHandler(SignatureExplorerFormDragEnter);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.AllowDrop = true;
            this.DragDrop += new DragEventHandler(SignatureExplorerFormDragDrop);
            this.DragEnter += new DragEventHandler(SignatureExplorerFormDragEnter);
        }

        void SignatureExplorerFormDragDrop(object sender, DragEventArgs e)
        {
            
            string[] filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));

            if (filePaths.Length > 0)
            {

                LoadSigFile(filePaths[0]);
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

            if (selectedNode != null && selectedNode.Parent != null)
            {
                var sp = selectedNode.Parent.Text.Split();
                var sigSize = Convert.ToInt32(sp[0]);
                var offset = Convert.ToUInt32(selectedNode.Text);

                var specificSig = (from s in sig.Signatures[sigSize].SignatureList where s.Offset == offset select s).First<BlockSignature>();

                var sb = new StringBuilder();
                foreach (var b in specificSig.MD5Signature)
                {
                    sb.AppendFormat("{0:X2}", b);
                }
                var msg = string.Format("Offset: {0}\nSize: {1}\nRollingSig: {2}\nMD5: {3}", specificSig.Offset.ToString(),
                                        specificSig.Size.ToString(), specificSig.RollingSignature.ToString(), sb.ToString());

                var dialog = MessageBox.Show(msg);
            }
        }

        private void PopulateSignatureTreeBySize( )
        {
            sigTreeView.Nodes.Clear();

            foreach (var size in sig.Signatures.Keys)
            {
                var sizeMsg = string.Format("{0} ({1})", size, sig.Signatures[size].SignatureList.Count());

                var node = sigTreeView.Nodes.Add(sizeMsg);
                foreach (var sSig in sig.Signatures[size].SignatureList)
                {
                    node.Nodes.Add(sSig.Offset.ToString());
                }

            }
        }

        private void PopulateSignatureTreeByOffset()
        {
            sigTreeView.Nodes.Clear();

            var sigList = new List<BlockSignature>();

            foreach (var size in sig.Signatures.Keys)
            {
                foreach (var sSig in sig.Signatures[size].SignatureList)
                {
                    sigList.Add(sSig);
                }
            }

            var sortedSigList = from s in sigList orderby s.Offset select s;

            uint lastSize = 0;
            TreeNode currentNode = null;
            foreach (var s in sortedSigList)
            {
                // if new size, then add new root node.
                if (s.Size != lastSize)
                {
                    
                    if (currentNode != null)
                    {
                        currentNode.Text = string.Format("{0} ({1})", lastSize, currentNode.Nodes.Count);
                    }

                    currentNode = sigTreeView.Nodes.Add(s.Size.ToString());
                    lastSize = s.Size;
                }
                
                currentNode.Nodes.Add(s.Offset.ToString());
                
            }


        }

        private void PopulateSignatureTree( bool performOffsetSort = false )
        {
            sigTreeView.Nodes.Clear();

            if (!performOffsetSort)
            {
                PopulateSignatureTreeBySize();
            }
            else
            {
                PopulateSignatureTreeByOffset();
            }

            sigTreeView.Update();
        }

        /// <summary>
        /// Loads the sig file.
        /// </summary>
        /// <param name="filename"></param>
        private void LoadSigFile(string filename)
        {
            using (var fs = new FileStream(filename, FileMode.Open))
            {
                sig = SerializationHelper.ReadSizeBasedBinarySignature( fs );
            }

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

            LoadSigFile(fn);

            
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
    }
}
