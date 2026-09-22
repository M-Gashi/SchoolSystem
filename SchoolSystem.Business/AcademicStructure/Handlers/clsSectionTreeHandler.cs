using SchoolSystem.Enums.AcademicStructure;
using SchoolSystem.Enums;
using SchoolSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using SchoolSystem.Business.Common.Exceptions;
using SchoolSystem.Data.AcademicStructure;
using SchoolSystem.Enums.Common;

namespace SchoolSystem.Business.AcademicStructure.Handlers
{
    public class clsSectionTreeHandler : ifbTreeHandler
    {
        public int addNew(string name, int? parentID, bool isActive, string notes)
            => clsSectionData.addNewSection(name, parentID.Value, isActive, notes);

        public bool update(int id, string name, int? parentID, bool isActive, string notes)
            => clsSectionData.updateSection(id, name, parentID.Value, isActive, notes);

        public enErrors delete(int id)
        {
            enErrors result = clsSectionData.deleteSection(id);

            if (result == enErrors.ForeignKeyViolation)
                throw new clsCannotDeleteException(enErrors.ForeignKeyViolation);

            if (result == enErrors.DuplicateEntry)
                throw new clsCannotDeleteException(enErrors.DuplicateEntry);


            return result;
        }

        public bool getInfoByID(int id, ref string name, ref int parentID, ref bool isActive, ref string notes)
            => clsSectionData.getSectionInfoByID(id, ref name, ref parentID, ref isActive, ref notes);


    }
}
