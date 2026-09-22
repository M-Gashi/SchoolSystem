namespace SchoolSystem.Presentation.Teachers
{
    partial class frmShowTeacherInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShowTeacherInfo));
            this.ctrTeacherInfo1 = new SchoolSystem.Presentation.Teachers.ctrTeacherInfo();
            this.SuspendLayout();
            // 
            // ctrTeacherInfo1
            // 
            this.ctrTeacherInfo1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(60)))), ((int)(((byte)(1)))));
            this.ctrTeacherInfo1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ctrTeacherInfo1.BackgroundImage")));
            this.ctrTeacherInfo1.Location = new System.Drawing.Point(12, 12);
            this.ctrTeacherInfo1.Name = "ctrTeacherInfo1";
            this.ctrTeacherInfo1.Size = new System.Drawing.Size(1167, 894);
            this.ctrTeacherInfo1.TabIndex = 0;
            // 
            // frmShowTeacherInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SchoolSystem.Presentation.Properties.Resources.WallPaper;
            this.ClientSize = new System.Drawing.Size(1216, 905);
            this.Controls.Add(this.ctrTeacherInfo1);
            this.Name = "frmShowTeacherInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmShowTeacherInfo";
            this.Load += new System.EventHandler(this.frmShowTeacherInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrTeacherInfo ctrTeacherInfo1;
    }
}