using AngularTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularTest.Service
{
    public class BrandTypeService
    {
        private readonly BrandTypeContext _brandTypeContext;
        private readonly IQueryable<BrandType> brandTypeIQ;

        public BrandTypeService(BrandTypeContext brandTypeContext, IQueryable<BrandType> brandTypeIQ)
        {
            _brandTypeContext = brandTypeContext;
            this.brandTypeIQ = brandTypeIQ;
        }

        public List<BrandType> GetBrandTypeList()
        {
            return brandTypeIQ.ToList();
        }
    }
}
