using SchoolSystem.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using SchoolSystem.Data.Configuration;
using SchoolSystem.Enums.Common;

namespace SchoolSystem.Data.AcademicStructure
{
    public class clsSectionData
    {

        public static int addNewSection(string sectionName, int gradeID, 
                                        bool isActive, string notes)
        {
            int sectionID = -1;
            string query = @"INSERT INTO Sections 
                                              (SectionName, GradeID, IsActive, Notes)
                                        VALUES 
                                              (@SectionName, @GradeID, @IsActive, @Notes);
                             SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@SectionName", SqlDbType.NVarChar, 50).Value = sectionName;
                command.Parameters.Add("@GradeID", SqlDbType.Int).Value              = gradeID;
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value             = isActive;
                command.Parameters.Add("@Notes", SqlDbType.NVarChar, 200).Value      =
                    (object)notes ?? DBNull.Value;


                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                    sectionID = (int)(decimal)result;
            }

            return sectionID;
        }
        //_________________________________________________________________
        public static bool updateSection(int sectionID, string sectionName, int gradeID,
                                        bool isActive, string notes)
        {
            string query = @"UPDATE Sections SET 

                         SectionName = @SectionName,
                         GradeID  = @GradeID,
                         IsActive = @IsActive,
                         Notes  = @Notes

                     WHERE SectionID = @SectionID";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@SectionID", SqlDbType.Int           ).Value = sectionID;
                command.Parameters.Add("@SectionName", SqlDbType.NVarChar, 50).Value = sectionName;
                command.Parameters.Add("@GradeID", SqlDbType.Int             ).Value = gradeID;
                command.Parameters.Add("@IsActive", SqlDbType.Bit            ).Value = isActive;
                command.Parameters.Add("@Notes", SqlDbType.NVarChar, 200     ).Value = (object)notes ?? DBNull.Value;

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
        //_________________________________________________________________
        public static enErrors deleteSection(int sectionID)
        {
            try
            {
                string query = "DELETE FROM Sections WHERE SectionID = @SectionID";

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@SectionID", SqlDbType.Int).Value = sectionID;
                    connection.Open();

                    int rows = command.ExecuteNonQuery();
                    return rows > 0 ? enErrors.Success : enErrors.NotFound;
                }
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                return enErrors.ForeignKeyViolation; // FK violation error
            }
  
        }



        //_________________________________________________________________
        public static bool getSectionInfoByID(int     sectionID, 
                                           ref string sectionName,
                                           ref int    gradeID,
                                           ref bool   isActive,
                                           ref string notes)
        {
            bool isFound = false;

            string query = @"SELECT * FROM Sections WHERE SectionID = @SectionID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@SectionID", SqlDbType.Int).Value = sectionID;

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sectionName = (string)reader["SectionName"];
                        gradeID     = (int)reader   ["GradeID"    ];
                        isActive    = (bool)reader  ["IsActive"   ];
                        notes       = reader        ["Notes"      ] as string;

                        isFound = true;
                    }
                }
            }

            return isFound;
        }
        //_________________________________________________________________
        public static DataTable getAllSection()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT
    s.SectionID,
    s.SectionName,
    s.GradeID,
    s.IsActive,
    s.Notes,
    COUNT(st.StudentID) AS StudentsCount
FROM Sections s
LEFT JOIN Students st
    ON st.SectionID = s.SectionID
GROUP BY
    s.SectionID,
    s.SectionName,
    s.GradeID,
    s.IsActive,
    s.Notes;";

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
        //_________________________________________________________________

 


    }
}
