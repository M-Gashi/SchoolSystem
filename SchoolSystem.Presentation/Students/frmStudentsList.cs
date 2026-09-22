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
using SchoolSystem.Business.People;
using SchoolSystem.Business.Quran;
using SchoolSystem.Business.Students;
using SchoolSystem.Enums.AcademicStructure;
using SchoolSystem.Enums.Common;
using SchoolSystem.Enums.Quran;
using SchoolSystem.Presentation.Quran.StudentProgress;
using SchoolSystem.Presentation.Subjects;

namespace SchoolSystem.Presentation.Students
{
    public partial class frmStudentsList : Form
    {    // ___________________________________________________________________________________
        // ___________________________________________________________ Enums _________________

        enPartMode _enPartMode;

        // =========================================================== Fields  =================
        // ===========================================================         =================

        enInvocationContext _invocationContext;

        private static DataTable _dtAllStudent = clsStudent.getAllStudent();
        private DataTable _dtStudents = _dtAllStudent.DefaultView.ToTable
            (
            false,

            "StudentID",
            "PersonID",
            "Studentname",
            "BirthDate",
            "Gender",
            "GradeName",
            "SectionName",

            "IsActive",
            "AdmissionDate"
            );
        //______________________________________________

        private int _studentID;

        // =========================================================== Constructor  ===========
        // ===========================================================              ===========

        public frmStudentsList()
        {
            InitializeComponent();
            _invocationContext = enInvocationContext.StudentSubjects;


        }


        // =========================================================== Form Initialization ====
        // ===========================================================                     ====
        
        private void _initializeForm()
        {
            this.WindowState = FormWindowState.Maximized;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }
        private void _configureGrid()
        {
            dgvStudents2.DataSource = _dtStudents;

            dgvStudents2.DefaultCellStyle.BackColor = Color.FromArgb(0, 90, 80);
            dgvStudents2.DefaultCellStyle.ForeColor = Color.White;
            dgvStudents2.DefaultCellStyle.Font =
                new Font(dgvStudents2.DefaultCellStyle.Font.FontFamily, 10, FontStyle.Bold);

            dgvStudents2.EnableHeadersVisualStyles = false;

            dgvStudents2.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(0, 90, 80);

            dgvStudents2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dgvStudents2.ColumnHeadersDefaultCellStyle.Font =
                new Font(dgvStudents2.ColumnHeadersDefaultCellStyle.Font.FontFamily,
                         11, FontStyle.Bold);

            dgvStudents2.ColumnHeadersHeight = 40;

            dgvStudents2.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgvStudents2.RowHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(0, 90, 80);

            dgvStudents2.RowHeadersDefaultCellStyle.ForeColor = Color.White;

            if (dgvStudents2.Rows.Count == 0)
                return;

            dgvStudents2.Columns[0].HeaderText = "StuID";
            dgvStudents2.Columns[0].Width = 50;

            dgvStudents2.Columns[1].HeaderText = "perID";
            dgvStudents2.Columns[1].Width = 50;

            dgvStudents2.Columns[2].HeaderText = "Student Name";
            dgvStudents2.Columns[2].Width = 200;

            dgvStudents2.Columns[3].HeaderText = "Date Of Birth";
            dgvStudents2.Columns[3].Width = 150;

            dgvStudents2.Columns[4].HeaderText = "Gender";
            dgvStudents2.Columns[4].Width = 80;

            dgvStudents2.Columns[5].HeaderText = "Grade";
            dgvStudents2.Columns[5].Width = 80;

            dgvStudents2.Columns[6].HeaderText = "Section";
            dgvStudents2.Columns[6].Width = 80;

            dgvStudents2.Columns[7].HeaderText = "IsActive";
            dgvStudents2.Columns[7].Width = 80;

            dgvStudents2.Columns[8].HeaderText = "Admission Date";
            dgvStudents2.Columns[8].Width = 150;

            
        }
        private void _setupLayout()
        {
            // العنوان
            lblTitle.Left = (this.ClientSize.Width - lblTitle.Width) / 2;
            lblTitle.Top = (this.ClientSize.Height - lblTitle.Height) / 7;

            // الجرد
            dgvStudents2.Left = (this.ClientSize.Width - dgvStudents2.Width) / 2;
            dgvStudents2.Top = (this.ClientSize.Height - dgvStudents2.Height) - 120;

            // زر الإضافة
            btnAddNew.Top = dgvStudents2.Top;
            btnAddNew.Left = dgvStudents2.Right + 30;

     
        }

        private void frmStudentsList_Load(object sender, EventArgs e)
        {
            _initializeForm();
            _configureGrid();
            _setupLayout();

        }

        // =========================================================== Student Data ===========
        // ===========================================================              ===========

        private void _refrishStudentsList()
        {
            _dtAllStudent = clsStudent.getAllStudent();

            _dtStudents = _dtAllStudent.DefaultView.ToTable
            (
            false,

            "StudentID",
            "PersonID",
            "Studentname",
            "BirthDate",
            "Gender",
            "GradeName",
            "SectionName",
            "IsActive",
            "AdmissionDate"
            );

            dgvStudents2.DataSource = _dtStudents;
        }

        // =========================================================== Form Layout ============
        // ===========================================================             ============

        private void frmStudentsList_Resize(object sender, EventArgs e)
        {

        }

        // =========================================================== Student Display =======
        // ===========================================================                 =======
  
        private void dgvStudents2_DoubleClick(object sender, EventArgs e)
        {

            clsQuranTracks _quranTracks = new clsQuranTracks();

            _quranTracks = clsQuranTracks.findQuranTrack(enQuranTrackName.الحفظ.ToString());

            _enPartMode = enPartMode.StudentPart;

            _studentID = (int)dgvStudents2.CurrentRow.Cells[0].Value;

            Form frm = new frmStudentParts(_enPartMode, _quranTracks.quranTrackID, enQuranTrackName.الحفظ, _studentID);
            frm.ShowDialog();

        }

        // =========================================================== Student Management =====
        // ===========================================================                    =====

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmShowStudentInfo((int)dgvStudents2.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddUpdateStudent frm = new frmAddUpdateStudent();

            frm.ShowDialog();

            _refrishStudentsList();
        }
        private void editToolStripMenu_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdateStudent((int)dgvStudents2.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _refrishStudentsList();
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if ((int)dgvStudents2.CurrentRow.Cells[0].Value == (11))
            {
                MessageBox.Show("We Cant Delete This Students", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete Person [" + dgvStudents2.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                //Perform Delele and refresh
                if (clsStudent.deleteStudentID((int)dgvStudents2.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Person was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            clsPerson.deletePerson((int)dgvStudents2.CurrentRow.Cells[1].Value);

            _refrishStudentsList();
        }


        // =========================================================== Student Subjects =======
        // ===========================================================                  =======

        private void btnTrreSub_Click(object sender, EventArgs e)
        {
            Form form = new frmSubjectTree();
            form.ShowDialog();
        }
        private void cmsMySubjects_Click(object sender, EventArgs e)
        {
            frmSubjectTree frm = new frmSubjectTree((int)dgvStudents2.CurrentRow.Cells[0].Value, _invocationContext);
            frm.ShowDialog();
        }

        // =========================================================== Form Closing ============
        // ===========================================================              ============

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

       
    }
}
