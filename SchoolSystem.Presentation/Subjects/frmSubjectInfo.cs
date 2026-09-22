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

namespace SchoolSystem.Presentation.Subjects
{
    public partial class frmSubjectInfo : Form
    {
        public frmSubjectInfo(int subjectID)
        {
            InitializeComponent();
            ctrSubjectInfo1.loadSubjectInfo(subjectID);
        }
    }
}
