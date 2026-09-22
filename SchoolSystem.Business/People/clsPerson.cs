using SchoolSystem.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SchoolSystem.Data;
//_______________________________________________________________________________________________
//_______________________________________________________________________________________________
using SchoolSystem.Data.People;
namespace SchoolSystem.Business.People
{
    public class clsPerson
    {
        public enum enMode { addNew = 0, update = 1 };
        public enMode mode = enMode.addNew;

        public int      personID      { set; get; }
        public string   firstName     { set; get; }
        public string   middleName    { set; get; }
        public string   lastName      { set; get; }
        public string   fullName
        {
            get { return firstName + " " + middleName + " " + lastName; }

        }
        public DateTime birthDate     { set; get; }
        public int      gender        { set; get; }
        public string   address       { set; get; }
        public string   phone         { set; get; }
        public string   email         { set; get; }
        public int      countryID     { set; get; }

        public clsCountry CountryInfo;

        private string _ImagePath;

        public string  imagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }

        public clsPerson()

        {
            this.personID = -1;
            this.firstName = "";
            this.middleName = "";
            this.lastName = "";
            this.birthDate = DateTime.Now;
            this.gender = 0;
            this.phone = "";
            this.email = "";
            this.address = "";
            this.countryID = 0;
            this.imagePath = "";

            mode = enMode.addNew;
        }

        private clsPerson(int      personID, 
                          string   firstName, 
                          string   middleName, 
                          string   lastName, 
                          DateTime birthDate,
                          int      gender,
                          string   phone,
                          string   email,
                          string   address,
                          int      countryID,
                          string   imagePath)

        {
            this.personID = personID;
            this.firstName = firstName;
            this.middleName = middleName;
            this.lastName = lastName;
            this.birthDate = birthDate;
            this.gender = gender;
            this.phone = phone;
            this.email = email;
            this.address = address;
            this.countryID = countryID;
            this.imagePath = imagePath;

            mode = enMode.update;
        }
        //_______________________________________________________________________________________________
        public static DataTable getAllPerson()
        {
            return clsPersonData.getAllPerson();
        }
        //_________________________________________________________________
        public static clsPerson find(int personID)
        {
            string firstName = "",
                     middleName = "",
                     lastName = "",
                     phone = "",
                     email = "",
                     address = "",
                     imagePath = "";
            DateTime birthDate = DateTime.Now;
            int countryID = 0;
            int gender = 0;
            //__________________________________
            bool IsFound = clsPersonData.getPersonInfoByID
                                (
                                     personID,
                                 ref firstName,
                                 ref middleName,
                                 ref lastName,
                                 ref birthDate,
                                 ref gender,
                                 ref phone,
                                 ref email,
                                 ref address,
                                 ref countryID,
                                 ref imagePath
                                );
            //__________________________________
            if (IsFound)
                //we return new object of that person with the right data
                return new clsPerson(personID,
                                     firstName,
                                     middleName,
                                     lastName,
                                     birthDate,
                                     gender,
                                     phone,
                                     email,
                                     address,
                                     countryID,
                                     imagePath);
            else
                return null;
        }
        //_________________________________________________________________
        private bool _addNewPerson()
        {
            //call DataAccess Layer 

            this.personID = clsPersonData.addNewPerson(
            this.firstName, 
            this.middleName, 
            this.lastName, 
            this.birthDate,
            this.gender,
            this.phone,
            this.email,
            this.address, 
            this.countryID,
            this.imagePath);

            return (this.personID != -1);
        }
        //______________________________
        private bool _updatePerson()
        {
            //call DataAccess Layer 

            return clsPersonData.updatePerson(
            this.personID,
            this.firstName,
            this.middleName,
            this.lastName,
            this.birthDate,
            this.gender,
            this.phone,
            this.email,
            this.address,
            this.countryID,
            this.imagePath);

            
        }
        //______________________________
        public bool save()
        {
            switch (mode)
            {
                case enMode.addNew:
                    if (_addNewPerson())
                    {

                        mode = enMode.update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.update:

                    return _updatePerson();
            }

            return false;
        }
        //_________________________________________________________________
        public static bool deletePerson(int ID)
        {
            return clsPersonData.deletePerson(ID);
        }

    }
}
