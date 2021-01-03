using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlineBookStore
{
    public partial class ShopCart : Form
    {
        string username;
        Customer customer = new Customer();
        ShoppingCard shopping = new ShoppingCard();

        public ShopCart(string username1)
        {
            username = username1;
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Menu myform = new Menu(username);
            myform.Show();
            this.Hide();
        }

        private void ShoppingCart_Load(object sender, EventArgs e)
        {
          
            listwiewCart.View = View.Details;
            listwiewCart.GridLines = true;
            listwiewCart.FullRowSelect = true;

            Customer logged = new Customer();
            Database yeni = new Database();
            logged = yeni.GetCustomerByUsername(username);
            txtName.Text = logged.Name;
            txtAddress.Text = logged.Address;
            txtEmail.Text = logged.Email;

            double amount = 0.0;

            
            shopping.Customer = logged;
            shopping.ItemsToPurchase = OnlineBookStore.Menu.itemsOnCart;
            for (int i = 0; i < OnlineBookStore.Menu.itemsOnCart.Count; i++)
            {
                amount += OnlineBookStore.Menu.itemsOnCart[i].Product.price * OnlineBookStore.Menu.itemsOnCart[i].Quantity;
            }
            shopping.PaymentAmount = amount;
            //txtName.Text = shopping.PaymentAmount.ToString();

            for (int i = 0; i < OnlineBookStore.Menu.itemsOnCart.Count; i++)
            {
                string[] row = { OnlineBookStore.Menu.itemsOnCart[i].Product.name, OnlineBookStore.Menu.itemsOnCart[i].Product.price.ToString(), OnlineBookStore.Menu.itemsOnCart[i].Quantity.ToString()};
                var viewItem = new ListViewItem(row);
                listwiewCart.Items.Add(viewItem);
            }

            tbTotal.Text = amount.ToString();

            listwiewCart.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listwiewCart.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Thank You  Your Fee is:  " + tbTotal.Text + "$");

            Customer logged = new Customer();
            Database yeni = new Database();
            logged = yeni.GetCustomerByUsername(username);

            try
            {
                string gmailAddress = logged.Email;

                SmtpClient sc = new SmtpClient();
                sc.Port = 587;
                sc.Timeout = 50000;
                sc.Host = "smtp.gmail.com";
                sc.EnableSsl = true;
                sc.Credentials = new NetworkCredential("k.t.b.kitapdergicd@gmail.com", "KtbKitabeVi.9756");

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("k.t.b.kitapdergicd@gmail.com", "BookStore");
                mail.To.Add(gmailAddress);
                mail.Subject = "Shopping Cart Details";
                mail.IsBodyHtml = true;
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < OnlineBookStore.Menu.itemsOnCart.Count; i++)
                {
                    sb.AppendLine("Name: "+OnlineBookStore.Menu.itemsOnCart[i].Product.name + "Price: "+ OnlineBookStore.Menu.itemsOnCart[i].Product.price+" $"+" Quantitiy: "+ OnlineBookStore.Menu.itemsOnCart[i].Quantity);
                    sb.Append(Environment.NewLine);
                }
                sb.AppendLine(" \n Thank You " + txtName.Text + " Your Fee is:  " + tbTotal.Text + "$");
                sb.Append(Environment.NewLine);
                sb.AppendLine(" Purcashed at: " +DateTime.Now.ToString());
                mail.Body = sb.ToString();
                sc.Send(mail);
                MessageBox.Show("We will send your fee to your email.");

            }
            catch (Exception)
            {
                //not logged
            }
        }

        private void tpPayment_Click(object sender, EventArgs e)
        {
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (listwiewCart.SelectedIndices.Count > 0)
            {
                int selectedIndex = listwiewCart.SelectedIndices[0];
                OnlineBookStore.Menu.itemsOnCart.RemoveAt(selectedIndex);
                ShopCart myform = new ShopCart(username);
                myform.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please Select An Item To Remove");
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are u Really Want To Cancel Your Order ?", "Cancel Order", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                OnlineBookStore.Menu.itemsOnCart.Clear();
                ShopCart myform = new ShopCart(username);
                myform.Show();
                this.Hide();
            }         
        }
    }
}
