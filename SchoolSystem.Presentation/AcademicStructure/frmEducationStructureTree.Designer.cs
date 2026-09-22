using SchoolSystem.Enums.AcademicStructure;
using SchoolSystem.Business.AcademicStructure.Trees;
namespace SchoolSystem.Presentation.AcademicStructure
{
    partial class frmEducationStructureTree
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
            this.components = new System.ComponentModel.Container();
            this.pnlTree = new System.Windows.Forms.Panel();
            this.cmsAddChildren2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addChildrenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsAddChildren2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTree
            // 
            this.pnlTree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(60)))));
            this.pnlTree.ContextMenuStrip = this.cmsAddChildren2;
            this.pnlTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTree.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold);
            this.pnlTree.Location = new System.Drawing.Point(0, 0);
            this.pnlTree.Name = "pnlTree";
            this.pnlTree.Size = new System.Drawing.Size(1301, 682);
            this.pnlTree.TabIndex = 31;
            this.pnlTree.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pnlTree_MouseDoubleClick);
            // 
            // cmsAddChildren2
            // 
            this.cmsAddChildren2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsAddChildren2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addChildrenToolStripMenuItem,
            this.updateToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.cmsAddChildren2.Name = "cmsAddChildren2";
            this.cmsAddChildren2.Size = new System.Drawing.Size(166, 76);
            // 
            // addChildrenToolStripMenuItem
            // 
            this.addChildrenToolStripMenuItem.Name = "addChildrenToolStripMenuItem";
            this.addChildrenToolStripMenuItem.Size = new System.Drawing.Size(165, 24);
            this.addChildrenToolStripMenuItem.Text = "Add Children";
            this.addChildrenToolStripMenuItem.Click += new System.EventHandler(this.addChildrenToolStripMenuItem_Click);
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(165, 24);
            this.updateToolStripMenuItem.Text = "Update";
            this.updateToolStripMenuItem.Click += new System.EventHandler(this.updateToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(165, 24);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // frmEducationStructureTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1301, 682);
            this.Controls.Add(this.pnlTree);
            this.Name = "frmEducationStructureTree";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "clsStructureTree";
            this.Load += new System.EventHandler(this.frmEducationStructureTree_Load);
            this.cmsAddChildren2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTree;
        private System.Windows.Forms.ContextMenuStrip cmsAddChildren2;
        private System.Windows.Forms.ToolStripMenuItem addChildrenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    }
}