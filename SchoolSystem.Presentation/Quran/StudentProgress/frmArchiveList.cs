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
using SchoolSystem.Business.Quran.StudentProgress;

namespace SchoolSystem.Presentation.Quran.StudentProgress
{
    public partial class frmArchiveList : Form
    {
        clsStudentParts _studentParts;

        private int _quranTrackID;

        public frmArchiveList(int quranTrackID)
        {
            InitializeComponent();

            _quranTrackID = quranTrackID;

        }
        // =================================================================================
        // ========================= Data =========================
        private static DataTable _dtAllStudents;
        private DataTable _dtStudents;
        // ========================= Grid Configuration =========================
        private void _loadSubjects()
        {
            _dtAllStudents = clsStudentParts.getAllStudentPartsArchived(_quranTrackID);

            _dtStudents = (_dtAllStudents == null) ? new DataTable() : _dtAllStudents.DefaultView.ToTable
                (
                    false,
                    "StudentName",
                    "PartName",
                    "isArchived",
                    "StudentPartID",
                    "QuranTrackID"
                    
                );

            dgvArchivelist.DataSource = _dtStudents;
            dgvArchivelist.Columns["StudentPartID"].Visible = false;
            dgvArchivelist.Columns["QuranTrackID"].Visible = false;

        }
        private void _configureGrid()
        {
            dgvArchivelist.EnableHeadersVisualStyles = false;

            // إخفاء العمود الافتراضي للـ Row Headers
            dgvArchivelist.RowHeadersVisible = false;

            // إظهار الإزاحة العمودية فقط
            dgvArchivelist.ScrollBars = ScrollBars.Vertical;

            // Cells
            dgvArchivelist.DefaultCellStyle.BackColor = Color.FromArgb(0, 90, 80);
            dgvArchivelist.DefaultCellStyle.ForeColor = Color.White;
            dgvArchivelist.DefaultCellStyle.Font =
                new Font(dgvArchivelist.Font.FontFamily, 10, FontStyle.Bold);

            // Column headers
            dgvArchivelist.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 90, 80);
            dgvArchivelist.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvArchivelist.ColumnHeadersDefaultCellStyle.Font =
                new Font(dgvArchivelist.Font.FontFamily, 11, FontStyle.Bold);

            dgvArchivelist.ColumnHeadersHeight = 40;
            dgvArchivelist.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgvArchivelist.BorderStyle = BorderStyle.FixedSingle;
            dgvArchivelist.GridColor = Color.White;

            // Columns
            if (dgvArchivelist.Columns.Count == 0)
                return;

            dgvArchivelist.Columns["StudentName"].HeaderText = "Student Name";

            // جعل عرض الأعمدة حسب العنوان
            foreach (DataGridViewColumn column in dgvArchivelist.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            

                dgvArchivelist.Columns["StudentName"].Width = 200;
                dgvArchivelist.Columns["PartName"].Width = 120;
                dgvArchivelist.Columns["isArchived"].Width = 100;
                


                dgvArchivelist.Columns["StudentPartID"].Width = 0;
                dgvArchivelist.Columns["QuranTrackID"].Width = 0;
            }

            // حساب مجموع عروض الأعمدة
            int totalWidth = 0;

            foreach (DataGridViewColumn column in dgvArchivelist.Columns)
            {
                totalWidth += column.Width;
            }

            // إضافة مساحة للحدود وشريط الإزاحة العمودي
            dgvArchivelist.Width = totalWidth - 10;

            //dgvArchivelist.Width = 450;

            foreach (DataGridViewColumn column in dgvArchivelist.Columns)
            {
                column.Resizable = DataGridViewTriState.False;
                dgvArchivelist.AllowUserToResizeColumns = false;
                dgvArchivelist.AllowUserToResizeRows = false;
            }
            // ========================= 

            dgvArchivelist.ReadOnly = false;

            foreach (DataGridViewColumn column in dgvArchivelist.Columns)
            {
                column.ReadOnly = true;
            }

            dgvArchivelist.Columns["isArchived"].ReadOnly = false;


        }

       
        private void frmArchiveList_Resize_1(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // تنصيف العنوان أفقيًا
            lblPartName.Left =
                (this.ClientSize.Width - lblPartName.Width) / 2;

            // وضع الزر في أقصى اليمين
            btnClose.Left =
                this.ClientSize.Width - btnClose.Width - 10;

            // محاذاة الزر عموديًا مع العنوان
            btnClose.Top = this.ClientSize.Height - btnClose.Height - 10;
        }
 
        // ========================= Load =========================          
        private void frmArchiveList_Load(object sender, EventArgs e)
        {
            _loadSubjects();
            _configureGrid();
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvArchivelist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            //_________________________________________
            
            //_________________________________________
            if (dgvArchivelist.Columns[e.ColumnIndex].Name == "IsArchived")
            {
                if (MessageBox.Show("هل تريد إلغاء أرشفة هذا الطالب؟", "تأكيد",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    _loadSubjects();
                    return;
                }
                _studentParts = clsStudentParts.findStudentPart(Convert.ToInt32(dgvArchivelist.Rows[e.RowIndex].Cells["StudentPartID"].Value));

                if (_studentParts == null)
                    return;

                //_studentParts.quranTrackID = _quranTrackID;
                _studentParts.isArchived = false;
             

                if (_studentParts.save())
                {
                    _loadSubjects();
                }
            }


        }

        
    }
}
