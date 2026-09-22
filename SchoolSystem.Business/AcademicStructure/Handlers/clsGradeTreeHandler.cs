using SchoolSystem.Enums.AcademicStructure;
using SchoolSystem.Enums;
using SchoolSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SchoolSystem.Business.Common.Exceptions;
using SchoolSystem.Data.AcademicStructure;
using SchoolSystem.Enums.Common;

namespace SchoolSystem.Business.AcademicStructure.Handlers
{
    internal class clsGradeTreeHandler : ifbTreeHandler
    {
        public int addNew(string name, int? parentID, bool isActive, string notes)
            => clsGradeData.addNewGrade(name, parentID.Value, isActive);

        public bool update(int id, string name, int? parentID, bool isActive, string notes)
            => clsGradeData.updateGrade(id, name, parentID.Value, isActive);

        public enErrors delete(int id)
        {
            enErrors result = clsGradeData.deleteGrade(id);

            if (result == enErrors.ForeignKeyViolation)
                throw new clsCannotDeleteException(enErrors.ForeignKeyViolation);

            if (result == enErrors.DuplicateEntry)
                throw new clsCannotDeleteException(enErrors.DuplicateEntry);


            return enErrors.Success;
        }

        public bool getInfoByID(int id, ref string name, ref int parentID, ref bool isActive, ref string notes)
            => clsGradeData.getGradeInfoByID(id, ref name, ref parentID, ref isActive);


    }
}
