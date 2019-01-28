using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularTest.Models;
using Microsoft.AspNetCore.Http;
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
            //if(_context.BrandTypeProductNos.Count() == 0)
            //{
            //    _context.Add(new BrandTypeProductNo("HUAWEI", "Mate20", "HUAWEIMate20-1"));
            //    _context.Add(new BrandTypeProductNo("HUAWEI", "MateRS", "HUAWEIMateRS-1"));
            //    _context.Add(new BrandTypeProductNo("HUAWEI", "Mate20Pro", "HUAWEIMate20Pro-1"));
            //    _context.Add(new BrandTypeProductNo("HUAWEI", "Nova3", "HUAWEINova3-1"));
            //    _context.Add(new BrandTypeProductNo("OPPO", "K1", "OPPOK1-1"));
            //    _context.Add(new BrandTypeProductNo("OPPO", "R17", "OPPOR17-1"));
            //    _context.Add(new BrandTypeProductNo("OPPO", "A7x", "OPPOA7x-1"));
            //    _context.Add(new BrandTypeProductNo("OPPO", "R15x", "OPPOR15x-1"));
            //    _context.Add(new BrandTypeProductNo("IPHONE", "iphone6Plus", "IPHONEiphone6Plus-1"));
            //    _context.Add(new BrandTypeProductNo("IPHONE", "iphone6", "IPHONEiphone6-1"));
            //    _context.Add(new BrandTypeProductNo("IPHONE", "iphone7Plus", "IPHONEiphone7Plus-1"));
            //    _context.Add(new BrandTypeProductNo("IPHONE", "iphoneX", "IPHONEiphoneX-1"));
            //    _context.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyS8", "SAMSUNGGalaxyS8-1"));
            //    _context.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyNote8", "SAMSUNGGalaxyNote8-1"));
            //    _context.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyA8s", "SAMSUNGGalaxyA8s-1"));
            //    _context.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyS9", "SAMSUNGGalaxyS9-1"));

            //    _context.Add(new BrandTypeProductNo("HUAWEI", "Mate20", "HUAWEIMate20-2"));
            //    _context.Add(new BrandTypeProductNo("HUAWEI", "MateRS", "HUAWEIMateRS-2"));
            //    _context.Add(new BrandTypeProductNo("HUAWEI", "Mate20Pro", "HUAWEIMate20Pro-2"));
            //    _context.Add(new BrandTypeProductNo("HUAWEI", "Nova3", "HUAWEINova3-2"));
            //    _context.Add(new BrandTypeProductNo("OPPO", "K1", "OPPOK1-2"));
            //    _context.Add(new BrandTypeProductNo("OPPO", "R17", "OPPOR17-2"));
            //    _context.Add(new BrandTypeProductNo("OPPO", "A7x", "OPPOA7x-2"));
            //    _context.Add(new BrandTypeProductNo("OPPO", "R15x", "OPPOR15x-2"));
            //    _context.Add(new BrandTypeProductNo("IPHONE", "iphone6Plus", "IPHONEiphone6Plus-2"));
            //    _context.Add(new BrandTypeProductNo("IPHONE", "iphone6", "IPHONEiphone6-2"));
            //    _context.Add(new BrandTypeProductNo("IPHONE", "iphone7Plus", "IPHONEiphone7Plus-2"));
            //    _context.Add(new BrandTypeProductNo("IPHONE", "iphoneX", "IPHONEiphoneX-2"));
            //    _context.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyS8", "SAMSUNGGalaxyS8-2"));
            //    _context.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyNote8", "SAMSUNGGalaxyNote8-2"));
            //    _context.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyA8s", "SAMSUNGGalaxyA8s-2"));
            //    _context.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyS9", "SAMSUNGGalaxyS9-2"));
            //    _context.SaveChanges();
            //}
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
                _context.Add(new BrandTypeProductNo("HUAWEI", "Mate20", "HUAWEIMate20-1"));
                _context.Add(new BrandTypeProductNo("HUAWEI", "MateRS", "HUAWEIMateRS-1"));
                _context.Add(new BrandTypeProductNo("HUAWEI", "Mate20Pro", "HUAWEIMate20Pro-1"));
                _context.Add(new BrandTypeProductNo("HUAWEI", "Nova3", "HUAWEINova3-1"));
                _context.Add(new BrandTypeProductNo("OPPO", "K1", "OPPOK1-1"));
                _context.Add(new BrandTypeProductNo("OPPO", "R17", "OPPOR17-1"));
                _context.Add(new BrandTypeProductNo("OPPO", "A7x", "OPPOA7x-1"));
                _context.Add(new BrandTypeProductNo("OPPO", "R15x", "OPPOR15x-1"));
                _context.Add(new BrandTypeProductNo("IPHONE", "iphone6Plus", "IPHONEiphone6Plus-1"));
                _context.Add(new BrandTypeProductNo("IPHONE", "iphone6", "IPHONEiphone6-1"));
                _context.Add(new BrandTypeProductNo("IPHONE", "iphone7Plus", "IPHONEiphone7Plus-1"));
                _context.Add(new BrandTypeProductNo("IPHONE", "iphoneX", "IPHONEiphoneX-1"));
                _context.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyS8", "SAMSUNGGalaxyS8-1"));
                _context.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyNote8", "SAMSUNGGalaxyNote8-1"));
                _context.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyA8s", "SAMSUNGGalaxyA8s-1"));
                _context.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyS9", "SAMSUNGGalaxyS9-1"));

                _context.Add(new BrandTypeProductNo("HUAWEI", "Mate20", "HUAWEIMate20-2"));
                _context.Add(new BrandTypeProductNo("HUAWEI", "MateRS", "HUAWEIMateRS-2"));
                _context.Add(new BrandTypeProductNo("HUAWEI", "Mate20Pro", "HUAWEIMate20Pro-2"));
                _context.Add(new BrandTypeProductNo("HUAWEI", "Nova3", "HUAWEINova3-2"));
                _context.Add(new BrandTypeProductNo("OPPO", "K1", "OPPOK1-2"));
                _context.Add(new BrandTypeProductNo("OPPO", "R17", "OPPOR17-2"));
                _context.Add(new BrandTypeProductNo("OPPO", "A7x", "OPPOA7x-2"));
                _context.Add(new BrandTypeProductNo("OPPO", "R15x", "OPPOR15x-2"));
                _context.Add(new BrandTypeProductNo("IPHONE", "iphone6Plus", "IPHONEiphone6Plus-2"));
                _context.Add(new BrandTypeProductNo("IPHONE", "iphone6", "IPHONEiphone6-2"));
                _context.Add(new BrandTypeProductNo("IPHONE", "iphone7Plus", "IPHONEiphone7Plus-2"));
                _context.Add(new BrandTypeProductNo("IPHONE", "iphoneX", "IPHONEiphoneX-2"));
                _context.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyS8", "SAMSUNGGalaxyS8-2"));
                _context.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyNote8", "SAMSUNGGalaxyNote8-2"));
                _context.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyA8s", "SAMSUNGGalaxyA8s-2"));
                _context.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyS9", "SAMSUNGGalaxyS9-2"));
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