namespace SchoolSystem.Presentation.Students
{
    partial class frmStudentsList
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
            this.dgvStudents2 = new System.Windows.Forms.DataGridView();
            this.cmsDelete = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.editToolStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsMySubjects = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnTrreSub = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents2)).BeginInit();
            this.cmsDelete.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvStudents2
            // 
            this.dgvStudents2.AllowUserToAddRows = false;
            this.dgvStudents2.AllowUserToDeleteRows = false;
            this.dgvStudents2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(80)))));
            this.dgvStudents2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvStudents2.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvStudents2.ColumnHeadersHeight = 25;
            this.dgvStudents2.ContextMenuStrip = this.cmsDelete;
            this.dgvStudents2.GridColor = System.Drawing.Color.White;
            this.dgvStudents2.Location = new System.Drawing.Point(77, 263);
            this.dgvStudents2.Name = "dgvStudents2";
            this.dgvStudents2.ReadOnly = true;
            this.dgvStudents2.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvStudents2.RowHeadersWidth = 51;
            this.dgvStudents2.RowTemplate.Height = 26;
            this.dgvStudents2.Size = new System.Drawing.Size(1134, 391);
            this.dgvStudents2.TabIndex = 5;
            this.dgvStudents2.DoubleClick += new System.EventHandler(this.dgvStudents2_DoubleClick);
            // 
            // cmsDelete
            // 
            this.cmsDelete.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsDelete.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoToolStripMenuItem,
            this.toolStripSeparator1,
            this.editToolStripMenu,
            this.deleteToolStripMenuItem,
            this.cmsMySubjects});
            this.cmsDelete.Name = "cmsDelete";
            this.cmsDelete.Size = new System.Drawing.Size(158, 106);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(157, 24);
            this.infoToolStripMenuItem.Text = "Info";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.infoToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(154, 6);
            // 
            // editToolStripMenu
            // 
            this.editToolStripMenu.Name = "editToolStripMenu";
            this.editToolStripMenu.Size = new System.Drawing.Size(157, 24);
            this.editToolStripMenu.Text = "Edit";
            this.editToolStripMenu.Click += new System.EventHandler(this.editToolStripMenu_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(157, 24);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // cmsMySubjects
            // 
            this.cmsMySubjects.Name = "cmsMySubjects";
            this.cmsMySubjects.Size = new System.Drawing.Size(157, 24);
            this.cmsMySubjects.Text = "My Subjects";
            this.cmsMySubjects.Click += new System.EventHandler(this.cmsMySubjects_Click);
            // 
            // btnAddNew
            // 
            this.btnAddNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(90)))));
            this.btnAddNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAddNew.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(80)))));
            this.btnAddNew.FlatAppearance.BorderSize = 0;
            this.btnAddNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNew.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold);
            this.btnAddNew.ForeColor = System.Drawing.Color.GreenYellow;
            this.btnAddNew.Location = new System.Drawing.Point(1235, 263);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(127, 41);
            this.btnAddNew.TabIndex = 8;
            this.btnAddNew.Text = "Add New";
            this.btnAddNew.UseVisualStyleBackColor = false;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Calibri", 50F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblTitle.Location = new System.Drawing.Point(738, 76);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(658, 103);
            this.lblTitle.TabIndex = 7;
            this.lblTitle.Text = "The Student\'s List";
            // 
            // btnTrreSub
            // 
            this.btnTrreSub.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(90)))));
            this.btnTrreSub.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnTrreSub.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(80)))));
            this.btnTrreSub.FlatAppearance.BorderSize = 0;
            this.btnTrreSub.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrreSub.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold);
            this.btnTrreSub.ForeColor = System.Drawing.Color.GreenYellow;
            this.btnTrreSub.Location = new System.Drawing.Point(77, 126);
            this.btnTrreSub.Name = "btnTrreSub";
            this.btnTrreSub.Size = new System.Drawing.Size(178, 41);
            this.btnTrreSub.TabIndex = 25;
            this.btnTrreSub.Text = "Tree Subjects";
            this.btnTrreSub.UseVisualStyleBackColor = false;
            this.btnTrreSub.Click += new System.EventHandler(this.btnTrreSub_Click);
            // 
            // frmStudentsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(60)))));
            this.BackgroundImage = global::SchoolSystem.Presentation.Properties.Resources.WallPaper;
            this.ClientSize = new System.Drawing.Size(1473, 763);
            this.Controls.Add(this.btnTrreSub);
            this.Controls.Add(this.dgvStudents2);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.lblTitle);
            this.Name = "frmStudentsList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmStudentsList";
            this.Load += new System.EventHandler(this.frmStudentsList_Load);
            this.Resize += new System.EventHandler(this.frmStudentsList_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents2)).EndInit();
            this.cmsDelete.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvStudents2;
        private System.Windows.Forms.ContextMenuStrip cmsDelete;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenu;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ToolStripMenuItem cmsMySubjects;
        private System.Windows.Forms.Button btnTrreSub;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}