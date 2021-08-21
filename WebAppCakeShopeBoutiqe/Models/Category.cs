using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCakeShopeBoutiqe.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public List<Product> Products { get; set; }

        public CategoryImage Image{ get; set; }

    }
}
