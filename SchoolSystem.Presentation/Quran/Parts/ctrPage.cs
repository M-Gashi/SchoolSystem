using SchoolSystem.Enums;
using SchoolSystem.Business;
using SchoolSystem.Presentation.Quran.Parts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SchoolSystem.Business.Quran;
using SchoolSystem.Enums.Quran;

namespace SchoolSystem.Presentation.Quran.Parts
{
    public partial class ctrPage : UserControl
    {
        // ___________________________________________________________________________________
        // ___________________________________________________________ Enums _________________
        public enum enMode { addNew, update }

        public enum enPageState { Saved = 0, Pending = 1 }
        enPageState _enPageState = enPageState.Pending;

        private enPartMode _enPartMode;

        private enQuranTrackName _enQuranTrack;

        

        // ___________________________________________________________________________________
        // ___________________________________________________________ Fields ________________

        clsPageProgress _pageProgress;

        int _rpeatCount = 0;

        private Timer _longPressTimer = new Timer();
        private bool _isLongPress = false;

        // ___________________________________________________________________________________
        // ___________________________________________________________ Delegates _____________
        // Mix
        private Action _loadTrackDataAction;
        private Action _pageDoubleClickAction;
        private Action _pageLongPressAction;

        // Hifz


        private Action _cancelEditAction;
        private Action<object> _repeatDoubleClickAction;
        private Action<KeyEventArgs> _repeatKeyDownAction;
        private Action _repeatLeaveAction;
        //________________________________________
        // Tudweer

        public event EventHandler pageDoubleClicked;
        // ___________________________________________________________________________________
        // ___________________________________________________________ Properties ____________

        public int pageID { get; set; }
        public int studentPartID { get; set; }

        public enPartMode enPartMode
        {
            get => _enPartMode;
            set
            {
                _enPartMode = value;

                bool isStudent = (_enPartMode == enPartMode.StudentPart);

                lblRepeat.Visible = isStudent;
                txtRepeat.Visible = false;

                pbPage.Selected = false;

                // إزالة الربط أولًا؛ تمنع التكرار.
                pbPage.MouseDown -= pbPage_MouseDown;
                pbPage.DoubleClick -= pbPage_DoubleClick;
                pbPage.MouseLeave -= pbPage_MouseLeave;

                // التفعيل فقط داخل أجزاء الطالب.
                if (isStudent)
                {
                    pbPage.MouseDown += pbPage_MouseDown;
                    pbPage.DoubleClick += pbPage_DoubleClick;
                    pbPage.MouseLeave += pbPage_MouseLeave;
                }
                else
                {
                    _longPressTimer.Stop();
                }

            }
        }

        public enQuranTrackName enQuranTrack
        {
            get => _enQuranTrack;
            set
            {
                _enQuranTrack = value;

                switch (_enQuranTrack)
                {
                    case enQuranTrackName.الحفظ:
                        _initializeHifz();
                        break;

                    case enQuranTrackName.التمكين:
                        _initializeTamkeen();
                        break;

                    case enQuranTrackName.التدوير:
                        _initializeTudweer();
                        break;
                }
            }

        }

        // ___________________________________________________________________________________
        // ___________________________________________________________ Constructor ___________

        public ctrPage()
        {
            InitializeComponent();

            lblRepeat.Tag = txtRepeat;
            txtRepeat.Tag = lblRepeat;
            //_____________
            _longPressTimer.Interval = 700; // 700ms

            _longPressTimer.Tick += LongPressTimer_Tick;

            lblRepeat.ForeColor = Color.FromArgb(222, 66, 222);
        }

        // ___________________________________________________________________________________
        //____________________________________________________________ Page Initialization ___

        public void setImage(string imagePath)
        {

            if (File.Exists(imagePath))
                pbPage.ImageLocation = imagePath;

            pbPage.SizeMode = PictureBoxSizeMode.Zoom;
            pbPage.Visible = true;
        }
        //________________________________________
        private void _resetData()
        {
            _pageProgress = new clsPageProgress();

            _pageProgress.studentPartID = studentPartID;
            _pageProgress.pageID = pageID;
            _pageProgress.status = 0;
            _pageProgress.repeatCount = 0;

            lblRepeat.Text = "";

            //______________________________________
            if (_enPageState == enPageState.Pending)
            {
                pbPage.Selected = true;
            }
            else
            {
                pbPage.Selected = false;
            }

      
        }
        //________________________________________
        private void _loadData()
        {
           
            _loadTrackDataAction?.Invoke();  
        }
        //________________________________________
        private void ctrPage_Load(object sender, EventArgs e)
        {
            if (_enPartMode == enPartMode.QuranPart)
                return;

            _loadData();

      
        }

        //________________________________________

        // ===================================================================================
        // Hifz
        // ===================================================================================
        //____________________________________________________________ Initialization ________

        private void _initializeHifz()
        {
            _loadTrackDataAction = _loadHifzData;

            _pageDoubleClickAction = _hifzDoubleClick;
            _pageLongPressAction = _hifzLongPress;

            _cancelEditAction = _hifzCancelEdit;
            _repeatDoubleClickAction = _hifzRepeatDoubleClick;
            _repeatLeaveAction = _hifzRepeatLeave;
            _repeatKeyDownAction = _hifzRepeatKeyDown;
        }

        // ___________________________________________________________________________________
        //____________________________________________________________ load Hifz _____________

        private void _loadHifzData()
        {
            _resetData();

            _pageProgress = clsPageProgress.findPageProgress(studentPartID, pageID);

            if (_pageProgress == null)
                return;


            if (_pageProgress == null || _pageProgress.repeatCount == 0)
            {
                lblRepeat.Text = " ";
               
            }
            else
                lblRepeat.ForeColor = Color.DeepSkyBlue;

            if (_pageProgress.repeatCount > 0)
            {
                lblRepeat.Text = _pageProgress.repeatCount.ToString();

            }

            
            _rpeatCount = _pageProgress.repeatCount;

            pbPage.Progress = _pageProgress.status;

            

            if (_pageProgress.status == 1 && enQuranTrack == enQuranTrackName.الحفظ)
            {
                _enPageState = enPageState.Saved;
                pbPage.Selected = false;


            }
            else
            {
                _enPageState = enPageState.Pending;
                

            }

            
        }

        // ___________________________________________________________________________________
        //____________________________________________________________ Repeat Count __________

        private void _hifzRepeatDoubleClick(object sender)
        {
            if (_enPageState == enPageState.Pending)
                return;

            Label lbl = (Label)sender;

            TextBox txt = (TextBox)lbl.Tag;

            txt.Text = lbl.Text;

            lbl.Visible = false;
            txt.Visible = true;

            

            txt.Focus();
            txt.SelectAll();
        }
        //________________________________________
        private void _hifzRepeatKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtRepeat_Leave(txtRepeat, EventArgs.Empty);

                if (_pageProgress == null || _pageProgress.pageProgressID == -1)
                {
                    _pageProgress = new clsPageProgress();

                    _pageProgress.studentPartID = studentPartID;
                    _pageProgress.pageID = pageID;
                    _pageProgress.repeatCount = _rpeatCount;
                    //___________________
                    _pageProgress.save();

                    _loadData();
                    return;
                }

                _pageProgress.repeatCount = _rpeatCount;
                //___________________
                _pageProgress.save();

                if (_rpeatCount == 0)
                {
                    clsPageProgress.deletePageProgress(_pageProgress.pageProgressID);
                    _enPageState = enPageState.Pending;
                }

                _loadData();




            }
            else if (e.KeyCode == Keys.Escape)
            {
                CancelEdit();
                e.SuppressKeyPress = true;
            }
        }
        //________________________________________
        private void _hifzRepeatLeave()
        {
            int.TryParse(txtRepeat.Text, out _rpeatCount);

            txtRepeat.Visible = false;
            lblRepeat.Visible = true;
        }
        //________________________________________
        private void _hifzCancelEdit()
        {
            txtRepeat.Text = lblRepeat.Text;

            txtRepeat.Visible = false;
            lblRepeat.Visible = true;
        }
        //________________________________________
        //________________________________________
        private void CancelEdit()
        {
            _cancelEditAction?.Invoke();
        }
        //________________________________________
        private void txtRepeat_Leave(object sender, EventArgs e)
        {
            _repeatLeaveAction?.Invoke();
        }
        //________________________________________
        private void lblRepeat_DoubleClick(object sender, EventArgs e)
        {
            _repeatDoubleClickAction?.Invoke(sender);
        }
        //________________________________________
        private void txtRepeat_KeyDown(object sender, KeyEventArgs e)
        {
            _repeatKeyDownAction?.Invoke(e);
        }

        // ___________________________________________________________________________________
        //____________________________________________________________ Layout  _______________

        private void ctrPage_Resize(object sender, EventArgs e)
        {
            lblRepeat.TextAlign = ContentAlignment.MiddleCenter;
            txtRepeat.TextAlign = HorizontalAlignment.Center;

            this.BackColor = Color.FromArgb(0, 0, 70, 60);
            
            lblRepeat.BackColor = Color.FromArgb(0, 0, 70, 60);

        }

        // ___________________________________________________________________________________
        // ____________________________________________________________ Page Click ___________

        private void _hifzDoubleClick()
        {
            _enPageState = enPageState.Saved;

            pbPage.Selected = false;

            if (_pageProgress == null || _pageProgress.pageProgressID == -1)
            {
                _pageProgress = new clsPageProgress();

                _pageProgress.studentPartID = studentPartID;
                _pageProgress.pageID = pageID;
                _pageProgress.status = 1;
                //___________________
                _pageProgress.save();
                _loadData();
                return;
            }

            _pageProgress.status = 1;
            //___________________
            _pageProgress.save();

            _loadData();

        }
        //___________________________________________
        //___________________________________________
        private void pbPage_DoubleClick(object sender, EventArgs e)
        {
            _longPressTimer.Stop();

            if (_isLongPress)
                return;

            

        

            _pageDoubleClickAction?.Invoke();

            pageDoubleClicked?.Invoke(this, EventArgs.Empty);
        }

        // ___________________________________________________________________________________
        // ____________________________________________________________ Page LongPress __________

        private void _hifzLongPress()
        {
            _enPageState = enPageState.Pending;

            if (_pageProgress == null || _pageProgress.pageProgressID == -1)
            {
                
                return;
            }

            _pageProgress.status = 0;
            pbPage.Selected = true;
            //___________________
            _pageProgress.save();



            if (_rpeatCount == 0)
            {
                clsPageProgress.deletePageProgress(_pageProgress.pageProgressID);
            }

            
                

            _loadData();
        }
        //___________________________________________
        //___________________________________________
        private void pbPage_MouseLeave(object sender, EventArgs e)
        {
            _longPressTimer.Stop();
        }
        //___________________________________________
        private void LongPressTimer_Tick(object sender, EventArgs e)
        {
            _longPressTimer.Stop();

            _isLongPress = true;

            _pageLongPressAction?.Invoke();

            pageDoubleClicked?.Invoke(this, EventArgs.Empty);

        }
        //___________________________________________
        private void pbPage_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            _isLongPress = false;
            _longPressTimer.Start();
        }

        // ======================================================================
        // Tamkeen
        // ======================================================================
        //____________________________________________________________ Initialization ________
        private void _initializeTamkeen()
        {
            pbPage.OverlayStyle = clsPictureBox.enOverlayStyle.FillLevel;

            _loadTrackDataAction = _loadTamkeenData;

            _pageDoubleClickAction = _tamkeenClick;
            _pageLongPressAction = _tamkeenLongPress;
        }
        // ___________________________________________________________________________________
        //____________________________________________________________ Tamkeen load  _________

        private void _loadTamkeenData()
        {
            _resetData();

            _pageProgress = clsPageProgress.findPageProgress(studentPartID, pageID);

            if (_pageProgress == null)
                return;


            if (_pageProgress.status == 0)
                lblRepeat.ForeColor = this.BackColor;
            else
                lblRepeat.ForeColor = Color.Yellow;

            pbPage.Progress = _pageProgress.status;

            lblRepeat.Text = _pageProgress.status.ToString();
  
        }
        // ___________________________________________________________________________________
        // ____________________________________________________________ Page Click ___________

        private void _tamkeenClick()
        {

            if (pbPage.Progress >= 5)
                return;

            pbPage.Progress++;

            if (_pageProgress == null || _pageProgress.pageProgressID == -1)
            {
                _pageProgress = new clsPageProgress();

                _pageProgress.studentPartID = studentPartID;
                _pageProgress.pageID = pageID;
                _pageProgress.status = pbPage.Progress;
                //___________________
                _pageProgress.save();
                _loadData();
                return;
            }

            _pageProgress.status = pbPage.Progress;
            //___________________
            _pageProgress.save();

            _loadData();




        }

        private void _tamkeenLongPress()
        {
            if (_pageProgress == null || _pageProgress.pageProgressID == -1)
            {
          
                return; }

            pbPage.Progress--;

            _pageProgress.status = pbPage.Progress;
            _pageProgress.save();
            _loadData();

            if (pbPage.Progress == 0)
            {
                clsPageProgress.deletePageProgress(_pageProgress.pageProgressID);

                _loadData();
                return;
            }
        }

        // ======================================================================
        // Tudweer
        // ======================================================================
        // ======================================================================
        //____________________________________________________________ Initialization ________

        private void _initializeTudweer()
        {
            _loadTrackDataAction = _loadTudweerData;

            _pageDoubleClickAction = _TudweerDoubleClick;
            _pageLongPressAction = _TudweerLongPress;

            
        }
        // ___________________________________________________________________________________
        //____________________________________________________________ Tudweer load  _________

        private void _loadTudweerData()
        {
            _resetData();

            lblRepeat.Visible = false;
            
            lblRepeat.Visible = false;

            _pageProgress = clsPageProgress.findPageProgress(studentPartID, pageID);

            if (_pageProgress == null)
                return;




            if (_pageProgress.status == 1 && enQuranTrack == enQuranTrackName.التدوير)
            {
                _enPageState = enPageState.Saved;
                pbPage.Selected = false;

            }
            else
            {
                _enPageState = enPageState.Pending;

            }

        }

        // ___________________________________________________________________________________
        // ____________________________________________________________ Page Click ___________

        private void _TudweerDoubleClick()
        {

            _enPageState = enPageState.Saved;

            pbPage.Selected = false;

            if (_pageProgress == null || _pageProgress.pageProgressID == -1)
            {
                _pageProgress = new clsPageProgress();

                _pageProgress.studentPartID = studentPartID;
                _pageProgress.pageID = pageID;
                _pageProgress.status = 1;
                //___________________
                _pageProgress.save();
                _loadData();
                return;
            }

            _pageProgress.status = 1;
            //___________________
            _pageProgress.save();

            _loadData();




        }

        private void _TudweerLongPress()
        {
            _hifzLongPress();
        }


    }

}
