using StoreFront.Models;
using StoreFront.DATA.EF.Models;//added for access to product

namespace StoreFront.Models
{
    public class CartItemViewModel
    {
        // Shopping Cart - Step 2
        // right clicked Models -> Add Class to create this file
        public int Qty { get; set; }
        public Product CartProd { get; set; }// Containment - using a complex data type as a prop/field in a class
        //complex data types are classes that can hold multiple values
        // vs primitive data types, which hold a single value

        public CartItemViewModel() { }

        public CartItemViewModel(int qty, Product product)
        {
            Qty = qty;
            CartProd = product;
        }
    }
}

