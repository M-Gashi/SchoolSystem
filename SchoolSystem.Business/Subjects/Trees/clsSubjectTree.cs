using SchoolSystem.Enums;
using SchoolSystem.Data;
using System;
using System.Collections.Generic;
using System.Data;
using SchoolSystem.Business.Subjects.StudentAssignments;
using SchoolSystem.Data.Subjects;

namespace SchoolSystem.Business.Subjects.Trees
{
    public class clsSubjectTree
    {
        public clsSubjectTreeNode root { get; private set; }

        //__________________________________________________________________________________
        //__________________________________________________________________________________
        public clsSubjectTree(clsSubjectTreeNode root)
        {
            this.root = root;
        }
        //__________________________________________________________________________________
        public static clsSubjectTree loadSubjectTree()
        {
  
            DataTable dt = clsSubjectData.getAllSubjects();

            Dictionary<int, clsSubjectTreeNode> nodes = new Dictionary<int, clsSubjectTreeNode>();

            // 1) إنشاء كل العقد
            foreach (DataRow row in dt.Rows)
            {

                int id = (int)row["SubjectID"];
                string name = row["SubjectName"].ToString();
                int? parentId = row["ParentSubjectID"] == DBNull.Value
                                ? (int?)null
                                : (int)row["ParentSubjectID"];

                string subjectCode = row["SubjectCode"].ToString();
                string description = row["Description"].ToString();
                int credits = (int)row["Credits"];

                bool isActive = (bool)row["IsActive"];

                nodes[id] = new clsSubjectTreeNode(
                    id,
                    name,
                    parentId,
                    isActive,
                    "",
                    subjectCode,
                    description,
                    credits,
                    clsSubjectTreeNode.enNodeType.Subject
                );
            }

            // Root

            clsSubjectTreeNode root =
                new clsSubjectTreeNode(
                    0,
                    "القرآن",
                    null,
                    true,
                    "",
                    "",
                    "",
                    0,
                    clsSubjectTreeNode.enNodeType.Root
                );

            // 2) الربط
            foreach (var node in nodes.Values)
            {
                if (node.parentID == null)
                {
                    //root = node;
                    root.addChild(node);
                }
                else
                {
                    //nodes[node.parentID.Value].children.Add(node);
                    nodes[node.parentID.Value].addChild(node);
                }
            }

            // 3) إنشاء الشجرة
            return new clsSubjectTree(root);
        }
    }
}
