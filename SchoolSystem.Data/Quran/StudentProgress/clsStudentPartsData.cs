
using System;
using System.Data;
using System.Data.SqlClient;
using SchoolSystem.Enums;
using SchoolSystem.Data.Configuration;
using SchoolSystem.Enums.Common;

namespace SchoolSystem.Data.Quran.StudentProgress
{
    public class clsStudentPartsData
    {
        //________________________________________________________
        public static int addNewStudentPart(int quranTrackID,
                                    int studentID,
                                    int partsID,
                                    bool isArchived,
                                    ref enErrors error)
        {
            int studentPartID = -1;

            error = enErrors.Success;

            string query = @"
        INSERT INTO StudentParts
        (QuranTrackID, StudentID, PartsID, IsArchived)
        VALUES
        (@QuranTrackID, @StudentID, @PartsID, @IsArchived);

        SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(
                    clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@QuranTrackID", SqlDbType.Int).Value = quranTrackID;
                    command.Parameters.Add("@StudentID", SqlDbType.Int).Value = studentID;
                    command.Parameters.Add("@PartsID", SqlDbType.Int).Value = partsID;
                    command.Parameters.Add("@IsArchived", SqlDbType.Bit).Value = isArchived;

                    connection.Open();

                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                        studentPartID = Convert.ToInt32(result);
                }
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                error = enErrors.DuplicateEntry;
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                error = enErrors.ForeignKeyViolation;
            }
            catch (SqlException)
            {
                error = enErrors.UnknownError;
            }

            return studentPartID;
        }

        //________________________________________________________
        public static bool updateStudentPart(int studentPartID,
                                             int quranTrackID,
                                             int studentID,
                                             int partsID,
                                             bool isArchived)
        {
            string query = @"
        UPDATE StudentParts
        SET QuranTrackID = @QuranTrackID,
            StudentID = @StudentID,
            PartsID = @PartsID,
            IsArchived = @IsArchived
        WHERE StudentPartID = @StudentPartID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@StudentPartID", SqlDbType.Int).Value = studentPartID;
                command.Parameters.Add("@QuranTrackID", SqlDbType.Int).Value = quranTrackID;
                command.Parameters.Add("@StudentID", SqlDbType.Int).Value = studentID;
                command.Parameters.Add("@PartsID", SqlDbType.Int).Value = partsID;
                command.Parameters.Add("@IsArchived", SqlDbType.Bit).Value = isArchived;

                connection.Open();

                return command.ExecuteNonQuery() > 0;
            }
        }

        //________________________________________________________
        public static enErrors deleteStudentPart(int studentPartID)
        {
            try
            {
                string query = @"
            DELETE FROM StudentParts
            WHERE StudentPartID = @StudentPartID";

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@StudentPartID", SqlDbType.Int).Value = studentPartID;

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
        public static bool getStudentPartByID(
            int studentPartID,
            ref int quranTrackID,
            ref int studentID,
            ref int partsID,
            ref bool isArchived)
        {
            bool isFound = false;

            string query = @"
        SELECT QuranTrackID,
               StudentID,
               PartsID,
               IsArchived
        FROM StudentParts
        WHERE StudentPartID = @StudentPartID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@StudentPartID", SqlDbType.Int).Value = studentPartID;

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        quranTrackID = (int)reader["QuranTrackID"];
                        studentID = (int)reader["StudentID"];
                        partsID = (int)reader["PartsID"];
                        isArchived = (bool)reader["IsArchived"];

                        isFound = true;
                    }
                }
            }

            return isFound;
        }

        //________________________________________________________
        public static DataTable getAllStudentParts()
        {
            DataTable dt = new DataTable();

            string query = @"
        SELECT
            StudentPartID,
            QuranTrackID,
            StudentID,
            PartsID,
            IsArchived
        FROM StudentParts";

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
        //________________________________________________________
        public static DataTable getAllStudentPartsArchived(int quranTrackID)
        {
            DataTable dt = new DataTable();

            string query = @"
SELECT StudentParts.StudentPartID, StudentParts.QuranTrackID, Persons.FirstName + ' ' + Persons.MiddleName + ' ' + Persons.LastName AS Studentname, StudentParts.IsArchived, Parts.PartName
FROM     StudentParts INNER JOIN
                  Students ON StudentParts.StudentID = Students.StudentID INNER JOIN
                  Persons ON Students.PersonID = Persons.PersonID INNER JOIN
                  Parts ON StudentParts.PartsID = Parts.PartsID
WHERE  (StudentParts.IsArchived = 1) AND (StudentParts.QuranTrackID = @QuranTrackID)";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@QuranTrackID", SqlDbType.Int).Value = quranTrackID;

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    dt.Load(reader);
                }

            }

            return dt;
        }
        //________________________________________________________
        public static DataTable getStudentsByPartID(int partID, int quranTrackID)
        {
            DataTable dt = new DataTable();

            string query = @"
        SELECT
            sp.StudentPartID,
            s.StudentID,
            (p.FirstName + ' ' + p.MiddleName + ' ' + p.LastName) AS StudentName

        FROM StudentParts sp

        INNER JOIN Students s
        ON sp.StudentID = s.StudentID

        INNER JOIN Persons p
        ON s.PersonID = p.PersonID

        WHERE sp.PartsID = @PartsID
        AND sp.QuranTrackID = @QuranTrackID
        AND sp.IsArchived = 0;";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@PartsID", SqlDbType.Int).Value = partID;
                command.Parameters.Add("@QuranTrackID", SqlDbType.Int).Value = quranTrackID;

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }

            return dt;
        }

        //________________________________________________________
        public static bool archiveStudentPart(int studentPartID)
        {
            string query = @"
UPDATE StudentParts
SET IsArchived = 1
WHERE StudentPartID = @StudentPartID";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@StudentPartID", SqlDbType.Int).Value =
                    studentPartID;

                connection.Open();

                return command.ExecuteNonQuery() > 0;
            }
        }
        //________________________________________________________
        //________________________________________________________
        public static bool isStudentPartExistsIsArchived(
            int quranTrackID,
            int studentID,
            int partsID,
            ref bool isArchived)
        {
            bool isFound = false;

            string query = @"
        SELECT IsArchived
        FROM StudentParts
        WHERE QuranTrackID = @QuranTrackID
        AND StudentID = @StudentID
        AND PartsID = @PartsID";

            using (SqlConnection connection = new SqlConnection(
                clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@QuranTrackID", SqlDbType.Int).Value = quranTrackID;
                command.Parameters.Add("@StudentID", SqlDbType.Int).Value = studentID;
                command.Parameters.Add("@PartsID", SqlDbType.Int).Value = partsID;

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        isArchived = (bool)reader["IsArchived"];
                        isFound = true;
                    }
                }
            }

            return isFound;
        }
        //________________________________________________________
    }
}

