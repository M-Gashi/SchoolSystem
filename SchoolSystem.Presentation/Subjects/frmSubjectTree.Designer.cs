namespace SchoolSystem.Presentation.Subjects
{
    partial class frmSubjectTree
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
            this.cmsOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsAddSubject = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStudent = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnQuranSync = new System.Windows.Forms.Button();
            this.btnStructureEducation = new System.Windows.Forms.Button();
            this.btnPerson = new System.Windows.Forms.Button();
            this.cmsOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTree
            // 
            this.pnlTree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(60)))), ((int)(((byte)(1)))));
            this.pnlTree.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold);
            this.pnlTree.Location = new System.Drawing.Point(0, 248);
            this.pnlTree.Name = "pnlTree";
            this.pnlTree.Size = new System.Drawing.Size(1507, 605);
            this.pnlTree.TabIndex = 32;
            this.pnlTree.DoubleClick += new System.EventHandler(this.pnlTree_DoubleClick);
            // 
            // cmsOptions
            // 
            this.cmsOptions.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsAddSubject,
            this.cmsUpdate,
            this.cmsDelete});
            this.cmsOptions.Name = "cmsAddChildren2";
            this.cmsOptions.Size = new System.Drawing.Size(160, 76);
            // 
            // cmsAddSubject
            // 
            this.cmsAddSubject.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmsAddSubject.Name = "cmsAddSubject";
            this.cmsAddSubject.Size = new System.Drawing.Size(159, 24);
            this.cmsAddSubject.Text = "Add Subject";
            this.cmsAddSubject.Click += new System.EventHandler(this.cmsAddSubject_Click);
            // 
            // cmsUpdate
            // 
            this.cmsUpdate.Name = "cmsUpdate";
            this.cmsUpdate.Size = new System.Drawing.Size(159, 24);
            this.cmsUpdate.Text = "Update";
            this.cmsUpdate.Click += new System.EventHandler(this.cmsUpdate_Click_1);
            // 
            // cmsDelete
            // 
            this.cmsDelete.Name = "cmsDelete";
            this.cmsDelete.Size = new System.Drawing.Size(159, 24);
            this.cmsDelete.Text = "Delete";
            this.cmsDelete.Click += new System.EventHandler(this.cmsDelete_Click);
            // 
            // btnStudent
            // 
            this.btnStudent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(60)))), ((int)(((byte)(1)))));
            this.btnStudent.BackgroundImage = global::SchoolSystem.Presentation.Properties.Resources._1;
            this.btnStudent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStudent.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnStudent.FlatAppearance.BorderSize = 0;
            this.btnStudent.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(60)))));
            this.btnStudent.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(60)))));
            this.btnStudent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStudent.Font = new System.Drawing.Font("Tahoma", 22F);
            this.btnStudent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnStudent.Location = new System.Drawing.Point(23, 37);
            this.btnStudent.Name = "btnStudent";
            this.btnStudent.Size = new System.Drawing.Size(172, 88);
            this.btnStudent.TabIndex = 33;
            this.btnStudent.UseVisualStyleBackColor = false;
            this.btnStudent.Click += new System.EventHandler(this.btnStudent_Click);
            this.btnStudent.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnStudent_MouseDown);
            this.btnStudent.MouseEnter += new System.EventHandler(this.btnStudent_MouseEnter);
            this.btnStudent.MouseLeave += new System.EventHandler(this.btnStudent_MouseLeave);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(90)))));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(80)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.GreenYellow;
            this.button1.Location = new System.Drawing.Point(1352, 57);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 41);
            this.button1.TabIndex = 34;
            this.button1.Text = "Parts";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnQuranSync
            // 
            this.btnQuranSync.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(90)))));
            this.btnQuranSync.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnQuranSync.Enabled = false;
            this.btnQuranSync.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(80)))));
            this.btnQuranSync.FlatAppearance.BorderSize = 0;
            this.btnQuranSync.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuranSync.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold);
            this.btnQuranSync.ForeColor = System.Drawing.Color.GreenYellow;
            this.btnQuranSync.Location = new System.Drawing.Point(1153, 57);
            this.btnQuranSync.Name = "btnQuranSync";
            this.btnQuranSync.Size = new System.Drawing.Size(178, 41);
            this.btnQuranSync.TabIndex = 27;
            this.btnQuranSync.Text = "Quran Sync";
            this.btnQuranSync.UseVisualStyleBackColor = false;
            this.btnQuranSync.Click += new System.EventHandler(this.btnQuranSync_Click);
            // 
            // btnStructureEducation
            // 
            this.btnStructureEducation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(60)))), ((int)(((byte)(1)))));
            this.btnStructureEducation.BackgroundImage = global::SchoolSystem.Presentation.Properties.Resources.Structure_Education_1;
            this.btnStructureEducation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStructureEducation.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnStructureEducation.FlatAppearance.BorderSize = 0;
            this.btnStructureEducation.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(60)))));
            this.btnStructureEducation.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(60)))));
            this.btnStructureEducation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStructureEducation.Font = new System.Drawing.Font("Tahoma", 22F);
            this.btnStructureEducation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnStructureEducation.Location = new System.Drawing.Point(434, 37);
            this.btnStructureEducation.Name = "btnStructureEducation";
            this.btnStructureEducation.Size = new System.Drawing.Size(344, 88);
            this.btnStructureEducation.TabIndex = 22;
            this.btnStructureEducation.UseVisualStyleBackColor = false;
            this.btnStructureEducation.Click += new System.EventHandler(this.btnStructureEducation_Click);
            this.btnStructureEducation.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnStructureEducation_MouseDown);
            this.btnStructureEducation.MouseEnter += new System.EventHandler(this.btnStructureEducation_MouseEnter);
            this.btnStructureEducation.MouseLeave += new System.EventHandler(this.btnStructureEducation_MouseLeave);
            // 
            // btnPerson
            // 
            this.btnPerson.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(60)))), ((int)(((byte)(1)))));
            this.btnPerson.BackgroundImage = global::SchoolSystem.Presentation.Properties.Resources.P1;
            this.btnPerson.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPerson.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnPerson.FlatAppearance.BorderSize = 0;
            this.btnPerson.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(60)))));
            this.btnPerson.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(60)))));
            this.btnPerson.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPerson.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnPerson.Location = new System.Drawing.Point(222, 37);
            this.btnPerson.Name = "btnPerson";
            this.btnPerson.Size = new System.Drawing.Size(172, 88);
            this.btnPerson.TabIndex = 35;
            this.btnPerson.UseVisualStyleBackColor = false;
            this.btnPerson.Click += new System.EventHandler(this.btnPerson_Click);
            this.btnPerson.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnPerson_MouseDown);
            this.btnPerson.MouseEnter += new System.EventHandler(this.btnPerson_MouseEnter);
            this.btnPerson.MouseLeave += new System.EventHandler(this.btnPerson_MouseLeave);
            // 
            // frmSubjectTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(60)))));
            this.BackgroundImage = global::SchoolSystem.Presentation.Properties.Resources.WallPaper;
            this.ClientSize = new System.Drawing.Size(1613, 858);
            this.ContextMenuStrip = this.cmsOptions;
            this.Controls.Add(this.btnPerson);
            this.Controls.Add(this.btnStructureEducation);
            this.Controls.Add(this.btnQuranSync);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnStudent);
            this.Controls.Add(this.pnlTree);
            this.Name = "frmSubjectTree";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSubjectTree";
            this.Load += new System.EventHandler(this.frmSubjectTree_Load);
            this.Resize += new System.EventHandler(this.frmSubjectTree_Resize);
            this.cmsOptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTree;
        private System.Windows.Forms.ContextMenuStrip cmsOptions;
        private System.Windows.Forms.ToolStripMenuItem cmsAddSubject;
        private System.Windows.Forms.ToolStripMenuItem cmsUpdate;
        private System.Windows.Forms.ToolStripMenuItem cmsDelete;
        private System.Windows.Forms.Button btnStudent;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnQuranSync;
        private System.Windows.Forms.Button btnStructureEducation;
        private System.Windows.Forms.Button btnPerson;
    }
}