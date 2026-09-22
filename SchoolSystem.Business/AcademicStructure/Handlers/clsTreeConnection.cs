using SchoolSystem.Enums.AcademicStructure;
using SchoolSystem.Enums;
using SchoolSystem.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SchoolSystem.Business.AcademicStructure.Handlers.clsTreeConnection;
using SchoolSystem.Enums.Common;


namespace SchoolSystem.Business.AcademicStructure.Handlers
{
    public class clsTreeConnection
    {

        private static readonly Dictionary<enNodeType, ifbTreeHandler> _handlers =
        new Dictionary<enNodeType, ifbTreeHandler>
        {
            { enNodeType.Stage,   new clsStageTreeHandler() },
            { enNodeType.Grade,   new clsGradeTreeHandler() },
            { enNodeType.Section, new clsSectionTreeHandler() }
        };

        public static int addNewNode(enNodeType type, string name, int? parentID, bool isActive, string notse)
            => _handlers[type].addNew(name, parentID, isActive, notse);

        public static bool updateNode(enNodeType type, int id, string name, int? parentID, bool isActive, string notse)
            => _handlers[type].update(id, name, parentID, isActive, notse);

        public static enErrors deleteNode(enNodeType type, int id)
            => _handlers[type].delete(id);

        public static bool getNodeInfoByID(enNodeType type, int id, ref string name, ref int parentID, ref bool isActive, ref string notse)
            => _handlers[type].getInfoByID( id, ref name, ref parentID, ref isActive, ref notse);


        //____________________________________________________
        public static bool deleteSubTree(int rootNodeID)
        {
            return false;
        }
        //____________________________________________________
        public static bool getNodeInfoByID(int nodeID,
                                           ref string nodeValue,
                                           ref int? parentID)
        {
            return false;
        }
        //____________________________________________________
        public static bool getroot(int nodeID,
                                           ref string nodeValue,
                                           ref int? parentID)
        {
            return false;
        }
        //____________________________________________________
        public static DataTable getAllNodes()
        {

            return null;
        }


    }
}
