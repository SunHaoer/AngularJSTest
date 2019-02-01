using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AngularTest.Models;
using System.Xml;

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
        }

        /// <summary>
        /// 初始化InitBrandTypeModelDB
        /// url: "/api/BrandTypeModels/InitBrandTypeModelDB"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public bool InitBrandTypeModelDB()
        {
            if (_context.BrandTypes.Count() == 0)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@".\phones\phonesDetail.xml");
                XmlNode root = doc.SelectSingleNode("Detail");
                XmlNodeList brands = root.ChildNodes;
                foreach (XmlNode brand in brands)
                {
                    string brandName = brand.Name;
                    XmlNodeList types = brand.ChildNodes;
                    foreach (XmlNode type in types)
                    {
                        string typeName = type.Name;
                        _context.Add(new BrandType { Brand = brandName, Type = typeName });
                    }
                }
                _context.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// 根据手机品牌获取型号
        /// url: '/api/BrandTypeModels/GetTypeByBrand'
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