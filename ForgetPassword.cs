using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

namespace OnlineBookStore
{
    public partial class ForgetPassword : Form
    {


        string registerkey;
        private object registerKey;
        Random random = new Random();


        public ForgetPassword()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Customer logged = new Customer();
            Database yeni = new Database();
            logged = yeni.GetCustomerByUsername(txtUsername.Text);

            if (logged != null)
            {
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
                    mail.Subject = "Profile Informations";
                    mail.IsBodyHtml = true;
                    mail.Body = "Your Password :" + logged.Password + "\nDo not share your informations with anyone";
                    sc.Send(mail);
                    MessageBox.Show("We will send your profile informations to your email address.");

                }
                catch (Exception)
                {
                    //not logged
                }
            }
            else
            {
                MessageBox.Show("Wrong user name. Please try again.");
            }
        }
    }
}
