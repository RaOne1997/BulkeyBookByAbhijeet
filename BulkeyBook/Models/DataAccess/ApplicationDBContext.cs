using BulkeyBook.Models.DataAccess.Modul;
using BulkyBook.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BulkeyBook.Models.DataAccess
{
    public class ApplicationDBContext : IdentityDbContext<UserINtoUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CoverType> CoverTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> shoppingCarts  { get; set; }

        public DbSet<Company> Companies { get; set; }
    }
}
