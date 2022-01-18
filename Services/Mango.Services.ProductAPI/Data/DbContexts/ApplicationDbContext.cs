using Mango.Services.ProductAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.Data.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

    }
}
