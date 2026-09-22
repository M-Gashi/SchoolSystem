namespace SchoolSystem.Presentation.Quran.Parts
{
    partial class frmQuranParts
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
            this.fLPPartsOfQuran = new System.Windows.Forms.FlowLayoutPanel();
            this.ctrQuranPartPages1 = new SchoolSystem.Presentation.Quran.Parts.ctrQuranPartPages();
            this.fLPPartsOfQuran.SuspendLayout();
            this.SuspendLayout();
            // 
            // fLPPartsOfQuran
            // 
            this.fLPPartsOfQuran.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(60)))));
            this.fLPPartsOfQuran.Controls.Add(this.ctrQuranPartPages1);
            this.fLPPartsOfQuran.Location = new System.Drawing.Point(7, 12);
            this.fLPPartsOfQuran.Name = "fLPPartsOfQuran";
            this.fLPPartsOfQuran.Size = new System.Drawing.Size(1818, 682);
            this.fLPPartsOfQuran.TabIndex = 0;
            // 
            // ctrQuranPartPages1
            // 
            this.ctrQuranPartPages1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(60)))));
            this.ctrQuranPartPages1.Location = new System.Drawing.Point(3, 3);
            this.ctrQuranPartPages1.Name = "ctrQuranPartPages1";
            this.ctrQuranPartPages1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.ctrQuranPartPages1.Size = new System.Drawing.Size(1646, 143);
            this.ctrQuranPartPages1.TabIndex = 0;
            // 
            // frmQuranParts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(1837, 706);
            this.Controls.Add(this.fLPPartsOfQuran);
            this.Name = "frmQuranParts";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmQuranParts";
            this.Load += new System.EventHandler(this.frmQuranParts_Load_1);
            this.fLPPartsOfQuran.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel fLPPartsOfQuran;
        private ctrQuranPartPages ctrQuranPartPages1;
    }
}