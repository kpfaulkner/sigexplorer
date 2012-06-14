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
            this.loadSigButton = new System.Windows.Forms.Button();
            this.sigTreeView = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.OffsetSortButton = new System.Windows.Forms.RadioButton();
            this.SizeSortButton = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // loadSigButton
            // 
            this.loadSigButton.Location = new System.Drawing.Point(12, 12);
            this.loadSigButton.Name = "loadSigButton";
            this.loadSigButton.Size = new System.Drawing.Size(75, 23);
            this.loadSigButton.TabIndex = 0;
            this.loadSigButton.Text = "Load Sig";
            this.loadSigButton.UseVisualStyleBackColor = true;
            this.loadSigButton.Click += new System.EventHandler(this.loadSigButton_Click);
            // 
            // sigTreeView
            // 
            this.sigTreeView.Location = new System.Drawing.Point(12, 120);
            this.sigTreeView.Name = "sigTreeView";
            this.sigTreeView.Size = new System.Drawing.Size(289, 542);
            this.sigTreeView.TabIndex = 1;
            this.sigTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.sigTreeView_AfterSelect);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.OffsetSortButton);
            this.groupBox1.Controls.Add(this.SizeSortButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(175, 60);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // OffsetSortButton
            // 
            this.OffsetSortButton.AutoSize = true;
            this.OffsetSortButton.Location = new System.Drawing.Point(7, 34);
            this.OffsetSortButton.Name = "OffsetSortButton";
            this.OffsetSortButton.Size = new System.Drawing.Size(75, 17);
            this.OffsetSortButton.TabIndex = 1;
            this.OffsetSortButton.Text = "Offset Sort";
            this.OffsetSortButton.UseVisualStyleBackColor = true;
            this.OffsetSortButton.CheckedChanged += new System.EventHandler(this.OffsetSortButton_CheckedChanged);
            // 
            // SizeSortButton
            // 
            this.SizeSortButton.AutoSize = true;
            this.SizeSortButton.Checked = true;
            this.SizeSortButton.Location = new System.Drawing.Point(7, 10);
            this.SizeSortButton.Name = "SizeSortButton";
            this.SizeSortButton.Size = new System.Drawing.Size(67, 17);
            this.SizeSortButton.TabIndex = 0;
            this.SizeSortButton.TabStop = true;
            this.SizeSortButton.Text = "Size Sort";
            this.SizeSortButton.UseVisualStyleBackColor = true;
            this.SizeSortButton.CheckedChanged += new System.EventHandler(this.SizeSortButton_CheckedChanged);
            // 
            // SignatureExplorerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 641);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.sigTreeView);
            this.Controls.Add(this.loadSigButton);
            this.Name = "SignatureExplorerForm";
            this.Text = "Signature Explorer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button loadSigButton;
        private System.Windows.Forms.TreeView sigTreeView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton SizeSortButton;
        private System.Windows.Forms.RadioButton OffsetSortButton;

    }
}

