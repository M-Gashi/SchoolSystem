using SchoolSystem.Enums;
using SchoolSystem.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SchoolSystem.Business.Subjects.StudentAssignments;

namespace SchoolSystem.Presentation.Subjects.StudentAssignments
{
    public partial class frmStudentSubjectList : Form
    {

            public enum enLayoutRole
            {
                Title,
                MainGrid,
                RightPrimaryAction,
                BottomSecondaryAction
            }

            private int _studentID;

            // ========================= Constructor =========================
            public frmStudentSubjectList(int studentID)
            {
                InitializeComponent();
                _studentID = studentID;
            }

            // ========================= Data =========================
            private DataTable _dtStudentSubjects;

            // ========================= Load Data =========================
            private void _loadData()
            {
                _dtStudentSubjects = clsStudentSubject.getSubjectsByStudent(_studentID);

                dgvStudentSubjects.DataSource = _dtStudentSubjects;
            }

            // ========================= Grid =========================
            private void _configureGrid()
            {
                dgvStudentSubjects.EnableHeadersVisualStyles = false;

                dgvStudentSubjects.DefaultCellStyle.BackColor = Color.FromArgb(0, 90, 80);
                dgvStudentSubjects.DefaultCellStyle.ForeColor = Color.White;
                dgvStudentSubjects.DefaultCellStyle.Font = new Font(dgvStudentSubjects.Font.FontFamily, 10, FontStyle.Bold);

                dgvStudentSubjects.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 90, 80);
                dgvStudentSubjects.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                if (dgvStudentSubjects.Columns.Count == 0)
                    return;

                dgvStudentSubjects.Columns["StudentSubjectID"].HeaderText = "ID";
                dgvStudentSubjects.Columns["StudentSubjectID"].Width = 60;

                dgvStudentSubjects.Columns["SubjectName"].HeaderText = "Subject";
                dgvStudentSubjects.Columns["SubjectName"].Width = 200;

                dgvStudentSubjects.Columns["Credits"].HeaderText = "Credits";
                dgvStudentSubjects.Columns["Credits"].Width = 80;

                dgvStudentSubjects.Columns["SemesterName"].HeaderText = "Semester";
                dgvStudentSubjects.Columns["SemesterName"].Width = 150;
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
                        c.Top = dgvStudentSubjects.Top;
                        _placeRight(c, dgvStudentSubjects);
                        break;

                    case enLayoutRole.BottomSecondaryAction:
                        c.Top = dgvStudentSubjects.Bottom + 15;
                        c.Left = btnAddNew.Left;
                        break;
                }
            }

            private void _applyLayout()
            {
                _applyLayout(lblTitle, enLayoutRole.Title);
                _applyLayout(dgvStudentSubjects, enLayoutRole.MainGrid);
                _applyLayout(btnAddNew, enLayoutRole.RightPrimaryAction);
                _applyLayout(btnClose, enLayoutRole.BottomSecondaryAction);
            }

            // ========================= Load =========================
            private void frmStudentSubjectsList_Load(object sender, EventArgs e)
            {
                _loadData();
                _configureGrid();
                _applyLayout();
            }

            private void frmStudentSubjectsList_Resize(object sender, EventArgs e)
            {
                _applyLayout();
            }

        // ========================= Delete =========================
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
            {
                int id = (int)dgvStudentSubjects.CurrentRow.Cells[0].Value;

                if (MessageBox.Show("Delete [" + id + "] ?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (clsStudentSubject.delete(id))
                        MessageBox.Show("Deleted successfully");
                    else
                        MessageBox.Show("Delete failed");
                }

                _loadData();
            }

            // ========================= Close =========================
            private void btnClose_Click(object sender, EventArgs e)
            {
                Close();
            }
        }
    }

