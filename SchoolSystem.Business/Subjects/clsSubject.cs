using SchoolSystem.Enums;
using SchoolSystem.Data;
using System.Data;
using SchoolSystem.Data.Subjects;
using SchoolSystem.Enums.Common;

namespace SchoolSystem.Business.Subjects
{
    public class clsSubject
    {
        public enum enMode { addNew, Update }
        public enMode _mode = enMode.addNew;

        public int subjectID { get; set; }
        public string subjectName { get; set; }
        public string subjectCode { get; set; }
        public string description { get; set; }
        public int credits { get; set; }
        public bool isActive { get; set; }
        public int? parentSubjectID { get; set; }

        //__________________________________________
        public clsSubject()
        {
            subjectID = -1;
            subjectName = "";
            subjectCode = "";
            description = "";
            credits = 0;
            isActive = true;
            parentSubjectID = null;

            _mode = enMode.addNew;
        }

        public clsSubject(int subjectID,
                  string subjectName,
                  string subjectCode,
                  string description,
                  int credits,
                  bool isActive,
                  int? parentSubjectID)
        {
            this.subjectID = subjectID;
            this.subjectName = subjectName;
            this.subjectCode = subjectCode;
            this.description = description;
            this.credits = credits;
            this.isActive = isActive;
            this.parentSubjectID = parentSubjectID;

            _mode = enMode.Update;
        }

        //_________________________________________________________
        public static clsSubject findSubject(int subjectID)
        {
            string subjectName = "";
            string subjectCode = "";
            string description = "";
            int credits = 0;
            bool isActive = true;

            int? parentSubjectID = null;

            bool isFound = clsSubjectData.getSubjectInfoByID(
                subjectID,
                ref subjectName,
                ref subjectCode,
                ref description,
                ref credits,
                ref isActive,
                ref parentSubjectID);

            if (isFound)
                return new clsSubject(subjectID, subjectName, subjectCode, description, credits, isActive, parentSubjectID);
            else
                return null;
        }
        //_________________________________________________________
        private bool _addNewSubject()
        {
            subjectID = clsSubjectData.addNewSubject(
                subjectName,
                subjectCode,
                description,
                credits,
                isActive,
                parentSubjectID);

            return subjectID != -1;
        }
        //_________________________________________________________
        private bool _updateSubject()
        {
            return clsSubjectData.updateSubject(
                subjectID,
                subjectName,
                subjectCode,
                description,
                credits,
                isActive,
                parentSubjectID);
        }
        //_________________________________________________________
        public bool save()
        {
            switch (_mode)
            {
                case enMode.addNew:
                    if (_addNewSubject())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _updateSubject();
            }
            return false;
        }
        //_________________________________________________________
        public static enErrors deleteSubjectID(int subjectID)
        {
            return clsSubjectData.deleteSubject(subjectID);
        }
        //_________________________________________________________
        public static DataTable getAllSubjects()
        {
            return clsSubjectData.getAllSubjects();
        }
    }
}
