using System;
using System.Collections.Generic;

namespace StoreFront.UI.MVC.Models
{
    public partial class ProductStockStatus
    {
        public ProductStockStatus()
        {
            Products = new HashSet<Product>();
        }

        public int StatusId { get; set; }
        public bool LimitedStock { get; set; }
        public bool ReStocking { get; set; }
        public bool Discontinued { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
