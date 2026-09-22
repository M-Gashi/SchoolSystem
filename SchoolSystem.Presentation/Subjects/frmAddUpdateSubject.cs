using SchoolSystem.Enums.Common;
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
using static SchoolSystem.Business.Subjects.Trees.clsSubjectTreeNode;
using SchoolSystem.Business.Subjects.Trees;



namespace SchoolSystem.Presentation.Subjects
{
    public partial class frmAddUpdateSubject : Form
    {
        enMode _mode = enMode.addNew;


        public int _parentID = -1;
        public int _nodeID = -1;

        
        clsSubjectTreeNode _subjectNode;

        //__________________________________________
        public frmAddUpdateSubject()
        {
            InitializeComponent();
            _mode = enMode.addNew;

        }
        public frmAddUpdateSubject(int subjectID)
        {
            InitializeComponent();

            _nodeID = subjectID;
            _mode = enMode.addNew;

        }
        public frmAddUpdateSubject(int currentNode, enMode mode)
        {
            InitializeComponent();

            if (mode == enMode.addNew)
            {
                _parentID = currentNode;

                _mode = enMode.addNew;
            }
            else
            {
                _nodeID = currentNode;

                _mode = enMode.update;
            }
        }
        //____________________________________________________________________________________
        //____________________________________________________________________________________
        private void _resetDefaultValues()
        {
            if (_mode == enMode.addNew)
            {
                lblTitle.Text = "Add New Subject";
                this.Text = "Add New Subject";

                _subjectNode = new clsSubjectTreeNode();

                if (_parentID == 0)
                {
                    _subjectNode.parentID = null;

                    //lblPrerequisit.Enabled = false;
                    //lblPrerequisite.Text = "";

                    lblPrerequisit.Hide();
                    lblCuma.Hide();

                }
                else
                {
                    _subjectNode.parentID = _parentID;


                    if (_subjectNode.parentID.HasValue)
                    {
                        lblPrerequisite.Text = clsSubjectTreeNode.findTree(_subjectNode.parentID.Value).nameNode;
                    }
                    else
                    {
                        lblPrerequisite.Text = "القرآن";
                    }
                }
            }
            else
            {
                lblTitle.Text = "Update Subject";
                this.Text = "Update Subject";

                _subjectNode = clsSubjectTreeNode.findTree(_nodeID);

                if (_subjectNode.parentID.HasValue)
                {
                    lblPrerequisite.Text = clsSubjectTreeNode.findTree(_subjectNode.parentID.Value).nameNode;
                }
                else
                {
                    lblPrerequisite.Text = "القرآن";
                }
            }


            //lblPrerequisite.Text = _subjectNode.parentID.ToString();
            txtSubjectName.Text  = string.Empty;
            txtSubjectCode.Text  = string.Empty;
            txtDescription.Text  = string.Empty;
            txtCredits.Text      = string.Empty;
            rbYes.Checked        = true;


          

        }
        //_______________________________________
        private void _loadData()
        {
            

            if (_subjectNode == null)
            {
                MessageBox.Show("Subject Not Found", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            lblSubjectID.Text   = _subjectNode.nodeID.ToString();
            txtSubjectName.Text = _subjectNode.nameNode;
            txtSubjectCode.Text = _subjectNode.subjectCode;
            txtDescription.Text = _subjectNode.description;
            txtCredits.Text     = _subjectNode.credits.ToString();
            rbYes.Checked       = _subjectNode.isActive;
        
        }
        //____________________________________________________________________________________
        private void frmAddUpdateSubject_Load(object sender, EventArgs e)
        {
            _resetDefaultValues();
            if (_mode == enMode.update)
                _loadData();

        }
        //____________________________________________________________________________________
        private void btnSaveNewSubject_Click(object sender, EventArgs e)
        {

            _subjectNode.nameNode    = txtSubjectName.Text;
            _subjectNode.subjectCode = txtSubjectCode.Text;
            _subjectNode.description = txtDescription.Text;
            _subjectNode.credits     = int.Parse(txtCredits.Text);
            _subjectNode.isActive    = rbYes.Checked;

            if (_subjectNode.save())
            {
                lblSubjectID.Text = _subjectNode.nodeID.ToString();
                _mode = enMode.update;

                lblTitle.Text = "Update Subject";
                this.Text = "Update Subject";

                MessageBox.Show("Data Saved Successfully",
                    "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Save Failed",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
