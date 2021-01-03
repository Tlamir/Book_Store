using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*\class ItemToPurchase 
* \brief Sepete eklenen ürünlerin sayısının ve hangi ürünler olduğunun tutulması. 
*
*/
namespace OnlineBookStore
{
    public class ItemToPurchase
    {
        private Product product;
        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        public Product Product
        {
            get { return product; }
            set { product = value; }
        }
    }
}
