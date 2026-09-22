using SchoolSystem.Enums;
using SchoolSystem.Data;
using System;
using System.Data;
using SchoolSystem.Data.Quran;
using SchoolSystem.Enums.Common;

namespace SchoolSystem.Business.Quran
{
    public class clsPageProgress
    {
        public enum enMode { addNew, Update }
        public enMode _mode = enMode.addNew;

        public int pageProgressID { get; set; }
        public int studentPartID { get; set; }
        public int pageID { get; set; }
        public int status { get; set; }
        public int repeatCount { get; set; }

        //__________________________________________
        public clsPageProgress()
        {
            pageProgressID = -1;
            studentPartID = -1;
            pageID = -1;
            status = 0;
            repeatCount = 0;

            _mode = enMode.addNew;
        }

        public clsPageProgress(int pageProgressID,
                               int studentPartID,
                               int pageID,
                               int status,
                               int repeatCount)
        {
            this.pageProgressID = pageProgressID;
            this.studentPartID = studentPartID;
            this.pageID = pageID;
            this.status = status;
            this.repeatCount = repeatCount;

            _mode = enMode.Update;
        }

        //__________________________________________
        public static clsPageProgress findPageProgress(int pageProgressID)
        {
            int studentPartID = 0;
            int pageID = 0;
            int status = 0;
            int repeatCount = 0;

            bool isFound = clsPageProgressData.getPageProgressByID(
                pageProgressID,
                ref studentPartID,
                ref pageID,
                ref status,
                ref repeatCount);

            if (isFound)
                return new clsPageProgress(
                    pageProgressID,
                    studentPartID,
                    pageID,
                    status,
                    repeatCount);
            else
                return null;
        }
        //__________________________________________
        public static clsPageProgress findPageProgress(int studentPartID, int pageID)
        {
            int pageProgressID = -1;
            int status = 0;
            int repeatCount = 0;

            bool isFound =
                clsPageProgressData.getPageProgressByStudentPartAndPageID(
                    studentPartID,
                    pageID,
                    ref pageProgressID,
                    ref status,
                    ref repeatCount);

            if (isFound)
            {
                return new clsPageProgress(
                    pageProgressID,
                    studentPartID,
                    pageID,
                    status,
                    repeatCount);
            }

            return null;
        }
        //__________________________________________

        public static DateTime? getOldestCreatedAtByStudentPartID(int studentPartID)
        {
            return clsPageProgressData.getOldestCreatedAtByStudentPartID(studentPartID);
        }

        //__________________________________________
        private bool _addNewPageProgress()
        {
            pageProgressID = clsPageProgressData.addNewPageProgress(
                studentPartID,
                pageID,
                status,
                repeatCount);

            return pageProgressID != -1;
        }

        //__________________________________________
        private bool _updatePageProgress()
        {
            return clsPageProgressData.updatePageProgress(
                pageProgressID,
                studentPartID,
                pageID,
                status,
                repeatCount);
        }

        //__________________________________________
        public bool save()
        {
            switch (_mode)
            {
                case enMode.addNew:
                    if (_addNewPageProgress())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _updatePageProgress();
            }

            return false;
        }

        //__________________________________________
        public static enErrors deletePageProgress(int pageProgressID)
        {
            return clsPageProgressData.deletePageProgress(pageProgressID);
        }

        //__________________________________________
        public static DataTable getAllPageProgress()
        {
            return clsPageProgressData.getAllPageProgress();
        }


        //________________________________________________________
        public static int getStatusCount(int studentPartID)
        {
            return clsPageProgressData.getStatusCount(studentPartID);
        }


    }
}