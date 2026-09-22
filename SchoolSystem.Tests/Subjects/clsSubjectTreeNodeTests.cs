using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolSystem.Business.Subjects.Trees;
using SchoolSystem.Enums.Common;

namespace SchoolSystem.Tests.Subjects
{
    [TestClass]
    public class clsSubjectTreeNodeTests
    {
        [TestMethod]
        public void DefaultConstructor_CreatesEmptyRootNode()
        {
            clsSubjectTreeNode node = new clsSubjectTreeNode();

            Assert.AreEqual(-1, node.nodeID);
            Assert.AreEqual(clsSubjectTreeNode.enNodeType.Root, node.nodeType);
            Assert.AreEqual(enMode.addNew, node.mode);
            Assert.IsNotNull(node.children);
            Assert.AreEqual(0, node.children.Count);
        }

        [TestMethod]
        public void ParameterizedConstructor_PreservesSubjectData()
        {
            clsSubjectTreeNode node = new clsSubjectTreeNode(
                12,
                "Mathematics",
                3,
                true,
                "Notes",
                "MATH-12",
                "Core subject",
                4,
                clsSubjectTreeNode.enNodeType.Subject);

            Assert.AreEqual(12, node.nodeID);
            Assert.AreEqual("Mathematics", node.nameNode);
            Assert.AreEqual(3, node.parentID);
            Assert.IsTrue(node.isActive);
            Assert.AreEqual("MATH-12", node.subjectCode);
            Assert.AreEqual(4, node.credits);
            Assert.AreEqual(enMode.update, node.mode);
        }

        [TestMethod]
        public void AddChild_AddsTheSameNodeInstance()
        {
            clsSubjectTreeNode parent = new clsSubjectTreeNode();
            clsSubjectTreeNode child = new clsSubjectTreeNode();

            parent.addChild(child);

            Assert.AreEqual(1, parent.children.Count);
            Assert.AreSame(child, parent.children[0]);
        }
    }
}
