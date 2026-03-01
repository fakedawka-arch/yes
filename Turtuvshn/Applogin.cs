using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Xml.Linq;
namespace Turtuvshn
{
    public partial class Applogin : Form
    {
        public Applogin()
        {
            InitializeComponent();
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            // 1. Хоосон утга шалгах
            if (string.IsNullOrWhiteSpace(txtUserName.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Хэрэглэгчийн нэр эсвэл нууц үг хоосон байна!");
                return;
            }

            DataHelper.GetConnection();

            try
            {
                using (var conn = DataHelper.GetConnection())
                {
                    conn.Open();
                    string Sql = "SELECT * FROM reviewer WHERE rName = @name AND rPass = @password";

                    using (MySqlCommand cmd = new MySqlCommand(Sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", txtUserName.Text.Trim()); // Trim() нь илүүдэл зайг устгана
                        cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows) 
                            {
                                MessageBox.Show("Амжилттай нэвтэрлээ!");
                                
                                Mainform mainForm = new Mainform();
                                mainForm.ShowDialog();
                                this.Close(); // Нэвтрэх цонхыг хаах


                            }
                            else
                            {
                                MessageBox.Show("Нэр эсвэл нууц үг буруу байна.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Алдаа гарлаа: " + ex.Message);
            }
        }

        private void Applogin_Load(object sender, EventArgs e)
        {

        }
    }
}