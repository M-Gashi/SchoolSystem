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
    public class clsStage
    {
        public enum enMode { addNew, Update }
        public enMode _mode = enMode.addNew;

        public int stageID      { get; set; }
        public string stageName { get; set; }
        public bool isActive    { get; set; }

        public string name
        {
            get => stageName;
            set => stageName = value;
        }
        //__________________________________________
        public clsStage()
        {
            this.stageID = -1;
            this.stageName = "";
            this.isActive = true;

            _mode = enMode.addNew;
        }
        public clsStage(int stageID, string stageName, bool isActive)
        {
            this.stageID   = stageID;
            this.stageName = stageName;
            this.isActive  = isActive;

            _mode = enMode.Update;
        }
        //_________________________________________________________________________________________
        public static clsStage findStage(int stageID)
        {
            string stageName = "";
            bool isActive    = true;

            bool isFound = clsStageData.getStageInfoByID(stageID, ref stageName, ref isActive);

            if (isFound)
                return new clsStage(stageID, stageName, isActive);
            
            else
                return null;
            
        }
        //_________________________________________________________
        private bool _addNewStage()
        {
            this.stageID = clsStageData.addNewStage(this.stageName, this.isActive);

            return (this.stageID != -1);
        }
        //___________________
        private bool _updateStage()
        {
            return clsStageData.updateStage(this.stageID, this.stageName, this.isActive);
             }
        //___________________
        public bool save()
        {

            switch (_mode)
            {
                case enMode.addNew:
                    if(_addNewStage())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
    
                case enMode.Update:
                    return _updateStage();
            }  
            return false;
        }
        //_________________________________________________________
        public static enErrors deleteStageID(int stageID)
        {
            return clsStageData.deleteStage(stageID);
        }
        //_________________________________________________________
        public static DataTable getAllStage()
        {
            return clsStageData.getAllStage();
        }


    }
}
