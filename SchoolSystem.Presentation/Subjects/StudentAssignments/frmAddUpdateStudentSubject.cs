using SchoolSystem.Enums;
using System;
using System.Windows.Forms;
using SchoolSystem.Business;
using SchoolSystem.Business.Subjects.StudentAssignments;
using SchoolSystem.Business.Subjects.Trees;

namespace SchoolSystem.Presentation.Subjects.StudentAssignments
{
    public partial class frmAddUpdateStudentSubject : Form
    {
        int _studentID = -1;
        int _subjectID = -1;
        int _semesterID = -1;
        int _sectionID = -1;

        int _parentSubjectID = -1;

        public frmAddUpdateStudentSubject(int studentID, int subjectID, int semesterID, int sectionID)
        {
            InitializeComponent();

            _studentID = studentID;
            _subjectID = subjectID;
            _semesterID = semesterID;
            _sectionID = sectionID;
        }

        private void frmAddUpdateStudentSubject_Load(object sender, EventArgs e)
        {
            var node = clsSubjectTreeNode.findTree(_subjectID);

            if (node.parentID.HasValue)
            {
                _parentSubjectID = node.parentID.Value;

                var parent = clsSubjectTreeNode.findTree(_parentSubjectID);

                DialogResult dr = MessageBox.Show(
                    $"هذه المادة لها متطلب سابق:\n{parent.nameNode}\n\nهل تريد المتابعة؟",
                    "تحقق المتطلبات",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning
                );

                if (dr == DialogResult.Cancel)
                    this.Close();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // تحقق من شرط الأب
            if (_parentSubjectID != -1)
            {
                bool hasParent = clsStudentSubject.isRegistered(
                    _studentID,
                    _parentSubjectID,
                    _semesterID
                );

                if (!hasParent)
                {
                    MessageBox.Show("يجب تسجيل المادة الأساسية أولاً",
                        "رفض الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            clsStudentSubject ss = new clsStudentSubject();
            ss.studentID = _studentID;
            ss.subjectID = _subjectID;
            ss.semesterID = _semesterID;
            ss.sectionID = _sectionID;

            if (ss.save())
            {
                MessageBox.Show("تم الحفظ بنجاح",
                    "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            else
            {
                MessageBox.Show("فشل الحفظ",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
