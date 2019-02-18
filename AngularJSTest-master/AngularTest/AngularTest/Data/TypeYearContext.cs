using Microsoft.EntityFrameworkCore;

namespace AngularTest.Models
{
    public class TypeYearContext : DbContext
    {
        public TypeYearContext(DbContextOptions<TypeYearContext> options)
            : base(options)
        {
        }

        public DbSet<TypeYear> TypeYears { get; set; }
    }
}