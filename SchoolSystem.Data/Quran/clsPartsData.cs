using SchoolSystem.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SchoolSystem.Data.Configuration;
using SchoolSystem.Enums.Common;

namespace SchoolSystem.Data.Quran
{
    public class clsPartsData
    {
        //________________________________________________________
        public static int addNewPart(int subjectID, int partNumber, string partName, int startPage, int endPage)
        {
            int partID = -1;

            string query = @"
                INSERT INTO Parts 
                    (SubjectID, PartNumber, PartName, StartPage, EndPage)
                VALUES 
                    (@SubjectID, @PartNumber, @PartName, @StartPage, @EndPage);

                SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@SubjectID", SqlDbType.Int).Value = subjectID;
                command.Parameters.Add("@PartNumber", SqlDbType.Int).Value = partNumber;
                command.Parameters.Add("@PartName", SqlDbType.NVarChar, 100).Value = partName;
                command.Parameters.Add("@StartPage", SqlDbType.Int).Value = startPage;
                command.Parameters.Add("@EndPage", SqlDbType.Int).Value = endPage;

                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                    partID = Convert.ToInt32(result);
            }

            return partID;
        }

        //________________________________________________________
        public static bool updatePart(int partID, int subjectID, int partNumber, string partName, int startPage, int endPage)
        {
            string query = @"
                UPDATE Parts SET
                    SubjectID = @SubjectID,
                    PartNumber = @PartNumber,
                    PartName = @PartName,
                    StartPage = @StartPage,
                    EndPage = @EndPage
                WHERE PartID = @PartID";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@PartID", SqlDbType.Int).Value = partID;
                command.Parameters.Add("@SubjectID", SqlDbType.Int).Value = subjectID;
                command.Parameters.Add("@PartNumber", SqlDbType.Int).Value = partNumber;
                command.Parameters.Add("@PartName", SqlDbType.NVarChar, 100).Value = partName;
                command.Parameters.Add("@StartPage", SqlDbType.Int).Value = startPage;
                command.Parameters.Add("@EndPage", SqlDbType.Int).Value = endPage;

                connection.Open();

                return command.ExecuteNonQuery() > 0;
            }
        }

        //________________________________________________________
        public static enErrors deletePart(int partID)
        {
            try
            {
                string query = @"DELETE FROM Parts WHERE PartID = @PartID";

                using (SqlConnection connection =
                       new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@PartID", SqlDbType.Int).Value = partID;

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
        public static bool getPartInfoByID(int partID,
                                           ref int subjectID,
                                           ref int partNumber,
                                           ref string partName,
                                           ref int startPage,
                                           ref int endPage)
        {
            bool isFound = false;

            string query = @"
                SELECT
                    SubjectID,
                    PartNumber,
                    PartName,
                    StartPage,
                    EndPage
                FROM Parts
                WHERE PartsID = @PartsID";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@PartsID", SqlDbType.Int).Value = partID;

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        subjectID = (int)reader["SubjectID"];
                        partNumber = (int)reader["PartNumber"];
                        partName = reader["PartName"].ToString();
                        startPage = (int)reader["StartPage"];
                        endPage = (int)reader["EndPage"];

                        isFound = true;
                    }
                }
            }

            return isFound;
        }

        //________________________________________________________
        public static DataTable getAllParts()
        {
            DataTable dt = new DataTable();

            string query = @"
                SELECT
                    PartID,
                    SubjectID,
                    PartNumber,
                    PartName,
                    StartPage,
                    EndPage
                FROM Parts";

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
        public static DataTable getAllPartNames()
        {
            DataTable dt = new DataTable();

            string query = @"
        SELECT
            PartsID,
            PartName
        FROM Parts
        ORDER BY PartNumber";

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
        public static Dictionary<int, clsPartInfo> getPartsMap()
        {
            Dictionary<int, clsPartInfo> partsMap =
                new Dictionary<int, clsPartInfo>();

            string query = @"

                SELECT
                    PartID,
                    SubjectID,
                    PartNumber,
                    PartName,
                    StartPage,
                    EndPage
                FROM Parts";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clsPartInfo part = new clsPartInfo();

                        part.PartID = (int)reader["PartID"];
                        part.SubjectID = (int)reader["SubjectID"];
                        part.PartNumber = (int)reader["PartNumber"];
                        part.PartName = reader["PartName"].ToString();
                        part.StartPage = (int)reader["StartPage"];
                        part.EndPage = (int)reader["EndPage"];

                        partsMap[part.PartNumber] = part;
                    }
                }
            }

            return partsMap;
        }



        //________________________________________________________
        public static int getPartIDByNumber(int partNumber)
        {
            int partID = -1;

            string query = @"
                SELECT PartID
                FROM Parts
                WHERE PartNumber = @PartNumber";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@PartNumber", SqlDbType.Int).Value = partNumber;

                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                    partID = Convert.ToInt32(result);
            }

            return partID;
        }

        //________________________________________________________
        public static bool getPartByNumber(
            int partNumber,
            ref int partID,
            ref int subjectID,
            ref string partName,
            ref int startPage,
            ref int endPage)
        {
            bool isFound = false;

            string query = @"
                SELECT
                    PartsID,
                    SubjectID,
                    PartName,
                    StartPage,
                    EndPage
                FROM Parts
                WHERE PartNumber = @PartNumber";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@PartNumber", SqlDbType.Int).Value = partNumber;

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        partID = (int)reader["PartsID"];
                        subjectID = (int)reader["SubjectID"];
                        partName = reader["PartName"].ToString();
                        startPage = (int)reader["StartPage"];
                        endPage = (int)reader["EndPage"];

                        isFound = true;
                    }
                }
            }

            return isFound;
        }
    }
}
