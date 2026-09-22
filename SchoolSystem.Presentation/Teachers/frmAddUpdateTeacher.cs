using SchoolSystem.Enums;
using SchoolSystem.Business;
using System;
using System.Windows.Forms;
using SchoolSystem.Business.Teachers;
using SchoolSystem.Presentation.People;

namespace SchoolSystem.Presentation.Teachers
{
    public partial class frmAddUpdateTeacher : Form
    {
        public enum enMode { addNew, update }
        enMode _mode;

        clsTeacher _teacher;
        int _teacherID = -1;

        //_______________________________
        public frmAddUpdateTeacher()
        {
            InitializeComponent();

            _mode = enMode.addNew;

            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.dataBack += dataBackEvent;
            frm.ShowDialog();
        }
        public frmAddUpdateTeacher(int teacherID)
        {
            InitializeComponent();

            _teacherID = teacherID;
            _mode = enMode.update;
        }
        //_______________________________
        private void dataBackEvent(object sender, int personID)
        {
            txtPersooonIDBack.Text = personID.ToString();
        }
        public void FilterFocus()
        {
            txtPersooonIDBack.Focus();
        }
        //_______________________________
        private void _resetDefaultValues()
        {
            if (_mode == enMode.addNew)
            {
                lblTitle.Text = "Add New Teacher";
                this.Text = "Add New Teacher";
                _teacher = new clsTeacher();
            }
            else
            {
                lblTitle.Text = "Update Teacher";
                this.Text = "Update Teacher";
            }


            txtEmployeeNo.Text = string.Empty;
            rbYes.Checked = true;
            txtSpecialization.Text = string.Empty;
            txtQualification.Text = string.Empty;
            txtNotes.Text = "";
            
        }
        //_______________________________
        private void _loadData()
        {
            _teacher = clsTeacher.find(_teacherID);

            if (_teacher == null)
            {
                MessageBox.Show("Teacher Not Found", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            txtPersooonIDBack.Text = _teacher.personID.ToString();
            lblTeacherID.Text      = _teacher.teacherID.ToString();
            txtEmployeeNo.Text     = _teacher.employeeNo.ToString();
            dtpHireDate.Value      = _teacher.hireDate;
            rbYes.Checked = _teacher.isActive;
            txtSpecialization.Text = _teacher.specialization;
            txtQualification.Text  = _teacher.qualification;
            txtNotes.Text          = _teacher.notes;
        }
        //_______________________________
        private void frmAddUpdateTeacher_Load(object sender, EventArgs e)
        {
            _resetDefaultValues();

            if (_mode == enMode.update)
                _loadData();
        }

        //_______________________________
        
        //_______________________________
        private void btnSaveNewEditP_Click(object sender, EventArgs e)
        {
            _teacher.personID = int.Parse(txtPersooonIDBack.Text);
            _teacher.employeeNo = txtEmployeeNo.Text.Trim();
            _teacher.hireDate = dtpHireDate.Value;
            _teacher.isActive = rbYes.Checked;
            _teacher.specialization = txtSpecialization.Text.Trim();
            _teacher.qualification = txtQualification.Text.Trim();
            _teacher.notes = txtNotes.Text.Trim();

            if (_teacher.save())
            {
                lblTeacherID.Text = _teacher.teacherID.ToString();
                _mode = enMode.update;

                lblTitle.Text = "Update Teacher";
                this.Text = "Update Teacher";

                MessageBox.Show("Data Saved Successfully",
                    "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Save Failed",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //_______________________________
        private void btnUpdatePerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson(_teacher.personID);    
            frm.ShowDialog();
        }
        //_______________________________
        private void btnCloseNewEditS_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
