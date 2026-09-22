using SchoolSystem.Enums;
using System;
using System.Data;
using System.Data.SqlClient;
using SchoolSystem.Data.Configuration;

namespace SchoolSystem.Data.Teachers
    {
        public class clsTeacherData
        {
            //____________________________________________________
            public static int addNewTeacher(int personID,
                                            string employeeNo,
                                            DateTime hireDate,
                                            string specialization,
                                            string qualification,
                                            string notes,
                                            bool isActive)
            {
                int teacherID = -1;

                string query = @"INSERT INTO Teachers
                             (PersonID, EmployeeNo, HireDate, Specialization, Qualification, Notes, IsActive)
                             VALUES
                             (@PersonID, @EmployeeNo, @HireDate, @Specialization, @Qualification, @Notes, @IsActive);
                             SELECT SCOPE_IDENTITY();";

                using (SqlConnection connection =
                       new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@PersonID", SqlDbType.Int).Value = personID;
                    command.Parameters.Add("@EmployeeNo", SqlDbType.NVarChar, 30).Value = employeeNo;
                    command.Parameters.Add("@HireDate", SqlDbType.Date).Value = hireDate;
                    command.Parameters.Add("@Specialization", SqlDbType.NVarChar, 100).Value = specialization;
                    command.Parameters.Add("@Qualification", SqlDbType.NVarChar, 100).Value = qualification;
                    command.Parameters.Add("@Notes", SqlDbType.NVarChar, 200).Value =
                        (object)notes ?? DBNull.Value;
                    command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = isActive;

                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null)
                        teacherID = (int)(decimal)result;
                }

                return teacherID;
            }

            //____________________________________________________
            public static bool updateTeacher(int teacherID,
                                             int personID,
                                             string employeeNo,
                                             DateTime hireDate,
                                             string specialization,
                                             string qualification,
                                             string notes,
                                             bool isActive)
            {
                string query = @"UPDATE Teachers SET
                             PersonID = @PersonID,
                             EmployeeNo = @EmployeeNo,
                             HireDate = @HireDate,
                             Specialization = @Specialization,
                             Qualification = @Qualification,
                             Notes = @Notes,
                             IsActive = @IsActive
                             WHERE TeacherID = @TeacherID";

                using (SqlConnection connection =
                       new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@TeacherID", SqlDbType.Int).Value = teacherID;
                    command.Parameters.Add("@PersonID", SqlDbType.Int).Value = personID;
                    command.Parameters.Add("@EmployeeNo", SqlDbType.NVarChar, 30).Value = employeeNo;
                    command.Parameters.Add("@HireDate", SqlDbType.Date).Value = hireDate;
                    command.Parameters.Add("@Specialization", SqlDbType.NVarChar, 100).Value = specialization;
                    command.Parameters.Add("@Qualification", SqlDbType.NVarChar, 100).Value = qualification;
                    command.Parameters.Add("@Notes", SqlDbType.NVarChar, 200).Value =
                        (object)notes ?? DBNull.Value;
                    command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = isActive;

                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }

            //____________________________________________________
            public static bool deleteTeacher(int teacherID)
            {
                try
                {
                    string query = "DELETE FROM Teachers WHERE TeacherID = @TeacherID";

                    using (SqlConnection connection =
                           new SqlConnection(clsDataAccessSettings.ConnectionString))
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@TeacherID", SqlDbType.Int).Value = teacherID;
                        connection.Open();
                        return command.ExecuteNonQuery() > 0;
                    }
                }
                catch (SqlException ex) when (ex.Number == 547)
                {
                    return false;
                }
                catch
                {
                    return false;
                }
            }

            //____________________________________________________
            public static bool getTeacherInfoByID(int teacherID,
                                                  ref int personID,
                                                  ref string employeeNo,
                                                  ref DateTime hireDate,
                                                  ref string specialization,
                                                  ref string qualification,
                                                  ref string notes,
                                                  ref bool isActive)
            {
                bool isFound = false;

                string query = @"SELECT * FROM Teachers WHERE TeacherID = @TeacherID";

                using (SqlConnection connection =
                       new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@TeacherID", SqlDbType.Int).Value = teacherID;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            personID = (int)reader["PersonID"];
                            employeeNo = (string)reader["EmployeeNo"];
                            hireDate = (DateTime)reader["HireDate"];
                            specialization = (string)reader["Specialization"];
                            qualification = (string)reader["Qualification"];
                            notes = reader["Notes"] as string;
                            isActive = (bool)reader["IsActive"];

                            isFound = true;
                        }
                    }
                }

                return isFound;
            }

            //____________________________________________________
            public static DataTable getAllTeachers()
            {
                DataTable dt = new DataTable();

                string query = @"SELECT
                                t.TeacherID,
                                t.PersonID,
                                t.EmployeeNo,
                                t.HireDate,
                                t.Specialization,
                                t.Qualification,
                                t.IsActive
                             FROM Teachers t;";

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




