using SchoolSystem.Business;
using SchoolSystem.Enums;
using SchoolSystem.Presentation.Students;
using SchoolSystem.Presentation.Subjects.StudentAssignments;
using SchoolSystem.Presentation.Subjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using static SchoolSystem.Business.Subjects.Trees.clsSubjectTreeNode;
using SchoolSystem.Business.Quran;
using SchoolSystem.Business.Quran.Pages;
using SchoolSystem.Business.Quran.Parts;
using SchoolSystem.Business.Subjects;
using SchoolSystem.Business.Subjects.StudentAssignments;
using SchoolSystem.Business.Subjects.Trees;
using SchoolSystem.Enums.AcademicStructure;
using SchoolSystem.Enums.Common;
using SchoolSystem.Enums.Quran;
using SchoolSystem.Presentation.AcademicStructure;
using SchoolSystem.Presentation.People;
using SchoolSystem.Presentation.Quran.Parts;
using SchoolSystem.Presentation.Quran.StudentProgress;

namespace SchoolSystem.Presentation.Subjects
{
    public partial class frmSubjectTree : Form
    {          // ================= Images =================
        Image imgPersonNormal;
        Image imgPersonHover;
        Image imgPersonDown;

        Image imgStudentNormal;
        Image imgStudentHover;
        Image imgStudentDown;

        Image imgStrNormal;
        Image imgStrHover;
        Image imgStrDown;

        Image imgTecNormal;
        Image imgTecHover;
        Image imgTecDown;
        //___________________________________________________________________________
        //___________________________________________________________________________
        private void _loadImages()
        {
            string imageFolder = Path.Combine(Application.StartupPath, "Image");

            // Person
            imgPersonNormal = Image.FromFile(Path.Combine(imageFolder, "P1.png"));
            imgPersonHover = Image.FromFile(Path.Combine(imageFolder, "P2.png"));
            imgPersonDown = Image.FromFile(Path.Combine(imageFolder, "P3.png"));

            // Student
            imgStudentNormal = Image.FromFile(Path.Combine(imageFolder, "1.png"));
            imgStudentHover = Image.FromFile(Path.Combine(imageFolder, "2.png"));
            imgStudentDown = Image.FromFile(Path.Combine(imageFolder, "3.png"));

            // Structure Education
            imgStrNormal = Image.FromFile(Path.Combine(imageFolder, "Structure Education 1.png"));
            imgStrHover = Image.FromFile(Path.Combine(imageFolder, "Structure Education 2.png"));
            imgStrDown = Image.FromFile(Path.Combine(imageFolder, "Structure Education 3.png"));

            imgTecNormal = Image.FromFile(Path.Combine(imageFolder, "Teachers 1.png"));
            imgTecHover = Image.FromFile(Path.Combine(imageFolder, "Teachers 2.png"));
            imgTecDown = Image.FromFile(Path.Combine(imageFolder, "Teachers 3.png"));

        }
        //___________________________________________________________________________
        private string _getQuranFolderPath()
        {
            string folderPath = Properties.Settings.Default.QuranFolderPath;

            if (Directory.Exists(folderPath))
                return folderPath;

            string deployedFolder = Path.Combine(Application.StartupPath, "Quran");
            string developmentFolder = Path.GetFullPath(
                Path.Combine(Application.StartupPath, @"..\..\..\Quran"));

            if (Directory.Exists(deployedFolder))
                folderPath = deployedFolder;
            else if (Directory.Exists(developmentFolder))
                folderPath = developmentFolder;
            else
            {
                using (FolderBrowserDialog dialog = new FolderBrowserDialog())
                {
                    dialog.Description = "اختر مجلد صور صفحات القرآن";
                    dialog.ShowNewFolderButton = false;

                    if (dialog.ShowDialog() != DialogResult.OK)
                        return null;

                    folderPath = dialog.SelectedPath;
                }
            }

            Properties.Settings.Default.QuranFolderPath = folderPath;
            Properties.Settings.Default.Save();

            return folderPath;
        }


        // ___________________________________________________________________________________
        // ___________________________________________________________ Enums _________________

        enNodeSubjects _enNodeSubjects;

        enPartMode _enPartMode;

        enInvocationContext _invocationContext;

        // ___________________________________________________________________________________
        // ___________________________________________________________ Fields ________________

        float scale = .7f;

        clsSubjectTree tree;

        int branchWidth = 180;
        int h_spase = 30;
        int verticalSpase = 100;

        clsSubjectTreeNode subjectNode;

        Font treeFont;
        Font rootFont;

        int _studentID = -1;
        //____________________
        Dictionary<int, int> studentSubjects = new Dictionary<int, int>();

        int _semesterID = 1;
        int _sectionID = 12;
        //___________________________________________________________________________________________________________
        public frmSubjectTree(int studentID , enInvocationContext invocationContext)
        {
            InitializeComponent();
            _loadImages();


            pnlTree.Paint += pnlTree_Paint;

            treeFont = new Font("Calibri", 20, FontStyle.Bold);
            rootFont = new Font("Calibri", 40, FontStyle.Bold);

            pnlTree.MouseDown += pnlTree_MouseDown;
            pnlTree.MouseDoubleClick += pnlTree_MouseDoubleClick;


            _studentID = studentID;

            _invocationContext = invocationContext;


        }
        //_________________________________________
        public frmSubjectTree()
        {
            InitializeComponent();
            _loadImages();


            pnlTree.Paint += pnlTree_Paint;

            treeFont = new Font("Calibri", 20, FontStyle.Bold);
            rootFont = new Font("Calibri", 40, FontStyle.Bold);

            pnlTree.MouseDown += pnlTree_MouseDown;
            pnlTree.MouseDoubleClick += pnlTree_MouseDoubleClick;

             _invocationContext = enInvocationContext.None;
        }
        //___________________________________________________________________________________
        int _getTreeDepth(clsSubjectTreeNode node)
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

            _calculateBranchWidth(tree.root);

            float treeWidth = tree.root.subTreeWidth;
            float treeHeight = (_getTreeDepth(tree.root) + 1) * verticalSpase;

            scale = Math.Min(
                (float)pnlTree.ClientSize.Width / (treeWidth + 80),
                (float)pnlTree.ClientSize.Height / (treeHeight + 80)
            );

            float offsetX = (pnlTree.ClientSize.Width / scale - treeWidth) / 2f;

            _setPositions(tree.root, (int)offsetX, 0);

            e.Graphics.ScaleTransform(scale, scale);
            _drawNode(e.Graphics, tree.root);
        }
        //___________________________________________________________________________________
        private int _calculateBranchWidth(clsSubjectTreeNode node)
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
        private void _setPositions(clsSubjectTreeNode node, int startX, int level)
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
        private void _fillDrawRect(Graphics g, Rectangle rect)
        {
        }
        //___________________________________________________________________________________
        //___________________________________
        void _drawNode(Graphics g, clsSubjectTreeNode node)
        {

            //studentSubjects = getStudentSubjectIDs();

            Rectangle rect;
            Font usedFont;
            Brush textBrush;

           

            var sfCenter = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            //_____________________________________________________
            if (node.nodeType == clsSubjectTreeNode.enNodeType.Root)
            {
                rect = new Rectangle(node.x - branchWidth - 5, node.y, 550, 60);
                usedFont = rootFont;
                textBrush = Brushes.White;
            }
            else
            {
                if (_invocationContext == enInvocationContext.StudentSubjects)
                {
                    if (!studentSubjects.ContainsKey(node.nodeID))
                    {
                        textBrush = new SolidBrush(Color.FromArgb(20, Color.Gray));
                    }
                    else
                    {
                        textBrush = new SolidBrush(Color.FromArgb(120, Color.White));
                    }
                }
                else
                {
                    textBrush = Brushes.White;
                }
                rect = new Rectangle(node.x, node.y, branchWidth, 60);
                usedFont = treeFont;
                
            }
            //_____________________________________________________
            _fillDrawRect(g, rect);
            g.DrawString(node.nameNode, usedFont, textBrush, rect, sfCenter);

            node.bounds = rect;

            foreach (var child in node.children)
            {

                

                Rectangle childRect = new Rectangle(child.x, child.y, branchWidth, 35);
                _drawLines(g, rect, childRect);

                
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
        private void frmSubjectTree_Load(object sender, EventArgs e)
        {

            _contextMenuBehavior();
            _setStudent(_studentID);

            tree = clsSubjectTree.loadSubjectTree();

            pnlTree.Invalidate();
        }
        //___________________________________________________________________________________________________________
        clsSubjectTreeNode _hitTestNodeRecursive(clsSubjectTreeNode node, Point p, int level, out int foundLevel)
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
        //___________________________________________________________________________________________________________
        private void pnlTree_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = new Point((int)(e.X / scale), (int)(e.Y / scale));

            int level;
            subjectNode = _hitTestNodeRecursive(tree.root, p, 0, out level);

            switch (level)
            {
                case 0:
                    _enNodeSubjects = enNodeSubjects.Root;
                    break;

                case 1:
                    _enNodeSubjects = enNodeSubjects.Subject;
                    break;

                default:
                    _enNodeSubjects = enNodeSubjects.non;
                    break;
            }
        }
        //___________________________________________________________________________________________________________
        //___________________________________________________________________________________________________________
        private Dictionary<int, int> getStudentSubjectIDs()
        {
            return clsStudentSubject.getStudentSubjectIDs(_studentID);
        }
        private void _setStudent(int studentID)
        {
            _studentID = studentID;

            // إعادة جلب مواد الطالب
            studentSubjects = getStudentSubjectIDs();

            // (اختياري) إعادة تحميل الشجرة إذا كانت تعتمد على الطالب
             tree = clsSubjectTree.loadSubjectTree();

            // إعادة الرسم
            pnlTree.Invalidate();
        }

        private void pnlTree_DoubleClick(object sender, EventArgs e)
        {
            if (subjectNode == null) return;

            if (subjectNode.nameNode  == "الحفظ")
            {
                clsQuranTracks _quranTracks = new clsQuranTracks();

                _quranTracks = clsQuranTracks.findQuranTrack(subjectNode.nameNode);

                _enPartMode = enPartMode.StudentPart;
                Form frm = new frmStudentParts(_enPartMode, _quranTracks.quranTrackID, enQuranTrackName.الحفظ);
                frm.ShowDialog();
            }

            if (subjectNode.nameNode == "التمكين")
            {
                clsQuranTracks _quranTracks = new clsQuranTracks();

                _quranTracks = clsQuranTracks.findQuranTrack(subjectNode.nameNode);

                _enPartMode = enPartMode.StudentPart;
                Form frm = new frmStudentParts(_enPartMode, _quranTracks.quranTrackID, enQuranTrackName.التمكين);
                frm.ShowDialog();
            }

            if (subjectNode.nameNode == "التدوير")
            {
                clsQuranTracks _quranTracks = new clsQuranTracks();

                _quranTracks = clsQuranTracks.findQuranTrack(subjectNode.nameNode);

                _enPartMode = enPartMode.StudentPart;
                Form frm = new frmStudentParts(_enPartMode, _quranTracks.quranTrackID, enQuranTrackName.التدوير);
                frm.ShowDialog();
            }
        }


        //___________________________________________________________________________________
        private void cmsAddSubject_Click(object sender, EventArgs e)
        {
            if (subjectNode == null) return;
            //_______________________

            int _parentSubjectID = -1;

            if (_invocationContext == enInvocationContext.StudentSubjects)
            {
                if (_enNodeSubjects == enNodeSubjects.Root) return;



                if (subjectNode.parentID.HasValue)
                    _parentSubjectID = subjectNode.parentID.Value;

                //  تحقق من شرط الأب
                if (_parentSubjectID != -1)
                {
                    bool hasParent = clsStudentSubject.isRegistered(_studentID, _parentSubjectID, _semesterID);

                    if (!hasParent)
                    {
                        MessageBox.Show("يجب تسجيل المادة الأساسية أولاً",
                            "رفض الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                clsStudentSubject studentSubject = new clsStudentSubject();

                studentSubject.studentID = _studentID;
                studentSubject.subjectID = subjectNode.nodeID;
                studentSubject.semesterID = _semesterID;
                studentSubject.sectionID = _sectionID;

                if (studentSubject.save())
                {
                    MessageBox.Show("تم الحفظ بنجاح",
                        "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);

                 
                }
                else
                {
                    MessageBox.Show("فشل الحفظ",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                _setStudent(_studentID);
                return;
            }
            //___________________________________________
            frmAddUpdateSubject frm = new frmAddUpdateSubject(subjectNode.nodeID, enMode.addNew);
            frm.ShowDialog();

            
            tree = clsSubjectTree.loadSubjectTree();
            pnlTree.Invalidate();
        }
        //___________________________________________________________________________________
        private void cmsUpdate_Click_1(object sender, EventArgs e)
        {
            if (subjectNode == null || _enNodeSubjects == enNodeSubjects.Root) return;

            frmAddUpdateSubject frm = new frmAddUpdateSubject(subjectNode.nodeID, enMode.update);
            frm.ShowDialog();

            tree = clsSubjectTree.loadSubjectTree();
            pnlTree.Invalidate();
        }
        //___________________________________________________________________________________
        private void cmsDelete_Click(object sender, EventArgs e)
        {

            if (subjectNode == null || _enNodeSubjects == enNodeSubjects.Root) return;

           

            if (_invocationContext == enInvocationContext.StudentSubjects)
            {
                int studentSubjectRecordID = studentSubjects[subjectNode.nodeID];

                if (MessageBox.Show("Delete [" + subjectNode.nodeID + "] ?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    clsStudentSubject.delete(studentSubjectRecordID);
                }

                _setStudent(_studentID);
                return;

            }
            //_____________________________________
            if (MessageBox.Show("Delete [" + subjectNode.nodeID + "] ?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                clsSubject.deleteSubjectID(subjectNode.nodeID);
            }

            tree = clsSubjectTree.loadSubjectTree();
            pnlTree.Invalidate();
        }
        //___________________________________________________________________________________
        private void pnlTree_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }


        // ========================= Invocation Context =========================
        private void _contextMenuBehavior()
        {
            switch (_invocationContext)
            {
                case enInvocationContext.SubjectManager:
                    {
                        studentSubjects.Clear();

                        cmsAddSubject.Visible = true;
                        cmsUpdate.Visible = true;
                        cmsDelete.Visible = true;

                        break;
                    }

                case enInvocationContext.StudentSubjects:
                    {
                        _setStudent(_studentID);

                        cmsAddSubject.Visible = true;
                        cmsUpdate.Visible = false;
                        cmsDelete.Visible = true;

                        break;
                    }

                default:
                    break;
            }
        }

        private void frmSubjectTree_Resize(object sender, EventArgs e)
        {
            button1.Visible = false;
            btnStudent.Visible = false;
            btnStructureEducation.Visible = false;
            btnPerson.Visible = false;
            btnQuranSync.Visible = false;

            //this.WindowState = FormWindowState.Maximized;
            //this.FormBorderStyle = FormBorderStyle.FixedSingle;
            //this.MaximizeBox = false;
            //this.MinimizeBox = false;

            this.WindowState = FormWindowState.Maximized;
            pnlTree.Dock = DockStyle.Bottom;

            pnlTree.Height = 700;
            pnlTree.Dock = DockStyle.Bottom;

        }


        //___________________________________________________________________________________
        //___________________________________________________________________________________
        private void btnStudent_Click(object sender, EventArgs e)
        {
            frmStudentsList fb = new frmStudentsList();
            fb.ShowDialog();
        }
        private void btnStudent_MouseEnter(object sender, EventArgs e)
        {
            btnStudent.BackgroundImage = imgStudentHover;
        }
        private void btnStudent_MouseLeave(object sender, EventArgs e)
        {
            btnStudent.BackgroundImage = imgStudentNormal;
            btnStudent.FlatAppearance.BorderColor = Color.FromArgb(1, 0, 0, 0);
        }
        private void btnStudent_MouseDown(object sender, MouseEventArgs e)
        {
            btnStudent.BackgroundImage = imgStudentDown;
        }

        

        private void btnStructureEducation_Click(object sender, EventArgs e)
        {
            frmEducationStructureTree frm = new frmEducationStructureTree();
            frm.ShowDialog();
        }
        private void btnStructureEducation_MouseEnter(object sender, EventArgs e)
        {
            btnStructureEducation.BackgroundImage = imgStrHover;

        }
        private void btnStructureEducation_MouseLeave(object sender, EventArgs e)
        {
            btnStructureEducation.BackgroundImage = imgStrNormal;
            btnStructureEducation.FlatAppearance.BorderColor = Color.FromArgb(1, 0, 0, 0);
        }
        private void btnStructureEducation_MouseDown(object sender, MouseEventArgs e)
        {
            btnStructureEducation.BackgroundImage = imgStrDown;
        }

        private void btnPerson_Click(object sender, EventArgs e)
        {
            frmPersonsList fb = new frmPersonsList();
            fb.ShowDialog();
        }
        private void btnPerson_MouseEnter(object sender, EventArgs e)
        {
            btnPerson.BackgroundImage = imgPersonHover;
        }
        private void btnPerson_MouseLeave(object sender, EventArgs e)
        {
            btnPerson.BackgroundImage = imgPersonNormal;
            btnPerson.FlatAppearance.BorderColor = Color.FromArgb(1, 0, 0, 0);
        }
        private void btnPerson_MouseDown(object sender, MouseEventArgs e)
        {
            btnPerson.BackgroundImage = imgPersonDown;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _enPartMode = enPartMode.QuranPart;
            Form frm = new frmQuranParts(_enPartMode);
            frm.ShowDialog();
        }

        private void btnQuranSync_Click(object sender, EventArgs e)
        {
            string folderPath = _getQuranFolderPath();

            if (string.IsNullOrWhiteSpace(folderPath))
                return;

            clsQuranPagesSync.SyncFromFolder(folderPath);

            clsQuranPartsSync.SyncParts(1035);
        }
    }
}
