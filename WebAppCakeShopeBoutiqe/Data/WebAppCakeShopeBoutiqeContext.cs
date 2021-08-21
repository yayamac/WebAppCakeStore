using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAppCakeShopeBoutiqe.Models;

namespace WebAppCakeShopeBoutiqe.Data
{
    public class WebAppCakeShopeBoutiqeContext : DbContext
    {
        public WebAppCakeShopeBoutiqeContext (DbContextOptions<WebAppCakeShopeBoutiqeContext> options)
            : base(options)
        {
        }

        public DbSet<WebAppCakeShopeBoutiqe.Models.Client> Client { get; set; }

        public DbSet<WebAppCakeShopeBoutiqe.Models.Product> Product { get; set; }

        public DbSet<WebAppCakeShopeBoutiqe.Models.Category> Category { get; set; }

        public DbSet<WebAppCakeShopeBoutiqe.Models.CategoryImage> CategoryImage { get; set; }

        //public DbSet<WebAppCakeShopeBoutiqe.Models.BannerImg> BannerImg { get; set; }
    }
}
