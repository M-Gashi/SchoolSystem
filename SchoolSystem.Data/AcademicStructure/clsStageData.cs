using System;
using System.Data;
using System.Data.SqlClient;
using static System.Collections.Specialized.BitVector32;
using SchoolSystem.Enums;
using SchoolSystem.Data.Configuration;
using SchoolSystem.Enums.Common;

namespace SchoolSystem.Data.AcademicStructure
{
    public class clsStageData
    {

        public static int addNewStage(string stageName, bool isActive)
        {
            int stageID = -1;

            string query = @"INSERT INTO Stages 
                                              (StageName, IsActive)
                                        VALUES 
                                              (@StageName, @IsActive);
                             SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
           
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@StageName", stageName);
                command.Parameters.AddWithValue("@IsActive", isActive);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                    stageID = Convert.ToInt32(result);
            }

            return stageID;
        }
        //_________________________________________________________________
        public static bool updateStage(int stageID, string stageName, bool isActive)
        {
            string query = @"UPDATE Stages SET 
                         StageName = @StageName,
                         IsActive  = @IsActive
                     WHERE StageID = @StageID";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@StageID", SqlDbType.Int           ).Value = stageID;
                command.Parameters.Add("@StageName", SqlDbType.NVarChar, 50).Value = stageName;
                command.Parameters.Add("@IsActive", SqlDbType.Bit          ).Value = isActive;

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
        //_________________________________________________________________
        public static enErrors deleteStage(int stageID)
        {
            try
            {
                string query = "DELETE FROM Stages WHERE StageID = @StageID";

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@StageID", SqlDbType.Int).Value = stageID;
                    connection.Open();

                    int rows = command.ExecuteNonQuery();
                    return rows > 0 ? enErrors.Success : enErrors.NotFound;
                }
            }
            catch (SqlException ex) when (ex.Number == 547)
            {

                return enErrors.ForeignKeyViolation;
            }

            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                
                return enErrors.DuplicateEntry;
            }

       
        }

        //_________________________________________________________________
        public static bool getStageInfoByID(int stageID, ref string stageName, ref bool isActive)
        {
            bool isFound = false;

            string query = @"SELECT * FROM Stages WHERE StageID = @StageID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@StageID", SqlDbType.Int).Value = stageID;

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        stageName = (string)reader["StageName"];
                        isActive  = (bool)reader["IsActive"];

                        isFound = true;
                    }
                }
            }

            return isFound;
        }
        //_________________________________________________________________
        public static DataTable getAllStage()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT 

                                    StageID, 
                                    StageName, 
                                    IsActive 

                             FROM Stages";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
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
