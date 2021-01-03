using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlineBookStore
{
    public partial class PersonalInfo : Form
    {
        string username;
        Customer customer = new Customer();

        Database data = Database.database();
        public PersonalInfo(string username1)
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

        private void bthHideCharacter_Click(object sender, EventArgs e)
        {        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtPasswordCon.UseSystemPasswordChar == true)
            {
                txtPasswordCon.UseSystemPasswordChar = false;
            }
            else
            {
                txtPasswordCon.UseSystemPasswordChar = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtPassword.UseSystemPasswordChar == true)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            lblinfo.Text = null;
            lblinfo.Visible = true;
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtSurname.Text) || string.IsNullOrWhiteSpace(txtAddress.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblinfo.Text = "Please fill in all the fields.";
            }
            else if (txtPassword.Text != txtPasswordCon.Text)
            {
                lblinfo.Text = "Passwords are not match.";
            }
            else
            {
                Customer logged = new Customer();
                Database yeni = new Database();
                logged = yeni.GetCustomerByUsername(username);
                logged.Name = txtName.Text;
                logged.Username = txtUsername.Text.Trim();
                logged.Surname = txtSurname.Text.Trim();
                logged.Address = txtAddress.Text.Trim();
                logged.Email = txtEmail.Text.Trim();
                logged.Password = txtPassword.Text.Trim();
                Login.DatabaseObject.Update(logged);
                lblinfo.Text = "Your registration has been successfully updated.";
            }
        }

        private void PersonalInfo_Load(object sender, EventArgs e)
        {
            Customer logged = new Customer();
            Database yeni = new Database();
            logged = yeni.GetCustomerByUsername(username);
            txtName.Text = logged.Name;
            txtSurname.Text = logged.Surname;
            txtAddress.Text = logged.Address;
            txtEmail.Text = logged.Email;
            txtUsername.Text = logged.Username;
            txtPassword.Text = logged.Password;
        }
    }
}
