using SchoolSystem.Enums;
using SchoolSystem.Data;
using System.Data;
using SchoolSystem.Business.Common.Exceptions;
using SchoolSystem.Data.Quran.StudentProgress;
using SchoolSystem.Enums.Common;

namespace SchoolSystem.Business.Quran.StudentProgress
{
    public class clsStudentParts
    {
        public enum enMode
        {
            addNew,
            Update
        }

        public enMode _mode = enMode.addNew;

        public int studentPartID { get; set; }
        public int quranTrackID { get; set; }
        public int studentID { get; set; }
        public int partsID { get; set; }
        public bool isArchived { get; set; }

        //__________________________________________
        public clsStudentParts()
        {
            studentPartID = -1;
            quranTrackID = -1;
            studentID = -1;
            partsID = -1;
            isArchived = false;

            _mode = enMode.addNew;
        }

        public clsStudentParts(int studentPartID,
                               int quranTrackID,
                               int studentID,
                               int partsID,
                               bool isArchived)
        {
            this.studentPartID = studentPartID;
            this.quranTrackID = quranTrackID;
            this.studentID = studentID;
            this.partsID = partsID;
            this.isArchived = isArchived;

            _mode = enMode.Update;
        }

        //__________________________________________
        public static clsStudentParts findStudentPart(int studentPartID)
        {
            int quranTrackID = 0;
            int studentID = 0;
            int partsID = 0;
            bool isArchived = false;

            bool isFound = clsStudentPartsData.getStudentPartByID(
                studentPartID,
                ref quranTrackID,
                ref studentID,
                ref partsID,
                ref isArchived);

            if (isFound)
                return new clsStudentParts(
                    studentPartID,
                    quranTrackID,
                    studentID,
                    partsID,
                    isArchived);

            return null;
        }

        //__________________________________________
        private bool _addNewStudentPart()
        {
            enErrors error = enErrors.Success;

            studentPartID = clsStudentPartsData.addNewStudentPart(
                quranTrackID,
                studentID,
                partsID,
                isArchived,
                ref error);

            if (error != enErrors.Success)
                throw new clsCannotAddException(error);

            return studentPartID != -1;
        }

        //__________________________________________
        private bool _updateStudentPart()
        {
            return clsStudentPartsData.updateStudentPart(
                studentPartID,
                quranTrackID,
                studentID,
                partsID,
                isArchived);
        }

        //__________________________________________
        public bool save()
        {
            switch (_mode)
            {
                case enMode.addNew:

                    if (_addNewStudentPart())
                    {
                        _mode = enMode.Update;
                        return true;
                    }

                    return false;

                case enMode.Update:

                    return _updateStudentPart();
            }

            return false;
        }

        //__________________________________________
        public static enErrors deleteStudentPart(int studentPartID)
        {
            return clsStudentPartsData.deleteStudentPart(studentPartID);
        }

        //__________________________________________
        public static DataTable getAllStudentParts()
        {
            return clsStudentPartsData.getAllStudentParts();
        }
        //__________________________________________
        public static DataTable getAllStudentPartsArchived(int quranTrackID)
        {
            return clsStudentPartsData.getAllStudentPartsArchived(quranTrackID);
        }
        //__________________________________________
        public static DataTable getStudentsByPartID(int partID, int quranTrackID)
        {
            return clsStudentPartsData.getStudentsByPartID(partID, quranTrackID);
        }
        //__________________________________________
        public static bool archiveStudentPart(int studentPartID)
        {
            return clsStudentPartsData.archiveStudentPart(studentPartID);
        }
        //________________________________________________________
        public static bool isStudentPartExistsIsArchived(int quranTrackID,int studentID,int partsID,ref bool isArchived)
        {
            return clsStudentPartsData.isStudentPartExistsIsArchived( quranTrackID,studentID,partsID,ref isArchived);
        }


    }
}

