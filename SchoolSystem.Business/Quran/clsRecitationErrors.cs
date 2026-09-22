using SchoolSystem.Enums;
using SchoolSystem.Data;
using System.Data;
using SchoolSystem.Data.Quran;
using SchoolSystem.Enums.Common;

namespace SchoolSystem.Business.Quran
{
    public class clsRecitationErrors
    {
        public enum enMode { addNew, Update }
        public enMode _mode = enMode.addNew;

        public int errorID { get; set; }
        public int pageProgressID { get; set; }
        public int ayahNumber { get; set; }
        public byte errorType { get; set; }

        //__________________________________________
        public clsRecitationErrors()
        {
            errorID = -1;
            pageProgressID = -1;
            ayahNumber = 0;
            errorType = 0;

            _mode = enMode.addNew;
        }

        public clsRecitationErrors(int errorID,
                                   int pageProgressID,
                                   int ayahNumber,
                                   byte errorType)
        {
            this.errorID = errorID;
            this.pageProgressID = pageProgressID;
            this.ayahNumber = ayahNumber;
            this.errorType = errorType;

            _mode = enMode.Update;
        }

        //__________________________________________
        public static clsRecitationErrors findRecitationError(int errorID)
        {
            int pageProgressID = 0;
            int ayahNumber = 0;
            byte errorType = 0;

            bool isFound = clsRecitationErrorsData.getRecitationErrorByID(
                errorID,
                ref pageProgressID,
                ref ayahNumber,
                ref errorType);

            if (isFound)
                return new clsRecitationErrors(errorID, pageProgressID, ayahNumber, errorType);
            else
                return null;
        }

        //__________________________________________
        private bool _addNewRecitationError()
        {
            errorID = clsRecitationErrorsData.addNewRecitationError(
                pageProgressID,
                ayahNumber,
                errorType);

            return errorID != -1;
        }

        //__________________________________________
        private bool _updateRecitationError()
        {
            return clsRecitationErrorsData.updateRecitationError(
                errorID,
                pageProgressID,
                ayahNumber,
                errorType);
        }

        //__________________________________________
        public bool save()
        {
            switch (_mode)
            {
                case enMode.addNew:
                    if (_addNewRecitationError())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _updateRecitationError();
            }
            return false;
        }

        //__________________________________________
        public static enErrors deleteRecitationError(int errorID)
        {
            return clsRecitationErrorsData.deleteRecitationError(errorID);
        }

        //__________________________________________
        public static DataTable getAllRecitationErrors()
        {
            return clsRecitationErrorsData.getAllRecitationErrors();
        }
    }
}