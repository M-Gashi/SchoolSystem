using SchoolSystem.Enums.AcademicStructure;
using SchoolSystem.Enums;
using SchoolSystem.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolSystem.Data.AcademicStructure;
using SchoolSystem.Enums.Common;

namespace SchoolSystem.Business.AcademicStructure
{
    public class clsSection
    {
        public enum enMode { AddNew, Update }
        public enMode _mode = enMode.AddNew;

        public int    sectionID   { get; set; }
        public string sectionName { get; set; }
        public int    gradeID     { get; set; }
        public bool   isActive    { get; set; }
        public string notes       { get; set; }


        public clsGrade gradeInfo
        {
            get
            {
                return clsGrade.findGrade(this.gradeID);
            }
        }
        //__________________________________________
        public clsSection(int gradeID)
        {
            this.sectionID   =   -1;
            this.sectionName =   "";

            this.isActive    = true;
            this.notes       =   "";

            this.gradeID   = gradeID;

            _mode = enMode.AddNew;
        }
        public clsSection(int sectionID, string sectionName, int gradeID, bool isActive, string notes)
        {
            this.sectionID   = sectionID;
            this.sectionName = sectionName;
            this.gradeID     = gradeID;
            this.isActive    = isActive;
            this.notes       = notes;

            _mode = enMode.Update;
        }
        //_________________________________________________________________________________________
        public static clsSection findSection(int sectionID)
        {
            string sectionName =    "";
            int    gradeID     =    -1;
            bool   isActive    = false;
            string notes       =    "";
            //__________________________________________
            bool isFound = clsSectionData.getSectionInfoByID(sectionID, ref sectionName, ref gradeID, ref isActive, ref notes);
            //____________________
            if (isFound)
                return new clsSection(sectionID, sectionName, gradeID, isActive, notes);

            else
                return null;
        }
        //_________________________________________________________
        private bool _addNewSection()
        {
            this.sectionID = clsSectionData.addNewSection(this.sectionName,
                                                          this.gradeID,
                                                          this.isActive,
                                                          this.notes);
            return (this.sectionID != -1);
        }
        //___________________
        private bool _updateSection()
        {
            return clsSectionData.updateSection(this.sectionID,
                                                          this.sectionName,
                                                          this.gradeID,
                                                          this.isActive,
                                                          this.notes);
        }
        //___________________
        public bool save()
        {
            switch (_mode)
            {
                case enMode.AddNew:
                    if (_addNewSection())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _updateSection();
            }
            return false;
        }
        //_________________________________________________________
        public static enErrors deleteSectionID(int sectionID)
        {
            return clsSectionData.deleteSection(sectionID);
        }
        //_________________________________________________________
        public static DataTable getAllSection()
        {
            return clsSectionData.getAllSection();
        }


    }
}
