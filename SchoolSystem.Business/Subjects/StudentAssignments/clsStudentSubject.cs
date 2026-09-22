using SchoolSystem.Enums;
using SchoolSystem.Data;
using System.Collections.Generic;
using System.Data;
using SchoolSystem.Data.Subjects.StudentAssignments;

namespace SchoolSystem.Business.Subjects.StudentAssignments
{
    public class clsStudentSubject
    {
        public enum enMode { addNew, Update }
        public enMode _mode = enMode.addNew;

        public int studentSubjectID { get; set; }
        public int studentID { get; set; }
        public int subjectID { get; set; }
        public int semesterID { get; set; }
        public int sectionID { get; set; }

        //__________________________________________
        public clsStudentSubject()
        {
            studentSubjectID = -1;
            studentID = -1;
            subjectID = -1;
            semesterID = -1;
            sectionID = -1;

            _mode = enMode.addNew;
        }

        public clsStudentSubject(int studentSubjectID,
                                 int studentID,
                                 int subjectID,
                                 int semesterID,
                                 int sectionID)
        {
            this.studentSubjectID = studentSubjectID;
            this.studentID = studentID;
            this.subjectID = subjectID;
            this.semesterID = semesterID;
            this.sectionID = sectionID;

            _mode = enMode.Update;
        }

        //_________________________________________________________
        private bool _addNew()
        {
            if (clsStudentSubjectData.subjectHasParent(subjectID))
            {
                int parentID = clsStudentSubjectData.getParentSubjectID(subjectID);

                bool hasParent = clsStudentSubjectData.isStudentRegisteredInSubject(studentID, parentID, semesterID);

                if (!hasParent)
                    return false;
            }


            this.studentSubjectID = clsStudentSubjectData.addNewStudentSubject(
                this.studentID,
                this.subjectID,
                this.semesterID,
                this.sectionID);

            return this.studentSubjectID != -1;
        }

        //_________________________________________________________
        private bool _update()
        {
            return clsStudentSubjectData.updateStudentSubject(
                studentSubjectID,
                studentID,
                subjectID,
                semesterID,
                sectionID);
        }

        //_________________________________________________________
        public bool save()
        {
            switch (_mode)
            {
                case enMode.addNew:
                    if (_addNew())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _update();
            }
            return false;
        }

        //_________________________________________________________
        public static bool delete(int studentSubjectID)
        {
            return clsStudentSubjectData.deleteStudentSubject(studentSubjectID);
        }

        //_________________________________________________________
        public static DataTable getSubjectsByStudent(int studentID)
        {
            return clsStudentSubjectData.getSubjectsByStudent(studentID);
        }
        //_________________________________________________________
        public static Dictionary<int, int> getStudentSubjectIDs(int studentID)
        {
            return clsStudentSubjectData.getStudentSubjectIDs(studentID);
        }
        //_________________________________________________________
        public static bool isRegistered(int studentID, int subjectID, int semesterID)
        {
            return clsStudentSubjectData.isStudentRegisteredInSubject(
                studentID, subjectID, semesterID);
        }
    }
}