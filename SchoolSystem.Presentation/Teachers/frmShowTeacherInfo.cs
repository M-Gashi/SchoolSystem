using SchoolSystem.Enums;
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

namespace SchoolSystem.Presentation.Teachers
{
    public partial class frmShowTeacherInfo : Form
    {
        public frmShowTeacherInfo(int TeacherID)
        {
            InitializeComponent();
            ctrTeacherInfo1.loadTeacherInfo(TeacherID);
        }


        private void frmShowTeacherInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
