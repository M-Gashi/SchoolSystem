using SchoolSystem.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//___________________________________________________________________________
//___________________________________________________________________________
using SchoolSystem.Data.Configuration;
namespace SchoolSystem.Data.People
{
    public class clsPersonData
    {

        //_________________________________________________________________________________________________________
        public static bool getPersonInfoByID(int          PersonID, 
                                             ref string   FirstName, 
                                             ref string   MiddleName,         
                                             ref string   LastName,  
                                             ref DateTime BirthDate,
                                             ref int      Gender,
                                             ref string   Phone,
                                             ref string   Email,
                                             ref string   Address,
                                             ref int      CountryID,
                                             ref string   ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Persons WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    FirstName = (string)reader["FirstName"];
                    MiddleName = (string)reader["MiddleName"];
                    LastName = (string)reader["LastName"];
                    BirthDate = (DateTime)reader["BirthDate"];
                    Gender = (int)reader["Gender"];
                    Phone = (string)reader["Phone"];
                    Address = (string)reader["Address"];



                    //Email: allows null in database so we should handle null
                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = (string)reader["Email"];
                    }
                    else
                    {
                        Email = "";
                    }



                    //ImagePath: allows null in database so we should handle null
                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }

                    CountryID   = (int)reader["CountryID"];
                    


                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();
            }
            catch (Exception)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        //_________________________________________________________________
        public static int addNewPerson(
                                       string   FirstName,
                                       string   MiddleName,
                                       string   LastName,
                                       DateTime BirthDate,
                                       int      Gender,
                                       string   Phone,
                                       string   Email,
                                       string   Address,
                                       int      CountryID,
                                       string   ImagePath)
        {
            //this function will return the new person id if succeeded and -1 if not.
            int PersonID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Persons (
                                                  FirstName, 
                                                  MiddleName, 
                                                  LastName,                                                 
                                                  BirthDate,
                                                  Gender,
                                                  Phone,
                                                  Email, 
                                                  Address, 
                                                  CountryID,
                                                  ImagePath)
                                          VALUES (
                                                  @FirstName, 
                                                  @MiddleName, 
                                                  @LastName,                                                 
                                                  @BirthDate,
                                                  @Gender,
                                                  @Phone,
                                                  @Email, 
                                                  @Address, 
                                                  @CountryID,
                                                  @ImagePath);
                                              
                                              SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@MiddleName", MiddleName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@BirthDate", BirthDate);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@Phone", Phone);
            
            if (Email != "" && Email != null)
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@CountryID", CountryID);

            if (ImagePath != "" && ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    PersonID = insertedID;
                }
            }

            catch (Exception)
            {
            }

            finally
            {
                connection.Close();
            }

            return PersonID;
        }
        //_________________________________________________________________
        public static bool updatePerson(int      PersonID,
                                        string   FirstName,
                                        string   MiddleName,
                                        string   LastName,
                                        DateTime BirthDate,
                                        int      Gender,
                                        string   Phone,
                                        string   Email,
                                        string   Address,
                                        int      CountryID,
                                        string   ImagePath)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  Persons  
                            set FirstName  = @FirstName,
                                MiddleName = @MiddleName,
                                LastName   = @LastName, 
                                BirthDate  = @BirthDate,
                                Gender     = @Gender,
                                Phone      = @Phone,
                                Email      = @Email, 
                                Address    = @Address,  
                                CountryID  = @CountryID,
                                ImagePath  = @ImagePath

                                where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@MiddleName", MiddleName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@BirthDate", BirthDate);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@Phone", Phone);
  
            if (Email != "" && Email != null)
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@CountryID", CountryID);

            if (ImagePath != "" && ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);


            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }
        //_________________________________________________________________
        public static DataTable getAllPerson()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            //_____________________________________
            string query =

              @"SELECT
                 Persons.PersonID, 
                 Persons.FirstName, 
                 Persons.MiddleName, 
                 Persons.LastName, 
                 Persons.BirthDate, 
                 Persons.Gender,  
                 		  CASE
                     WHEN Persons.Gender = 0 THEN 'Male'
                 
                     ELSE 'Female'
                 
                     END as GenderCaption ,
                 Persons.Address, 
                 Persons.Phone, 
                 Persons.Email, 
                 
				 Countries.CountryName,
                 Persons.ImagePath
				 
                FROM     Persons INNER JOIN
                  Countries ON Persons.CountryID = Countries.CountryID
				 
                ORDER BY Persons.FirstName";
            //_____________________________________
            SqlCommand command = new SqlCommand(query, connection);
            //_____________________________________
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception)
            {
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }
        //_________________________________________________________________
        public static bool deletePerson(int PersonID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete Persons 
                                where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception)
            {
            }
            finally
            {

                connection.Close();

            }

            return (rowsAffected > 0);

        }
        //_________________________________________________________________


    }
}
