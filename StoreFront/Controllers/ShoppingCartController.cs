using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreFront.DATA.EF.Models;
using StoreFront.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StoreFront.Controllers
{
    public class ShoppingCartController : Controller
    {
        //Properties
        private readonly StoreFrontContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        //Constructor
        public ShoppingCartController(StoreFrontContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            //Retrieve the contents from the Session shopping cart (stored as JSON) and convert them to C# using Newtonsoft.Json.
            //After converting to C#, we can pass the collection back to a strongly-typed view

            // retrieve the cart
            var sessionCart = HttpContext.Session.GetString("cart");

            // create the shell for the local (C#) shopping cart
            Dictionary<int, CartItemViewModel> shoppingCart = null;

            //if the session cart is null, or if there are 0 items in the session cart, return a message to notify the user that cart is empty
            if (sessionCart == null || sessionCart.Count() == 0)
            {
                ViewBag.Message = "There are no items in your cart.";

                shoppingCart = new Dictionary<int, CartItemViewModel>();
            }
            else
            {
                ViewBag.Message = null;

                shoppingCart = JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(sessionCart);
            }

            // no matter what, return the collection to the View
            return View(shoppingCart);
        }


        public IActionResult AddToCart(int id)
        {
            // Get the cart ready
            // We will have 2 instances of the cart - a local variable for the shopping cart, and the session variable.
            // The local variable will be dealing with the C# instances of cart items -> the session variable will be dealing with the 
            // JSON instances of the cart items.

            //Local cart instance
            Dictionary<int, CartItemViewModel> shoppingCart = null;

            //retrieve the session instance of the cart to see if it exists yet
            var sessionCart = HttpContext.Session.GetString("cart");

            // if the session cart is null, instantiate the local shopping cart
            if (sessionCart == null)
            {
                shoppingCart = new Dictionary<int, CartItemViewModel>();
            }

            //otherwise, retrieve and convert the contents from the session cart
            //here, we are taking the JSON that is in sessionCart, and converting it into C# for our local instance shoppingCart
            else
            {
                shoppingCart = JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(sessionCart);
                // Deserialize is just a fancy term for 'convert' -- it is converting JSON into C#
            }


            // Retrieve the product for the cart from the DB
            Product product = _context.Products.Find(id);

            // Create the CartItemViewModel for the product being added
            CartItemViewModel civm = new CartItemViewModel(1, product);

            // If the product was already in the cart, increase the qty by 1
            // Otherwise, add the new item into the local shopping cart
            if (shoppingCart.ContainsKey(product.ProductId))
            {
                //update qty
                shoppingCart[product.ProductId].Qty++;
            }
            else
            {
                shoppingCart.Add(product.ProductId, civm);
            }

            //update the session version of the cart
            // take the local copy, serialize (box it up) as JSON
            // then store that JSON value in session
            string jsonCart = JsonConvert.SerializeObject(shoppingCart);
            HttpContext.Session.SetString("cart", jsonCart);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int id)
        {
            // retrieve our cart from session
            var sessionCart = HttpContext.Session.GetString("cart");

            // convert JSON cart to C#
            Dictionary<int, CartItemViewModel> shoppingCart = JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(sessionCart);

            // remove the cart item from the C# collection
            shoppingCart.Remove(id);

            // update session again
            // - if there are no items remaining in the cart, remove the cart from session
            if (shoppingCart.Count == 0)
            {
                HttpContext.Session.Remove("cart");
            }
            // - otherwise, update the session variable with our local cart contents
            else
            {
                string jsonCart = JsonConvert.SerializeObject(shoppingCart);
                HttpContext.Session.SetString("cart", jsonCart);
            }

            // send the user back to the shopping cart index
            return RedirectToAction("Index");
        }

        public IActionResult UpdateCart(int productId, int qty)
        {
            // retrieve the cart
            var sessionCart = HttpContext.Session.GetString("cart");

            // deserialize from JSON to C#
            Dictionary<int, CartItemViewModel> shoppingCart = JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(sessionCart);

            // update the qty for the productId provided in the params of this action
            shoppingCart[productId].Qty = qty;

            // update session
            string jsonCart = JsonConvert.SerializeObject(shoppingCart);
            HttpContext.Session.SetString("cart", jsonCart);

            return RedirectToAction("Index");
        }

        // This method MUST be async in order to invoke the UserManager's async methods in this action.
        public async Task<IActionResult> SubmitOrder()
        {
            #region Planning out Order Submission
            // Create Order object -> Then save to DB
            // - UserId (get from Identity)
            // - OrderDate (Current date/time aka DateTime.Now)
            // - ShipToName -- the person who is ordering (UserDetails)
            // - ShipCity (UserDetails)
            // - ShipState (UserDetails)
            // - ShipZip (UserDetails)
            // Add the record to _context
            // Save DB Changes

            // Create OrderProduct objects for each item in the cart -> Then save to DB
            // - ProductId (Cart)
            // - OrderId (Order object)
            // - Quantity (Cart)
            // - ProductPrice (Cart)
            // Add the record to _context
            // Save DB Changes
            #endregion

            // Retrieve current user's Id
            string? userId = (await _userManager.GetUserAsync(HttpContext.User))?.Id;

            // Retrieve the UserDetails record from the DB
            UserDetail ud = _context.UserDetails.Find(userId);

            // Create the Order object
            Order o = new Order()
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                ShipToName = ud.FullName,
                ShipCity = ud.City,
                ShipState = ud.State,
                ShipZip = ud.Zip
            };

            // Add the order to _context
            _context.Orders.Add(o);


            // Retrieve the session cart and convert to C#
            var sessionCart = HttpContext.Session.GetString("cart");
            Dictionary<int, CartItemViewModel> shoppingCart = JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(sessionCart);

            // Create an OrderProduct record for every Product in our cart
            foreach (var item in shoppingCart)
            {
                OrderProduct op = new OrderProduct()
                {
                    OrderId = o.OrderId,
                    ProductId = item.Value.CartProd.ProductId,
                    ProductPrice = item.Value.CartProd.UnitPrice,
                    Quantity = (short?)item.Value.Qty
                };

                //ONLY need to add items to an existing entity (here -> the order 'o') if the items are a related record (like the OrderProduct here)
                o.OrderProducts.Add(op);

            }

            //Save changes to DB
            _context.SaveChanges();

            // Now that the order has been saved to the database, we can empty the cart.            
            HttpContext.Session.Remove("cart");


            return RedirectToAction("Index", "Orders");
        }
    }
}
