# Database MySQL and .NET Application for Album and Song Management 

## Getting Project in Your Local Machine

type the following command from terminal or command line `git clone https://github.com/GokayGulsoy/DatabaseSQLMusicApp.git` 

NOTE: You should have already installed `git` in your computer to successfully execute the above command

# Some Important Notes

- I have developed the desktop application with Visual Studio 2022 Community Edition as Windows Forms application.
- You need to give your own password for connecting to MySQL database in place of `******` in the code snippet
given in the top of the `AlbumDAO.cs` file when creating connection string.
- You can add Albums to database by filling out the `Add Album` form with your preferred Album Name, Artist, Year,
ImageURL, and Description information then clicking the add button.
- You can load all the available albums in your database by clicking `Load Albums` button.
- You can enter specific pattern in the search bar and fetch the Album(s) that contain the given pattern.
- You can select specific album by clicking it and all the tracks that belong to given album will be displayed in Tracks grid.
- You can select specific track by clicking specific track in Tracks grid and click `Delete Selected Track` button to delete it.
- Image of selected album will be displayed on the right side of the albums grid.

  ![Application UI](https://github.com/GokayGulsoy/DatabaseSQLMusicApp/blob/master/App_UI.png)
