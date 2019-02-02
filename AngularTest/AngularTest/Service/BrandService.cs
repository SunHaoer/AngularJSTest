using AngularTest.Data;
using AngularTest.Models;
using System.Collections.Generic;
using System.Linq;

namespace AngularTest.Service
{
    public class BrandService
    {
        private readonly BrandContext _brandContext;
        private readonly IQueryable<Brand> brandIQ;

        public BrandService(BrandContext brandContext, IQueryable<Brand> brandIQ)
        {
            _brandContext = brandContext;
            this.brandIQ = brandIQ;
        }

        public List<Brand> GetBrandList()
        {
            return brandIQ.ToList();
        }
    }
}
