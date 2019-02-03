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

        public BrandTypeService(BrandTypeContext brandTypeContext)
        {
            _brandTypeContext = brandTypeContext;
            this.brandTypeIQ = _brandTypeContext.BrandTypes;
        }

        public List<BrandType> GetBrandTypeList()
        {
            return brandTypeIQ.ToList();
        }
    }
}
