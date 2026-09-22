using SchoolSystem.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SchoolSystem.Presentation.Subjects.StudentAssignments;

namespace SchoolSystem.Presentation.Students
{
    public partial class frmShowStudentInfo : Form
    {

        int _studentID = -1;


        public frmShowStudentInfo(int studentID)
        {
            InitializeComponent();
            ctrStudent1.loadStudintInfo(studentID);

            _studentID = studentID;

        }



        private void btnStudentSubjects_Click(object sender, EventArgs e)
        {
            Form frm = new frmStudentSubjectList(_studentID);
            frm.ShowDialog();

        }


    }
}
