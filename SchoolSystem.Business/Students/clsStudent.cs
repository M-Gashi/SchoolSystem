using SchoolSystem.Enums;
using SchoolSystem.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using SchoolSystem.Business.AcademicStructure;
using SchoolSystem.Business.People;
using SchoolSystem.Data.Students;

namespace SchoolSystem.Business.Students
{
    public class clsStudent
    {
        public enum enMode { addNew = 0, update = 1 };
        public enMode mode = enMode.addNew;

        public int       studentID     { set; get; }
        public int       personID      { set; get; }
        public int       sectionID     { set; get; }
        public int       gradeID       { set; get; }
        public DateTime  admissionDate { set; get; }
        public bool      IsActive      { set; get; }
        public string    notes         { set; get; }

        public clsPerson PersonInfo;

        public clsSection sectionInfo
        {
            get
            {
                return clsSection.findSection(this.sectionID);
            }
        }

        //__________________________________________
        public clsStudent()

        {
            this.studentID     = -1;
            this.admissionDate = DateTime.Now;
            this.IsActive      = true;
            this.notes         = "";

            mode = enMode.addNew;
        }
        //___________________
        public clsStudent(int      studentID,
                          int      personID,
                          int      sectionID,
                          int      gradeID,
                          DateTime admissionDate,
                          bool     IsActive,
                          string   notes
                          )

        {

            this.studentID     = studentID;
            this.personID      = personID;
            this.sectionID     = sectionID;
            this.gradeID       = gradeID;
            this.admissionDate = admissionDate;
            this.IsActive      = IsActive;
            this.notes         = notes;

            this.PersonInfo    = clsPerson.find(personID);

            mode = enMode.update;
        }
        //___________________________________________________________________________________
        //___________________________________________________________________________________
        private bool _addNewStudent()
        {
            this.studentID = clsStudentData.addNewStudent
                (this.personID,
                this.sectionID,
                this.gradeID,
                this.admissionDate,
                this.IsActive,
                this.notes);

            return (this.studentID != -1);
        }
        //_________________________________________________________
        private bool _updateStudent()
        {
            return clsStudentData.updateStudent
                (this.studentID,
                this.personID,
                this.sectionID,
                this.gradeID,
                this.admissionDate,
                this.IsActive,
                this.notes);    
        }
        //_________________________________________________________
        public static clsStudent find(int studintID)
        {
            int personID = -1;
            int sectionID = -1;
            int gradeID = -1;
            DateTime admissionDate = DateTime.Now;
            bool isActive = false;
            string notes = "";

            bool isFound = clsStudentData.getStudentInfoByID(
                    studintID,
                ref personID,
                ref sectionID,
                ref gradeID,
                ref admissionDate,
                ref isActive,
                ref notes);

            if (isFound) 
                return new clsStudent(
                     studintID,
                     personID,
                     sectionID,
                     gradeID,
                     admissionDate,
                     isActive,
                     notes
                    );
                else 
                    return null;
        }
        //_________________________________________________________
        public bool save()
        {

            switch (mode)
            {
                //____________________
                case enMode.addNew:
                    if (_addNewStudent())
                    {
                        mode = enMode.update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                //____________________   
                case enMode.update: 
                    return _updateStudent();
            }
            return false;
        }
        //_________________________________________________________
        public static DataTable getAllStudent()
        {
            return clsStudentData.getAllStudents();
        }
        //_________________________________________________________
        public static bool deleteStudentID(int studintID)
        {
            return clsStudentData.deleteStudentID(studintID);
        }


    }
}
