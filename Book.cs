using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*\class Book
* \brief Product sınıfıntan türetilmiştir book için gerekli bilgileri tutar.
*
*/
namespace OnlineBookStore
{
    public class Book : Product
    {
        private string page;
        private string isbn;
        private string author;
        private string publisher;
        private string content;
        private string topic;
        private string image;


        public string Topic
        {
            get
            {
                return topic;
            }

            set
            {
                topic = value;
            }
        }

        public string Isbn
        {
            get
            {
                return isbn;
            }

            set
            {
                isbn = value;
            }
        }
        public string Author
        {
            get
            {
                return author;
            }

            set
            {
                author = value;
            }
        }
        public string Publisher
        {
            get
            {
                return publisher;
            }

            set
            {
                publisher = value;
            }
        }
        public string Page
        {
            get
            {
                return page;
            }

            set
            {
                page = value;
            }
        }
        public string Content
        {
            get
            {
                return content;
            }

            set
            {
                content = value;
            }
        }
        public string Image
        {
            get
            {
                return image;
            }

            set
            {
                content = image;
            }
        }

        public Book(string id, string name, double price) : base(id, name, price) { }

        public void saveBook(string id, string name, string publisher, string topic, string page, double price, string isbn, string image)
        {
            this.id = id;
            this.name = name;
            this.publisher = publisher;
            this.topic = topic;
            this.page = page;
            this.price = price;
            this.isbn = isbn;
            this.image = image;
        }

        public override void ShowDetails()
        {
            //BookForm bookForm = new BookForm(this);
            //bookForm.ShowDialog();
            //bookForm.Dispose();
        }
    }
}
