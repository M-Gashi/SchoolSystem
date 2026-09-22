using SchoolSystem.Enums;
using SchoolSystem.Business;
using SchoolSystem.Presentation.Students;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SchoolSystem.Business.Students;

namespace SchoolSystem.Presentation.Quran.StudentProgress
{
    public partial class ctrStudentList : UserControl
    {
        public delegate void dataBackStudentName(int studentNameID);

        // Declare an event using the delegate
        public event dataBackStudentName DataBack;
        // ========================= Constructor =========================
        public ctrStudentList()
        {
            InitializeComponent();
            dgvStudents.RowHeadersVisible = false;
            dgvStudents.ColumnHeadersVisible = false;

        }
        // =================================================================================
        // ========================= Data =========================
        private static DataTable _dtAllStudents;
        private DataTable _dtStudents;
        // ========================= Grid Configuration =========================
        private void _loadSubjects()
        {
            _dtAllStudents = clsStudent.getAllStudent();

            _dtStudents = (_dtAllStudents == null) ? new DataTable() : _dtAllStudents.DefaultView.ToTable
                (
                    false,
                    "Studentname",
                    "StudentID"
                );

            dgvStudents.DataSource = _dtStudents;
        }
        private void _configureGrid()
        {
            dgvStudents.EnableHeadersVisualStyles = false;

            // Cells
            dgvStudents.DefaultCellStyle.BackColor = Color.FromArgb(0, 90, 80);
            dgvStudents.DefaultCellStyle.ForeColor = Color.White;
            dgvStudents.DefaultCellStyle.Font = new Font(dgvStudents.Font.FontFamily, 10, FontStyle.Bold);

            // Column headers
            dgvStudents.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 90, 80);
            dgvStudents.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvStudents.ColumnHeadersDefaultCellStyle.Font = new Font(dgvStudents.Font.FontFamily, 11, FontStyle.Bold);
            dgvStudents.ColumnHeadersHeight = 40;
            dgvStudents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgvStudents.BorderStyle = BorderStyle.FixedSingle;
            dgvStudents.GridColor = Color.White;

            // Row headers
            dgvStudents.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 70, 200);
            dgvStudents.RowHeadersDefaultCellStyle.ForeColor = Color.White;

            // Columns
            if (dgvStudents.Columns.Count == 0)
                return;

            dgvStudents.Columns["Studentname"].HeaderText = "Student Name";
            dgvStudents.Columns["Studentname"].Width = 290;
        }
        // ========================= Load =========================          
        public void loadData()
        {
            _loadSubjects();
            _configureGrid();

        }

        private void dgvStudents_DoubleClick(object sender, EventArgs e)
        {
            // Trigger the event to send data back to the caller form.
            DataBack?.Invoke((int)dgvStudents.CurrentRow.Cells[1].Value);
        }
    }
}
