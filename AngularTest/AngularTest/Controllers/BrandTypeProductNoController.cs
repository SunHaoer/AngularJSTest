using System.Linq;
using AngularTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace AngularTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BrandTypeProductNoController : ControllerBase
    {
        private readonly BrandTypeProductNoContext _context;
        public BrandTypeProductNoController(BrandTypeProductNoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 初始化BrandTypeProductNo数据库
        /// url: "/api/BrandTypeProductNo/InitBrandTypeProductNoDB"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public bool InitBrandTypeProductNoDB()
        {
            if (_context.BrandTypeProductNos.Count() == 0)
            {
                _context.Add(new BrandTypeProductNo("HUAWEI", "Mate20", "HUAWEIMate201"));
                _context.Add(new BrandTypeProductNo("HUAWEI", "MateRS", "HUAWEIMateRS1"));
                _context.Add(new BrandTypeProductNo("HUAWEI", "Mate20Pro", "HUAWEIMate20Pro1"));
                _context.Add(new BrandTypeProductNo("HUAWEI", "Nova3", "HUAWEINova31"));
                _context.Add(new BrandTypeProductNo("OPPO", "K1", "OPPOK11"));
                _context.Add(new BrandTypeProductNo("OPPO", "R17", "OPPOR171"));
                _context.Add(new BrandTypeProductNo("OPPO", "A7x", "OPPOA7x1"));
                _context.Add(new BrandTypeProductNo("OPPO", "R15x", "OPPOR15x1"));
                _context.Add(new BrandTypeProductNo("IPHONE", "iphone6Plus", "IPHONEiphone6Plus1"));
                _context.Add(new BrandTypeProductNo("IPHONE", "iphone6", "IPHONEiphone61"));
                _context.Add(new BrandTypeProductNo("IPHONE", "iphone7Plus", "IPHONEiphone7Plus1"));
                _context.Add(new BrandTypeProductNo("IPHONE", "iphoneX", "IPHONEiphoneX1"));
                _context.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyS8", "SAMSUNGGalaxyS81"));
                _context.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyNote8", "SAMSUNGGalaxyNote81"));
                _context.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyA8s", "SAMSUNGGalaxyA8s1"));
                _context.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyS9", "SAMSUNGGalaxyS91"));

                _context.Add(new BrandTypeProductNo("HUAWEI", "Mate20", "HUAWEIMate202"));
                _context.Add(new BrandTypeProductNo("HUAWEI", "MateRS", "HUAWEIMateRS2"));
                _context.Add(new BrandTypeProductNo("HUAWEI", "Mate20Pro", "HUAWEIMate20Pro2"));
                _context.Add(new BrandTypeProductNo("HUAWEI", "Nova3", "HUAWEINova32"));
                _context.Add(new BrandTypeProductNo("OPPO", "K1", "OPPOK12"));
                _context.Add(new BrandTypeProductNo("OPPO", "R17", "OPPOR172"));
                _context.Add(new BrandTypeProductNo("OPPO", "A7x", "OPPOA7x2"));
                _context.Add(new BrandTypeProductNo("OPPO", "R15x", "OPPOR15x2"));
                _context.Add(new BrandTypeProductNo("IPHONE", "iphone6Plus", "IPHONEiphone6Plus2"));
                _context.Add(new BrandTypeProductNo("IPHONE", "iphone6", "IPHONEiphone62"));
                _context.Add(new BrandTypeProductNo("IPHONE", "iphone7Plus", "IPHONEiphone7Plus2"));
                _context.Add(new BrandTypeProductNo("IPHONE", "iphoneX", "IPHONEiphoneX2"));
                _context.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyS8", "SAMSUNGGalaxyS82"));
                _context.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyNote8", "SAMSUNGGalaxyNote82"));
                _context.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyA8s", "SAMSUNGGalaxyA8s2"));
                _context.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyS9", "SAMSUNGGalaxyS92"));
                _context.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// 验证productNo是否合法
        /// url:"/api/BrandTypeProductNo/ValidateProductNo"
        /// </summary>
        /// <param name="productNo"></param>
        /// <param name="brand"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        public bool ValidateProductNo(string productNo = "", string brand = "", string type = "")
        {
            if (string.IsNullOrEmpty(productNo)) return false; 
            productNo = productNo.Trim();
            return _context.BrandTypeProductNos.Any(item => item.ProductNo.Equals(productNo) && item.Brand.Equals(brand) && item.Type.Equals(type));
        }

        /// <summary>
        /// 根据产品编号获取品牌，型号
        /// url:"/api/BrandTypeProductNo/GetBrandTypeByProductNo"
        /// </summary>
        /// <param name="productNo"></param>
        /// <returns></returns>
        public BrandTypeProductNo GetBrandTypeByProductNo(string productNo= "")
        {
            if (string.IsNullOrEmpty(productNo)) return null;
            productNo = productNo.Trim();
            return _context.BrandTypeProductNos.FirstOrDefault(item => item.ProductNo.Equals(productNo));
        }
    }
}