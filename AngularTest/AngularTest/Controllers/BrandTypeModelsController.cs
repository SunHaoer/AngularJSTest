using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AngularTest.Models;

namespace AngularTest.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class BrandTypeModelsController : ControllerBase
    {
        private readonly BrandTypeContext _context;

        public BrandTypeModelsController(BrandTypeContext context)
        {
            _context = context;
            if (context.BrandTypes.Count() == 0)
            {
                _context.Add(new BrandType { Brand = "HUAWEI", Type = "Mate 20" });
                _context.Add(new BrandType { Brand = "HUAWEI", Type = "Mate RS" });
                _context.Add(new BrandType { Brand = "HUAWEI", Type = "Mate 20Pro" });
                _context.Add(new BrandType { Brand = "HUAWEI", Type = "Nova 3" });
                _context.Add(new BrandType { Brand = "IPHONE", Type = "X" });
                _context.Add(new BrandType { Brand = "IPHONE", Type = "7Plus" });
                _context.Add(new BrandType { Brand = "IPHONE", Type = "6" });
                _context.Add(new BrandType { Brand = "IPHONE", Type = "6s" });
                _context.Add(new BrandType { Brand = "OPPO", Type = "K1" });
                _context.Add(new BrandType { Brand = "OPPO", Type = "R17" });
                _context.Add(new BrandType { Brand = "OPPO", Type = "A7x" });
                _context.Add(new BrandType { Brand = "OPPO", Type = "R15x" });
                _context.Add(new BrandType { Brand = "SAMSUNG", Type = "Galaxy S8" });
                _context.Add(new BrandType { Brand = "SAMSUNG", Type = "Galaxy Note8" });
                _context.Add(new BrandType { Brand = "SAMSUNG", Type = "Galaxy A8s" });
                _context.Add(new BrandType { Brand = "SAMSUNG", Type = "Galaxy S9" });
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// 根据手机品牌获取型号
        /// url: 'api/BrandTypeModels/GetTypeByBrand'
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<BrandType>> GetTypeByBrand(string brand)
        {
            IQueryable<BrandType> BrandTypeModelsIQ = _context.BrandTypes;
            BrandTypeModelsIQ = BrandTypeModelsIQ.Where(item => item.Brand.Equals(brand));
            return BrandTypeModelsIQ.ToList();
        }

    }
}