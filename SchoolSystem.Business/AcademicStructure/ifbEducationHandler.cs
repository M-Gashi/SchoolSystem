using SchoolSystem.Enums.AcademicStructure;
using SchoolSystem.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Business.AcademicStructure
{
    public interface ifbEducationHandler
    {
        string name { get; set; }

        bool save();

       
    }
}
