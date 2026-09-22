namespace SchoolSystem.Presentation.Students
{
    partial class frmShowStudentInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShowStudentInfo));
            this.ctrStudent1 = new SchoolSystem.Presentation.Students.ctrStudent();
            this.SuspendLayout();
            // 
            // ctrStudent1
            // 
            this.ctrStudent1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(60)))), ((int)(((byte)(1)))));
            this.ctrStudent1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ctrStudent1.BackgroundImage")));
            this.ctrStudent1.Location = new System.Drawing.Point(-2, 3);
            this.ctrStudent1.Name = "ctrStudent1";
            this.ctrStudent1.Size = new System.Drawing.Size(1110, 860);
            this.ctrStudent1.TabIndex = 0;
            // 
            // frmShowStudentInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(60)))));
            this.BackgroundImage = global::SchoolSystem.Presentation.Properties.Resources.WallPaper;
            this.ClientSize = new System.Drawing.Size(1120, 879);
            this.Controls.Add(this.ctrStudent1);
            this.Name = "frmShowStudentInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmShowStudentInfo";
            this.ResumeLayout(false);

        }

        #endregion

        private ctrStudent ctrStudent1;
    }
}