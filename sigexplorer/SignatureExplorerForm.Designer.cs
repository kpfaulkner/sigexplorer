namespace sigexplorer
{
    partial class SignatureExplorerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sigTreeView = new System.Windows.Forms.TreeView();
            this.sigTreeView2 = new System.Windows.Forms.TreeView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.newSize = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.sharedSize = new System.Windows.Forms.Label();
            this.file2TotalSize = new System.Windows.Forms.Label();
            this.file1TotalSize = new System.Windows.Forms.Label();
            this.file2Label = new System.Windows.Forms.Label();
            this.file1Label = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // sigTreeView
            // 
            this.sigTreeView.Location = new System.Drawing.Point(14, 102);
            this.sigTreeView.Name = "sigTreeView";
            this.sigTreeView.Size = new System.Drawing.Size(267, 474);
            this.sigTreeView.TabIndex = 1;
            this.sigTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.sigTreeView_AfterSelect);
            // 
            // sigTreeView2
            // 
            this.sigTreeView2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sigTreeView2.Enabled = false;
            this.sigTreeView2.Location = new System.Drawing.Point(298, 102);
            this.sigTreeView2.Name = "sigTreeView2";
            this.sigTreeView2.Size = new System.Drawing.Size(293, 474);
            this.sigTreeView2.TabIndex = 4;
            this.sigTreeView2.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.sigTreeView2_AfterSelect);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.AutoSize = true;
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.Controls.Add(this.newSize);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.sharedSize);
            this.groupBox2.Controls.Add(this.file2TotalSize);
            this.groupBox2.Controls.Add(this.file1TotalSize);
            this.groupBox2.Controls.Add(this.file2Label);
            this.groupBox2.Controls.Add(this.file1Label);
            this.groupBox2.Controls.Add(this.sigTreeView2);
            this.groupBox2.Controls.Add(this.sigTreeView);
            this.groupBox2.Location = new System.Drawing.Point(15, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(605, 595);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // newSize
            // 
            this.newSize.AutoSize = true;
            this.newSize.Location = new System.Drawing.Point(453, 66);
            this.newSize.Name = "newSize";
            this.newSize.Size = new System.Drawing.Size(0, 13);
            this.newSize.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(405, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "New";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(144, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Shared";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(295, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Size";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Size";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // sharedSize
            // 
            this.sharedSize.AutoSize = true;
            this.sharedSize.Location = new System.Drawing.Point(186, 67);
            this.sharedSize.Name = "sharedSize";
            this.sharedSize.Size = new System.Drawing.Size(0, 13);
            this.sharedSize.TabIndex = 9;
            // 
            // file2TotalSize
            // 
            this.file2TotalSize.AutoSize = true;
            this.file2TotalSize.Location = new System.Drawing.Point(328, 66);
            this.file2TotalSize.Name = "file2TotalSize";
            this.file2TotalSize.Size = new System.Drawing.Size(0, 13);
            this.file2TotalSize.TabIndex = 8;
            // 
            // file1TotalSize
            // 
            this.file1TotalSize.AutoSize = true;
            this.file1TotalSize.Location = new System.Drawing.Point(47, 67);
            this.file1TotalSize.Name = "file1TotalSize";
            this.file1TotalSize.Size = new System.Drawing.Size(0, 13);
            this.file1TotalSize.TabIndex = 7;
            this.file1TotalSize.Click += new System.EventHandler(this.file1TotalSize_Click);
            // 
            // file2Label
            // 
            this.file2Label.AutoSize = true;
            this.file2Label.Location = new System.Drawing.Point(295, 16);
            this.file2Label.Name = "file2Label";
            this.file2Label.Size = new System.Drawing.Size(0, 13);
            this.file2Label.TabIndex = 6;
            // 
            // file1Label
            // 
            this.file1Label.AutoSize = true;
            this.file1Label.Location = new System.Drawing.Point(11, 15);
            this.file1Label.Name = "file1Label";
            this.file1Label.Size = new System.Drawing.Size(0, 13);
            this.file1Label.TabIndex = 5;
            this.file1Label.Click += new System.EventHandler(this.file1Label_Click);
            // 
            // SignatureExplorerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 693);
            this.Controls.Add(this.groupBox2);
            this.Name = "SignatureExplorerForm";
            this.Text = "Signature Explorer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView sigTreeView;
        private System.Windows.Forms.TreeView sigTreeView2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label file1Label;
        private System.Windows.Forms.Label file2Label;
        private System.Windows.Forms.Label file1TotalSize;
        private System.Windows.Forms.Label file2TotalSize;
        private System.Windows.Forms.Label sharedSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label newSize;
        private System.Windows.Forms.Label label4;

    }
}

