using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int CategoryId { get; set; }
        public short QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public int StatusId { get; set; }
        public string? ProductImage { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ProductStockStatus Status { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
