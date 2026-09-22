using SchoolSystem.Enums;
using SchoolSystem.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SchoolSystem.Data.Quran;

namespace SchoolSystem.Business.Quran.Pages
{
    public class clsQuranPagesSync
    {
        // نقطة الدخول الأساسية لعملية المزامنة
        public static void SyncFromFolder(string folderPath)
        {
            if (!IsValidFolder(folderPath))
                return;

            List<string> files = LoadImageFiles(folderPath);

            Dictionary<int, string> dbPages = clsPagesData.getPagesMap();

            // 1) INSERT / UPDATE
            foreach (string file in files)
            {
                int pageNumber = ExtractPageNumber(file);

                if (pageNumber <= 0)
                    continue;

                if (!dbPages.ContainsKey(pageNumber))
                {
                    clsPagesData.addNewPage(pageNumber, file);
                }
                else
                {
                    string dbPath = dbPages[pageNumber];

                    if (dbPath != file)
                    {
                        int pageID = clsPagesData.getPageIDByNumber(pageNumber);

                        if (pageID > 0)
                        {
                            clsPagesData.updatePage(pageID, pageNumber, file);
                        }
                    }
                }
            }

            // 2) DELETE
            HashSet<int> filePageNumbers = new HashSet<int>();

            foreach (string file in files)
            {
                int pageNumber = ExtractPageNumber(file);

                if (pageNumber > 0)
                    filePageNumbers.Add(pageNumber);
            }

            foreach (var dbPage in dbPages)
            {
                int pageNumber = dbPage.Key;
                string dbPath = dbPage.Value;

                if (!filePageNumbers.Contains(pageNumber))
                {
                    int pageID = clsPagesData.getPageIDByNumber(pageNumber);

                    if (pageID > 0)
                    {
                        clsPagesData.deletePage(pageID);
                    }
                }
            }
        }
        //________________________________________________________
        // قراءة ملفات الصور من المجلد
        private static List<string> LoadImageFiles(string folderPath)
        {
            List<string> imageFiles = new List<string>();

            if (!IsValidFolder(folderPath))
                return imageFiles;

            string[] allowedExtensions = new string[]
            {
        ".jpg", ".jpeg", ".png", ".bmp", ".webp", ".pdf"
            };

            string[] files = Directory.GetFiles(folderPath,"*.*",SearchOption.AllDirectories);

            foreach (string file in files)
            {
                string extension = Path.GetExtension(file).ToLower();

                if (Array.Exists(allowedExtensions, e => e == extension))
                {
                    imageFiles.Add(file);
                }
            }

            // ترتيب الملفات (مهم للصفحات)
            imageFiles.Sort();

            return imageFiles;
        }
        //________________________________________________________
        // استخراج رقم الصفحة من اسم الملف
        internal static int ExtractPageNumber(string filePath)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);

            if (string.IsNullOrWhiteSpace(fileName))
                return -1;

            // إزالة أي حروف غير رقمية تدريجيًا
            string numericPart = "";

            foreach (char c in fileName)
            {
                if (char.IsDigit(c))
                    numericPart += c;
            }

            // إذا لم نجد أي رقم
            if (string.IsNullOrEmpty(numericPart))
                return -1;

            // تحويل الرقم
            if (int.TryParse(numericPart, out int pageNumber))
                return pageNumber;

            return -1;
        }
        //________________________________________________________
        // تحقق بسيط من صحة المسار
        private static bool IsValidFolder(string folderPath)
        {
            if (string.IsNullOrWhiteSpace(folderPath))
                return false;

            if (!Directory.Exists(folderPath))
                return false;

            return true;
        }


    }
}
