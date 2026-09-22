using SchoolSystem.Business;
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
using SchoolSystem.Business.Common.Exceptions;
using SchoolSystem.Business.Quran.StudentProgress;
using SchoolSystem.Enums.Common;

namespace SchoolSystem.Presentation.Quran.StudentProgress
{
    public partial class frmAddStudentPart : Form
    {
        
        int _studentNameID = -1;

        clsStudentParts _studentParts;

        private int _partID;
        private string _partName;
        private int _quranTrackID;

        public frmAddStudentPart(int partID, string partName, int quranTrackID)
        {
            InitializeComponent();

            ctrStudentList1.loadData();
            ctrStudentList1.DataBack += _dataBackStudentName;

            _partID = partID;
            _partName = partName;
            _quranTrackID = quranTrackID;
        }
        //__________________________________________________________________________________
        private void frmAddStudentPart_Load(object sender, EventArgs e)
        {
            lblPartName.TextAlign = ContentAlignment.MiddleCenter;
            lblPartName.Text = _partName;
        }
        //__________________________________________________________________________________
        private void _dataBackStudentName(int studentNameID)
        {
            _studentNameID = studentNameID;

            _studentParts = new clsStudentParts();

            _studentParts.partsID = _partID;
            _studentParts.studentID = _studentNameID;
            _studentParts.quranTrackID = _quranTrackID;

            bool isArchived = false;

            if (clsStudentParts.isStudentPartExistsIsArchived(
                _quranTrackID,
                _studentNameID,
                _partID,
                ref isArchived))
            {
                if (isArchived)
                {
                    MessageBox.Show(
                        "الطالب موجود في هذا الجزء، ولكن بياناته مؤرشفة.",
                        "بيانات مؤرشفة",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        "الطالب موجود بالفعل في هذا الجزء.",
                        "تكرار",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }

                return;
            }

            try
            {
                if (_studentParts.save())
                {
                    MessageBox.Show(
                        "تم حفظ الطالب في الجزء بنجاح",
                        "نجاح",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (clsCannotAddException ex)
            {
                switch (ex.ErrorType)
                {
                    case enErrors.ForeignKeyViolation:

                        MessageBox.Show(
                            "لا يمكن إضافة الطالب بسبب وجود مشكلة في البيانات المرتبطة.",
                            "خطأ",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        break;

                    case enErrors.ValidationError:

                        MessageBox.Show(
                            "بيانات الطالب غير صحيحة.",
                            "خطأ في البيانات",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        break;

                    default:

                        MessageBox.Show(
                            "حدث خطأ أثناء إضافة الطالب.",
                            "خطأ",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        break;
                }
            }
        }
        //__________________________________________________________________________________
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        //__________________________________________________________________________________

        //__________________________________________________________________________________


    }
}
