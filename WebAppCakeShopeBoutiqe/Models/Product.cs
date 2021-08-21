using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCakeShopeBoutiqe.Models
{
    public class Product
    {
       

        [Required]
        [Key]
        public int ProductId { get; set; } // Product ID

        [Required]
        [Display(Name = "Cake Name")]
        public string ProductName { get; set; }//animal cake, 42 Age cake........

        [Required]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Range(0, 9999)]

        public double Price { get; set; }

        [Required]
        public string Description { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

    }
}
