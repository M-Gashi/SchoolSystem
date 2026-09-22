using SchoolSystem.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SchoolSystem.Data.Configuration;
using SchoolSystem.Enums.Common;

namespace SchoolSystem.Data.AcademicStructure
{
    public class clsGradeData
    {

        public static int addNewGrade(string gradeName, int stageID, bool isActive)
        {
            int gradeID = -1;

            string query = @"INSERT INTO Grades 
                                              (GradeName, StageID, IsActive)
                                        VALUES 
                                              (@GradeName, @StageID, @IsActive);
                             SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@GradeName", gradeName);
                command.Parameters.AddWithValue("@StageID", stageID);
                command.Parameters.AddWithValue("@IsActive", isActive);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                    gradeID = (int)(decimal)result;
            }

            return gradeID;
        }
        //_________________________________________________________________
        public static bool updateGrade(int gradeID, string gradeName, int stageID, bool isActive)
        {
            string query = @"UPDATE Grades SET 

                         GradeName = @GradeName,
                         StageID   = @StageID,
                         IsActive  = @IsActive

                     WHERE GradeID = @GradeID";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {   

                command.Parameters.Add("@GradeID", SqlDbType.Int           ).Value = gradeID;
                command.Parameters.Add("@GradeName", SqlDbType.NVarChar, 50).Value = gradeName;
                command.Parameters.Add("@StageID", SqlDbType.Int           ).Value = stageID;
                command.Parameters.Add("@IsActive", SqlDbType.Bit          ).Value = isActive;

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
        //_________________________________________________________________
        public static enErrors deleteGrade(int gradeID)
        {
            try
            {
                string query = "DELETE FROM Grades WHERE GradeID = @GradeID";

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@GradeID", SqlDbType.Int).Value = gradeID;
                    connection.Open();

                    int rows = command.ExecuteNonQuery();
                    return rows > 0 ? enErrors.Success : enErrors.NotFound;
                }
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                return enErrors.ForeignKeyViolation;
            }
            catch
            {
                return enErrors.UnknownError;
            }
        }


        //_________________________________________________________________
        public static bool getGradeInfoByID(int    gradeID, 
                                        ref string gradeName, 
                                        ref int    stageID, 
                                        ref bool   isActive)
        {
            bool isFound = false;

            string query = @"SELECT * FROM Grades WHERE GradeID = @GradeID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@GradeID", SqlDbType.Int).Value = gradeID;

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        gradeName = (string)reader["GradeName"];
                        stageID   = (int)reader   ["StageID"];
                        isActive  = (bool)reader  ["IsActive"];

                        isFound = true;
                    }
                }
            }

            return isFound;
        }
        //_________________________________________________________________
        public static DataTable getAllGrade()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT 

                                    GradeID, 
                                    GradeName,
                                    stageID,
                                    IsActive 

                             FROM Grades";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }

            return dt;
        }




    }
}
