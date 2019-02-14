using AngularTest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularTest.Data
{
    public class DeleteReasonContext : DbContext
    {
        public DeleteReasonContext(DbContextOptions<DeleteReasonContext> options)
            : base(options)
        {
        }

        public DbSet<DeleteReason> DeleteReasons { get; set; }
    }
}
