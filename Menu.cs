using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlineBookStore
{
    public partial class Menu : Form
    {
      // static List<Product> itemsOnCart = new List<Product>();
        static public List<ItemToPurchase> itemsOnCart = new List<ItemToPurchase>();
        

        string username;
        int index = 0;
        public Menu(string username1)
        {
            username = username1;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do You Really Want To Exit ?", "BookStore", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Login myForm = new Login();
                this.Hide();
                myForm.ShowDialog();
                this.Close();
            }
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            PersonalInfo myform = new PersonalInfo(username);
            myform.Show();
            this.Hide();
        }

        private void btnCart_Click(object sender, EventArgs e)
        {
            ShopCart myform = new ShopCart(username);
            myform.Show();
            this.Hide();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            Customer logged = new Customer();
            Database yeni = new Database();
            logged = yeni.GetCustomerByUsername(username);

            if (logged.IsAdmin==true)
            {
                AdminInterface myform = new AdminInterface(username);
                myform.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Sorry You Are Not Admin");
            }
            
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(Database.ConnectionString))
            {             
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

        private void dgvBooks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                index = e.RowIndex;
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

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (tbMagazineName.Text!=""||tbMusicCDName.Text!=""||txtBookName.Text!="")
            {

                ItemToPurchase Item1 = new ItemToPurchase();
                Book book = new Book("0",txtBookName.Text, Convert.ToDouble(txtBookPrice.Text));
                Item1.Product = book;
                Item1.Quantity = int.Parse(txtQuantitiybook.Text);
                itemsOnCart.Add(Item1);
                MessageBox.Show("item is added to your shopping cart");              
            }
            else
            {
                MessageBox.Show("Please Select Product To add Shoppingcart");
            }
           
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

        private void lbBooksTopic_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnAddtocartmagazine_Click(object sender, EventArgs e)
        {
            if (tbMagazineName.Text != "" || tbMusicCDName.Text != "" || txtBookName.Text != "")
            {
                ItemToPurchase Item2 = new ItemToPurchase();
                Magazine magazine = new Magazine("0", tbMagazineName.Text, Convert.ToDouble(tbMagazinePrice.Text));
                Item2.Product = magazine;
                Item2.Quantity = int.Parse(tbquantiymagazine.Text);
                itemsOnCart.Add(Item2);
                MessageBox.Show("item is added to your shopping cart");
            }
            else
            {
                MessageBox.Show("Please Select Product To add Shoppingcart");
            }
        }
        private void btnaddtocartmusic_Click(object sender, EventArgs e)
        {
            if (tbMagazineName.Text != "" || tbMusicCDName.Text != "" || txtBookName.Text != "")
            {
                ItemToPurchase Item3 = new ItemToPurchase();
                MusicCD music = new MusicCD("0", tbMusicCDName.Text, Convert.ToDouble(tbMusicCDPrice.Text));
                Item3.Product = music;
                Item3.Quantity = int.Parse(tbquantymusic.Text);
                itemsOnCart.Add(Item3);
                MessageBox.Show("item is added to your shopping cart");
            }
            else
            {
                MessageBox.Show("Please Select Product To add Shoppingcart");
            }
        }

        private void dgvBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
