using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
<<<<<<< HEAD
using System.Xml;
=======
>>>>>>> origin/hubert
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
<<<<<<< HEAD
            if (_context.BrandModels.Count() == 0)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@".\phones\phonesDetail.xml");
                XmlNode root = doc.SelectSingleNode("Detail");
                XmlNodeList brands = root.ChildNodes;
                foreach (XmlNode brand in brands)
                {
                    string brandName = brand.Name;
                    _context.Add(new BrandModel { Brand = brandName });
                }
                _context.SaveChanges();
            }
        }
            
        
=======
            if (context.BrandModels.Count() == 0)
            {
                _context.Add(new BrandModel { Brand = "HUAWEI"});
                _context.Add(new BrandModel { Brand = "OPPO"});
                _context.Add(new BrandModel { Brand = "IPHONE"});
                _context.Add(new BrandModel { Brand = "SAMSUNG"});
                _context.SaveChanges();
            }
        }
>>>>>>> origin/hubert

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