using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*\class Product
* \brief Music CD, Book, Magazin sınıflarının genel bilgilerini içerir.
* Her ürünün bir idsi olması gerektiği gibi bu bilgiler bu sınıfta tutulur.
*
*/

namespace OnlineBookStore
{
    public abstract class Product
    {
        public string id;
        public string name;
        public string category;
        public double price;
        public string picture;

        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
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

        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }

        public string Picture
        {
            get
            {
                return picture;
            }
            set
            {
                picture = value;
            }
        }

        public string Category
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
            }
        }

        public Product(string id, string name, double price)
        {
            this.id = id;
            this.name = name;
            this.price = price;
        }

        public abstract void ShowDetails();
    }
}
