using SchoolSystem.Business;
using SchoolSystem.Enums;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SchoolSystem.Business.Subjects;
using SchoolSystem.Enums.Common;

namespace SchoolSystem.Presentation.Subjects
{
    public partial class frmSubjectsList : Form
    {
        public enum enLayoutRole
        {
            Title,
            MainGrid,
            RightPrimaryAction,
            BottomSecondaryAction
        }
        // ========================= Constructor =========================
        public frmSubjectsList()
        {
            InitializeComponent();
        }
        // =================================================================================
        // ========================= Data =========================
        private static DataTable _dtAllSubjects;
        private DataTable _dtSubjects;
        // ========================= Grid Configuration =========================
        private void _loadSubjects()
        {
            _dtAllSubjects = clsSubject.getAllSubjects();

            _dtSubjects = (_dtAllSubjects == null)? new DataTable(): _dtAllSubjects.DefaultView.ToTable
                (
                    false,
                    "SubjectID",
                    "SubjectName",
                    "Credits",
                    "IsActive"
                );

            dgvSubjects.DataSource = _dtSubjects;
        }
        private void _configureGrid()
        {
            dgvSubjects.EnableHeadersVisualStyles = false;

            // Cells
            dgvSubjects.DefaultCellStyle.BackColor = Color.FromArgb(0, 90, 80);
            dgvSubjects.DefaultCellStyle.ForeColor = Color.White;
            dgvSubjects.DefaultCellStyle.Font = new Font(dgvSubjects.Font.FontFamily, 10, FontStyle.Bold);

            // Column headers
            dgvSubjects.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 90, 80);
            dgvSubjects.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvSubjects.ColumnHeadersDefaultCellStyle.Font = new Font(dgvSubjects.Font.FontFamily, 11, FontStyle.Bold);
            dgvSubjects.ColumnHeadersHeight = 40;
            dgvSubjects.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Row headers
            dgvSubjects.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 70, 200);
            dgvSubjects.RowHeadersDefaultCellStyle.ForeColor = Color.White;

            // Columns
            if (dgvSubjects.Columns.Count == 0)
                return;

            dgvSubjects.Columns["SubjectID"].HeaderText = "SID";
            dgvSubjects.Columns["SubjectID"].Width = 60;

            dgvSubjects.Columns["SubjectName"].HeaderText = "Subject Name";
            dgvSubjects.Columns["SubjectName"].Width = 220;

            dgvSubjects.Columns["Credits"].HeaderText = "Credits";
            dgvSubjects.Columns["Credits"].Width = 80;

            dgvSubjects.Columns["IsActive"].HeaderText = "Active";
            dgvSubjects.Columns["IsActive"].Width = 80;

            
            
        }
        // ========================= Layout =========================
        private void _centerHorizontally(Control c)
        {
            c.Left = (ClientSize.Width - c.Width) / 2;
        }
        private void _stickToBottom(Control c, int margin = 60)
        {
            c.Top = ClientSize.Height - c.Height - margin;
        }
        private void _placeRight(Control c, Control anchor, int margin = 15)
        {
            c.Left = anchor.Right + margin;
        }
        private void _fitGridWidthToColumns(DataGridView dgv)
        {
            int totalWidth = dgv.RowHeadersVisible ? dgv.RowHeadersWidth : 0;

            foreach (DataGridViewColumn col in dgv.Columns)
                if (col.Visible)
                    totalWidth += col.Width;

            dgv.Width = totalWidth + 2;
        }
        // =============================
        private void _applyLayout(Control c, enLayoutRole role)
        {
            switch (role)
            {
                case enLayoutRole.Title:
                    _centerHorizontally(c);
                    c.Top = 40;
                    break;

                case enLayoutRole.MainGrid:
                    _fitGridWidthToColumns((DataGridView)c);
                    _centerHorizontally(c);
                    _stickToBottom(c);
                    break;

                case enLayoutRole.RightPrimaryAction:
                    c.Top = dgvSubjects.Top;
                    _placeRight(c, dgvSubjects);
                    break;

                case enLayoutRole.BottomSecondaryAction:
                    c.Top = dgvSubjects.Bottom + 15;
                    c.Left = btnAddNew.Left;
                    break;
            }
        }
        private void _applyLayout()
        {
            _applyLayout(lblTitle, enLayoutRole.Title);
            _applyLayout(dgvSubjects, enLayoutRole.MainGrid);
            _applyLayout(btnAddNew, enLayoutRole.RightPrimaryAction);
            _applyLayout(btnClose, enLayoutRole.BottomSecondaryAction);
        }
        // ========================= Load =========================
        private void frmSubjectsList_Load(object sender, EventArgs e)
        {
            _loadSubjects();
            _configureGrid();
            _applyLayout();
        }
        private void frmSubjectsList_Resize(object sender, EventArgs e)
        {
            _applyLayout();
        }
        // =================================================================================
        // ========================= Add New =========================
        private void btnAddNew_Click(object sender, EventArgs e)
        {
        }
        // ========================= Edit =========================
        private void editToolStripMenu_Click(object sender, EventArgs e)
        {
        }
        // ========================= Delete =========================
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to delete Subject [" + dgvSubjects.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                //Perform Delele and refresh
                if (clsSubject.deleteSubjectID((int)dgvSubjects.CurrentRow.Cells[0].Value) == enErrors.Success)
                {
                    MessageBox.Show("Subject Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Subject was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            _loadSubjects();
        }
        // ========================= Close =========================
        private void btnClose_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvSubjects_DoubleClick(object sender, EventArgs e)
        {
            Form frm = new frmSubjectInfo((int)dgvSubjects.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void dgvSubjects_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

