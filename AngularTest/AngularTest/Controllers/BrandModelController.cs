using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularTest.Data;
using AngularTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BrandModelController : ControllerBase
    {
        private readonly BrandModelContext _context;

        public BrandModelController(BrandModelContext context)
        {
            _context = context;
            if (context.BrandModels.Count() == 0)
            {
                _context.Add(new BrandModel { Brand = "HUAWEI"});
                _context.Add(new BrandModel { Brand = "OPPO"});
                _context.Add(new BrandModel { Brand = "IPHONE"});
                _context.Add(new BrandModel { Brand = "SAMSUNG"});
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// 获取所有手机品牌
        /// url: '/api/BrandModel/GetBrandAll'
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<BrandModel>> GetBrandAll()
        {
            return _context.BrandModels.ToList();
        }
    }
}