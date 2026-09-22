using System;
using System.Data;
using System.Data.SqlClient;
using SchoolSystem.Enums;
using SchoolSystem.Data.Configuration;
using SchoolSystem.Enums.Common;

namespace SchoolSystem.Data.Quran
{
    public class clsRecitationErrorsData
    {
        //________________________________________________________
        public static int addNewRecitationError(int pageProgressID,
                                                 int ayahNumber,
                                                 byte errorType)
        {
            int errorID = -1;

            string query = @"INSERT INTO RecitationErrors
                            (PageProgressID, AyahNumber, ErrorType)
                             VALUES
                            (@PageProgressID, @AyahNumber, @ErrorType);
                             SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@PageProgressID", SqlDbType.Int).Value = pageProgressID;
                command.Parameters.Add("@AyahNumber", SqlDbType.Int).Value = ayahNumber;
                command.Parameters.Add("@ErrorType", SqlDbType.TinyInt).Value = errorType;

                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                    errorID = Convert.ToInt32(result);
            }

            return errorID;
        }

        //________________________________________________________
        public static bool updateRecitationError(int errorID,
                                                 int pageProgressID,
                                                 int ayahNumber,
                                                 byte errorType)
        {
            string query = @"UPDATE RecitationErrors SET
                                PageProgressID = @PageProgressID,
                                AyahNumber = @AyahNumber,
                                ErrorType = @ErrorType
                             WHERE ErrorID = @ErrorID";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@ErrorID", SqlDbType.Int).Value = errorID;
                command.Parameters.Add("@PageProgressID", SqlDbType.Int).Value = pageProgressID;
                command.Parameters.Add("@AyahNumber", SqlDbType.Int).Value = ayahNumber;
                command.Parameters.Add("@ErrorType", SqlDbType.TinyInt).Value = errorType;

                connection.Open();

                return command.ExecuteNonQuery() > 0;
            }
        }

        //________________________________________________________
        public static enErrors deleteRecitationError(int errorID)
        {
            try
            {
                string query = @"DELETE FROM RecitationErrors
                                 WHERE ErrorID = @ErrorID";

                using (SqlConnection connection =
                       new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@ErrorID", SqlDbType.Int).Value = errorID;

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
        public static bool getRecitationErrorByID(int errorID,
                                                  ref int pageProgressID,
                                                  ref int ayahNumber,
                                                  ref byte errorType)
        {
            bool isFound = false;

            string query = @"SELECT
                                PageProgressID,
                                AyahNumber,
                                ErrorType
                             FROM RecitationErrors
                             WHERE ErrorID = @ErrorID";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@ErrorID", SqlDbType.Int).Value = errorID;

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        pageProgressID = (int)reader["PageProgressID"];
                        ayahNumber = (int)reader["AyahNumber"];
                        errorType = (byte)reader["ErrorType"];

                        isFound = true;
                    }
                }
            }

            return isFound;
        }

        //________________________________________________________
        public static DataTable getAllRecitationErrors()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT
                                ErrorID,
                                PageProgressID,
                                AyahNumber,
                                ErrorType
                             FROM RecitationErrors";

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