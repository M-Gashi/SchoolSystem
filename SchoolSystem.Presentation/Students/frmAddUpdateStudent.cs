using SchoolSystem.Enums;
using SchoolSystem.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using SchoolSystem.Business.AcademicStructure;
using SchoolSystem.Business.People;
using SchoolSystem.Business.Students;
using SchoolSystem.Presentation.AcademicStructure;
using SchoolSystem.Presentation.People;

namespace SchoolSystem.Presentation.Students
{
    public partial class frmAddUpdateStudent : Form
    {
        public enum enMode { addNew, update }
        enMode _mode;

        clsStudent _student;

        int _studentID = -1;

        static int _sectionIDDataBack;

        clsSection _sectionInfo;



        //________________________________
        public frmAddUpdateStudent()
        {
            InitializeComponent();

            _mode = enMode.addNew;

            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.dataBack += dataBackEvent;
            frm.ShowDialog();

        }
        public frmAddUpdateStudent(int studentID)
        {
            InitializeComponent();

            _studentID = studentID;
            _mode = enMode.update;

        }
        //________________________________________________________________________________
        private void frmAddUpdateStudent_Shown(object sender, EventArgs e)
        {
            FilterFocus();
        }
        //________________________________________________________________________________
        private void dataBackEvent(object sender, int personID)
        {
            txtPersooonIDBack.Text = personID.ToString();

        }

        //________________________________________________________________________________
        public void FilterFocus()
        {
            txtPersooonIDBack.Focus();
        }
        //________________________________________________________________________________
        private void _resetDefultValues()
        {
            if (_mode == enMode.addNew)
            {
                lblTitle.Text = "Add New Student";
                this.Text = "Add New Student";

                _student = new clsStudent();
            }
            else
            {
                lblTitle.Text = "Update Student";
                this.Text = "Update Student";
            }

            rbYes.Enabled   = true;
            lblGrade.Text   = string.Empty;
            lblSection.Text = string.Empty;
            txtNote.Text    = string.Empty;


        }
        //________________________________________________________________________________
        private void _loadData()
        {
            _student = clsStudent.find(_studentID);
            //___________________________________________
            if (_student == null)
            {
                MessageBox.Show("No Student with ID = " + _student, "Student Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }
            //___________________________________________
            txtPersooonIDBack.Text = _student.personID.ToString();
            lblStudentID.Text      = _student.studentID.ToString();
            dtpDateOfBirthee.Value = _student.admissionDate;
            rbYes.Checked          = _student.IsActive;
            lblStage.Text          = _student.sectionInfo.gradeInfo.stageInfo.stageName;
            lblGrade.Text          = _student.sectionInfo.gradeInfo.gardeName;
            lblSection.Text        = _student.sectionInfo.sectionName;
            txtNote.Text           = _student.notes;

        }
        //________________________________________________________________________________
        private void frmAddUpdateStudent_Load(object sender, EventArgs e)
        {
            _resetDefultValues();

            if (_mode == enMode.update)
                _loadData();
        }
        //________________________________________________________________________________
        private void btnUpdatePerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson(_student.personID);
            frm.ShowDialog();
        }
        //________________________________________________________________________________
        private void btnCloseNewEditS_Click(object sender, EventArgs e)
        {
            Close();

            if(txtPersooonIDBack.Text != "")
            clsPerson.deletePerson(int.Parse(txtPersooonIDBack.Text));

        }
        //________________________________________________________________________________
        private void btnSaveNewEditP_Click(object sender, EventArgs e)
        {


            _student.personID = int.Parse(txtPersooonIDBack.Text.Trim());
            _student.admissionDate = dtpDateOfBirthee.Value;
            if (rbYes.Checked)
                _student.IsActive = rbYes.Checked;
            else
                _student.IsActive = false;

            _student.sectionInfo.gradeInfo.stageInfo.stageName = lblStage.Text.Trim();
            _student.sectionInfo.gradeInfo.gardeName           = lblGrade.Text.Trim();
            _student.sectionInfo.sectionName                   = lblSection.Text.Trim();


            _student.notes     = txtNote.Text.Trim();

            if (_student.save())
            {
                lblStudentID.Text = _student.studentID.ToString();

                //change form mode to update.
                _mode = enMode.update;

                lblTitle.Text = "Update User";
                this.Text = "Update User";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        //________________________________________________________________________________
        private void frmEducationStructureTree_DataBack(int sectionnID)
        {
            
            // Handle the data received from Form2
            _sectionIDDataBack = sectionnID;

            _student.sectionID = _sectionIDDataBack;

            _sectionInfo = clsSection.findSection(_sectionIDDataBack);

            lblStage.Text = _sectionInfo.gradeInfo.stageInfo.stageName;
            lblGrade.Text = _sectionInfo.gradeInfo.gardeName;
            lblSection.Text = _sectionInfo.sectionName;
        }
        //____________________________________
        private void btnAssignment_Click(object sender, EventArgs e)
        {
            frmEducationStructureTree frT = new frmEducationStructureTree();
            frT.disableCmsAddChildren2();
            frT.sectionBack += frmEducationStructureTree_DataBack; 
            frT.ShowDialog();  
        }


        //________________________________________________________________________________



        //________________________________________________________________________________

        //________________________________________________________________________________



    }
}
