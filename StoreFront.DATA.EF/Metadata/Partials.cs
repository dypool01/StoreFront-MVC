using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreFront.DATA.EF.Metadata;

namespace StoreFront.DATA.EF.Models//.Metadata
{
    //internal class Partials
    //{
    //}

    [ModelMetadataType(typeof(CategoryMetadata))]
    public partial class Category { }

    [ModelMetadataType(typeof(ProductMetadata))]

    public partial class Product { }

    [ModelMetadataType(typeof(ProductStockStatusMetadata))]

    public partial class ProductStockStatus { }

    [ModelMetadataType(typeof(UserDetailsMetadata))]
    public partial class UserDetail { }

}//end namespace
