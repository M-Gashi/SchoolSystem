using System;
using System.Data;
using System.Data.SqlClient;
using SchoolSystem.Enums;
using SchoolSystem.Data.Configuration;
using SchoolSystem.Enums.Common;

namespace SchoolSystem.Data.Quran
{
    public class clsPageProgressData
    {
        //________________________________________________________
        public static int addNewPageProgress(int studentPartID,
                                             int pageID,
                                             int status,
                                             int repeatCount)
        {
            int pageProgressID = -1;

            string query = @"INSERT INTO PageProgress
                            (StudentPartID, PageID, Status, RepeatCount)
                             VALUES
                            (@StudentPartID, @PageID, @Status, @RepeatCount);
                             SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@StudentPartID", SqlDbType.Int).Value = studentPartID;
                command.Parameters.Add("@PageID", SqlDbType.Int).Value = pageID;
                command.Parameters.Add("@Status", SqlDbType.Int).Value = status;
                command.Parameters.Add("@RepeatCount", SqlDbType.Int).Value = repeatCount;

                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                    pageProgressID = Convert.ToInt32(result);
            }

            return pageProgressID;
        }

        //________________________________________________________
        public static bool updatePageProgress(int pageProgressID,
                                              int studentPartID,
                                              int pageID,
                                              int status,
                                              int repeatCount)
        {
            string query = @"UPDATE PageProgress SET
                                StudentPartID = @StudentPartID,
                                PageID = @PageID,
                                Status = @Status,
                                RepeatCount = @RepeatCount
                             WHERE PageProgressID = @PageProgressID";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@PageProgressID", SqlDbType.Int).Value = pageProgressID;
                command.Parameters.Add("@StudentPartID", SqlDbType.Int).Value = studentPartID;
                command.Parameters.Add("@PageID", SqlDbType.Int).Value = pageID;
                command.Parameters.Add("@Status", SqlDbType.Int).Value = status;
                command.Parameters.Add("@RepeatCount", SqlDbType.Int).Value = repeatCount;

                connection.Open();

                return command.ExecuteNonQuery() > 0;
            }
        }

        //________________________________________________________
        public static enErrors deletePageProgress(int pageProgressID)
        {
            try
            {
                string query = @"DELETE FROM PageProgress
                                 WHERE PageProgressID = @PageProgressID";

                using (SqlConnection connection =
                       new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@PageProgressID", SqlDbType.Int).Value = pageProgressID;

                    connection.Open();

                    int rows = command.ExecuteNonQuery();

                    return rows > 0
                        ? enErrors.Success
                        : enErrors.NotFound;
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
            catch (SqlException)
            {
                return enErrors.UnknownError;
            }
        }

        //________________________________________________________
        public static bool getPageProgressByID(int pageProgressID,
                                               ref int studentPartID,
                                               ref int pageID,
                                               ref int status,
                                               ref int repeatCount)
        {
            bool isFound = false;

            string query = @"SELECT
                                StudentPartID,
                                PageID,
                                Status,
                                RepeatCount
                             FROM PageProgress
                             WHERE PageProgressID = @PageProgressID";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@PageProgressID", SqlDbType.Int).Value = pageProgressID;

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        studentPartID = (int)reader["StudentPartID"];
                        pageID = (int)reader["PageID"];
                        status = (int)reader["Status"];
                        repeatCount = (int)reader["RepeatCount"];

                        isFound = true;
                    }
                }
            }

            return isFound;
        }

        //________________________________________________________
        public static bool getPageProgressByStudentPartAndPageID(
            int studentPartID,
            int pageID,
            ref int pageProgressID,
            ref int status,
            ref int repeatCount)
        {
            bool isFound = false;

            string query = @"SELECT
                        PageProgressID,
                        Status,
                        RepeatCount
                     FROM PageProgress
                     WHERE StudentPartID = @StudentPartID
                     AND PageID = @PageID";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@StudentPartID", SqlDbType.Int).Value = studentPartID;
                command.Parameters.Add("@PageID", SqlDbType.Int).Value = pageID;

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        pageProgressID = (int)reader["PageProgressID"];
                        status = (int)reader["Status"];
                        repeatCount = (int)reader["RepeatCount"];

                        isFound = true;
                    }
                }
            }

            return isFound;
        }
        //________________________________________________________

        public static DateTime? getOldestCreatedAtByStudentPartID(int studentPartID)
        {
            string query = @"SELECT MIN(CreatedAt)
                     FROM PageProgress
                     WHERE StudentPartID = @StudentPartID";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@StudentPartID", SqlDbType.Int).Value = studentPartID;

                connection.Open();

                object result = command.ExecuteScalar();

                if (result == DBNull.Value)
                    return null;

                return (DateTime)result;
            }
        }

        //________________________________________________________

        public static DataTable getAllPageProgress()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT 
                        PageProgressID, 
                        StudentPartID, 
                        PageID, 
                        Status, 
                        RepeatCount,
                        CreatedAt,
                        UpdatedAt
                     FROM PageProgress";

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





        //________________________________________________________
        public static int getStatusCount(int studentPartID)
        {
            int statusCount = 0;

            string query = @"
        SELECT ISNULL(SUM(Status), 0)
        FROM PageProgress
        WHERE StudentPartID = @StudentPartID";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@StudentPartID", SqlDbType.Int).Value = studentPartID;

                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                    statusCount = Convert.ToInt32(result);
            }

            return statusCount;
        }


    }
}