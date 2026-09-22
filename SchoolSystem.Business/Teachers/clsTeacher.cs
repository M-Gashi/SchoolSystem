using SchoolSystem.Enums;
using SchoolSystem.Data;
using System;
using System.Data;
using SchoolSystem.Business.People;
using SchoolSystem.Data.Teachers;

namespace SchoolSystem.Business.Teachers
    {
        public class clsTeacher
        {
            public enum enMode { addNew = 0, update = 1 };
            public enMode mode = enMode.addNew;

            public int teacherID { get; set; }
            public int personID { get; set; }
            public string employeeNo { get; set; }
            public DateTime hireDate { get; set; }
            public string specialization { get; set; }
            public string qualification { get; set; }
            public bool isActive { get; set; }
            public string notes { get; set; }

            public clsPerson PersonInfo;

            //__________________________________________
            public clsTeacher()
            {
                this.teacherID = -1;
                this.employeeNo = "";
                this.hireDate = DateTime.Now;
                this.specialization = "";
                this.qualification = "";
                this.isActive = true;
                this.notes = "";

                mode = enMode.addNew;
            }

            //__________________________________________
            public clsTeacher(int teacherID,
                              int personID,
                              string employeeNo,
                              DateTime hireDate,
                              string specialization,
                              string qualification,
                              bool isActive,
                              string notes)
            {
                this.teacherID = teacherID;
                this.personID = personID;
                this.employeeNo = employeeNo;
                this.hireDate = hireDate;
                this.specialization = specialization;
                this.qualification = qualification;
                this.isActive = isActive;
                this.notes = notes;

                this.PersonInfo = clsPerson.find(personID);

                mode = enMode.update;
            }

            //_________________________________________________________
            private bool _addNewTeacher()
            {
                this.teacherID = clsTeacherData.addNewTeacher(
                    this.personID,
                    this.employeeNo,
                    this.hireDate,
                    this.specialization,
                    this.qualification,
                    this.notes,
                    this.isActive);

                return (this.teacherID != -1);
            }

            //_________________________________________________________
            private bool _updateTeacher()
            {
                return clsTeacherData.updateTeacher(
                    this.teacherID,
                    this.personID,
                    this.employeeNo,
                    this.hireDate,
                    this.specialization,
                    this.qualification,
                    this.notes,
                    this.isActive);
            }

            //_________________________________________________________
            public static clsTeacher find(int teacherID)
            {
                int personID = -1;
                string employeeNo = "";
                DateTime hireDate = DateTime.Now;
                string specialization = "";
                string qualification = "";
                bool isActive = false;
                string notes = "";

                bool isFound = clsTeacherData.getTeacherInfoByID(
                    teacherID,
                    ref personID,
                    ref employeeNo,
                    ref hireDate,
                    ref specialization,
                    ref qualification,
                    ref notes,
                    ref isActive);

                if (isFound)
                    return new clsTeacher(
                        teacherID,
                        personID,
                        employeeNo,
                        hireDate,
                        specialization,
                        qualification,
                        isActive,
                        notes);
                else
                    return null;
            }

            //_________________________________________________________
            public bool save()
            {
                switch (mode)
                {
                    case enMode.addNew:
                        if (_addNewTeacher())
                        {
                            mode = enMode.update;
                            return true;
                        }
                        else
                            return false;

                    case enMode.update:
                        return _updateTeacher();
                }
                return false;
            }

            //_________________________________________________________
            public static DataTable getAllTeachers()
            {
                return clsTeacherData.getAllTeachers();
            }

            //_________________________________________________________
            public static bool deleteTeacherID(int teacherID)
            {
                return clsTeacherData.deleteTeacher(teacherID);
            }
        }
    }


