using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
/*
 * Sadece Admin yetkisi olan kişilerin erişimine izin vardır.
 */
namespace OnlineBookStore
{
    public partial class AdminInterface : Form
    {
        DataSet dataset;
        Bitmap image;
        int index = 0;
        string username;
        //static string ImagePath;
        //Book book;
        public static int idbook;
        public static int idmusic;
        public static int idmagazine;
        string idbooktxt;
        string idmusictxt;
        string idmagazinetxt;
        public static int id;
        static TextBox[] bookTextArray;
        private static Database database = new Database();
        public AdminInterface(string username1)
        {
            dataset = new DataSet();
            username = username1;
            InitializeComponent();
            bookTextArray = new TextBox[] { txtBookName, txtBookPrice, txtBookISBN, txtBookAuthor, txtBookPublisher, txtBookPage, txtBookTopic };
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Menu myform = new Menu(username);
            myform.Show();
            this.Hide();
        }

        private void AdminInterface_Load(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(Database.ConnectionString))
            {
                //Fill DataGridView Customer
                sqlCon.Open();
                SqlDataAdapter sqlData = new SqlDataAdapter("SELECT * FROM dbo.Customer", sqlCon);
                DataTable dtbCustomer = new DataTable();
                sqlData.Fill(dtbCustomer);
                dgvUsers.DataSource = dtbCustomer;
                //Fill DataGridView Books
                SqlDataAdapter sqlData2 = new SqlDataAdapter("SELECT * FROM dbo.Book1", sqlCon);
                DataTable dtbBooks = new DataTable();
                sqlData2.Fill(dtbBooks);
                dgvBooks.DataSource = dtbBooks;
                 //Fill DataGridView Music
                 SqlDataAdapter sqlData3 = new SqlDataAdapter("SELECT * FROM dbo.musicCD2", sqlCon);
                 DataTable dtbMusic = new DataTable();
                 sqlData3.Fill(dtbMusic);
                 dgvMusicCDs.DataSource = dtbMusic;
                 //Fill DataGridView Magazine
                 SqlDataAdapter sqlData4 = new SqlDataAdapter("SELECT * FROM dbo.magazine3", sqlCon);
                 DataTable dtbmagazine = new DataTable();
                 sqlData4.Fill(dtbmagazine);
                 dgvMagazines.DataSource = dtbmagazine;
            }
        }
        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;

            DataGridViewRow row = dgvUsers.Rows[index];
            DataGridViewCellCollection cells = row.Cells;
            txtCustomerUsername.Text = cells[1].Value.ToString().Trim();
            txtCustomerPassword.Text = cells[2].Value.ToString().Trim();
            txtCustomerName.Text = cells[3].Value.ToString().Trim();
            txtCustomerSurname.Text = cells[4].Value.ToString().Trim();
            txtCustomerEmail.Text = cells[5].Value.ToString().Trim();
            txtCustomerAddress.Text = cells[6].Value.ToString().Trim();
            if (Convert.ToInt32(cells[7].Value) == 1)
                cbIsAdmin.Checked = true;
            else
                cbIsAdmin.Checked = false;
        }
        private void btnUserUpdate_Click(object sender, EventArgs e)
        {
            
            Customer tempCustomer = new Customer();
            tempCustomer.CustomerID = Convert.ToInt32(dgvUsers.CurrentRow.Cells[0].Value.ToString().Trim());
            tempCustomer.Name = txtCustomerName.Text;
            tempCustomer.Surname = txtCustomerSurname.Text;
            tempCustomer.Address = txtCustomerAddress.Text;
            tempCustomer.Email = txtCustomerEmail.Text;
            tempCustomer.Username = txtCustomerUsername.Text;
            tempCustomer.Password = txtCustomerPassword.Text;
            if (cbIsAdmin.Checked == true)
            {
                tempCustomer.IsAdmin = true;
            }
            else
            {
                tempCustomer.IsAdmin = false;
            }
            Login.DatabaseObject.Update(tempCustomer);

            AdminInterface myform = new AdminInterface(username); //Fast Solution
            myform.Show();
            this.Hide();
            
        }

        private void btnUserDelete_Click(object sender, EventArgs e)
        {
            /*
            delete(btnUserDelete);
            */
        }

        private void delete(object sender)
        {
            /*
            string table = "";
            string Id;
            var btn = sender as Button;
            if (btn.Name == "btnUserDelete")
            {
                Customer cstmr = new Customer();
                table = "[Customer]";
                DataGridViewRow row = dgvUsers.Rows[index];

                Id = row.Cells[0].Value.ToString();
                if (Id == cstmr.CustomerID.ToString())
                {
                    MessageBox.Show("You Cannot Delete Yourself!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                table = "[Book]";
                DataGridViewRow row = dgvBooks.Rows[index];
                Id = row.Cells[0].Value.ToString();
            }
            Login.DatabaseObject.Delete(table, Id);
            AdminInterface myform = new AdminInterface(username); //Fast Solution
            myform.Show();
            this.Hide();
            */
        }

        private void btnBookUpdate_Click(object sender, EventArgs e)
        {
            
            string[] values = {dgvBooks.CurrentRow.Cells[0].Value.ToString().Trim(), txtBookName.Text.Trim(),txtBookPrice.Text.Trim(), txtBookISBN.Text.Trim(),
                txtBookAuthor.Text.Trim(), txtBookPublisher.Text.Trim(),txtBookPage.Text.Trim(), txtBookTopic.Text.Trim() };
            Login.DatabaseObject.BookUpdate(values);
            dataset.Clear();
            //Login.DatabaseObject.Refresh(dataset);
            AdminInterface myform = new AdminInterface(username); //Fast Solution
            myform.Show();
            this.Hide();

        }

        private void btnBookAdd_Click(object sender, EventArgs e)
        {
            String basepic = "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";
            idbook = Login.DatabaseObject.FindCounterforBook() + 1;
            idbooktxt = idbook.ToString();
            string[] values = {idbooktxt, txtBookName.Text.Trim(),txtBookPrice.Text.Trim(), txtBookISBN.Text.Trim(), //Burada 123 id unoiqe olmasi lazim dgv den alsak olabilir
                txtBookAuthor.Text.Trim(), txtBookPublisher.Text.Trim(),txtBookPage.Text.Trim(), txtBookTopic.Text.Trim(),basepic};
            Login.DatabaseObject.AddBook(values);
            dataset.Clear();
            //Login.DatabaseObject.Refresh(dataset);
            AdminInterface myform = new AdminInterface(username); //Fast Solution
            myform.Show();
            this.Hide();
        }

        private void dgvBooks_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                index = e.RowIndex;
                btnBookUpdate.Enabled = true;
                btnBookDelete.Enabled = true;
                // btnBookImageChange.Enabled = true;
                DataGridViewRow row = dgvBooks.Rows[index];
                DataGridViewCellCollection cells = row.Cells;
                txtBookName.Text = cells[1].Value.ToString().Trim();
                txtBookPrice.Text = cells[2].Value.ToString().Trim();
                txtBookISBN.Text = cells[3].Value.ToString().Trim();
                txtBookAuthor.Text = cells[4].Value.ToString().Trim();
                txtBookPublisher.Text = cells[5].Value.ToString().Trim();
                txtBookPage.Text = cells[6].Value.ToString().Trim();
                txtBookTopic.Text = cells[7].Value.ToString().Trim();

                byte[] imageBytes = Convert.FromBase64String(cells[8].Value.ToString());
                // Convert byte[] to Image
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    pbBook.Image = Image.FromStream(ms, true);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
        }

        private void btnBookPhoto_Click(object sender, EventArgs e)
        {
            string base64Text;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG" +
            "|All files(*.*)|*.*";
            dialog.CheckFileExists = true;
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image = new Bitmap(dialog.FileName);
                pbBook.Image = (Image)image;

                byte[] imageArray = System.IO.File.ReadAllBytes(dialog.FileName);
                base64Text = Convert.ToBase64String(imageArray);

                /*string[] values = {base64Text};
                Login.DatabaseObject.Bookpicupdate(values);
                dataset.Clear();
                //Login.DatabaseObject.Refresh(dataset);
                AdminInterface myform = new AdminInterface(username); //Fast Solution
                myform.Show();
                this.Hide();*/

                var sql = "UPDATE dbo.Book1 SET Picture=@Picture where Isbn=@Isbn ";
                try
                {
                    using (var connection = new SqlConnection(Database.ConnectionString))
                    {
                        using (var command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.Add("@Picture", SqlDbType.NVarChar).Value = base64Text;
                            command.Parameters.Add("@Isbn", SqlDbType.NVarChar).Value = txtBookISBN.Text;

                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception )
                {
                    MessageBox.Show("Error!!");
                }
            }
        }

        private void btnMagazineUpdate_Click(object sender, EventArgs e)
        {
            string[] values = {dgvMagazines.CurrentRow.Cells[0].Value.ToString().Trim(), tbMagazineName.Text.Trim(),tbMagazinePrice.Text.Trim(), tbMagazineType.Text.Trim(),tbMagazineIssue.Text.Trim()};
            Login.DatabaseObject.MagazineUpdate(values);
            dataset.Clear();
            //Login.DatabaseObject.Refresh(dataset);
            AdminInterface myform = new AdminInterface(username); //Fast Solution
            myform.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] values = { dgvMusicCDs.CurrentRow.Cells[0].Value.ToString().Trim(),tbMusicCDName.Text.Trim(),tbMusicCDSinger.Text.Trim(), tbMusicCDsType.Text.Trim(), tbMusicCDPrice.Text.Trim() };
            Login.DatabaseObject.MusicCDUpdate(values);
            dataset.Clear();
            //Login.DatabaseObject.Refresh(dataset);
            AdminInterface myform = new AdminInterface(username); //Fast Solution
            myform.Show();
            this.Hide();
        }

        private void btnMusicAdd_Click(object sender, EventArgs e)
        {
            String basepic = "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";
            idmusic = Login.DatabaseObject.FindCounterforMusic() + 1;
            idmusictxt = idmusic.ToString();
            string[] values = {idmusictxt,tbMusicCDName.Text.Trim(), tbMusicCDSinger.Text.Trim(), tbMusicCDsType.Text.Trim(), tbMusicCDPrice.Text.Trim(),basepic};
            Login.DatabaseObject.addMusic(values);
            dataset.Clear();
            //Login.DatabaseObject.Refresh(dataset);
            AdminInterface myform = new AdminInterface(username); //Fast Solution
            myform.Show();
            this.Hide();
        }

        private void dgvMusicCDs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                index = e.RowIndex;
                DataGridViewRow row = dgvMusicCDs.Rows[index];
                DataGridViewCellCollection cells = row.Cells;
                tbMusicCDName.Text = cells[1].Value.ToString().Trim();
                tbMusicCDSinger.Text = cells[2].Value.ToString().Trim();
                tbMusicCDsType.Text = cells[3].Value.ToString().Trim();
                tbMusicCDPrice.Text = cells[4].Value.ToString().Trim();

                byte[] imageBytes = Convert.FromBase64String(cells[5].Value.ToString());
                // Convert byte[] to Image
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    pbMucisCDs.Image = Image.FromStream(ms, true);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
        }

        private void btnUpdateMusicPic_Click(object sender, EventArgs e)
        {
            string base64Text;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG" +
            "|All files(*.*)|*.*";
            dialog.CheckFileExists = true;
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image = new Bitmap(dialog.FileName);
                pbMucisCDs.Image = (Image)image;

                byte[] imageArray = System.IO.File.ReadAllBytes(dialog.FileName);
                base64Text = Convert.ToBase64String(imageArray);

         

                var sql = "UPDATE dbo.musicCD2 SET picture=@picture where singer=@singer ";
                try
                {
                    using (var connection = new SqlConnection(Database.ConnectionString))
                    {
                        using (var command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.Add("@picture", SqlDbType.NVarChar).Value = base64Text;
                            command.Parameters.Add("@singer", SqlDbType.NVarChar).Value = tbMusicCDSinger.Text;

                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error!!");
                }
            }
        }

        private void btnMagazineAdd_Click(object sender, EventArgs e)
        {
            String basepic = "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";
            idmagazine = Login.DatabaseObject.FindCounterforMagazine() + 1;
            idmagazinetxt = idmagazine.ToString();
            string[] values = { idmagazinetxt, tbMagazineName.Text.Trim(), tbMagazineIssue.Text.Trim(), tbMagazineType.Text.Trim(), tbMagazineType.Text.Trim(), basepic };
            Login.DatabaseObject.addMagazine(values);
            dataset.Clear();
            //Login.DatabaseObject.Refresh(dataset);
            AdminInterface myform = new AdminInterface(username); //Fast Solution
            myform.Show();
            this.Hide();
        }

        private void dgvMagazines_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                index = e.RowIndex;
                DataGridViewRow row = dgvMagazines.Rows[index];
                DataGridViewCellCollection cells = row.Cells;
                tbMagazineName.Text = cells[1].Value.ToString().Trim();
                tbMagazinePrice.Text = cells[2].Value.ToString().Trim();
                tbMagazineType.Text = cells[3].Value.ToString().Trim();
                tbMagazineIssue.Text = cells[4].Value.ToString().Trim();

                byte[] imageBytes = Convert.FromBase64String(cells[5].Value.ToString());
                // Convert byte[] to Image
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    pbMagazines.Image = Image.FromStream(ms, true);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
        }

        private void btnMagazinePhoto_Click(object sender, EventArgs e)
        {

            string base64Text;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG" +
            "|All files(*.*)|*.*";
            dialog.CheckFileExists = true;
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image = new Bitmap(dialog.FileName);
                pbMagazines.Image = (Image)image;

                byte[] imageArray = System.IO.File.ReadAllBytes(dialog.FileName);
                base64Text = Convert.ToBase64String(imageArray);

                var sql = "UPDATE magazine3 SET picture=@picture where name=@name ";
                try
                {
                    using (var connection = new SqlConnection(Database.ConnectionString))
                    {
                        using (var command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.Add("@picture", SqlDbType.NVarChar).Value = base64Text;
                            command.Parameters.Add("@name", SqlDbType.NVarChar).Value = tbMagazineName.Text;

                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error!!");
                }
            }
        }

        private void btnBookDelete_Click(object sender, EventArgs e)
        {
            database.Delete("Book1", txtBookName.Text);
            AdminInterface myform = new AdminInterface(username); //Fast Solution
            myform.Show();
            this.Hide();
        }

        private void btnMagazineDelete_Click(object sender, EventArgs e)
        {
            database.Delete("magazine3", tbMagazineName.Text);
            AdminInterface myform = new AdminInterface(username); //Fast Solution
            myform.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            database.Delete("musicCD2", tbMusicCDName.Text);
            AdminInterface myform = new AdminInterface(username); //Fast Solution
            myform.Show();
            this.Hide();
        }

        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgvBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
