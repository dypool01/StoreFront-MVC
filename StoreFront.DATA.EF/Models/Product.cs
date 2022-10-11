using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int CategoryId { get; set; }
        public short QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public int StatusId { get; set; }
        public string? ProductImage { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ProductStockStatus Status { get; set; } = null!;
    }
}
