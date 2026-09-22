using SchoolSystem.Enums;
using SchoolSystem.Data;
using System.Data;
using SchoolSystem.Data.Quran;
using SchoolSystem.Enums.Common;

namespace SchoolSystem.Business.Quran
{
    public class clsQuranTracks
    {
        public enum enMode { addNew, Update }
        public enMode _mode = enMode.addNew;

        public int quranTrackID { get; set; }
        public string trackName { get; set; }

        //__________________________________________
        public clsQuranTracks()
        {
            quranTrackID = -1;
            trackName = "";

            _mode = enMode.addNew;
        }

        public clsQuranTracks(int quranTrackID,
                              string trackName)
        {
            this.quranTrackID = quranTrackID;
            this.trackName = trackName;

            _mode = enMode.Update;
        }

        //__________________________________________
        public static clsQuranTracks findQuranTrack(int quranTrackID)
        {
            string trackName = "";

            bool isFound = clsQuranTracksData.getQuranTrackInfoByID(
                quranTrackID,
                ref trackName);

            if (isFound)
                return new clsQuranTracks(quranTrackID, trackName);
            else
                return null;
        }
        //__________________________________________

        public static clsQuranTracks findQuranTrack(string trackName)
        {
            int quranTrackID = -1;

            bool isFound = clsQuranTracksData.getQuranTrackInfoByName(
                trackName,
                ref quranTrackID);

            if (isFound)
                return new clsQuranTracks(quranTrackID, trackName);
            else
                return null;
        }
        //__________________________________________
        private bool _addNewQuranTrack()
        {
            quranTrackID = clsQuranTracksData.addNewQuranTrack(
                trackName);

            return quranTrackID != -1;
        }

        //__________________________________________
        private bool _updateQuranTrack()
        {
            return clsQuranTracksData.updateQuranTrack(
                quranTrackID,
                trackName);
        }

        //__________________________________________
        public bool save()
        {
            switch (_mode)
            {
                case enMode.addNew:
                    if (_addNewQuranTrack())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _updateQuranTrack();
            }

            return false;
        }

        //__________________________________________
        public static enErrors deleteQuranTrack(int quranTrackID)
        {
            return clsQuranTracksData.deleteQuranTrack(quranTrackID);
        }

        //__________________________________________
        public static DataTable getAllQuranTracks()
        {
            return clsQuranTracksData.getAllQuranTracks();
        }
    }
}