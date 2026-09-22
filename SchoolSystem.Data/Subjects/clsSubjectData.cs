using System;
using System.Data;
using System.Data.SqlClient;
using SchoolSystem.Enums;
using SchoolSystem.Data.Configuration;
using SchoolSystem.Enums.Common;

namespace SchoolSystem.Data.Subjects
{
    public class clsSubjectData
    {
        //________________________________________________________
        //________________________________________________________
        public static int addNewSubject(string subjectName,
                                        string subjectCode,
                                        string description,
                                        int credits,
                                        bool isActive,
                                        int? parentSubjectID)
        {
            int subjectID = -1;

            string query = @"INSERT INTO Subjects
                        (SubjectName, SubjectCode, Description, Credits, IsActive, ParentSubjectID)
                     VALUES
                        (@SubjectName, @SubjectCode, @Description, @Credits, @IsActive, @ParentSubjectID);
                     SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@SubjectName", subjectName);
                command.Parameters.AddWithValue("@SubjectCode", subjectCode);

                command.Parameters.AddWithValue("@Description",
                    string.IsNullOrWhiteSpace(description) ? (object)DBNull.Value : description);

                command.Parameters.AddWithValue("@Credits", credits);
                command.Parameters.AddWithValue("@IsActive", isActive);

                if (parentSubjectID == null)
                    command.Parameters.AddWithValue("@ParentSubjectID", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@ParentSubjectID", parentSubjectID);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                    subjectID = Convert.ToInt32(result);
            }

            return subjectID;
        }

        //________________________________________________________
        //________________________________________________________
        public static bool updateSubject(int subjectID,
                                         string subjectName,
                                         string subjectCode,
                                         string description,
                                         int credits,
                                         bool isActive,
                                         int? parentSubjectID)
        {
            string query = @"UPDATE Subjects SET
                        SubjectName = @SubjectName,
                        SubjectCode = @SubjectCode,
                        Description = @Description,
                        Credits     = @Credits,
                        IsActive    = @IsActive,
                        ParentSubjectID = @ParentSubjectID
                     WHERE SubjectID = @SubjectID";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@SubjectID", SqlDbType.Int).Value = subjectID;
                command.Parameters.Add("@SubjectName", SqlDbType.NVarChar, 100).Value = subjectName;
                command.Parameters.Add("@SubjectCode", SqlDbType.NVarChar, 20).Value = subjectCode;

                command.Parameters.Add("@Description", SqlDbType.NVarChar, 200).Value =
                    string.IsNullOrWhiteSpace(description) ? (object)DBNull.Value : description;

                command.Parameters.Add("@Credits", SqlDbType.Int).Value = credits;
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = isActive;

                if (parentSubjectID == null)
                    command.Parameters.Add("@ParentSubjectID", SqlDbType.Int).Value = DBNull.Value;
                else
                    command.Parameters.Add("@ParentSubjectID", SqlDbType.Int).Value = parentSubjectID;

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        //________________________________________________________
        public static enErrors deleteSubject(int subjectID)
        {
            try
            {
                string query = @"DELETE FROM Subjects WHERE SubjectID = @SubjectID";

                using (SqlConnection connection =
                       new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@SubjectID", SqlDbType.Int).Value = subjectID;

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
        //________________________________________________________
        public static bool getSubjectInfoByID(int subjectID,
                                              ref string subjectName,
                                              ref string subjectCode,
                                              ref string description,
                                              ref int credits,
                                              ref bool isActive,
                                              ref int? parentSubjectID)
        {
            bool isFound = false;

            string query = @"SELECT * FROM Subjects WHERE SubjectID = @SubjectID";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@SubjectID", SqlDbType.Int).Value = subjectID;

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        subjectName = (string)reader["SubjectName"];
                        subjectCode = (string)reader["SubjectCode"];

                        description = reader["Description"] == DBNull.Value
                            ? ""
                            : (string)reader["Description"];

                        credits = (int)reader["Credits"];
                        isActive = (bool)reader["IsActive"];

                        parentSubjectID = reader["ParentSubjectID"] == DBNull.Value
                            ? (int?)null
                            : (int)reader["ParentSubjectID"];

                        isFound = true;
                    }
                }
            }

            return isFound;
        }

        //________________________________________________________
        public static DataTable getAllSubjects()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT
                                SubjectID,
                                SubjectName,
                                SubjectCode,
                                Description,
                                Credits,
                                IsActive,
                                ParentSubjectID
                             FROM Subjects";

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