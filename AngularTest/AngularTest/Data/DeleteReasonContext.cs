using AngularTest.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularTest.Data
{
    public class DeleteReasonContext: DbContext
    {
        public DeleteReasonContext(DbContextOptions<DeleteReasonContext> options)
            : base(options)
        {
        }

        public DbSet<deleteReason> DeleteReasons { get; set; }
    }
}
