using SchoolSystem.Enums;
using SchoolSystem.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using SchoolSystem.Data.Subjects;
using SchoolSystem.Enums.Common;

namespace SchoolSystem.Business.Subjects.Trees
{
    public class clsSubjectTreeNode
    {
        public enMode mode = enMode.addNew;

        public enum enNodeType { Root, Subject }

        public int nodeID { get; set; }
        public string nameNode { get; set; }
        public int? parentID { get; set; }
        public bool isActive { get; set; }
        public string notse { get; set; }

        public string subjectCode { get; set; }
        public string description { get; set; }
        public int credits { get; set; }

        public int subTreeWidth { get; set; }
        public int treeDept { get; set; }
        public int x { get; set; }
        public int y { get; set; }

        public Rectangle bounds;

        public enNodeType nodeType { get; set; }

        public List<clsSubjectTreeNode> children { get; private set; }
        //_____________________
        public clsSubjectTreeNode()
        {
            this.nodeID = -1;
            this.nameNode = "";
            this.parentID = null;
            this.isActive = false;
            this.notse = "";

            this.subjectCode = "";
            this.description = "";
            this.credits = 0;

            this.subTreeWidth = 0;
            this.treeDept = 0;
            this.x = 100;
            this.y = 0;

            this.nodeType = enNodeType.Root;

            this.children = new List<clsSubjectTreeNode>();

            mode = enMode.addNew;
        }

        public clsSubjectTreeNode(int nodeId, string nameNode, int? parentID,
                                 bool isActive, string notse,
                                 string subjectCode, string description, int credits,
                                 enNodeType nodeType)
        {
            this.nodeID = nodeId;
            this.nameNode = nameNode;
            this.parentID = parentID;
            this.isActive = isActive;
            this.notse = notse;

            this.subjectCode = subjectCode;
            this.description = description;
            this.credits = credits;

            this.nodeType = nodeType;

            this.children = new List<clsSubjectTreeNode>();

            mode = enMode.update;
        }
        //____________________________________________________________________________________
        public static DataTable getAllNodes()
        {
            return clsSubjectData.getAllSubjects();
        }
        //____________________________________________________________________________________
        public static clsSubjectTreeNode findTree(int nodeID)
        {
            string name = "";
            string code = "";
            string description = "";
            int credits = 0;
            bool isActive = false;
            int? parentID = null;

            bool isFound = clsSubjectData.getSubjectInfoByID(
                nodeID,
                ref name,
                ref code,
                ref description,
                ref credits,
                ref isActive,
                ref parentID);

            if (isFound)
            {
                return new clsSubjectTreeNode(
                    nodeID,
                    name,
                    parentID,
                    isActive,
                    "",
                    code,
                    description,
                    credits,
                    enNodeType.Subject
                );
            }
            else
                return null;
        }
        //____________________________________________________________________________________
        private bool _addNewtree()
        {
            this.nodeID = clsSubjectData.addNewSubject(
                this.nameNode,
                this.subjectCode,
                this.description,
                this.credits,
                this.isActive,
                this.parentID);

            return (this.nodeID != -1);
        }
        //____________________________________________________________________________________
        private bool _updateTree()
        {
            return clsSubjectData.updateSubject(
                this.nodeID,
                this.nameNode,
                this.subjectCode,
                this.description,
                this.credits,
                this.isActive,
                this.parentID);
        }
        //____________________________________________________________________________________
        public bool save()
        {
            switch (mode)
            {
                case enMode.addNew:
                    if (_addNewtree())
                    {
                        mode = enMode.update;
                        return true;
                    }
                    return false;

                case enMode.update:
                    return _updateTree();
            }
            return false;
        }
        //____________________________________________________________________________________
        public static enErrors deleteNodeID(int nodeID)
        {
            return clsSubjectData.deleteSubject(nodeID);
        }
        //___________________________________________
        public void addChild(clsSubjectTreeNode child)
        {
            children.Add(child);
        }
    }
}