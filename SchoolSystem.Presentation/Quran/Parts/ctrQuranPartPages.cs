using SchoolSystem.Enums;
using SchoolSystem.Business;
using SchoolSystem.Presentation.Quran.Parts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static SchoolSystem.Presentation.Quran.Parts.ctrPage;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using SchoolSystem.Business.Quran;
using SchoolSystem.Business.Quran.Pages;
using SchoolSystem.Business.Quran.Parts;
using SchoolSystem.Enums.Quran;

namespace SchoolSystem.Presentation.Quran.Parts
{
    public partial class ctrQuranPartPages : UserControl
    {
        // ==================================================================== Enums ========
        // ====================================================================       ========



        // ==================================================================== Fields ========
        // ====================================================================        ========

        //enPartMode _enPartMode;

        enQuranTrackName _enQuranTrack;
       
        clsParts _part;
       
        public event Action<ctrQuranPartPages, bool> onSelected;
     
        private List<clsPages> _pages;

        // ==================================================================== Properties ====
        // ====================================================================            ====

        public int studentPartID { get; set; }

        public enPartMode enPartMode { get; set; }

        public int studentID { get; set; }

        // ==================================================================== Constructors ==
        // ====================================================================              ==

        public ctrQuranPartPages()
        {
            InitializeComponent();

            _attachMouse(this);
        }

        public ctrQuranPartPages(enQuranTrackName enQuranTrack)
        {
            InitializeComponent();

            _attachMouse(this);

            _enQuranTrack = enQuranTrack;
        }

        // =========================================================== Page Initialization ====
        // ===========================================================                     ====

        private void resetPages()
        {
            ctrPage[] pictureCtrPage =
            {
                ctrPage1, ctrPage2, ctrPage3, ctrPage4,ctrPage5, ctrPage6, ctrPage7,
                ctrPage8, ctrPage9, ctrPage10, ctrPage11, ctrPage12, ctrPage13, ctrPage14,
                ctrPage15, ctrPage16, ctrPage17, ctrPage18, ctrPage19, ctrPage20
            };

            foreach (ctrPage page in pictureCtrPage)
            {
                page.setImage(null); // أو ""
            }


        }
       
        private void _loadPagesImages()
        {


            ctrPage[] pictureCtrPage =
                                   {
                                       ctrPage1, ctrPage2, ctrPage3, ctrPage4,
                                       ctrPage5, ctrPage6, ctrPage7, ctrPage8,
                                       ctrPage9, ctrPage10, ctrPage11, ctrPage12,
                                       ctrPage13, ctrPage14, ctrPage15, ctrPage16,
                                       ctrPage17, ctrPage18, ctrPage19, ctrPage20
                                   };

            for (int i = 0; i < _pages.Count && i < pictureCtrPage.Length; i++)
            {
                ctrPage pageControl = pictureCtrPage[i];



                pageControl.studentPartID = this.studentPartID;
                pageControl.pageID = _pages[i].pageID;

                pageControl.enPartMode = this.enPartMode;
                pageControl.enQuranTrack = this._enQuranTrack;

                pageControl.setImage(_pages[i].pagePath);

                pageControl.pageDoubleClicked -= _pageDoubleClicked;
                pageControl.pageDoubleClicked += _pageDoubleClicked;

            }

        }

        private void _loadDate()
        {
            DateTime? oldestDate = clsPageProgress.getOldestCreatedAtByStudentPartID(studentPartID);

            if (oldestDate == null)
            {
                lblDate.Text = "";
                lblProgressDays.Text = "";
                return;
            }

            lblDate.Text = oldestDate.Value.ToString("yyyy/MM/dd");

            int progressDays = (DateTime.Today - oldestDate.Value.Date).Days + 1;

            lblProgressDays.Text = progressDays.ToString();

        
        }
       
        public void loadPartPages(int startPage, int endPage, int partID)
        {
            _pages = clsPages.getPagesBetween(startPage, endPage);

            _part = clsParts.findPart(partID);
            lblName.Text = _part.PartName;

            if (_pages == null || _pages.Count == 0)
            {
                resetPages();
                return;
            }

            _loadDate();
            _loadPagesImages();

        }

        public void loadPartPages(int startPage, int endPage, string studentName)
        {
            _pages = clsPages.getPagesBetween(startPage, endPage);

            //_part = clsParts.findPart(partID);
            lblName.Text = studentName;
            lblName.Font = new Font(lblName.Font.FontFamily, 12);

            if (_pages == null || _pages.Count == 0)
            {
                resetPages();
                return;
            }

            _loadDate();
            _loadPagesImages();

        }
      
        private void _pageDoubleClicked(object sender, EventArgs e)
        {
            _loadDate();

        }


        // =========================================================== Selection ==============
        // ===========================================================           ==============

        public void setSelected(bool selected)
        {
            if (selected)
            {

                this.BackColor = Color.FromArgb(0, 90, 80);

            }
            else
            {
                lblName.BackColor = Color.Transparent;
                lblName.ForeColor = Color.White;

            }
        }


        // =========================================================== Mouse Handling =========
        // ===========================================================                =========

        private void _attachMouse(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                c.MouseDown += ctrQuranPartPages_MouseDown;

                if (c.HasChildren)
                    _attachMouse(c);
            }
        }
        private void ctrQuranPartPages_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            int statusCount = clsPageProgress.getStatusCount(studentPartID);

            bool canDelete = statusCount == 0;

            onSelected?.Invoke(this, canDelete);
        }
        


        

        //_______________________________________________________
        
    }
}
