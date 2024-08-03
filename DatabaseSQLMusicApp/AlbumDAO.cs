using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;

namespace DatabaseSQLMusicApp
{
    internal class AlbumDAO
    {
        // use your own password for connecting to MySQL database
        string connectionString =
            "datasource=localhost;port=3306;username=root;password=******;database=music";
        public List<Album> GetAllAlbums()
        {
            // start with an empty list 
            List<Album> returnThese = new List<Album>();

            // connect to mysql server
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = new MySqlCommand("SELECT * FROM ALBUM", connection);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Album a = new Album
                    {
                        ID = reader.GetInt32(0),
                        AlbumName = reader.GetString(1),
                        ArtistName = reader.GetString(2),
                        Year = reader.GetInt32(3),
                        ImageURL = reader.GetString(4),
                        Description = reader.GetString(5)
                    };


                    a.Tracks = GetTracksForAlbum(a.ID);
                    returnThese.Add(a);
                }

            }

            connection.Close();

            return returnThese;
        }


        public List<Album> SearchTitles(string searchTerm)
        {
            // start with an empty list 
            List<Album> returnThese = new List<Album>();

            // connect to mysql server
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string searchWildPhrase = "%" + searchTerm + "%";
            MySqlCommand command = new MySqlCommand("SELECT ID, ALBUM_TITLE, ARTIST," +
                "YEAR, IMAGE_NAME, DESCRIPTION FROM ALBUM WHERE ALBUM_TITLE LIKE @search", connection);

            command.Parameters.AddWithValue("@search", searchWildPhrase);
            command.Connection = connection;

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Album a = new Album
                    {
                        ID = reader.GetInt32(0),
                        AlbumName = reader.GetString(1),
                        ArtistName = reader.GetString(2),
                        Year = reader.GetInt32(3),
                        ImageURL = reader.GetString(4),
                        Description = reader.GetString(5)
                    };

                    returnThese.Add(a);
                }

            }

            connection.Close();

            return returnThese;
        }


        public int addOneAlbum(Album album)
        {
            // connect to mysql server
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();


            // define sql command to add an entry to music database
            MySqlCommand command = new MySqlCommand("INSERT INTO `album`(`ALBUM_TITLE`, `ARTIST`, `YEAR`, `IMAGE_NAME`, `DESCRIPTION`) VALUES " +
                "(@albumtitle,@artist,@year,@imageURL,@description)", connection);

            command.Parameters.AddWithValue("@albumtitle", album.AlbumName);

            command.Parameters.AddWithValue("@artist", album.ArtistName);

            command.Parameters.AddWithValue("@year", album.Year);

            command.Parameters.AddWithValue("@imageURL", album.ImageURL);

            command.Parameters.AddWithValue("@description", album.Description);

            int newRows = command.ExecuteNonQuery();

            connection.Close();

            return newRows;
        }

        public List<Track> GetTracksForAlbum(int albumID)
        {
            // start with an empty list 
            List<Track> returnThese = new List<Track>();

            // connect to mysql server
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = new MySqlCommand("SELECT * FROM TRACKS WHERE album_ID = @albumid", connection);
            command.Parameters.AddWithValue("@albumid", albumID);
            command.Connection = connection;

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Track a = new Track
                    {
                        ID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Number = reader.GetInt32(2),
                        VideoURL = reader.GetString(3),
                        Lyrics = reader.GetString(4)
                    };

                    returnThese.Add(a);
                }
            }

            connection.Close();

            return returnThese;
        }

        public List<JObject> GetTracksUsingJoin(int albumID)
        {
            // start with an empty list 
            List<JObject> returnThese = new List<JObject>();

            // connect to mysql server
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = new MySqlCommand("SELECT tracks.ID AS TrackID, album.ALBUM_TITLE, `track_title`, `number`, `video_url`, `lyrics` FROM `tracks`" +
                "JOIN album ON album_ID = album.ID WHERE album.ID = @albumid;", connection);
            command.Parameters.AddWithValue("@albumid", albumID);
            command.Connection = connection;

            using (MySqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    JObject newTrack = new JObject();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        newTrack.Add(reader.GetName(i).ToString(), reader.GetValue(i).ToString());
                    }

                    returnThese.Add(newTrack);
                }
            }
            connection.Close();

            return returnThese;
        }

        public int DeleteTrack(int trackID)
        {
            // connect to mysql server
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            // define sql command to delete an track from specific album
            MySqlCommand command = new MySqlCommand("DELETE FROM `tracks` " +
                "WHERE tracks.ID = @trackID;", connection);

            command.Parameters.AddWithValue("@trackID", trackID);

            int result = command.ExecuteNonQuery();

            connection.Close();

            return result;
        }
    }
}
