using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Enums.Common
{
    public enum enInvocationContext
    {
        None = 0,

        // Subjects
        SubjectManager = 1,
        StudentSubjects = 2,

        // Teachers
        TeacherManager = 3,
        TeacherSubjects = 4,

        // Exams
        ExamManager = 5,
        StudentExams = 6,

        // Schedule
        ScheduleManager = 7,
        StudentSchedule = 8,

        // Structure
        EducationStructure = 9


    }
}
