namespace SchoolSystem.Presentation.Quran.StudentProgress
{
    partial class frmArchiveList
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
            this.dgvArchivelist = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArchivelist)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPartName
            // 
            this.lblPartName.AutoSize = true;
            this.lblPartName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(60)))));
            this.lblPartName.Font = new System.Drawing.Font("Calibri", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPartName.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblPartName.Location = new System.Drawing.Point(84, 43);
            this.lblPartName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPartName.Name = "lblPartName";
            this.lblPartName.Size = new System.Drawing.Size(153, 51);
            this.lblPartName.TabIndex = 250;
            this.lblPartName.Text = "Archive";
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
            this.btnClose.Location = new System.Drawing.Point(291, 538);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(109, 41);
            this.btnClose.TabIndex = 251;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgvArchivelist
            // 
            this.dgvArchivelist.AllowUserToAddRows = false;
            this.dgvArchivelist.AllowUserToDeleteRows = false;
            this.dgvArchivelist.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(80)))));
            this.dgvArchivelist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvArchivelist.Location = new System.Drawing.Point(5, 126);
            this.dgvArchivelist.Name = "dgvArchivelist";
            this.dgvArchivelist.ReadOnly = true;
            this.dgvArchivelist.RowHeadersWidth = 51;
            this.dgvArchivelist.RowTemplate.Height = 26;
            this.dgvArchivelist.Size = new System.Drawing.Size(450, 340);
            this.dgvArchivelist.TabIndex = 252;
            this.dgvArchivelist.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvArchivelist_CellContentClick);
            // 
            // frmArchiveList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(412, 591);
            this.Controls.Add(this.dgvArchivelist);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblPartName);
            this.Name = "frmArchiveList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmArchiveList";
            this.Load += new System.EventHandler(this.frmArchiveList_Load);
            this.Resize += new System.EventHandler(this.frmArchiveList_Resize_1);
            ((System.ComponentModel.ISupportInitialize)(this.dgvArchivelist)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblPartName;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dgvArchivelist;
    }
}