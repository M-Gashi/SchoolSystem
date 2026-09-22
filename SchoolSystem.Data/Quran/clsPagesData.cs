using SchoolSystem.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SchoolSystem.Data.Configuration;
using SchoolSystem.Enums.Common;

namespace SchoolSystem.Data.Quran
{
    public class clsPagesData
    {
        //________________________________________________________
        public static int addNewPage(int pageNumber,
                                     string pagePath)
        {
            int pageID = -1;

            string query = @"INSERT INTO Pages
                            (PageNumber, PagePath)
                             VALUES
                            (@PageNumber, @PagePath);
                             SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@PageNumber", SqlDbType.Int).Value = pageNumber;

                command.Parameters.Add("@PagePath", SqlDbType.NVarChar, 300).Value =
                    string.IsNullOrWhiteSpace(pagePath)
                    ? (object)DBNull.Value
                    : pagePath;

                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                    pageID = Convert.ToInt32(result);
            }

            return pageID;
        }

        //________________________________________________________
        public static bool updatePage(int pageID,
                                      int pageNumber,
                                      string pagePath)
        {
            string query = @"UPDATE Pages SET
                                PageNumber = @PageNumber,
                                PagePath = @PagePath
                             WHERE PageID = @PageID";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@PageID", SqlDbType.Int).Value = pageID;
                command.Parameters.Add("@PageNumber", SqlDbType.Int).Value = pageNumber;

                command.Parameters.Add("@PagePath", SqlDbType.NVarChar, 300).Value =
                    string.IsNullOrWhiteSpace(pagePath)
                    ? (object)DBNull.Value
                    : pagePath;

                connection.Open();

                return command.ExecuteNonQuery() > 0;
            }
        }

        //________________________________________________________
        public static enErrors deletePage(int pageID)
        {
            try
            {
                string query = @"DELETE FROM Pages WHERE PageID = @PageID";

                using (SqlConnection connection =
                       new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@PageID", SqlDbType.Int).Value = pageID;

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
        public static bool getPageInfoByID(int pageID,
                                           ref int pageNumber,
                                           ref string pagePath)
        {
            bool isFound = false;

            string query = @"SELECT
                                PageNumber,
                                PagePath
                             FROM Pages
                             WHERE PageID = @PageID";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@PageID", SqlDbType.Int).Value = pageID;

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        pageNumber = (int)reader["PageNumber"];

                        pagePath = reader["PagePath"] == DBNull.Value
                            ? ""
                            : reader["PagePath"].ToString();

                        isFound = true;
                    }
                }
            }

            return isFound;
        }
        //________________________________________________________
        public static int getPageIDByNumber(int pageNumber)
        {
            int pageID = -1;

            string query = @"SELECT PageID
                     FROM Pages
                     WHERE PageNumber = @PageNumber";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@PageNumber", SqlDbType.Int).Value = pageNumber;

                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                    pageID = Convert.ToInt32(result);
            }

            return pageID;
        }
        //________________________________________________________
        public static DataTable getAllPages()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT
    PageID,
    PageNumber,
    PagePath
FROM Pages
ORDER BY PageNumber ASC";

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
        public static Dictionary<int, string> getPagesMap()
        {
            Dictionary<int, string> pagesMap = new Dictionary<int, string>();

            string query = @"SELECT PageNumber, PagePath
                     FROM Pages";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int pageNumber = (int)reader["PageNumber"];

                        string pagePath = reader["PagePath"] == DBNull.Value
                            ? ""
                            : reader["PagePath"].ToString();

                        // حماية من التكرار
                        if (!pagesMap.ContainsKey(pageNumber))
                        {
                            pagesMap.Add(pageNumber, pagePath);
                        }
                    }
                }
            }

            return pagesMap;
        }
    }
}