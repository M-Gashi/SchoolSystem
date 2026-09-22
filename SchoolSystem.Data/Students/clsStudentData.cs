using SchoolSystem.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using SchoolSystem.Data.Configuration;

namespace SchoolSystem.Data.Students
{
    public class clsStudentData
    {
        //_________________________________________________________________
        public static bool getStudentInfoByID(int studentID, 
                                              ref int personId, 
                                              ref int sectionID, 
                                              ref int gradeID,
                                              ref DateTime admissionDate,
                                              ref bool isActive,
                                              ref string notes)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Students WHERE studentID = @studentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@studentID", studentID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();


                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    personId = (int)reader["personID"];
                    sectionID = (int)reader["SectionID"];
                    gradeID = (int)reader["GradeID"];
                    admissionDate = (DateTime)reader["AdmissionDate"];
                    isActive = (bool)reader["IsActive"];
                    notes = (string)reader["Notes"];

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
        public static int addNewStudent(
                                       int personID,
                                       int sectionID,
                                       int gradeID,
                                       DateTime admissionDate,
                                       bool isActive,
                                       string notes)
                                       
        {
            //this function will return the new person id if succeeded and -1 if not.
            int studintID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Students (
                                                  personID, 
                                                  sectionID, 
                                                  gradeID,
                                                  admissionDate,
                                                  isActive,
                                                  notes)
                                          VALUES (
                                                  @personID, 
                                                  @sectionID, 
                                                  @gradeID,
                                                  @admissionDate,
                                                  @isActive,
                                                  @notes
                                                  );
                                              
                                              SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@personID", personID);
            command.Parameters.AddWithValue("@sectionID", sectionID);
            command.Parameters.AddWithValue("@gradeID", gradeID);
            command.Parameters.AddWithValue("@admissionDate", admissionDate);
            command.Parameters.AddWithValue("@isActive", isActive);
            command.Parameters.AddWithValue("@notes", notes);
            
            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    studintID = insertedID;
                }
            }

            catch (Exception)
            {
            }

            finally
            {
                connection.Close();
            }

            return studintID;
        }
        //_________________________________________________________________
        public static bool updateStudent(
                               int studentID,
                               int personID,
                               int sectionID,
                               int gradeID,
                               DateTime admissionDate,
                               bool isActive,
                               string notes)

        {
            //this function will return the new person id if succeeded and -1 if not.
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  Students set 
                                personID  = @personID,
                                sectionID = @sectionID,
                                gradeID   = @gradeID,
                                admissionDate = @admissionDate,
                                isActive  = @IsActive,
                                notes     = @notes

                                where StudentID = @StudentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@StudentID", studentID);
            command.Parameters.AddWithValue("@personID", personID);
            command.Parameters.AddWithValue("@sectionID", sectionID);
            command.Parameters.AddWithValue("@gradeID", gradeID);
            command.Parameters.AddWithValue("@admissionDate", admissionDate);
            command.Parameters.AddWithValue("@isActive", isActive);
            command.Parameters.AddWithValue("@notes", notes);

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
        public static DataTable getAllStudents()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);



            string query = @"SELECT Students.StudentID, 
Persons.PersonID,
(Persons.FirstName + ' ' + Persons.MiddleName + ' '+ Persons.LastName) As Studentname,
Persons.BirthDate,
Grades.GradeName, 
Sections.SectionName,
Persons.Gender,
Students.AdmissionDate,
Students.IsActive, 
Students.Notes 
FROM     Grades INNER JOIN
                  Sections ON Grades.GradeID = Sections.GradeID INNER JOIN
                  Students ON Sections.SectionID = Students.SectionID INNER JOIN
                  Persons ON Students.PersonID = Persons.PersonID
";

            SqlCommand command = new SqlCommand(query, connection);

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
        public static bool deleteStudentID(int studentID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete Students 
                                where studentID = @studentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@studentID", studentID);

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


    }


}
