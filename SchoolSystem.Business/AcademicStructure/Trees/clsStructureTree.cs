using SchoolSystem.Enums.AcademicStructure;
using SchoolSystem.Enums;
using SchoolSystem.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolSystem.Data.AcademicStructure;

namespace SchoolSystem.Business.AcademicStructure.Trees
{
    public class clsStructureTree
    {
        public clsStructureTreeNode root { get; private set; }

        public clsStructureTree(clsStructureTreeNode root)
        {
            this.root = root;
        }
        //_____________________________________________________________________________________
        public static clsStructureTree loadFamilyTree()
        {
            DataTable dtStages   = clsStageData.getAllStage();

            DataTable dtGrades   = clsGradeData.getAllGrade();

            DataTable dtSections = clsSectionData.getAllSection();

            Dictionary<int, clsStructureTreeNode> stageDict =
                new Dictionary<int, clsStructureTreeNode>();

            Dictionary<int, clsStructureTreeNode> gradeDict =
                new Dictionary<int, clsStructureTreeNode>();


            clsStructureTreeNode root = new clsStructureTreeNode(enNodeType.Root, 0, "Structure Education", null);


            // Stages
            foreach (DataRow r in dtStages.Rows)
            {
                int id = (int)r["StageID"];
                var stageNode = new clsStructureTreeNode(enNodeType.Stage, id, r["StageName"].ToString(), root.nodeID);
                root.addChild(stageNode);
                stageDict[(int)r["StageID"]] = stageNode;
            }

            // Grades
            foreach (DataRow r in dtGrades.Rows)
            {
                int id = (int)r["GradeID"];
                var gradeNode = new clsStructureTreeNode(enNodeType.Grade, id, r["GradeName"].ToString(),
                    stageDict[(int)r["StageID"]].nodeID);

                stageDict[(int)r["StageID"]].addChild(gradeNode);
                gradeDict[(int)r["GradeID"]] = gradeNode;
            }

            // Sections
            foreach (DataRow r in dtSections.Rows)
            {
                int id = (int)r["SectionID"];
                var sectionNode = new clsStructureTreeNode(enNodeType.Section, id, r["SectionName"].ToString(),
                    gradeDict[(int)r["GradeID"]].nodeID);

                sectionNode.studentsCount = (int)r["StudentsCount"];

                gradeDict[(int)r["GradeID"]].addChild(sectionNode);



            }

            return new clsStructureTree(root);  
        }


    }
}
