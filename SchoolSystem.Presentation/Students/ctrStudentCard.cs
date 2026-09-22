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
using SchoolSystem.Business.Students;

namespace SchoolSystem.Presentation.Students
{
    public partial class ctrStudent : UserControl
    {
        public ctrStudent()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint
                        | ControlStyles.UserPaint
                        | ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();

        }

        private clsStudent _student;

        private int _studentID = -1;

        public int studentID
        {
            get { return _studentID; }
        }
        //_____________________________________________________________________
        //_____________________________________________________________________
        public void _fillStudintInfo()
        {

            ctrPerson1.loadPersonInfo(_student.personID);

            lblStudentID.Text = _student.studentID.ToString();
            lblStage.Text      = _student.sectionInfo.gradeInfo.stageInfo.stageName;
            lblGrade.Text     = _student.sectionInfo.gradeInfo.gardeName;
            lblSection.Text   = _student.sectionInfo.sectionName;
            lblAdmDate.Text   = _student.admissionDate.ToString();
            lblIsActive.Text  = _student.IsActive.ToString();
            lblNotes.Text     = _student.notes.ToString();

        }
        //_____________________________________________________________________
        public void loadStudintInfo(int studentID)
        {
            _student = clsStudent.find(studentID);
            if (_student == null)
            {
                MessageBox.Show("No Person with PersonID = " + studentID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            _fillStudintInfo();
        }

        private void ctrPerson1_Load(object sender, EventArgs e)
        {

        }

        private void ctrStudent_Load(object sender, EventArgs e)
        {

        }


        //_____________________________________________________________________


    }
}
