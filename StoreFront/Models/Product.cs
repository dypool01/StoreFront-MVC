using System;
using System.Collections.Generic;

namespace StoreFront.UI.MVC.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int CategoryId { get; set; }
        public short QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; } = null!;
        public int StatusId { get; set; }
        public string ProductImage { get; set; } = null!;

        public virtual Category Category { get; set; }
        public virtual ProductStockStatus Status { get; set; }
    }
}
