
using SchoolSystem.Enums;
using SchoolSystem.Business;
using System.Runtime.ConstrainedExecution;
using System.Windows.Forms;
using SchoolSystem.Business.Quran.Parts;
using SchoolSystem.Enums.Quran;

namespace SchoolSystem.Presentation.Quran.Parts
{
    public partial class frmQuranParts : Form
    {
        enPartMode _enPartMode;

        clsParts _part;

        ctrQuranPartPages _ctrQuranPartPages;

        //___________________________________________________________________________________
        public frmQuranParts(enPartMode partMode)
        {
            InitializeComponent();

            

            fLPPartsOfQuran.AutoScroll = true;
            fLPPartsOfQuran.WrapContents = true;
            fLPPartsOfQuran.Dock = DockStyle.Fill;

            this.WindowState = FormWindowState.Maximized;

            _enPartMode = partMode;
           
        }
        //___________________________________________________________________________________
        private void _loadParts()
        {
            fLPPartsOfQuran.Controls.Clear();

            

            for (int partNumber = 1; partNumber <= 30; partNumber++)
            {
                _ctrQuranPartPages = new ctrQuranPartPages();

                _ctrQuranPartPages.enPartMode = _enPartMode;

                _part = clsParts.findPartByNumber(partNumber);

                if (_part == null)
                    continue;

                _ctrQuranPartPages.loadPartPages(_part.startPage, _part.endPage, _part.partID);
               
                

                fLPPartsOfQuran.Controls.Add(_ctrQuranPartPages);
               
            }

            Panel spacer = new Panel();
            spacer.Width = fLPPartsOfQuran.ClientSize.Width;
            spacer.Height = 1;

            fLPPartsOfQuran.Controls.Add(spacer);

            _ctrQuranPartPages = new ctrQuranPartPages();

            _ctrQuranPartPages.loadPartPages(602, 604, 30);

            fLPPartsOfQuran.Controls.Add(_ctrQuranPartPages);


        }
        //___________________________________________________________________________________
        private void frmQuranParts_Load_1(object sender, System.EventArgs e)
        {
            _loadParts();
        }
        //___________________________________________________________________________________



    }
       
}

      
