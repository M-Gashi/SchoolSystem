using SchoolSystem.Enums;
using SchoolSystem.Business;
using System;
using System.Windows.Forms;
using SchoolSystem.Business.Teachers;

namespace SchoolSystem.Presentation.Teachers
{
    public partial class ctrTeacherInfo : UserControl
    {
        public ctrTeacherInfo()
        {
            InitializeComponent();
        }

        private clsTeacher _teacher;
        private int _teacherID = -1;

        public int TeacherID
        {
            get { return _teacherID; }
        }

        //_____________________________________________________________________
        private void _fillTeacherInfo()
        {
            ctrPerson1.loadPersonInfo(_teacher.personID);

            lblTeacherID.Text = _teacher.teacherID.ToString();
            lblEmployeeNo.Text = _teacher.employeeNo;
            lblSpecialization.Text = _teacher.specialization;
            lblQualification.Text = _teacher.qualification;
            lblHireDate.Text = _teacher.hireDate.ToString();
            lblIsActive.Text = _teacher.isActive.ToString();
            lblNotes.Text = _teacher.notes;
        }

        //_____________________________________________________________________
        public void loadTeacherInfo(int teacherID)
        {
            _teacher = clsTeacher.find(teacherID);

            if (_teacher == null)
            {
                MessageBox.Show(
                    "No Teacher with ID = " + teacherID,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            _teacherID = teacherID;
            _fillTeacherInfo();
        }

        private void ctrTeacherInfo_Load(object sender, EventArgs e)
        {
        }
    }
}
