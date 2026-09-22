using SchoolSystem.Enums.AcademicStructure;
using SchoolSystem.Enums;
using SchoolSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolSystem.Business.Common.Exceptions;
using SchoolSystem.Data.AcademicStructure;
using SchoolSystem.Enums.Common;

namespace SchoolSystem.Business.AcademicStructure.Handlers
{
    internal class clsStageTreeHandler : ifbTreeHandler
    {
        public int addNew(string name, int? parentID, bool isActive, string notes)
            => clsStageData.addNewStage(name, isActive);

        public bool update(int id, string name, int? parentID, bool isActive, string notes)
            => clsStageData.updateStage(id, name, isActive);

        public enErrors delete(int id)
        {
            enErrors result = clsStageData.deleteStage(id);

            if (result == enErrors.ForeignKeyViolation)
                throw new clsCannotDeleteException(enErrors.ForeignKeyViolation);

            if (result == enErrors.DuplicateEntry)
                throw new clsCannotDeleteException(enErrors.DuplicateEntry);


            return result;
        }

        public bool getInfoByID(int id, ref string name, ref int parentID, ref bool isActive, ref string notes)
            => clsStageData.getStageInfoByID(id, ref name, ref isActive);


    }
}
