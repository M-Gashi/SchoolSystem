using SchoolSystem.Enums;
using SchoolSystem.Data;
using System.Collections.Generic;
using System.Data;
using SchoolSystem.Data.Quran;
using SchoolSystem.Enums.Common;

namespace SchoolSystem.Business.Quran.Pages
{
    public class clsPages
    {
        public enum enMode { addNew, Update }
        public enMode _mode = enMode.addNew;

        public int pageID { get; set; }
        public int pageNumber { get; set; }
        public string pagePath { get; set; }

        //__________________________________________
        public clsPages()
        {
            pageID = -1;
            pageNumber = 0;
            pagePath = "";

            _mode = enMode.addNew;
        }

        public clsPages(int pageID,
                        int pageNumber,
                        string pagePath)
        {
            this.pageID = pageID;
            this.pageNumber = pageNumber;
            this.pagePath = pagePath;

            _mode = enMode.Update;
        }

        //__________________________________________
        public static clsPages findPage(int pageID)
        {
            int pageNumber = 0;
            string pagePath = "";

            bool isFound = clsPagesData.getPageInfoByID(
                pageID,
                ref pageNumber,
                ref pagePath);

            if (isFound)
                return new clsPages(pageID, pageNumber, pagePath);
            else
                return null;
        }

        //__________________________________________
        private bool _addNewPage()
        {
            pageID = clsPagesData.addNewPage(
                pageNumber,
                pagePath);

            return pageID != -1;
        }

        //__________________________________________
        private bool _updatePage()
        {
            return clsPagesData.updatePage(
                pageID,
                pageNumber,
                pagePath);
        }

        //__________________________________________
        public bool save()
        {
            switch (_mode)
            {
                case enMode.addNew:
                    if (_addNewPage())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _updatePage();
            }

            return false;
        }

        //__________________________________________
        public static enErrors deletePage(int pageID)
        {
            return clsPagesData.deletePage(pageID);
        }

        //__________________________________________
        public static DataTable getAllPages()
        {
            return clsPagesData.getAllPages();
        }

        //__________________________________________
        public static List<clsPages> getPagesBetween(int startPage,int endPage)
        {
            List<clsPages> pages = new List<clsPages>();

            DataTable dt = clsPagesData.getAllPages();

            foreach (DataRow row in dt.Rows)
            {
                int pageNumber = (int)row["PageNumber"];

                if (pageNumber >= startPage &&
                    pageNumber <= endPage)
                {
                    pages.Add(
                        findPage((int)row["PageID"])
                    );
                }
            }

            return pages;
        }
    }
}