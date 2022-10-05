using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class VwStoreFront
    {
        public string CategoryName { get; set; } = null!;
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public short QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public bool LimitedStock { get; set; }
        public bool ReStocking { get; set; }
        public bool Discontinued { get; set; }
    }
}
