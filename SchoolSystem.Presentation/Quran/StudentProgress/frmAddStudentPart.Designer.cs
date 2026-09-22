namespace SchoolSystem.Presentation.Quran.StudentProgress
{
    partial class frmAddStudentPart
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
            this.lblPartName = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.ctrStudentList1 = new SchoolSystem.Presentation.Quran.StudentProgress.ctrStudentList();
            this.SuspendLayout();
            // 
            // lblPartName
            // 
            this.lblPartName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(60)))));
            this.lblPartName.Font = new System.Drawing.Font("Calibri", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPartName.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblPartName.Location = new System.Drawing.Point(76, 25);
            this.lblPartName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPartName.Name = "lblPartName";
            this.lblPartName.Size = new System.Drawing.Size(222, 80);
            this.lblPartName.TabIndex = 249;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(90)))));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(80)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.GreenYellow;
            this.btnClose.Location = new System.Drawing.Point(249, 492);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(109, 41);
            this.btnClose.TabIndex = 250;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ctrStudentList1
            // 
            this.ctrStudentList1.Location = new System.Drawing.Point(12, 127);
            this.ctrStudentList1.Name = "ctrStudentList1";
            this.ctrStudentList1.Size = new System.Drawing.Size(350, 343);
            this.ctrStudentList1.TabIndex = 0;
            // 
            // frmAddStudentPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(370, 552);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblPartName);
            this.Controls.Add(this.ctrStudentList1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmAddStudentPart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmAddStudent";
            this.Load += new System.EventHandler(this.frmAddStudentPart_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrStudentList ctrStudentList1;
        private System.Windows.Forms.Label lblPartName;
        private System.Windows.Forms.Button btnClose;
    }
}