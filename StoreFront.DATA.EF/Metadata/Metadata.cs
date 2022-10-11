using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreFront.DATA.EF.Models;

namespace StoreFront.DATA.EF.Metadata
{
    //internal class Metadata
    //{
    //}

    public class CategoryMetadata
    {
        public int CategoryId { get; set; }

        [StringLength(50, ErrorMessage = "*Cannot exceed 50 characters")]
        [Required(ErrorMessage = "*The category name is required")]
        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        [StringLength(500, ErrorMessage = "*Cannot exceed 500 characters")]
        [DataType(DataType.MultilineText)]
        public string CategoryDescription { get; set; }
    }//end CategoryMetadata

    public class ProductMetadata
    {
        public int ProductID { get; set; }

        [StringLength(50, ErrorMessage = "*Cannot exceed 50 characters")]
        [Required(ErrorMessage = "*The product name is required")]
        [Display(Name = "Product")]
        public string ProductName { get; set; }


        public int CategoryID { get; set; }

        [Required(ErrorMessage = "*Quantity per unit is required")]
        [Display(Name = "Units")]
        [Range(0, short.MaxValue)]
        public short QuantityPerUnit { get; set; }


        [Display(Name = "Price")]
        [Required(ErrorMessage = "*Price is required")]
        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = false)]
        [Range(0, (double)decimal.MaxValue)]
        public decimal UnitPrice { get; set; }


        public int StatusID { get; set; }

        [Display(Name = "Image")]
        [StringLength(100, ErrorMessage = "*Cannot exceed 100 characters")]
        public string ProductImage { get; set; }
    }//end ProductMetadata

    public class ProductStockStatusMetadata
    {
        public int StatusID { get; set; }

        [Display(Name = "LimitedStock")]
        public bool Limitedstock { get; set; }

        [Display(Name = "ReStocking")]
        public bool ReStocking { get; set; }

        [Display(Name = "Discontinued")]
        public bool Discontinued { get; set; }
    }//end ProductStockStatusMetadata

    public class UserDetailsMetadata
    {
        public string UserId { get; set; } = null!;
        [StringLength(50)]
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; } = null!;
        [StringLength(50)]
        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; } = null!;
        [StringLength(150)]
        public string? Address { get; set; }
        [StringLength(50)]
        public string? City { get; set; }
        [StringLength(2)]
        public string? State { get; set; }
        [StringLength(5)]
        [DataType(DataType.PostalCode)]
        public string? Zip { get; set; }
        [StringLength(24)]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }
    }

}
