using SchoolSystem.Enums.Quran;
using SchoolSystem.Presentation.Quran.Parts;
namespace SchoolSystem.Presentation.Quran.StudentProgress
{
    partial class frmStudentParts
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
            this.fLPStudentParts = new System.Windows.Forms.FlowLayoutPanel();
            this.cmsTools = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsArchive = new System.Windows.Forms.ToolStripMenuItem();
            this.cbPartsName = new System.Windows.Forms.ComboBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnAddStudentToPart = new System.Windows.Forms.Button();
            this.ctrQuranPartPages1 = new SchoolSystem.Presentation.Quran.Parts.ctrQuranPartPages();
            this.btnArchive = new System.Windows.Forms.Button();
            this.cmsTools.SuspendLayout();
            this.SuspendLayout();
            // 
            // fLPStudentParts
            // 
            this.fLPStudentParts.AutoScroll = true;
            this.fLPStudentParts.ContextMenuStrip = this.cmsTools;
            this.fLPStudentParts.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.fLPStudentParts.Location = new System.Drawing.Point(0, 171);
            this.fLPStudentParts.Name = "fLPStudentParts";
            this.fLPStudentParts.Size = new System.Drawing.Size(1776, 625);
            this.fLPStudentParts.TabIndex = 0;
            this.fLPStudentParts.TabStop = true;
            // 
            // cmsTools
            // 
            this.cmsTools.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsDelete,
            this.cmsArchive});
            this.cmsTools.Name = "cmsDelete";
            this.cmsTools.Size = new System.Drawing.Size(128, 52);
            // 
            // cmsDelete
            // 
            this.cmsDelete.Name = "cmsDelete";
            this.cmsDelete.Size = new System.Drawing.Size(127, 24);
            this.cmsDelete.Text = "Delete";
            this.cmsDelete.Click += new System.EventHandler(this.cmsDelete_Click_1);
            // 
            // cmsArchive
            // 
            this.cmsArchive.Name = "cmsArchive";
            this.cmsArchive.Size = new System.Drawing.Size(127, 24);
            this.cmsArchive.Text = "Archive";
            this.cmsArchive.Click += new System.EventHandler(this.cmsArchive_Click);
            // 
            // cbPartsName
            // 
            this.cbPartsName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(80)))));
            this.cbPartsName.Font = new System.Drawing.Font("Calibri", 12F);
            this.cbPartsName.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.cbPartsName.FormattingEnabled = true;
            this.cbPartsName.Location = new System.Drawing.Point(1586, 133);
            this.cbPartsName.Name = "cbPartsName";
            this.cbPartsName.Size = new System.Drawing.Size(175, 32);
            this.cbPartsName.TabIndex = 1;
            this.cbPartsName.SelectedIndexChanged += new System.EventHandler(this.cbPartsName_SelectedIndexChanged_1);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(60)))), ((int)(((byte)(1)))));
            this.lblTitle.Font = new System.Drawing.Font("Calibri", 45F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblTitle.Location = new System.Drawing.Point(552, 36);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(587, 114);
            this.lblTitle.TabIndex = 76;
            this.lblTitle.Text = "الحفظ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAddStudentToPart
            // 
            this.btnAddStudentToPart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(90)))));
            this.btnAddStudentToPart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAddStudentToPart.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(80)))));
            this.btnAddStudentToPart.FlatAppearance.BorderSize = 0;
            this.btnAddStudentToPart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddStudentToPart.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold);
            this.btnAddStudentToPart.ForeColor = System.Drawing.Color.GreenYellow;
            this.btnAddStudentToPart.Location = new System.Drawing.Point(15, 109);
            this.btnAddStudentToPart.Name = "btnAddStudentToPart";
            this.btnAddStudentToPart.Size = new System.Drawing.Size(180, 41);
            this.btnAddStudentToPart.TabIndex = 0;
            this.btnAddStudentToPart.Text = "Add Student";
            this.btnAddStudentToPart.UseVisualStyleBackColor = false;
            this.btnAddStudentToPart.Click += new System.EventHandler(this.btnAddStudentToPart_Click);
            // 
            // ctrQuranPartPages1
            // 
            this.ctrQuranPartPages1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(60)))));
            this.ctrQuranPartPages1.enPartMode = SchoolSystem.Enums.Quran.enPartMode.QuranPart;
            this.ctrQuranPartPages1.Location = new System.Drawing.Point(3, 3);
            this.ctrQuranPartPages1.Name = "ctrQuranPartPages1";
            this.ctrQuranPartPages1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.ctrQuranPartPages1.Size = new System.Drawing.Size(1746, 126);
            this.ctrQuranPartPages1.studentPartID = 0;
            this.ctrQuranPartPages1.TabIndex = 0;
            // 
            // btnArchive
            // 
            this.btnArchive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(90)))));
            this.btnArchive.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnArchive.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(80)))));
            this.btnArchive.FlatAppearance.BorderSize = 0;
            this.btnArchive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnArchive.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold);
            this.btnArchive.ForeColor = System.Drawing.Color.GreenYellow;
            this.btnArchive.Location = new System.Drawing.Point(218, 109);
            this.btnArchive.Name = "btnArchive";
            this.btnArchive.Size = new System.Drawing.Size(180, 41);
            this.btnArchive.TabIndex = 1;
            this.btnArchive.Text = "Archive";
            this.btnArchive.UseVisualStyleBackColor = false;
            this.btnArchive.Click += new System.EventHandler(this.btnArchive_Click);
            // 
            // frmStudentParts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(1776, 796);
            this.Controls.Add(this.btnArchive);
            this.Controls.Add(this.btnAddStudentToPart);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.cbPartsName);
            this.Controls.Add(this.fLPStudentParts);
            this.Name = "frmStudentParts";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmStudentParts";
            this.Load += new System.EventHandler(this.frmStudentParts_Load);
            this.Resize += new System.EventHandler(this.frmStudentParts_Resize);
            this.cmsTools.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel fLPStudentParts;
        private System.Windows.Forms.ComboBox cbPartsName;
        private System.Windows.Forms.Label lblTitle;
        private ctrQuranPartPages ctrQuranPartPages1;
        private System.Windows.Forms.Button btnAddStudentToPart;
        private System.Windows.Forms.ContextMenuStrip cmsTools;
        private System.Windows.Forms.ToolStripMenuItem cmsDelete;
        private System.Windows.Forms.ToolStripMenuItem cmsArchive;
        private System.Windows.Forms.Button btnArchive;
    }
}