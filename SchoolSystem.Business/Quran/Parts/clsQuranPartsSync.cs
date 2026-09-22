using SchoolSystem.Enums;
using SchoolSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using SchoolSystem.Data.Quran;

namespace SchoolSystem.Business.Quran.Parts
{
    public class clsQuranPartsSync
    {
        //=============   نقطة الدخول الأساسية   ===============
        public static void SyncParts(int subjectID)
        {
            Dictionary<int, clsPartInfo> dbParts =
                clsPartsData.getPartsMap();

            List<clsPartInfo> calculatedParts =
                BuildPartsDefinition(subjectID);

            HashSet<int> validParts = new HashSet<int>();

            //====================================================
            // INSERT / UPDATE
            //====================================================
            foreach (var part in calculatedParts)
            {
                validParts.Add(part.PartNumber);

                // INSERT
                if (!dbParts.ContainsKey(part.PartNumber))
                {
                    clsPartsData.addNewPart(
                        subjectID,
                        part.PartNumber,
                        part.PartName,
                        part.StartPage,
                        part.EndPage
                    );

                    continue;
                }

                // UPDATE
                clsPartInfo dbPart = dbParts[part.PartNumber];

                if (dbPart.StartPage != part.StartPage ||
                    dbPart.EndPage != part.EndPage ||
                    dbPart.PartName != part.PartName)
                {
                    clsPartsData.updatePart(
                        dbPart.PartID,
                        subjectID,
                        part.PartNumber,
                        part.PartName,
                        part.StartPage,
                        part.EndPage
                    );
                }
            }

            //====================================================
            // DELETE
            //====================================================
            foreach (var dbPart in dbParts)
            {
                if (!validParts.Contains(dbPart.Key))
                {
                    clsPartsData.deletePart(dbPart.Value.PartID);
                }
            }
        }

        //=============   مصدر تعريف الأجزاء   ===============
        private static List<clsPartInfo> BuildPartsDefinitionب(int subjectID)
        {
            List<clsPartInfo> parts = new List<clsPartInfo>();

            Dictionary<int, string> pagesMap = clsPagesData.getPagesMap();
            if (pagesMap == null || pagesMap.Count == 0)
                return parts;

            List<int> pageNumbers = pagesMap.Keys.ToList();
            pageNumbers.Sort();

            int totalPages = pageNumbers.Count;
            int partSize = 20;

            // ---------------------------
            // الجزء الأول
            // ---------------------------
            parts.Add(new clsPartInfo
            {
                SubjectID = subjectID,
                PartNumber = 1,
                PartName = _partNames[0],
                StartPage = pageNumbers[0],
                EndPage = pageNumbers[partSize]
            });

            int partNumber = 2;
            int theRest = 0;

            // ---------------------------
            // الأجزاء الوسطى
            // ---------------------------
            for (int i = partSize; i < totalPages - partSize && partNumber <= 29; i += partSize)
            {
                parts.Add(new clsPartInfo
                {
                    SubjectID = subjectID,
                    PartNumber = partNumber,
                    PartName = _partNames[partNumber - 1],
                    StartPage = pageNumbers[i + 1],
                    EndPage = pageNumbers[partSize + i]
                });

                theRest = pageNumbers[i + partSize];

                partNumber++;
            }

            // ---------------------------
            // الجزء الأخير
            // ---------------------------
            parts.Add(new clsPartInfo
            {
                SubjectID = subjectID,
                PartNumber = partNumber,
                PartName = _partNames[partNumber - 1],
                StartPage = pageNumbers[theRest],
                EndPage = pageNumbers[totalPages - 1]
            });

            return parts;
        }



        private static List<clsPartInfo> BuildPartsDefinition(int subjectID)
        {
            List<clsPartInfo> parts = new List<clsPartInfo>();

            Dictionary<int, string> pagesMap = clsPagesData.getPagesMap();
            if (pagesMap == null || pagesMap.Count == 0)
                return parts;

            List<int> pageNumbers = pagesMap.Keys.ToList();
            pageNumbers.Sort();

            int totalPages = pageNumbers.Count;
            int partSize = 20;

            //// ---------------------------
            //// الجزء الأول
            //// ---------------------------
            parts.Add(new clsPartInfo
            {
                SubjectID = subjectID,
                PartNumber = 1,
                PartName = _partNames[0],
                StartPage = pageNumbers[1],
                EndPage = pageNumbers[partSize]
            });

            int partNumber = 2;
            int theRest = 0;
            // ---------------------------
            // الأجزاء الوسطى (2 → 29)
            // ---------------------------
            for (int i = partSize; i < totalPages - partSize && partNumber <= 29; i += partSize)
            {
                

                parts.Add(new clsPartInfo
                {
                    SubjectID = subjectID,
                    PartNumber = partNumber,
                    PartName = _partNames[partNumber - 1],
                    StartPage = pageNumbers[i + 1],
                    EndPage = pageNumbers[i + partSize]
                });

                theRest = pageNumbers[i + partSize];

                

                partNumber++;

                
            }

            // ---------------------------
            // الجزء الأخير (30)
            // ---------------------------
            parts.Add(new clsPartInfo
            {
                SubjectID = subjectID,
                PartNumber = partNumber,
                PartName = _partNames[partNumber - 1],
                StartPage = pageNumbers[theRest ],
                EndPage = pageNumbers[totalPages - 1]
            });

            return parts;
        }
        //=============   اسماء الاجزاء   ===============
        private static readonly string[] _partNames =
{
    "الجزء 1",
    "الجزء 2",
    "الجزء 3",
    "الجزء 4",
    "الجزء 5",
    "الجزء 6",
    "الجزء 7",
    "الجزء 8",
    "الجزء 9",
    "الجزء 10",
    "الجزء 11",
    "الجزء 12",
    "الجزء 13",
    "الجزء 14",
    "الجزء 15",
    "الجزء 16",
    "الجزء 17",
    "الجزء 18",
    "الجزء 19",
    "الجزء 20",
    "الجزء 21",
    "الجزء 22",
    "الجزء 23",
    "الجزء 24",
    "الجزء 25",
    "الجزء 26",
    " الذاريات ",
    " المجادلة",
    " تبارك ",
    " عم "
};
    }
}
