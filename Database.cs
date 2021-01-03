using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace OnlineBookStore
{
    class Database
    {
        private SqlConnection sqlConnection;
        private SqlCommand command;
        public int counter = 0;
        private static Database dataBase;
        private SqlDataReader sqlDataReader;
        private static string connectionString = " Data Source=den1.mssql8.gear.host;Initial Catalog = nesnedb; User Id=nesnedb; Password=Admin.";
        public int counter1 = 0;

        public static string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        public static Database database()
        {
            if (dataBase == null)
            {
                dataBase = new Database();
            }
            return dataBase;
        }
        public static bool password_control(string user, string password)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            connection.Open();
            SqlCommand command = new SqlCommand("select * from Customer where username ='" + user + "'and password ='" + password + "'", connection);

            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                connection.Close();
                return true;
            }
            else
            {
                connection.Close();
                return false;
            }
        }
        public int FindCounter()
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                command = new SqlCommand("select count(*) from Customer", sqlConnection);
                sqlConnection.Open();
                counter = Convert.ToInt32(command.ExecuteScalar());
                sqlConnection.Close();
            }

            return counter;
        }
        public int CountNo()
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                command = new SqlCommand("select count(*) from ActiveUser1", sqlConnection);
                sqlConnection.Open();
                counter1 = Convert.ToInt32(command.ExecuteScalar());
                sqlConnection.Close();
            }

            return counter1;
        }

        public bool newsign(Customer customer)
        {
            SignUp.id++;
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("select * from Customer where username ='" + customer.Username + "'", connection);
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                command.ExecuteNonQuery();
                connection.Close();
                return false;
            }
            else
            {
                counter++;
                dataReader.Close();
                string sorgu = "Insert into Customer (username,password,id,name,surname,email,address,isadmin) values (@username,@password,@id,@name,@surname,@email,@address,@isadmin)";
                SqlCommand komut = new SqlCommand(sorgu, connection);
                komut.Parameters.AddWithValue("@username", customer.Username);
                komut.Parameters.AddWithValue("@password", customer.Password);
                komut.Parameters.AddWithValue("@id", customer.CustomerID);
                komut.Parameters.AddWithValue("@name", customer.Name);
                komut.Parameters.AddWithValue("@surname", customer.Surname);
                komut.Parameters.AddWithValue("@email", customer.Email);
                komut.Parameters.AddWithValue("@address", customer.Address);
                komut.Parameters.AddWithValue("@isadmin", customer.IsAdmin);
                komut.ExecuteNonQuery();
                connection.Close();
                return true;
            }
        }
        public bool newCurrentUser(Customer customer)
        {
            Login.id1++;
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("select * from ActiveUser1 where username ='" + customer.Username + "'", connection);
            SqlDataReader dataReader = command.ExecuteReader();
            if (connection.State == ConnectionState.Closed)
            {
                command.ExecuteNonQuery();
                connection.Close();
                return false;
            }
            else
            {
                counter1++;
                dataReader.Close();

                string sorgu = "Insert into ActiveUser1 (no,username,time) values (@no,@username,@time)";
                SqlCommand komut = new SqlCommand(sorgu, connection);
                komut.Parameters.AddWithValue("@no", customer.No);
                komut.Parameters.AddWithValue("@username", customer.Username);
                komut.Parameters.AddWithValue("@time", customer.Time);
                komut.ExecuteNonQuery();
                connection.Close();
                return true;
            }
        }

        public void Update(Customer customer)
        {
            string updateString = "UPDATE dbo.Customer SET name=@name,surname=@Surname,address=@Address,email=@Email,password=@Password,isadmin=@IsAdmin WHERE username=@Username";
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            command = new SqlCommand(updateString, sqlConnection);
            command.Parameters.AddWithValue("Name", customer.Name.Trim());
            command.Parameters.AddWithValue("Surname", customer.Surname.Trim());
            command.Parameters.AddWithValue("Address", customer.Address.Trim());
            command.Parameters.AddWithValue("Email", customer.Email.Trim());
            command.Parameters.AddWithValue("Username", customer.Username.Trim());
            command.Parameters.AddWithValue("Password", customer.Password.Trim());
            if (customer.IsAdmin == true)
            {
                command.Parameters.AddWithValue("IsAdmin", 1);
            }
            else
            {
                command.Parameters.AddWithValue("IsAdmin", 0);
            }
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }
        public void Delete(string table, string name)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            string path = "DELETE FROM " + table + " WHERE name = '" + name + "'";
            command = new SqlCommand(path, sqlConnection);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }
        public Customer GetCustomerByUsername(string username)
        {
            Customer customer = null;
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("select * from Customer where username ='" + username + "'", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (Convert.ToBoolean(reader["isadmin"]))
                {
                    customer = new Customer();
                    customer.Name = reader["name"].ToString().Trim();
                    customer.Surname = reader["surname"].ToString().Trim();
                    customer.Address = reader["address"].ToString().Trim();
                    customer.Email = reader["email"].ToString().Trim();
                    customer.Username = reader["username"].ToString().Trim();
                    customer.Password = reader["password"].ToString().Trim();
                    customer.CustomerID = Convert.ToInt32(reader["id"]);
                    customer.IsAdmin = true;
                }

                else if (username == reader["username"].ToString().Trim())
                {
                    customer = new Customer();
                    customer.Name = reader["name"].ToString().Trim();
                    customer.Surname = reader["surname"].ToString().Trim();
                    customer.Address = reader["address"].ToString().Trim();
                    customer.Email = reader["email"].ToString().Trim();
                    customer.Username = reader["username"].ToString().Trim();
                    customer.Password = reader["password"].ToString().Trim();
                    customer.CustomerID = Convert.ToInt32(reader["id"]);
                    customer.IsAdmin = false;
                }
            }
            connection.Close();
            return customer;
        }
        public void AddBook(string[] values)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            using (command = new SqlCommand("INSERT INTO [dbo].[Book1] ([Id],[Name],[Price],[Isbn],[Author],[Publisher] ,[Page],[Topic],[Picture]) VALUES (@Id, @Name, @Price, @Isbn, @Author, @Publisher, @Page,@Topic,@Picture)", sqlConnection))
            {
                command.Parameters.AddWithValue("Id", values[0].Trim());
                command.Parameters.AddWithValue("Name", values[1].Trim());
                command.Parameters.AddWithValue("Price", Convert.ToDouble(values[2]));
                command.Parameters.AddWithValue("Isbn", values[3].Trim());
                command.Parameters.AddWithValue("Author", values[4].Trim());
                command.Parameters.AddWithValue("Publisher", values[5].Trim());
                command.Parameters.AddWithValue("Page", values[6].Trim());
                command.Parameters.AddWithValue("Topic", values[7].Trim());
                command.Parameters.AddWithValue("Picture", values[8]);
                command.ExecuteNonQuery();
            }
            sqlConnection.Close();
        }

        public List<Product> BookLoader()
        {
            List<Product> booklist = new List<Product>();
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            command = new SqlCommand("SELECT * FROM [dbo].[Book1]", sqlConnection);
            sqlDataReader = command.ExecuteReader();
            Book book;
            while (sqlDataReader.Read())
            {
                book = new Book("", "", 0);
                book.ID = (string)sqlDataReader["Id"];
                book.Name = (string)sqlDataReader["Name"];
                book.Price = (double)sqlDataReader["Price"];
                book.Isbn = (string)sqlDataReader["Isbn"];
                book.Author = (string)sqlDataReader["Author"];
                book.Publisher = (string)sqlDataReader["Publisher"];
                book.Page = (string)sqlDataReader["Page"];
                book.Content = sqlDataReader["Topic"].ToString().Trim();
                book.Image = sqlDataReader["Image"].ToString().Trim();
                booklist.Add(book);
            }
            sqlConnection.Close();
            return booklist;
        }
        public void BookUpdate(string[] values)
        {
            string path = "UPDATE Book1 SET Name=@1,Price=@2,Isbn=@3,Author=@4,Publisher=@5,Page=@6,Topic=@7 where Id=@0";
            Update(values, path);
        }

        public void MusicCDUpdate(string[] values)
        {
            string path = "UPDATE musicCD2 SET name=@1,singer=@2,type=@3,price=@4 where Id=@0";
            Update(values, path);


        }

        public void Bookpicupdate(string[] values)
        {
            string path = "UPDATE Book1 SET Picture=@1 where Id=@0";
            Update(values, path);
        }

        public void Update(string[] values, string path)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            command = new SqlCommand(path, sqlConnection);
            for (int i = 0; i < values.Length; i++)
            {
                if (i == 2)
                {
                    command.Parameters.AddWithValue("" + i, Convert.ToDouble(values[i]));
                }
                else
                {
                    command.Parameters.AddWithValue("" + i, values[i]);
                }

            }
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }
        public void addMusic(string[] values)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            using (command = new SqlCommand("INSERT INTO [dbo].[musicCD2] ([id],[name],[singer],[type],[price],[picture]) VALUES (@id,@name,@singer,@type,@price,@picture)", sqlConnection))
            {
                command.Parameters.AddWithValue("id", values[0].Trim());
                command.Parameters.AddWithValue("name", values[1].Trim());
                command.Parameters.AddWithValue("singer", values[2].Trim());
                command.Parameters.AddWithValue("type", values[3].Trim());
                command.Parameters.AddWithValue("price", values[4].Trim());
                command.Parameters.AddWithValue("picture", values[5].Trim());
                command.ExecuteNonQuery();
            }
            sqlConnection.Close();
        }
        public void MagazineUpdate(string[] values)
        {
            string path = "UPDATE magazine3 SET name=@1,price=@2,type=@3,issue=@4 where int=@0";           
            Update(values, path);
        }
        public void addMagazine(string[] values)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            using (command = new SqlCommand("INSERT INTO [dbo].[magazine3] ([int],[name],[price],[type],[issue],[picture]) VALUES (@int,@name,@price,@type,@issue,@picture)", sqlConnection))
            {
                command.Parameters.AddWithValue("int", values[0].Trim());
                command.Parameters.AddWithValue("name", values[1].Trim());
                command.Parameters.AddWithValue("price", values[2].Trim());
                command.Parameters.AddWithValue("type", values[3].Trim());
                command.Parameters.AddWithValue("issue", values[4].Trim());
                command.Parameters.AddWithValue("picture", values[5].Trim());
                command.ExecuteNonQuery();
            }
            sqlConnection.Close();
        }
        public int FindCounterforBook()
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                command = new SqlCommand("select count(*) from Book1", sqlConnection);
                sqlConnection.Open();
                counter = Convert.ToInt32(command.ExecuteScalar());
                sqlConnection.Close();
            }
            return counter;
        }
        public int FindCounterforMusic()
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                command = new SqlCommand("select count(*) from musicCD2", sqlConnection);
                sqlConnection.Open();
                counter = Convert.ToInt32(command.ExecuteScalar());
                sqlConnection.Close();
            }
            return counter;
        }
        public int FindCounterforMagazine()
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                command = new SqlCommand("select count(*) from magazine3", sqlConnection);
                sqlConnection.Open();
                counter = Convert.ToInt32(command.ExecuteScalar());
                sqlConnection.Close();
            }
            return counter;
        }
    }
}