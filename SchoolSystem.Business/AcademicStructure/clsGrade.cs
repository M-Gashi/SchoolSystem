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
    public class clsGrade
    {
        public enum enMode {addNew, Update}
        public enMode _mode = enMode.addNew;

        public int    gardeID   {get; set;}
        public string gardeName {get; set;}
        public int    stageID   {get; set;}
        public bool   isActive  {get; set;}

        public clsStage stageInfo
        {
            get
            {
                return clsStage.findStage(this.stageID);
            }
        }
        //__________________________________________
        public clsGrade()
        {
            this.gardeID   = -1;
            this.gardeName = "";
           
            this.isActive  = true;

            _mode = enMode.addNew;
        }
        public clsGrade(int    gardeID, 
                        string gardeName, 
                        int    stageID, 
                        bool   isActive)
        {
            this.gardeID   = gardeID;
            this.gardeName = gardeName;
            this.stageID = stageID;
            this.isActive = isActive;

            _mode = enMode.Update;
        }
        //_________________________________________________________________________________________
        public static clsGrade findGrade(int gradeID)
        {
            string gardeName =    "";
            int    stageID   =    -1;
            bool   isActive  = false;
            //__________________________________________
            bool isFound = clsGradeData.getGradeInfoByID(gradeID, ref gardeName, ref stageID, ref isActive);
            //____________________
            if (isFound)
                return new clsGrade(gradeID, gardeName, stageID, isActive);

            else
                return null;
        }
        //_________________________________________________________
        private bool _addNewGrade()
        {
            this.gardeID = clsGradeData.addNewGrade(this.gardeName,
                                                    this.stageID,
                                                    this.isActive);
            return (this.gardeID != -1);
        }
        //___________________
        private bool _updateGrade()
        {
            return clsGradeData.updateGrade(this.gardeID, this.gardeName,
                                                    this.stageID,
                                                    this.isActive);

        }
        //___________________
        public bool save()
        {
            switch(_mode)
            {
                case enMode.addNew:
                    if (_addNewGrade())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _updateGrade();
            }
            return false;
        }
        //_________________________________________________________
        public static enErrors deleteGardeID(int  gradeID)
        {
            return clsGradeData.deleteGrade(gradeID);
        }
        //_________________________________________________________
        public static DataTable getAllGrade()
        {
            return clsGradeData.getAllGrade();
        }


    }
}
