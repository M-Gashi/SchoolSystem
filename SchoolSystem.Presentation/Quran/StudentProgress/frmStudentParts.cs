using SchoolSystem.Enums;
using SchoolSystem.Business;
using SchoolSystem.Presentation.Quran.Parts;
using SchoolSystem.Presentation.Quran.StudentProgress;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SchoolSystem.Presentation.Quran.StudentProgress.ctrStudentList;
using SchoolSystem.Business.Quran.Parts;
using SchoolSystem.Business.Quran.StudentProgress;
using SchoolSystem.Enums.Common;
using SchoolSystem.Enums.Quran;

namespace SchoolSystem.Presentation.Quran.StudentProgress
{
    public partial class frmStudentParts : Form
    {
        // ___________________________________________________________________________________
        // ___________________________________________________________ Enums _________________

        enPartMode _enPartMode;

        enQuranTrackName _enQuranTrack;
        
        // ___________________________________________________________________________________
        // ___________________________________________________________ Fields ________________

        private int _quranTrackID;

        ctrQuranPartPages _selectedControl;

        string _title = "";

        private int _studentID;

        // ___________________________________________________________________________________
        // ___________________________________________________________ Properties ____________

        // ___________________________________________________________________________________
        // ___________________________________________________________ Constructor ___________

        public frmStudentParts(enPartMode partMode, int quranTrackID, enQuranTrackName enQuranTrack)
        {
            InitializeComponent();

            _enPartMode = partMode;

            _quranTrackID = quranTrackID;

            _enQuranTrack = enQuranTrack;

            _title = enQuranTrack.ToString();
        }
        //______________________________________________
        public frmStudentParts(enPartMode partMode, int quranTrackID, enQuranTrackName enQuranTrack, int studentID)
        {
            InitializeComponent();

            _enPartMode = partMode;

            _quranTrackID = quranTrackID;

            _enQuranTrack = enQuranTrack;

            _title = enQuranTrack.ToString();

            _studentID = studentID;
        }

        // ___________________________________________________________________________________
        // ___________________________________________________________ Form Initialization ___

        private void _fillPartNamesInComoboBox()
        {
            

            DataTable dtParts = clsParts.AllPartNames();

            cbPartsName.DataSource = dtParts;
            cbPartsName.DisplayMember = "PartName";
            cbPartsName.ValueMember = "PartsID";

            cbPartsName.SelectedIndex = 29;
        }
        //______________________________________________
        private void frmStudentParts_Load(object sender, EventArgs e)
        {
            _fillPartNamesInComoboBox();
        }

        // ___________________________________________________________________________________
        // ___________________________________________________________ Form Layout ___________

        private void _setupLayout()
        {
            fLPStudentParts.AutoSize = false;

            // يأخذ عرض الفورم بالكامل
            fLPStudentParts.Left = 0;
            fLPStudentParts.Width = this.ClientSize.Width;

            // يبدأ من تحت 150px
            fLPStudentParts.Top = 150;

            // يأخذ باقي الارتفاع
            fLPStudentParts.Height = this.ClientSize.Height - 150;

            // مهم جداً للـ scroll
            fLPStudentParts.AutoScroll = true;

            // ترتيب العناصر
            fLPStudentParts.FlowDirection = FlowDirection.LeftToRight;
            fLPStudentParts.WrapContents = true;

            this.WindowState = FormWindowState.Maximized;
        }
        //______________________________________________
        private void frmStudentParts_Resize(object sender, EventArgs e)
        {
            lblTitle.Left = (this.ClientSize.Width - lblTitle.Width) / 2;
            lblTitle.Top = 50;
            lblTitle.Text = _title;
            lblTitle.TabStop = false;
            _setupLayout();
        }
        // ___________________________________________________________________________________
        // ___________________________________________________________ Student Parts _________
        
        private void _loadStudentsInPart(int partID)
        {
            

            fLPStudentParts.Controls.Clear();



            DataTable dt = clsStudentParts.getStudentsByPartID(partID, _quranTrackID);

            clsParts _part = clsParts.findPart(partID);



            string studentName = "";

            foreach (DataRow row in dt.Rows)
            {

                ctrQuranPartPages _ctrQuranPartPages = new ctrQuranPartPages(_enQuranTrack);

                _ctrQuranPartPages.studentID = Convert.ToInt32(row["StudentID"]);

                _ctrQuranPartPages.studentPartID = Convert.ToInt32(row["StudentPartID"]);
                _ctrQuranPartPages.onSelected += Ctr_OnSelected;
                _ctrQuranPartPages.enPartMode = _enPartMode;

                studentName = row["Studentname"].ToString();
                _ctrQuranPartPages.loadPartPages(_part.startPage, _part.endPage, studentName);


                fLPStudentParts.Controls.Add(_ctrQuranPartPages);
            }

             
        }
        //______________________________________________
        private void _selectStudent(int studentID)
        {
            foreach (Control control in fLPStudentParts.Controls)
            {
                if (control is ctrQuranPartPages studentControl)
                {
                    bool isSelected = studentControl.studentID == studentID;

                    studentControl.setSelected(isSelected);

                    if (isSelected)
                    {
                        fLPStudentParts.ScrollControlIntoView(studentControl);
                        break;
                    }
                }
            }
        }
        //______________________________________________
        private void _refrishStudentsPart()
        {
            int partID;

            if (!int.TryParse(cbPartsName.SelectedValue.ToString(), out partID))
                return;

            _loadStudentsInPart(partID);

            if (_studentID > 0)
                _selectStudent(_studentID);
        }
        //______________________________________________
        private void cbPartsName_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cbPartsName.SelectedValue == null)
                return;

            _refrishStudentsPart();
        }

        // ___________________________________________________________________________________
        // ___________________________________________________________ Student Selection _____

        private void Ctr_OnSelected(ctrQuranPartPages ctr, bool canDelete)
        {
            _selectedControl = ctr;

            cmsDelete.Visible = canDelete;
        }

        // ___________________________________________________________________________________
        // ___________________________________________________________ Student Management ____

        private void btnAddStudentToPart_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddStudentPart((int)cbPartsName.SelectedValue, cbPartsName.Text, _quranTrackID);
            frm.ShowDialog();
            _refrishStudentsPart();
        }
        //______________________________________________
        private void cmsDelete_Click_1(object sender, EventArgs e)
        {
            

         enErrors result = clsStudentParts.deleteStudentPart(_selectedControl.studentPartID);

            switch (result)
            {
                case enErrors.Success:
                    fLPStudentParts.Controls.Remove(_selectedControl);
                    _selectedControl.Dispose();
                    _selectedControl = null;
                    break;

                case enErrors.ForeignKeyViolation:
                    MessageBox.Show(
                        "لا يمكن حذف هذا الجزء، لأنه يحتوي على بيانات تكرار للصفحات.",
                        "منع الحذف",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    break;

                default:
                    MessageBox.Show(
                        "حدث خطأ أثناء الحذف.",
                        "خطأ",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    break;
            }
        }
        //______________________________________________
        private void cmsArchive_Click(object sender, EventArgs e)
        {
           if (clsStudentParts.archiveStudentPart(_selectedControl.studentPartID))
            {
                if (clsStudentParts.archiveStudentPart(_selectedControl.studentPartID))
                {
                    _refrishStudentsPart();

                    _selectedControl = null;
                }
            }
        }

        private void btnArchive_Click(object sender, EventArgs e)
        {
            Form frm = new frmArchiveList(_quranTrackID);
            frm.ShowDialog();
            _refrishStudentsPart();
        }


    }
}
