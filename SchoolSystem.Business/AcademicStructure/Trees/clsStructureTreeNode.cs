using SchoolSystem.Enums.AcademicStructure;
using SchoolSystem.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolSystem.Business.AcademicStructure.Handlers;
using SchoolSystem.Enums.Common;




namespace SchoolSystem.Business.AcademicStructure.Trees
{
    public class clsStructureTreeNode
    {
        
        public enMode mode = enMode.addNew;

        public int nodeID { get; set; }
        public string nameNode { get; set; }
        public int? parentID { get; set; }
        public bool isActive { get; set; }
        public string notse {  get; set; }

        public int subTreeWidth { get; set; }
        public int treeDept { get; set; }
        public int x { get; set; }
        public int y { get; set; }

        public Rectangle bounds;

        public enNodeType nodeType {  get; set; }

        public int studentsCount { get; set; }


        public List<clsStructureTreeNode> children { get; private set; }
        //_____________________
        public clsStructureTreeNode()
        {
            this.nodeID = -1;
            this.nameNode = "";
            this.parentID = null;
            this.isActive = false;
            this.notse = "";
            this.subTreeWidth = 0;
            this.treeDept = 0;
            this.x = 100;
            this.y = 0;
            this.studentsCount = 0;
            this.nodeType = enNodeType.Root;



            this.children = new List<clsStructureTreeNode>();

            mode = enMode.addNew;
        }
        public clsStructureTreeNode(enNodeType nodeType, int nodeId, string nameNode, int? parentID)
        {
            this.nodeID = nodeId;
            this.nameNode = nameNode;
            this.parentID = parentID;
            this.nodeType = nodeType;

            this.children = new List<clsStructureTreeNode>();

            mode = enMode.update;
        }
        public clsStructureTreeNode(int nodeId, string nameNode, int? parentID, bool isActive, string notse)
        {
            this.nodeID = nodeId;
            this.nameNode = nameNode;
            this.parentID = parentID;
            this.isActive = isActive;
            this.notse = notse;

            this.children = new List<clsStructureTreeNode>();

            mode = enMode.update;
        }
        //____________________________________________________________________________________
        public static DataTable getAllNodes()
        {
            return clsTreeConnection.getAllNodes();

        }
        //____________________________________________________________________________________
        public static clsStructureTreeNode findTree(enNodeType type, int nodeID)
        {

            string nodeName = "";
            int parentID = -1;
            bool isActive = false;
            string notse = "";

            bool isFound = clsTreeConnection.getNodeInfoByID(type, nodeID, ref nodeName, ref parentID, ref isActive, ref notse);

            if (isFound)
            {
                return new clsStructureTreeNode(nodeID, nodeName, parentID, isActive, notse);
            }
            else
                return null;
        }
        //____________________________________________________________________________________
        private bool _addNewtree()
        {
            this.nodeID = clsTreeConnection.addNewNode
                (this.nodeType,
                this.nameNode,
                this.parentID,
                this.isActive,
                this.notse);

            return (this.nodeID != -1);
        }
        //____________________________________________________________________________________
        private bool _updateTree()
        {
            return clsTreeConnection.updateNode(this.nodeType, this.nodeID, this.nameNode, this.parentID,
                this.isActive,
                this.notse);
        }
        //____________________________________________________________________________________
        public bool save()
        {

            switch (mode)
            {
                //____________________
                case enMode.addNew:
                    if (_addNewtree())
                    {
                        mode = enMode.update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                //____________________   
                case enMode.update:
                    return _updateTree();
            }
            return false;
        }
        //____________________________________________________________________________________
        public static enErrors deleteNodeID(enNodeType nodeType,  int nodeID)
        {
            enErrors isDelete;

            isDelete = clsTreeConnection.deleteNode(nodeType, nodeID);

            return isDelete;

        }
        //___________________________________________
        public void addChild(clsStructureTreeNode child)
        {
            children.Add(child);
        }
        //___________________________________________


    }
}
