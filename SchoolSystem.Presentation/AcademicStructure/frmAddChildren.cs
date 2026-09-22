using SchoolSystem.Enums.Common;
using SchoolSystem.Enums.AcademicStructure;
using SchoolSystem.Enums;
using SchoolSystem.Business;
using SchoolSystem.Presentation;
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
using System.Xml.Linq;
using SchoolSystem.Business.AcademicStructure;
using SchoolSystem.Business.AcademicStructure.Handlers;
using SchoolSystem.Business.AcademicStructure.Trees;


namespace SchoolSystem.Presentation.AcademicStructure
{
    public partial class frmAddChildren : Form
    {

        public delegate void rootDataBack(string root);

        // Declare an event using the delegate
        public event rootDataBack rootBack;
        //________________________________________

        enMode _mode = enMode.addNew;

        enNodeType _enNodeType;

        public int _parentID = -1;
        public int _nodeID = -1;

        clsStructureTreeNode _node;

        public frmAddChildren(enNodeType enNodeType, int currentNode, enMode mode)
        {
            InitializeComponent();

            if (mode == enMode.addNew)
            {
                _parentID = currentNode;

                _enNodeType = (enNodeType)((int)enNodeType + 1);

                _mode = enMode.addNew;
            }
            else
            {
                _nodeID = currentNode;

                _enNodeType = enNodeType;

                _mode = enMode.update;
            }


        }
        public frmAddChildren()
        {
            InitializeComponent();

            _mode = enMode.addNew;
            _node = new clsStructureTreeNode();
        }
        //___________________________________________________________________________________
        //___________________________________________________________________________________
        private void _resetDefultValues()
        {

            if (_mode == enMode.addNew)
            {
                lblAddUpdate.Text = "Children";
                this.Text = "Add Children";

                _node = new clsStructureTreeNode();

                _node.parentID = _parentID;
                _node.nodeType = _enNodeType;
            }
            else
            {
                lblAddUpdate.Text = "Node";
                this.Text = "Add Node";

            }

            txtChildren.Text = string.Empty;
        }
        //___________________________________________________________________________________
        private void _loadData()
        {
            _node = clsStructureTreeNode.findTree(_enNodeType, _nodeID);
            _node.nodeType = _enNodeType;
            //___________________________________________
            if (_node == null)
            {
                MessageBox.Show("No Node with ID = " + _node, "Node Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }
            //___________________________________________
            txtChildren.Text = _node.nameNode.ToString();

        }
        //___________________________________________________________________________________
        private void frmAddChildren_Load(object sender, EventArgs e)
        {
            _resetDefultValues();

            if (_mode == enMode.update)
                _loadData();


        }
        //___________________________________________________________________________________
        private void btnSave_Click_1(object sender, EventArgs e)
        {
            _node.nameNode = txtChildren.Text;

            string root = txtChildren.Text;

            // Trigger the event to send data back to Form1
            rootBack?.Invoke(root);

            if (isTextBoxEmpty(_node.nameNode))
            {
                if (_node.save())
                {
                    MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    lblAddUpdate.Text = "Node";
                    _mode = enMode.update;
                }
                else
                    MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                MessageBox.Show("Entrer Node Name.", "Node Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                frmAddChildren newForm = new frmAddChildren();
                newForm.rootBack += rootBack;
                newForm.ShowDialog();
                this.Close();

            }
            //_______________________________

            //_______________________________


            Close();

        }
        //___________________________________________________________________________________
        private bool isTextBoxEmpty(string text)
        {
            if (text == "")
                return false;
            else
                return true;
        }
    }
}
