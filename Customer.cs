using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore
{
    /*\class Customer
* \brief Kayıt olan kullanıcı ve Adminlerin bilgisi bu sınıfta tutulur 
*
*/
    class Customer
    {
        public Customer() { }
        private bool isAdmin;
        private int customerID;
        private string name;
        private string surname;
        private string address;
        private string email;
        private string username;
        private string password;
        private string passwordCon;
        static int customercount;
        private string time;
        private int no;

        public bool IsAdmin
        {
            get
            {
                return isAdmin;
            }
            set
            {
                isAdmin = value;
            }
        }
        public int CustomerID
        {
            get
            {
                return customerID;
            }

            set
            {
                customerID = value;
            }
        }
        public int No
        {
            get
            {
                return no;
            }

            set
            {
                no = value;
            }
        }
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
            }
        }
        public string Address
        {
            get
            {
                return address;
            }

            set
            {
                address = value;
            }
        }
        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }
        public string Username
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
            }
        }
        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }
        public string PasswordCon
        {
            get
            {
                return passwordCon;
            }
            set
            {
                passwordCon = value;
            }
        }
        public string Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
            }
        }
        
        public static int Customercount { get { return customercount; } set { customercount = value; } }

        public void saveCustomer(int customerID, string name, string surname, string address, string email, string username, string password)
        {
            this.customerID = customerID;
            this.name = name;
            this.surname = surname;
            this.address = address;
            this.email = email;
            this.username = username;
            this.password = password;
            this.isAdmin = false;
        }
        public void saveCurrentUser(int no, string username, string time)
        {
            this.no = no;
            this.username = username;
            this.time = time;
        }
        public void printCustomerDetails(Customer customer)
        {
        }
        public void printCustomerPurchases()
        {
        }
    }
}
