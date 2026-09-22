using SchoolSystem.Enums.AcademicStructure;
using SchoolSystem.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolSystem.Enums.Common;

namespace SchoolSystem.Business.AcademicStructure.Handlers
{
    internal interface ifbTreeHandler
    {
        int  addNew(string name, int? parentID, bool isActive, string notes);

        bool update(int id, string name, int? parentID, bool isActive, string notes);

        enErrors delete(int id);

        bool getInfoByID(int id, ref string name, ref int parentID, ref bool isActive, ref string notes);


    }
}
