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

        public BrandTypeProductNoService(BrandTypeProductNoContext brandTypeProductNoContext)
        {
            _brandTypeProductNoContext = brandTypeProductNoContext;
            brandTypeProductNoIQ = _brandTypeProductNoContext.BrandTypeProductNos;
        }

        public bool ValidateBrandTypeProductNo(string brand, string type, string productNo)
        {
            return brandTypeProductNoIQ.Any(item => item.Brand.Equals(brand) && item.Type.Equals(type) && item.ProductNo.Equals(productNo));
        }

        public void InitBrandTypeProductNoDataBase()
        {
            if (_brandTypeProductNoContext.BrandTypeProductNos.Count() == 0)
            {
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("HUAWEI", "Mate20", "HUAWEIMate201"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("HUAWEI", "MateRS", "HUAWEIMateRS1"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("HUAWEI", "Mate20Pro", "HUAWEIMate20Pro1"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("HUAWEI", "Nova3", "HUAWEINova31"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("OPPO", "K1", "OPPOK11"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("OPPO", "R17", "OPPOR171"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("OPPO", "A7x", "OPPOA7x1"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("OPPO", "R15x", "OPPOR15x1"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("IPHONE", "iphone6Plus", "IPHONEiphone6Plus1"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("IPHONE", "iphone6", "IPHONEiphone61"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("IPHONE", "iphone7Plus", "IPHONEiphone7Plus1"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("IPHONE", "iphoneX", "IPHONEiphoneX1"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyS8", "SAMSUNGGalaxyS81"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyNote8", "SAMSUNGGalaxyNote81"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyA8s", "SAMSUNGGalaxyA8s1"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyS9", "SAMSUNGGalaxyS91"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("HUAWEI", "Mate20", "HUAWEIMate202"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("HUAWEI", "MateRS", "HUAWEIMateRS2"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("HUAWEI", "Mate20Pro", "HUAWEIMate20Pro2"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("HUAWEI", "Nova3", "HUAWEINova32"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("OPPO", "K1", "OPPOK12"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("OPPO", "R17", "OPPOR172"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("OPPO", "A7x", "OPPOA7x2"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("OPPO", "R15x", "OPPOR15x2"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("IPHONE", "iphone6Plus", "IPHONEiphone6Plus2"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("IPHONE", "iphone6", "IPHONEiphone62"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("IPHONE", "iphone7Plus", "IPHONEiphone7Plus2"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("IPHONE", "iphoneX", "IPHONEiphoneX2"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyS8", "SAMSUNGGalaxyS82"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyNote8", "SAMSUNGGalaxyNote82"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyA8s", "SAMSUNGGalaxyA8s2"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyS9", "SAMSUNGGalaxyS92"));
                _brandTypeProductNoContext.SaveChanges();
            }
        }

    }
}
