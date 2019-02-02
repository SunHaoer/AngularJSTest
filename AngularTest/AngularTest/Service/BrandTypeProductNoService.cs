using AngularTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularTest.Service
{
    public class BrandTypeProductNoService
    {
        private readonly BrandTypeProductNoContext _brandTypeProductNoContext;
        private readonly IQueryable<BrandTypeProductNo> brandTypeProductNoIQ;

        public BrandTypeProductNoService(BrandTypeProductNoContext brandTypeProductNoContext, IQueryable<BrandTypeProductNo> brandTypeProductNoIQ)
        {
            _brandTypeProductNoContext = brandTypeProductNoContext;
            this.brandTypeProductNoIQ = brandTypeProductNoIQ;
        }

        public bool ValidateBrandTypeProductNo(string brand, string type, string productNo)
        {
            return brandTypeProductNoIQ.Any(item => item.Brand.Equals(brand) && item.Type.Equals(type) && item.ProductNo.Equals(productNo));
        }
    }
}
