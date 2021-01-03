using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace OnlineBookStore
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        Customer customer;
        public static string timestr;
        public static int id1;
        private static Database databaseObject = Database.database();

        internal static Database DatabaseObject
        {
            get
            {
                return databaseObject;
            }
            set
            {
                databaseObject = value;
            }
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            Form yeni = new SignUp();
            yeni.Show();
            this.Hide();
        }

        private void Login_Load(object sender, EventArgs e)
        {
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString();
            timestr = lblTime.Text;
            
            if (Database.password_control(txtUsername.Text,txtPassword.Text)==true)
            {
                //log kaydı oluşturmak için
                customer = new Customer();
                id1 = DatabaseObject.CountNo() + 1;
                customer.saveCurrentUser(id1, txtUsername.Text, timestr);
                DatabaseObject.newCurrentUser(customer);

                //MessageBox.Show("Welcome "+txtUsername.Text);
                Menu myform = new Menu(customer.Username);
                myform.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid User Name Or Password");
            }
        }

        private void bthHideCharacter_Click(object sender, EventArgs e)
        {
            if (txtPassword.UseSystemPasswordChar==true)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form myform = new ForgetPassword();
            myform.Show();
        }
    }
}
