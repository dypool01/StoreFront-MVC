using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreFront.DATA.EF.Metadata;

namespace StoreFront.DATA.EF.Models//.Metadata
{
    //internal class Partials
    //{
    //}



    #region Category
    [ModelMetadataType(typeof(CategoryMetadata))]
    public partial class Category { }
    #endregion

    #region Product
    [ModelMetadataType(typeof(ProductMetadata))]

    public partial class Product 
    {
        [NotMapped]
        public IFormFile? Image { get; set; }
    }
    #endregion

    #region ProductStockStatus
    [ModelMetadataType(typeof(ProductStockStatusMetadata))]

    public partial class ProductStockStatus { }
    #endregion

    #region UserDetails
    [ModelMetadataType(typeof(UserDetailsMetadata))]
    public partial class UserDetail 
    { 
        public string FullName { get { return $"{FirstName} {LastName}"; } }
    }
    #endregion


}//end namespace
