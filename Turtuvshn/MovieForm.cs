using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using static Turtuvshn.Repositity;
namespace Turtuvshn
{
    public partial class MovieForm : Form
    {
        string connStr = "server=localhost;database=gf;port=3306;uid=root;pwd=;";
        private object newID;
        private object txtmovieid;

        public object DataeHelper { get; private set; }

        public MovieForm()
        {
            InitializeComponent();
        }
        MySqlConnection GetConnection()
        {
            return new MySqlConnection(connStr);
        }

        private void dgvMovie_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvMovie.Rows[e.RowIndex];
            textBox1.Text = row.Cells[0].Value.ToString();
            txtTitle.Text = row.Cells[1].Value.ToString();
            txtYear1.Text = row.Cells[2].Value.ToString();
            txtDirector.Text = row.Cells[3].Value.ToString();
        }
        private void MovieForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // Хүснэгтийг шинэчлэн унших функц
        void LoadData()
        {
            DataHelper.GetConnection();

            try
            {
                using (var conn = DataHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT * FROM movie;";
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvMovie.DataSource = dt;

                    if (dgvMovie.Columns.Count > 0)
                    {
                        dgvMovie.Columns["movieid"].HeaderText = "ID";
                        dgvMovie.Columns["title"].HeaderText = "Movie Name";
                        dgvMovie.Columns["year1"].HeaderText = "Year";
                        dgvMovie.Columns["Director"].HeaderText = "Director";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Мэдээлэл уншихад алдаа гарлаа: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            {
                if (string.IsNullOrWhiteSpace(txtTitle.Text) || string.IsNullOrWhiteSpace(txtDirector.Text))
                {
                    MessageBox.Show("Мэдээллээ бүрэн бөглөнө үү!");
                    return;
                }

                try
                {
                    using (var conn = GetConnection())
                    {
                        conn.Open();

                        MovieRepository repo = new MovieRepository();

                        string nextID = repo.GetNextMovieID();

                        MessageBox.Show("Үүссэн ID: " + nextID);

                        string sqlInsert = "INSERT INTO movie (movieid, title, year1, Director) VALUES (@id, @title, @year, @dir)";
                        using (MySqlCommand cmd = new MySqlCommand(sqlInsert, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", nextID); ;
                            cmd.Parameters.AddWithValue("@title", txtTitle.Text);
                            cmd.Parameters.AddWithValue("@year", txtYear1.Text);
                            cmd.Parameters.AddWithValue("@dir", txtDirector.Text);

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Шинэ кино амжилттай хадгалагдлаа! ID: " + newID);

                            LoadData();

                            txtTitle.Clear();
                            txtYear1.Clear();
                            txtDirector.Clear();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Хадгалахад алдаа гарлаа: " + ex.Message);
                }
            }

        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Хүснэгтээс мөр сонгосон эсэхийг шалгах
            if (dgvMovie.SelectedRows.Count > 0)
            {
                // Сонгогдсон мөрний movieid-г (жишээ нь M105) авах
                string selectedID = dgvMovie.SelectedRows[0].Cells["movieid"].Value.ToString();

                // Устгахдаа итгэлтэй байгаа эсэхийг асуух
                DialogResult result = MessageBox.Show(selectedID + " ID-тай бичлэгийг устгах уу?", "Устгах", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (var conn = GetConnection())
                        {
                            conn.Open();
                            // ID-аар нь шүүж устгах тушаал
                            string sql = "DELETE FROM movie WHERE movieid = @id";
                            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                            {
                                cmd.Parameters.AddWithValue("@id", selectedID);
                                cmd.ExecuteNonQuery();

                                MessageBox.Show("Амжилттай устгагдлаа!");
                                {
                                    LoadData(); txtTitle.Clear();
                                }
                            }
                        }
                    }

                    catch (Exception ex) { MessageBox.Show("Устгахад алдаа гарлаа: " + ex.Message); }
                }
            }
            else
            {
                MessageBox.Show("Устгах мөрөө хүснэгтээс сонгоно уу!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            txtTitle.Clear();
            txtYear1.Clear();
            txtDirector.Clear();

        }

        private void btnUpdt_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Засах киногоо хүснэгтээс сонгоно уу!");
                return;
            }

            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();

                    string sql = "UPDATE movie SET title = @title, year1 = @year, Director = @dir WHERE movieid = @id";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@title", txtTitle.Text);
                        cmd.Parameters.AddWithValue("@year", int.Parse(txtYear1.Text));
                        cmd.Parameters.AddWithValue("@dir", txtDirector.Text);
                        cmd.Parameters.AddWithValue("@id", textBox1.Text);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Мэдээлэл амжилттай засагдлаа!");
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Засахад алдаа гарлаа. ID буруу байна.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Алдаа гарлаа: " + ex.ToString());
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
