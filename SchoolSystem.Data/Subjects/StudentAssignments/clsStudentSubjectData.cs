using SchoolSystem.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SchoolSystem.Data.Configuration;

namespace SchoolSystem.Data.Subjects.StudentAssignments
{
    public class clsStudentSubjectData
    {
        //________________________________________________________
        public static int addNewStudentSubject(int studentID,
                                       int subjectID,
                                       int semesterID,
                                       int sectionID)
        {
            int studentSubjectID = -1;

            string query = @"INSERT INTO StudentSubjects
                    (StudentID, SubjectID, SemesterID, SectionID)
                 VALUES
                    (@StudentID, @SubjectID, @SemesterID, @SectionID);

                 SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@StudentID", studentID);
                command.Parameters.AddWithValue("@SubjectID", subjectID);
                command.Parameters.AddWithValue("@SemesterID", semesterID);
                command.Parameters.AddWithValue("@SectionID", sectionID);

                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                    studentSubjectID = Convert.ToInt32(result);
            }

            return studentSubjectID;
        }
        //________________________________________________________
        public static bool subjectHasParent(int subjectID)
        {
            string query = @"SELECT ParentSubjectID
                     FROM Subjects
                     WHERE SubjectID = @SubjectID";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@SubjectID", SqlDbType.Int).Value = subjectID;

                connection.Open();

                object result = command.ExecuteScalar();

                return (result != DBNull.Value && result != null);
            }
        }
        //________________________________________________________
        public static int getParentSubjectID(int subjectID)
        {
            int parentID = -1;

            string query = @"SELECT ParentSubjectID
                     FROM Subjects
                     WHERE SubjectID = @SubjectID";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@SubjectID", SqlDbType.Int).Value = subjectID;

                connection.Open();

                object result = command.ExecuteScalar();

                if (result != DBNull.Value && result != null)
                    parentID = Convert.ToInt32(result);
            }

            return parentID;
        }
        //________________________________________________________

        public static bool updateStudentSubject(int studentSubjectID,
                                        int studentID,
                                        int subjectID,
                                        int semesterID,
                                        int sectionID)
        {
            string query = @"UPDATE StudentSubjects SET
                        StudentID  = @StudentID,
                        SubjectID  = @SubjectID,
                        SemesterID = @SemesterID,
                        SectionID  = @SectionID
                     WHERE StudentSubjectID = @StudentSubjectID";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@StudentSubjectID", SqlDbType.Int).Value = studentSubjectID;
                command.Parameters.Add("@StudentID", SqlDbType.Int).Value = studentID;
                command.Parameters.Add("@SubjectID", SqlDbType.Int).Value = subjectID;
                command.Parameters.Add("@SemesterID", SqlDbType.Int).Value = semesterID;
                command.Parameters.Add("@SectionID", SqlDbType.Int).Value = sectionID;

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        //________________________________________________________
        public static bool deleteStudentSubject(int studentSubjectID)
        {
            string query = @"DELETE FROM StudentSubjects
                             WHERE StudentSubjectID = @StudentSubjectID";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@StudentSubjectID", SqlDbType.Int).Value = studentSubjectID;

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        //________________________________________________________
        public static DataTable getSubjectsByStudent(int studentID)
        {
            DataTable dt = new DataTable();

            string query = @"SELECT 
                                ss.StudentSubjectID,
                                sub.SubjectName,
                                sub.Credits,
                                sem.SemesterName
                             FROM StudentSubjects ss
                             JOIN Subjects sub ON ss.SubjectID = sub.SubjectID
                             JOIN Semesters sem ON ss.SemesterID = sem.SemesterID
                             WHERE ss.StudentID = @StudentID";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@StudentID", SqlDbType.Int).Value = studentID;

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }

            return dt;
        }

        //________________________________________________________
        public static bool isStudentRegisteredInSubject(int studentID,
                                                        int subjectID,
                                                        int semesterID)
        {
            string query = @"SELECT 1
                             FROM StudentSubjects
                             WHERE StudentID = @StudentID
                               AND SubjectID = @SubjectID
                               AND SemesterID = @SemesterID";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@StudentID", SqlDbType.Int).Value = studentID;
                command.Parameters.Add("@SubjectID", SqlDbType.Int).Value = subjectID;
                command.Parameters.Add("@SemesterID", SqlDbType.Int).Value = semesterID;

                connection.Open();
                return command.ExecuteScalar() != null;
            }
        }
        //________________________________________________________

        public static Dictionary<int, int> getStudentSubjectIDs(int studentID)
        {
            Dictionary<int, int> studentSubjects = new Dictionary<int, int>();

            string query = @"SELECT StudentSubjectID, SubjectID
                     FROM StudentSubjects
                     WHERE StudentID = @StudentID";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@StudentID", studentID);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        studentSubjects.Add(
                            (int)reader["SubjectID"],
                            (int)reader["StudentSubjectID"]
                        );
                    }
                }
            }

            return studentSubjects;
        }
    }
}