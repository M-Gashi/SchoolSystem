using System;
using System.Data;
using System.Data.SqlClient;
using SchoolSystem.Enums;
using SchoolSystem.Data.Configuration;
using SchoolSystem.Enums.Common;

namespace SchoolSystem.Data.Quran
{
    public class clsQuranTracksData
    {
        //________________________________________________________
        public static int addNewQuranTrack(string trackName)
        {
            int quranTrackID = -1;

            string query = @"INSERT INTO QuranTracks
                            (TrackName)
                             VALUES
                            (@TrackName);
                             SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@TrackName", SqlDbType.NVarChar, 100).Value = trackName;

                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                    quranTrackID = Convert.ToInt32(result);
            }

            return quranTrackID;
        }

        //________________________________________________________
        public static bool updateQuranTrack(int quranTrackID,
                                            string trackName)
        {
            string query = @"UPDATE QuranTracks SET
                                TrackName = @TrackName
                             WHERE QuranTrackID = @QuranTrackID";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@QuranTrackID", SqlDbType.Int).Value = quranTrackID;
                command.Parameters.Add("@TrackName", SqlDbType.NVarChar, 100).Value = trackName;

                connection.Open();

                return command.ExecuteNonQuery() > 0;
            }
        }

        //________________________________________________________
        public static enErrors deleteQuranTrack(int quranTrackID)
        {
            try
            {
                string query = @"DELETE FROM QuranTracks
                                 WHERE QuranTrackID = @QuranTrackID";

                using (SqlConnection connection =
                       new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@QuranTrackID", SqlDbType.Int).Value = quranTrackID;

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
        public static bool getQuranTrackInfoByID(int quranTrackID,
                                                 ref string trackName)
        {
            bool isFound = false;

            string query = @"SELECT
                                TrackName
                             FROM QuranTracks
                             WHERE QuranTrackID = @QuranTrackID";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@QuranTrackID", SqlDbType.Int).Value = quranTrackID;

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        trackName = reader["TrackName"].ToString();

                        isFound = true;
                    }
                }
            }

            return isFound;
        }


        //________________________________________________________
        public static bool getQuranTrackInfoByName(string trackName,
                                           ref int quranTrackID)
        {
            bool isFound = false;

            string query = @"SELECT
                        QuranTrackID
                     FROM QuranTracks
                     WHERE TrackName = @TrackName";

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@TrackName", SqlDbType.NVarChar, 100).Value = trackName;

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        quranTrackID = Convert.ToInt32(reader["QuranTrackID"]);

                        isFound = true;
                    }
                }
            }

            return isFound;
        }
        //________________________________________________________
        public static DataTable getAllQuranTracks()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT
                                QuranTrackID,
                                TrackName
                             FROM QuranTracks";

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