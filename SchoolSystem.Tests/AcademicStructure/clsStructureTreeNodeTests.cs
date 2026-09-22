using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolSystem.Business.AcademicStructure.Trees;
using SchoolSystem.Enums.AcademicStructure;
using SchoolSystem.Enums.Common;

namespace SchoolSystem.Tests.AcademicStructure
{
    [TestClass]
    public class clsStructureTreeNodeTests
    {
        [TestMethod]
        public void DefaultConstructor_CreatesEmptyRootNode()
        {
            clsStructureTreeNode node = new clsStructureTreeNode();

            Assert.AreEqual(-1, node.nodeID);
            Assert.AreEqual(enNodeType.Root, node.nodeType);
            Assert.AreEqual(enMode.addNew, node.mode);
            Assert.AreEqual(0, node.studentsCount);
            Assert.IsNotNull(node.children);
            Assert.AreEqual(0, node.children.Count);
        }

        [TestMethod]
        public void TypedConstructor_PreservesHierarchyData()
        {
            clsStructureTreeNode node = new clsStructureTreeNode(
                enNodeType.Grade,
                7,
                "Grade 7",
                2);

            Assert.AreEqual(7, node.nodeID);
            Assert.AreEqual("Grade 7", node.nameNode);
            Assert.AreEqual(2, node.parentID);
            Assert.AreEqual(enNodeType.Grade, node.nodeType);
            Assert.AreEqual(enMode.update, node.mode);
        }

        [TestMethod]
        public void AddChild_AddsTheSameNodeInstance()
        {
            clsStructureTreeNode parent = new clsStructureTreeNode();
            clsStructureTreeNode child = new clsStructureTreeNode();

            parent.addChild(child);

            Assert.AreEqual(1, parent.children.Count);
            Assert.AreSame(child, parent.children[0]);
        }
    }
}
