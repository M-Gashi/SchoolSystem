using SchoolSystem.Enums.AcademicStructure;
using SchoolSystem.Business;
using SchoolSystem.Enums;
using SchoolSystem.Presentation;
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
using SchoolSystem.Business.AcademicStructure.Trees;
using SchoolSystem.Business.Common.Exceptions;
using SchoolSystem.Enums.Common;

namespace SchoolSystem.Presentation.AcademicStructure
{
    public partial class frmEducationStructureTree : Form
    {
        public delegate void sectionIDDataBack(int sectionID);

        // Declare an event using the delegate
        public event sectionIDDataBack sectionBack;
        //______________________
        enNodeType nodeType;

        float scale = .7f;
        
        clsStructureTree tree;

        int branchWidth = 180;
        int h_spase = 30;
        int verticalSpase = 100;

        clsStructureTreeNode selectedNode;
        
        Font treeFont;
        Font rootFont;
        Font countSection;
        //______________________
        

        //___________________________________________________________________________________________________________
        //___________________________________________________________________________________________________________
        public frmEducationStructureTree()
        {
            InitializeComponent();

            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;


            pnlTree.Paint += pnlTree_Paint;

            treeFont = new Font("Calibri", 20, FontStyle.Bold);
            rootFont = new Font("Calibri", 40, FontStyle.Bold);
            countSection = new Font("Calibri", 16, FontStyle.Bold);

            pnlTree.MouseDown += pnlTree_MouseDown_1;
        }
        //____________________________
        //______________
        //_______
        //___________________________________________________________________________________
        int _getTreeDepth(clsStructureTreeNode node)
        {
            if (node.children.Count == 0) return 0;

            int max = 0;
            foreach (var c in node.children)
                max = Math.Max(max, _getTreeDepth(c));

            return max + 1;
        }
        //___________________________________________________________________________________
        private void pnlTree_Paint(object sender, PaintEventArgs e)
        {
            

            if (tree == null || tree.root == null) return;

            e.Graphics.Clear(Color.FromArgb(0, 70, 60));

            


            _calculateBranchWidth(tree.root);
            _calculateBranchWidth(tree.root);

            // حساب أبعاد الشجرة
            float treeWidth = tree.root.subTreeWidth;
            float treeHeight = (_getTreeDepth(tree.root) + 1) * verticalSpase;

            // حساب الـ scale (كما عندك)
            scale = Math.Min(
                (float)pnlTree.ClientSize.Width / (treeWidth + 80),
                (float)pnlTree.ClientSize.Height / (treeHeight + 80)
            );

            // حساب إزاحة التوسيط
            float offsetX = (pnlTree.ClientSize.Width / scale - treeWidth) / 2f;

            // تعيين المواقع بعد معرفة الإزاحة
            _setPositions(tree.root, (int)offsetX, 0);

            // تطبيق التحجيم ثم الرسم
            e.Graphics.ScaleTransform(scale, scale);
            _drawNode(e.Graphics, tree.root);





        }
        //___________________________________________________________________________________
        private int _calculateBranchWidth(clsStructureTreeNode node)
        {
            if (node.children.Count == 0)
            {
                node.subTreeWidth = branchWidth;
                return branchWidth;
            }

            int sum = 0;
            foreach (var child in node.children)
                sum += _calculateBranchWidth(child) + h_spase;

            sum -= h_spase;
            node.subTreeWidth = Math.Max(sum, branchWidth);
            return node.subTreeWidth;
        }
        //___________________________________________________________________________________
        private void _setPositions(clsStructureTreeNode node, int startX, int level)
        {

            node.y = level * verticalSpase + 40;

            if (node.children.Count == 0)
            {
                node.x = startX;
                return;
            }



            int currentX = startX;
            foreach (var child in node.children)
            {
                _setPositions(child, currentX, level + 1);
                currentX += child.subTreeWidth + h_spase;
            }

            var first = node.children[0];
            var last = node.children[node.children.Count - 1];
            node.x = (first.x + last.x) / 2;
        }
        //___________________________________________________________________________________
        private void _fillDrwaRect(Graphics g,Rectangle parentRect)
        {
            g.FillRectangle(new SolidBrush(Color.FromArgb(0, 70, 60)), parentRect); 
            g.DrawRectangle(new Pen(Color.FromArgb(0, 70, 60)), parentRect);
        }
        //________________________________________
        void _drawNode(Graphics g, clsStructureTreeNode node)
        {
            Rectangle parentRect;
            Font usedFont;
            Brush textBrush;

            var sfCenter = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            // ================= ROOT =================
            if (node.nodeType == enNodeType.Root)
            {
                parentRect = new Rectangle(node.x - branchWidth - 5, node.y, 550, 60);
                usedFont = rootFont;
                textBrush = Brushes.Yellow;
                                  
                _fillDrwaRect(g, parentRect);
                g.DrawString(node.nameNode, usedFont, textBrush, parentRect, sfCenter);
            }
            // ================= SECTION =================
            else if (node.nodeType == enNodeType.Section)
            {
                parentRect = new Rectangle(node.x, node.y, branchWidth, 90);
                usedFont = treeFont;
                textBrush = Brushes.White;

                Rectangle nameRect = new Rectangle(parentRect.X, parentRect.Y, parentRect.Width, 50);
                Rectangle countRect = new Rectangle(parentRect.X, parentRect.Y + 50, parentRect.Width, 40);

                _fillDrwaRect(g, parentRect);
                g.DrawString(node.nameNode, usedFont, textBrush, nameRect, sfCenter);

                if (node.studentsCount > 0)
                {
                    g.DrawString( node.studentsCount.ToString(),countSection,Brushes.BlueViolet,countRect,sfCenter);
                }
            }
            // ================= STAGE / GRADE =================
            else
            {
                parentRect = new Rectangle(node.x, node.y, branchWidth, 60);
                usedFont = treeFont;
                textBrush = Brushes.White;

                _fillDrwaRect(g, parentRect);
                g.DrawString(node.nameNode, usedFont, textBrush, parentRect, sfCenter);
            }

            node.bounds = parentRect;

            // رسم الأبناء
            foreach (var child in node.children)
            {
                Rectangle childRect = new Rectangle(child.x, child.y, branchWidth, 35);
                _drawLines(g, parentRect, childRect);
                _drawNode(g, child);
            }
        }
        //___________________________________________________________________________________
        void _drawLines(Graphics g, Rectangle parent, Rectangle child)
        {
            float parentX = parent.X + parent.Width / 2;
            float parentY = parent.Bottom;

            float childX = child.X + child.Width / 2;
            float childY = child.Y + 15;
            float midY = ((parentY + childY) / 2f) - 15;

            g.DrawLine(Pens.White, parentX, parentY, parentX, midY);
            g.DrawLine(Pens.White, parentX, midY, childX, midY);
            g.DrawLine(Pens.White, childX, midY, childX, childY);
        }
        //___________________________________________________________________________________________________________
        //___________________________________________________________________________________________________________


        private void frmEducationStructureTree_Load(object sender, EventArgs e)
        {
            tree = clsStructureTree.loadFamilyTree();
            
            pnlTree.Invalidate();
        }
        //____________________________
        //______________
        //_______
    
        //___________________________________________________________________________________________________________
        //___________________________________________________________________________________________________________

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            typeof(Panel).InvokeMember(
                "DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null,
                pnlTree,
                new object[] { true }
            );
        }
        //___________________________________________________________________________________________________________
        private void pnlTree_MouseWheel(object sender, MouseEventArgs e)
        {
            scale += e.Delta > 0 ? 0.01f : -0.01f;
            if (scale < 0.2f) scale = 0.2f;
            if (scale > 2.0f) scale = 2.0f;

            pnlTree.Invalidate();
        }
        //___________________________________________________________________________________________________________
        clsStructureTreeNode _hitTestNodeRecursive(clsStructureTreeNode node, Point p, int level, out int foundLevel)
        {
            if (node == null)
            {
                foundLevel = -1;
                return null;
            }

            if (node.bounds.Contains(p))
            {
                foundLevel = level;
                return node;
            }

            foreach (var child in node.children)
            {
                var hit = _hitTestNodeRecursive(child, p, level + 1, out foundLevel);
                if (hit != null)
                    return hit;
            }

            foundLevel = -1;
            return null;
        }
        //....................................
        clsStructureTreeNode _hitTestNode(Point p, out int level)
        {
            return _hitTestNodeRecursive(tree.root, p, 0, out level);
        }
        //....................................
        private void pnlTree_MouseDown_1(object sender, MouseEventArgs e)
        {
            Point p = new Point((int)(e.X / scale), (int)(e.Y / scale));

            int level;
            selectedNode = _hitTestNode(p, out level);

            switch (level)
            {
                case 0:
                    nodeType = enNodeType.Root;
                    break;

                case 1:
                    nodeType = enNodeType.Stage;
                    break;

                case 2:
                    nodeType = enNodeType.Grade;
                    break;

                case 3:
                    nodeType = enNodeType.Section;
                    break;

                default:
                    nodeType = enNodeType.non;
                    break;
            }
        }
        //___________________________________________________________________________________
        private void addChildrenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedNode == null || nodeType == enNodeType.Section) return;

            frmAddChildren frm = new frmAddChildren(nodeType, selectedNode.nodeID, enMode.addNew);
            frm.ShowDialog();

            tree = clsStructureTree.loadFamilyTree();
            pnlTree.Invalidate();
        }
        //___________________________________________________________________________________
        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            if (selectedNode == null || nodeType == enNodeType.Root) return;

            frmAddChildren frm = new frmAddChildren(nodeType, selectedNode.nodeID, enMode.update);
            frm.ShowDialog();

            tree = clsStructureTree.loadFamilyTree();
            pnlTree.Invalidate();
        }
        //___________________________________________________________________________________
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedNode == null || nodeType == enNodeType.Root) return;

            if (selectedNode.children.Count > 0 )
            {
                MessageBox.Show("We Cant Delete This Level", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            

            if (MessageBox.Show("Are you sure you want to delete it [" + selectedNode.nodeID + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    clsStructureTreeNode.deleteNodeID(nodeType, selectedNode.nodeID);
                    MessageBox.Show("Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


                catch (clsCannotDeleteException ex)
                {
                    switch (ex.ErrorType)
                    {
                        case enErrors.ForeignKeyViolation:
                            MessageBox.Show("We Cant Delete This Level", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case enErrors.DuplicateEntry:
                            MessageBox.Show("القيمة مكررة");
                            break;
                    }
                }

            }

            tree = clsStructureTree.loadFamilyTree();
            pnlTree.Invalidate();
        }
        //___________________________________________________________________________________
        public void disableCmsAddChildren2()
        {
            cmsAddChildren2.Enabled = false;
        }
        //__________________________________________
        private void pnlTree_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (selectedNode == null || nodeType == enNodeType.Root || nodeType == enNodeType.Stage || nodeType == enNodeType.Grade) return;


            if (MessageBox.Show("Are you sure you want to Assignment hear? [" + selectedNode.nodeID + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sectionBack?.Invoke(selectedNode.nodeID);
                Close();

            }


        }


    }
}
