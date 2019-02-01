using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
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
        }

        /// <summary>
        /// 初始化品牌数据库
        /// url: "/api/BrandModel/InitBrandModelDB"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public bool InitBrandModelDB()
        {
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
            return true;
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