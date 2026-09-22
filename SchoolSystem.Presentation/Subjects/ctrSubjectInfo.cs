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
using SchoolSystem.Business.Subjects;

namespace SchoolSystem.Presentation.Subjects
{
    public partial class ctrSubjectInfo : UserControl
    {
        public ctrSubjectInfo()
        {
            InitializeComponent();
        }

        private clsSubject _subject;

        private int _subjectID = -1;

        public int subjectID
        {
            get { return _subjectID; }
        }

        //_________________________________________________________________________________________________________
        public void resetSubjectInfo()
        {
            _subjectID = -1;
            lblSubjectName.Text = "";
            lblSubjectCode.Text = "";
            lblDescription.Text = "";
            lblCredits.Text = "";
            lblIsActive.Text = "";
        }
        //_______________________________________________________
        private void _FillSubjectInfo()
        {
            lblSubjectID.Text   = _subject.subjectID.ToString();
            lblSubjectName.Text = _subject.subjectName.ToString();
            lblSubjectCode.Text = _subject.subjectCode;
            lblDescription.Text = _subject.description;
            lblCredits.Text = _subject.credits.ToString();
            lblIsActive.Text = _subject.isActive.ToString();
 
        }
        //_______________________________________________________
        public void loadSubjectInfo(int subjectID)
        {
            _subject = clsSubject.findSubject(subjectID);
            if (_subject == null)
            {
                resetSubjectInfo();
                MessageBox.Show("No Person with PersonID = " + subjectID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillSubjectInfo();
        }

        private void ctrSubjectInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
