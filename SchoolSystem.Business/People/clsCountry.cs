using SchoolSystem.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SchoolSystem.Data;
using SchoolSystem.Data.People;

namespace SchoolSystem.Business.People
{
    public class clsCountry
    {

        public int ID { set; get; }
        public string countryName { set; get; }

        public clsCountry()

        {
            this.ID = -1;
            this.countryName = "";

        }

        private clsCountry(int ID, string countryName)

        {
            this.ID = ID;
            this.countryName = countryName;
        }
        //_________________________________________________________________

        public static clsCountry find(int ID)
        {
            string countryName = "";

            if (clsCountryData.getCountryInfoByID(ID, ref countryName))

                return new clsCountry(ID, countryName);
            else
                return null;

        }

        public static clsCountry find(string CountryName)
        {

            int ID = -1;

            if (clsCountryData.getCountryInfoByName(CountryName, ref ID))

                return new clsCountry(ID, CountryName);
            else
                return null;

        }

        public static DataTable getAllCountries()
        {
            return clsCountryData.getAllCountries();

        }



    }
}
