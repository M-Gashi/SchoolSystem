using SchoolSystem.Enums;
using SchoolSystem.Business;
using SchoolSystem.Presentation.Students;
using SchoolSystem.Presentation.Subjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//___________________________________________________________________________
//___________________________________________________________________________
using SchoolSystem.Business.Quran.Pages;
using SchoolSystem.Business.Quran.Parts;
using SchoolSystem.Enums.Quran;
using SchoolSystem.Presentation.AcademicStructure;
using SchoolSystem.Presentation.People;
using SchoolSystem.Presentation.Quran.Parts;
using SchoolSystem.Presentation.Teachers;
namespace SchoolSystem.Presentation.Shell
{
    public partial class frmMain : Form
    {
        enPartMode _enPartMode;

        // ================= Images =================
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

        // ================= Constructor =================
        public frmMain()
        {
            InitializeComponent();

            _loadImages();
        }
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
        //___________________________________________________________________________
        //___________________________________________________________________________
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
        //___________________________________________________________________________
        private void btnStudent_Click(object sender, EventArgs e)
        {
            frmStudentsList fb = new frmStudentsList();
            fb.ShowDialog();
        }
        private void mouse_EnterStudent(object sender, EventArgs e)
        {
            btnStudent.BackgroundImage = imgStudentHover;
        }
        private void mouse_LeaveStudent(object sender, EventArgs e)
        {
            btnStudent.BackgroundImage = imgStudentNormal;
            btnStudent.FlatAppearance.BorderColor = Color.FromArgb(1, 0, 0, 0);
        }
        private void btnStudent_MouseDown(object sender, MouseEventArgs e)
        {
            btnStudent.BackgroundImage = imgStudentDown;
        }
        //___________________________________________________________________________
        private void btnStructureEducation_Click_1(object sender, EventArgs e)
        {


            frmEducationStructureTree frm = new frmEducationStructureTree();
            frm.ShowDialog();
        }
        private void btnStructureEducation_MouseEnter_1(object sender, EventArgs e)
        {
            btnStructureEducation.BackgroundImage = imgStrHover;
        }
        private void btnStructureEducation_MouseLeave_1(object sender, EventArgs e)
        {
            btnStructureEducation.BackgroundImage = imgStrNormal;
            btnStructureEducation.FlatAppearance.BorderColor = Color.FromArgb(1, 0, 0, 0);
        }
        private void btnStructureEducation_MouseDown_1(object sender, MouseEventArgs e)
        {
            btnStructureEducation.BackgroundImage = imgStrDown;
        }
        //___________________________________________________________________________
        private void btnTeachers_Click_1(object sender, EventArgs e)
        {
            frmTeachersList frm = new frmTeachersList();
            frm.ShowDialog();
        }
        private void btnTeachers_MouseEnter(object sender, EventArgs e)
        {
            btnTeachers.BackgroundImage = imgTecHover;
        }
        private void btnTeachers_MouseLeave(object sender, EventArgs e)
        {
            btnTeachers.BackgroundImage = imgTecNormal;
            btnTeachers.FlatAppearance.BorderColor = Color.FromArgb(1, 0, 0, 0);
        }
        private void btnTeachers_MouseDown(object sender, MouseEventArgs e)
        {
            btnTeachers.BackgroundImage = imgTecDown;
        }
        //___________________________________________________________________________
        


    

        private void frmMain_Load(object sender, EventArgs e)
        {
        }

        private void btnTrreSub_Click(object sender, EventArgs e)
        {
            Form form = new frmSubjectTree();
            form.ShowDialog();
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

        private void frmMain_Resize(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
