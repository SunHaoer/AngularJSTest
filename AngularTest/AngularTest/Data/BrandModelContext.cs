using AngularTest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularTest.Data
{
    public class BrandModelContext : DbContext
    {
        public BrandModelContext(DbContextOptions<BrandModelContext> options)
    : base(options)
        {
        }

        public DbSet<BrandModel> BrandModels { get; set; }
    }
}
