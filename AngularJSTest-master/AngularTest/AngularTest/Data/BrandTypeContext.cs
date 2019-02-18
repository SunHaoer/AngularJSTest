using Microsoft.EntityFrameworkCore;

namespace AngularTest.Models
{
    public class BrandTypeContext : DbContext
    {
        public BrandTypeContext(DbContextOptions<BrandTypeContext> options)
            : base(options)
        {
        }

        public DbSet<BrandType> BrandTypes { get; set; }
    }
}