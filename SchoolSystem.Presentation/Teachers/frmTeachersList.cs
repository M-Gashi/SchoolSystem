using SchoolSystem.Enums;
using SchoolSystem.Business;
using SchoolSystem.Presentation.Students;
using SchoolSystem.Presentation.Teachers;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SchoolSystem.Business.People;
using SchoolSystem.Business.Teachers;

namespace SchoolSystem.Presentation.Teachers
{
    public partial class frmTeachersList : Form
    {
        public frmTeachersList()
        {
            InitializeComponent();
        }

  
        private static DataTable _dtAllTeachers = clsTeacher.getAllTeachers();

        private DataTable _dtTeachers =(_dtAllTeachers == null)? new DataTable(): _dtAllTeachers.DefaultView.ToTable
    
        (
            false,
            "TeacherID",
            "PersonID",
            "HireDate",
            "Specialization",
            "Qualification",
            "IsActive"
  


        
        );
   
        private void _refreshTeachersList()
        {
            _dtAllTeachers = clsTeacher.getAllTeachers();

            _dtTeachers = _dtAllTeachers.DefaultView.ToTable
            (
                false,
                "TeacherID",
            "PersonID",
            "HireDate",
            "Specialization",
            "Qualification",
            "IsActive"
            );

            dgvTeacher.DataSource = _dtTeachers;
        }

        private void _fitGridWidthToColumns(DataGridView dgv)
        {
            int totalWidth = 0;

            foreach (DataGridViewColumn col in dgv.Columns)
                if (col.Visible)
                    totalWidth += col.Width;

            if (dgv.RowHeadersVisible)
                totalWidth += dgv.RowHeadersWidth;

            dgv.Width = totalWidth + 2; // هامش بسيط
        }

        private void frmTeachersList_Load(object sender, EventArgs e)
        {
            
            //centroed the title
            lblTitle.Left = (this.ClientSize.Width - lblTitle.Width) / 2;
            lblTitle.Top = (this.ClientSize.Height - lblTitle.Height) / 7;
            //___________________________________
            dgvTeacher.Left = (this.ClientSize.Width - dgvTeacher.Width) / 2;
            dgvTeacher.Top = (this.ClientSize.Height - dgvTeacher.Height) - 60;
            //___________________________________
            // نفس ارتفاع الجرد
            btnAddNew.Top = dgvTeacher.Top;
            // على يمين الجرد بمسافة ثابتة
            btnAddNew.Left = dgvTeacher.Right + 60;

            // أسفل الجرد مباشرة
            btnClose.Top = dgvTeacher.Bottom + 20;
            // على يمين الجرد بمسافة ثابتة
            btnClose.Left = dgvTeacher.Right + 60;
            //___________________________________
            //___________________________________
            dgvTeacher.DefaultCellStyle.BackColor = Color.FromArgb(0, 90, 80);
            dgvTeacher.DefaultCellStyle.ForeColor = Color.White; // لون النص
            dgvTeacher.DefaultCellStyle.Font =
                new Font(dgvTeacher.DefaultCellStyle.Font.FontFamily, 10, FontStyle.Regular);
            dgvTeacher.DefaultCellStyle.Font = new Font(dgvTeacher.DefaultCellStyle.Font, FontStyle.Bold);
            //___________________________________
            dgvTeacher.EnableHeadersVisualStyles = false;  // يسمح باستخدام الألوان المخصصة
            dgvTeacher.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 90, 80);
            dgvTeacher.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;  // اختر لون نص مختلف للظهور
            dgvTeacher.ColumnHeadersDefaultCellStyle.Font =
                 new Font(dgvTeacher.ColumnHeadersDefaultCellStyle.Font.FontFamily, 11, FontStyle.Regular);
            //___________________________________
            dgvTeacher.ColumnHeadersDefaultCellStyle.Font = new Font(dgvTeacher.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold);
            // تعيين ارتفاع ثابت
            dgvTeacher.ColumnHeadersHeight = 40;
            //___________________________________
            // إذا أردت تعطيل تغيير الارتفاع تلقائيًا
            dgvTeacher.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //___________________________________
            // تغيير لون خلفية رأس الصف
            dgvTeacher.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 90, 80);
            // تغيير لون السهم (الـ ForeColor)
            dgvTeacher.RowHeadersDefaultCellStyle.ForeColor = Color.White;
            //___________________________________

            dgvTeacher.DataSource = _dtTeachers;

            if (dgvTeacher.Rows.Count > 0)
            {
                dgvTeacher.Columns[0].HeaderText = "TID";
                dgvTeacher.Columns[0].Width = 60;

                dgvTeacher.Columns[1].HeaderText = "PerID";
                dgvTeacher.Columns[1].Width = 60;

                dgvTeacher.Columns[2].HeaderText = "Teacher Name";
                dgvTeacher.Columns[2].Width = 200;

                dgvTeacher.Columns[3].HeaderText = "Birth Date";
                dgvTeacher.Columns[3].Width = 150;

                dgvTeacher.Columns[4].HeaderText = "Gender";
                dgvTeacher.Columns[4].Width = 80;

                dgvTeacher.Columns[5].HeaderText = "IsActive";
                dgvTeacher.Columns[5].Width = 80;

                //dgvTeacher.Columns[6].HeaderText = "Hire Date";
                //dgvTeacher.Columns[6].Width = 150;
            }

            _fitGridWidthToColumns(dgvTeacher);

        }





        private void btnAddNew_Click_1(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdateTeacher();
            frm.ShowDialog();
            _refreshTeachersList();
        }

        private void dgvTeacher_DoubleClick_1(object sender, EventArgs e)
        {
            if (dgvTeacher.SelectedRows.Count == 0)
                return;

            Form frm = new frmShowTeacherInfo((int)dgvTeacher.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void editToolStripMenu_Click(object sender, EventArgs e)
        {
            if (dgvTeacher.SelectedRows.Count == 0)
                return;

            frmAddUpdateTeacher frm = new frmAddUpdateTeacher((int)dgvTeacher.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _refreshTeachersList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (dgvTeacher.SelectedRows.Count == 0)
                return;

            if ((int)dgvTeacher.CurrentRow.Cells[0].Value == (11))
            {
                MessageBox.Show("We Cant Delete This Teacher", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete Teacher [" + dgvTeacher.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                //Perform Delele and refresh
                if (clsTeacher.deleteTeacherID((int)dgvTeacher.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Teacher Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Teacher was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            clsPerson.deletePerson((int)dgvTeacher.CurrentRow.Cells[1].Value);

            _refreshTeachersList();
        }
    }
}
