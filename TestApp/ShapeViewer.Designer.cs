namespace TestApp
{
    partial class ShapeViewer
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ImageOutput = new System.Windows.Forms.PictureBox();
            this.ShapeTree = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImageOutput)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ShapeTree);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ImageOutput);
            this.splitContainer1.Size = new System.Drawing.Size(481, 454);
            this.splitContainer1.SplitterDistance = 160;
            this.splitContainer1.TabIndex = 0;
            // 
            // ImageOutput
            // 
            this.ImageOutput.BackColor = System.Drawing.Color.White;
            this.ImageOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImageOutput.Location = new System.Drawing.Point(0, 0);
            this.ImageOutput.Name = "ImageOutput";
            this.ImageOutput.Size = new System.Drawing.Size(317, 454);
            this.ImageOutput.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ImageOutput.TabIndex = 2;
            this.ImageOutput.TabStop = false;
            // 
            // ShapeTree
            // 
            this.ShapeTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ShapeTree.Location = new System.Drawing.Point(0, 0);
            this.ShapeTree.Name = "ShapeTree";
            this.ShapeTree.Size = new System.Drawing.Size(160, 454);
            this.ShapeTree.TabIndex = 0;
            this.ShapeTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ShapeTree_AfterSelect);
            // 
            // ShapeViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 454);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ShapeViewer";
            this.Text = "ShapeViewer";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ImageOutput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView ShapeTree;
        private System.Windows.Forms.PictureBox ImageOutput;


    }
}