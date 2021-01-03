using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*\class ShoppingCard
* \brief Sepete eklerken ürünlerin list şeklinde eklenmesini sağlar 
*
*/

namespace OnlineBookStore
{
    class ShoppingCard
    {

        public List<ItemToPurchase> ItemsToPurchase;
        public Customer Customer;
        public double PaymentAmount;
        

        public ShoppingCard()
        {
            ItemsToPurchase = new List<ItemToPurchase>();
        }
    }
}
