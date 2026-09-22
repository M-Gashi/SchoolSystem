using SchoolSystem.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SchoolSystem.Business;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

//___________________________________________________________________________
//___________________________________________________________________________
using SchoolSystem.Business.People;

namespace SchoolSystem.Presentation.People
{
    public partial class frmPersonsList : Form
    {
        public frmPersonsList()
        {
            InitializeComponent();
        }
        //___________________________________________________________________________
        private static DataTable _dtAllPerson = clsPerson.getAllPerson();
        private DataTable _dtPerson = _dtAllPerson.DefaultView.ToTable
            (
            false,
            "PersonID",
            "FirstName",
            "LastName",
            "BirthDate",
            "Phone",
            "Email",
            "CountryName"
            );

        //___________________________________________________________________________
        private void _RefreshPeoplList()
        {
            _dtAllPerson = clsPerson.getAllPerson();
            _dtPerson = _dtAllPerson.DefaultView.ToTable
            (
            false,
            "PersonID",
            "FirstName",
            "LastName",
            "BirthDate",
            "Phone",
            "Email",
            "CountryName"
            );



            dgvStudents.DataSource = _dtPerson;

        }
        //_________________________________________________________________
        //________________________________
        //_________________
        //_______
        private void frmPersonsList_Load(object sender, EventArgs e)
        {
            //centroed the title
            lblTitle.Left = (this.ClientSize.Width - lblTitle.Width) / 2;
            lblTitle.Top = (this.ClientSize.Height - lblTitle.Height) / 7;
            //___________________________________
            dgvStudents.Left = (this.ClientSize.Width - dgvStudents.Width) / 2;
            dgvStudents.Top = (this.ClientSize.Height - dgvStudents.Height) - 60;
            //___________________________________
            dgvStudents.DefaultCellStyle.BackColor = Color.FromArgb(0, 90, 80);
            dgvStudents.DefaultCellStyle.ForeColor = Color.White; // لون النص
            dgvStudents.DefaultCellStyle.Font =
                new Font(dgvStudents.DefaultCellStyle.Font.FontFamily, 10, FontStyle.Regular);
            dgvStudents.DefaultCellStyle.Font = new Font(dgvStudents.DefaultCellStyle.Font, FontStyle.Bold);
            //___________________________________
            dgvStudents.EnableHeadersVisualStyles = false;  // يسمح باستخدام الألوان المخصصة
            dgvStudents.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 90, 80);
            dgvStudents.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;  // اختر لون نص مختلف للظهور
            dgvStudents.ColumnHeadersDefaultCellStyle.Font =
                new Font(dgvStudents.ColumnHeadersDefaultCellStyle.Font.FontFamily, 11, FontStyle.Regular);
            //___________________________________
            dgvStudents.ColumnHeadersDefaultCellStyle.Font = new Font(dgvStudents.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold);
            // تعيين ارتفاع ثابت
            dgvStudents.ColumnHeadersHeight = 40;
            //___________________________________
            // إذا أردت تعطيل تغيير الارتفاع تلقائيًا
            dgvStudents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //___________________________________
            // تغيير لون خلفية رأس الصف
            dgvStudents.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 90, 80);
            // تغيير لون السهم (الـ ForeColor)
            dgvStudents.RowHeadersDefaultCellStyle.ForeColor = Color.White;
            //___________________________________
            dgvStudents.DataSource = _dtPerson;

            //cbFilterBy.SelectedIndex = 0;
            //lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();

            if (dgvStudents.Rows.Count > 0)
            {



                dgvStudents.Columns[0].HeaderText = "ID";
                dgvStudents.Columns[0].Width = 30;


                dgvStudents.Columns[1].HeaderText = "First Name";
                dgvStudents.Columns[1].Width = 120;

                dgvStudents.Columns[2].HeaderText = "Last Name";
                dgvStudents.Columns[2].Width = 120;

                dgvStudents.Columns[3].HeaderText = "Date Of Birth";
                dgvStudents.Columns[3].Width = 130;


                dgvStudents.Columns[4].HeaderText = "Phone";
                dgvStudents.Columns[4].Width = 120;

                dgvStudents.Columns[5].HeaderText = "Email";
                dgvStudents.Columns[5].Width = 170;

                dgvStudents.Columns[6].HeaderText = "Country Name";
                dgvStudents.Columns[6].Width = 100;
            }
            //___________________________________
            btnAddNew.Top = (this.ClientSize.Height - dgvStudents.Height) - 60;
            //___________________________________





        }
        //_______
        //_________________
        //________________________________
        //___________________________________________________________________________
        private void dgvStudents_DoubleClick(object sender, EventArgs e)
        {
            Form frm = new frmShowPersonInfo((int)dgvStudents.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        //___________________________________________________________________________
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdatePerson();
            frm.ShowDialog();
            _RefreshPeoplList();

        }
        //___________________________________________________________________________
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((int)dgvStudents.CurrentRow.Cells[0].Value == (1))
            {
                MessageBox.Show("We Cant Delete This Person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (MessageBox.Show("Are you sure you want to delete Person [" + dgvStudents.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)

            {

                //Perform Delele and refresh
                if (clsPerson.deletePerson((int)dgvStudents.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                else
                    MessageBox.Show("Person was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            _RefreshPeoplList();
        }
        //_________________________________________________________________
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdatePerson((int)dgvStudents.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshPeoplList();
        }
        //_________________________________________________________________
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //___________________________________________________________________________


    }
}
