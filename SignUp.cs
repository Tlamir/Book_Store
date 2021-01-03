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
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }
        public static int id;
        Customer customer;
        private static Database database = new Database();

        private void SignUp_Load(object sender, EventArgs e)
        {
            
              btnBack.FlatAppearance.MouseOverBackColor = btnBack.BackColor;
              lbl_info.Visible = false;
        }

        /*
         * Gerekli bilgileri sağladı takdirde kullanıcı kayıt olabilir ve database'e eklenir.
         */
        private void btn_Log_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtSurname.Text) || string.IsNullOrWhiteSpace(txtAddress.Text) ||string.IsNullOrWhiteSpace(txtPasswordCon.Text)|| string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lbl_info.Visible = true;
                lbl_info.Text = "Please fill all the fields!";
            }
            else if (!IsValidEmail(txtEmail.Text))
            {
                lbl_info.Visible = true;
                lbl_info.Text = "Invalid email address!";
            }
            else if (txtPassword.Text != txtPasswordCon.Text)
            {
                lbl_info.Visible = true;
                lbl_info.Text = "Passwords did not match!";
            }
            else
            {
                customer = new Customer();
                id = Login.DatabaseObject.FindCounter() + 1; //Sıralı bir şekilde id atamak için kullanılır.
                customer.saveCustomer(id, txtName.Text, txtSurname.Text, txtAddress.Text, txtEmail.Text, txtUsername.Text, txtPassword.Text);
                Login.DatabaseObject.newsign(customer);
                MessageBox.Show("You Have Succsesfully Signed Up");

                //REturn to login
                Login myForm = new Login();
                this.Hide();
                myForm.ShowDialog();
                this.Close();
            }
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var address = new System.Net.Mail.MailAddress(email);
                return address.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Login myForm = new Login();
            this.Hide();
            myForm.ShowDialog();
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void SignUp_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
