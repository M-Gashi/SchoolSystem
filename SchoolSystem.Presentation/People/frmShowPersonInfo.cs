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

namespace SchoolSystem.Presentation.People
{
    public partial class frmShowPersonInfo : Form
    {
        public frmShowPersonInfo(int personID)
        {
            InitializeComponent();
            ctrPerson1.loadPersonInfo(personID);
        }

        private void frmShowPersonInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
