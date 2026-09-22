namespace SchoolSystem.Presentation.Quran.Parts
{
    partial class ctrPage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtRepeat = new System.Windows.Forms.TextBox();
            this.lblRepeat = new System.Windows.Forms.Label();
            this.pbPage = new SchoolSystem.Presentation.Quran.Parts.clsPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbPage)).BeginInit();
            this.SuspendLayout();
            // 
            // txtRepeat
            // 
            this.txtRepeat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(60)))));
            this.txtRepeat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRepeat.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepeat.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.txtRepeat.Location = new System.Drawing.Point(9, 0);
            this.txtRepeat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtRepeat.MaxLength = 50;
            this.txtRepeat.Name = "txtRepeat";
            this.txtRepeat.Size = new System.Drawing.Size(49, 26);
            this.txtRepeat.TabIndex = 215;
            this.txtRepeat.Visible = false;
            this.txtRepeat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRepeat_KeyDown);
            this.txtRepeat.Leave += new System.EventHandler(this.txtRepeat_Leave);
            // 
            // lblRepeat
            // 
            this.lblRepeat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(60)))));
            this.lblRepeat.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
            this.lblRepeat.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblRepeat.Location = new System.Drawing.Point(0, 33);
            this.lblRepeat.Name = "lblRepeat";
            this.lblRepeat.Size = new System.Drawing.Size(66, 23);
            this.lblRepeat.TabIndex = 214;
            this.lblRepeat.Text = "              ";
            this.lblRepeat.DoubleClick += new System.EventHandler(this.lblRepeat_DoubleClick);
            // 
            // pbPage
            // 
            this.pbPage.Location = new System.Drawing.Point(0, 59);
            this.pbPage.Name = "pbPage";
            this.pbPage.OverlayAlpha = 120;
            this.pbPage.OverlayColor = System.Drawing.Color.Black;
            this.pbPage.OverlayStyle = SchoolSystem.Presentation.Quran.Parts.clsPictureBox.enOverlayStyle.Full;
            this.pbPage.Progress = 0;
            this.pbPage.Selected = false;
            this.pbPage.Size = new System.Drawing.Size(66, 95);
            this.pbPage.TabIndex = 217;
            this.pbPage.TabStop = false;
            this.pbPage.DoubleClick += new System.EventHandler(this.pbPage_DoubleClick);
            this.pbPage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbPage_MouseDown);
            this.pbPage.MouseLeave += new System.EventHandler(this.pbPage_MouseLeave);
            // 
            // ctrPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(60)))));
            this.Controls.Add(this.pbPage);
            this.Controls.Add(this.txtRepeat);
            this.Controls.Add(this.lblRepeat);
            this.Name = "ctrPage";
            this.Size = new System.Drawing.Size(66, 153);
            this.Load += new System.EventHandler(this.ctrPage_Load);
            this.Resize += new System.EventHandler(this.ctrPage_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pbPage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRepeat;
        private System.Windows.Forms.Label lblRepeat;
        private clsPictureBox pbPage;
    }
}
