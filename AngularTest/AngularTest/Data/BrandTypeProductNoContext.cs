using Microsoft.EntityFrameworkCore;

namespace AngularTest.Models
{
    public class BrandTypeProductNoContext : DbContext
    {
        public BrandTypeProductNoContext(DbContextOptions<BrandTypeProductNoContext> options)
            : base(options)
        {
        }

        public DbSet<BrandTypeProductNo> BrandTypeProductNos { get; set; }
    }
}