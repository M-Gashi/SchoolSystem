using SchoolSystem.Enums;
using SchoolSystem.Data;
using System.Data;
using SchoolSystem.Data.Quran;
using SchoolSystem.Enums.Common;

namespace SchoolSystem.Business.Quran.Parts
{
    public class clsParts
    {
        public enum enMode { addNew, Update }
        public enMode _mode = enMode.addNew;

        public int partID { get; set; }
        public int subjectID { get; set; }
        public int partNumber { get; set; }
        public string PartName { get; set; }
        public int startPage { get; set; }
        public int endPage { get; set; }

        //__________________________________________
        public clsParts()
        {
            partID = -1;
            subjectID = -1;
            partNumber = 0;
            PartName = "";
            startPage = 0;
            endPage = 0;

            _mode = enMode.addNew;
        }

        //__________________________________________
        public clsParts(int partID, int subjectID, int partNumber, string partName, int startPage, int endPage)
        {
            this.partID = partID;
            this.subjectID = subjectID;
            this.partNumber = partNumber;
            this.PartName = partName;
            this.startPage = startPage;
            this.endPage = endPage;

            _mode = enMode.Update;
        }

        //__________________________________________
        public static clsParts findPart(int partID)
        {
            int subjectID = 0;
            int partNumber = 0;
            string partName = "";
            int startPage = 0;
            int endPage = 0;

            bool isFound = clsPartsData.getPartInfoByID(
                partID,
                ref subjectID,
                ref partNumber,
                ref partName,
                ref startPage,
                ref endPage
            );

            if (isFound)
                return new clsParts(partID, subjectID, partNumber, partName, startPage, endPage);
            else
                return null;
        }

        //__________________________________________
        private bool _addNewPart()
        {
            partID = clsPartsData.addNewPart(
                subjectID,
                partNumber,
                PartName,
                startPage,
                endPage
            );

            return partID != -1;
        }

        //__________________________________________
        private bool _updatePart()
        {
            return clsPartsData.updatePart(
                partID,
                subjectID,
                partNumber,
                PartName,
                startPage,
                endPage
            );
        }

        //__________________________________________
        public bool save()
        {
            switch (_mode)
            {
                case enMode.addNew:
                    if (_addNewPart())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _updatePart();
            }
            return false;
        }

        //__________________________________________
        public static enErrors deletePart(int partID)
        {
            return clsPartsData.deletePart(partID);
        }

        //__________________________________________
        public static DataTable getAllParts()
        {
            return clsPartsData.getAllParts();
        }
        //__________________________________________
        public static DataTable AllPartNames()
        {
            return clsPartsData.getAllPartNames();
        }
        //__________________________________________
        public static clsParts findPartByNumber(int partNumber)
        {
            int partID = -1;
            int subjectID = 0;
            string partName = "";
            int startPage = 0;
            int endPage = 0;

            bool isFound = clsPartsData.getPartByNumber(
                partNumber,
                ref partID,
                ref subjectID,
                ref partName,
                ref startPage,
                ref endPage
            );

            if (!isFound)
                return null;

            return new clsParts(
                partID,
                subjectID,
                partNumber,
                partName,
                startPage,
                endPage
            );
        }
    }
}