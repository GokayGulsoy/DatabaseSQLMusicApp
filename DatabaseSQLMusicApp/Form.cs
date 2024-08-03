using System.Windows.Forms;

namespace DatabaseSQLMusicApp
{
    public partial class Form : System.Windows.Forms.Form
    {
        BindingSource albumBindingSource = new BindingSource();
        BindingSource trackBindingSource = new BindingSource();
        List<Album> albums = new List<Album>();

        public Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            AlbumDAO albumDAO = new AlbumDAO();
            // connect the list to the grid view control 
            albums = albumDAO.GetAllAlbums();
            albumBindingSource.DataSource = albumDAO.GetAllAlbums();
            dataGridView1.DataSource = albumBindingSource;

            pictureBox1.Load("https://upload.wikimedia.org/wikipedia/en/thumb/4/42/Beatles_-_Abbey_Road.jpg/220px-Beatles_-_Abbey_Road.jpg");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AlbumDAO albumDAO = new AlbumDAO();
            // connect the list to grid view control
            dataGridView1.DataSource = albumDAO.SearchTitles(textBox1.Text);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            // get the row number clicked
            int rowClicked = dataGridView.CurrentRow.Index;
            // MessageBox.Show("You clicked row " + rowClicked);

            string imageURL = dataGridView.Rows[rowClicked].Cells[3].Value.ToString();
            // MessageBox.Show(imageURL);
            pictureBox1.Load(imageURL);

            trackBindingSource.DataSource = albums[rowClicked].Tracks;
            dataGridView2.DataSource = trackBindingSource;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // add a new item to the database
            Album album = new Album
            {
                AlbumName = txt_albumName.Text,
                ArtistName = txt_albumArtist.Text,
                Year = Int32.Parse(txt_albumYear.Text),
                ImageURL = txt_ImageURL.Text,
                Description = txt_Description.Text,
            };

            AlbumDAO albumDAO = new AlbumDAO();
            int result = albumDAO.addOneAlbum(album);
            MessageBox.Show(result + " new row(s) inserted");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int rowClickedTrack = dataGridView2.CurrentRow.Index;
            MessageBox.Show("You clicked row " + rowClickedTrack);
            int trackID = (int)dataGridView2.Rows[rowClickedTrack].Cells[0].Value;
            MessageBox.Show("You selected track with ID " + trackID);

            AlbumDAO albumDAO = new AlbumDAO();
            int result = albumDAO.DeleteTrack(trackID);
            MessageBox.Show("Deleting " + result + " track");

            // updating the UI synchronously after DELETE query
            int rowClickedAlbum = dataGridView1.CurrentRow.Index;
            int albumID = (int)dataGridView1.Rows[rowClickedAlbum].Cells[0].Value;
            albums[rowClickedAlbum].Tracks = albumDAO.GetTracksForAlbum(albumID);
            trackBindingSource.DataSource = albums[rowClickedTrack].Tracks;
            dataGridView2.DataSource = trackBindingSource;
        }
    }
}
