using System.Linq;
using System.Xml;
using AngularTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace AngularTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TypeYearController : ControllerBase
    {
        private readonly TypeYearContext _context;

        public TypeYearController(TypeYearContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 初始化TypeYear数据库
        /// url: "/api/TypeYear/InitTypeYearDB"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public bool InitTypeYearDB()
        {
            if (_context.TypeYears.Count() == 0)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@".\phones\phonesDetail.xml");
                XmlNode root = doc.SelectSingleNode("Detail");
                XmlNodeList brands = root.ChildNodes;
                foreach (XmlNode brand in brands)
                {
                    XmlNodeList types = brand.ChildNodes;
                    foreach (XmlNode type in types)
                    {
                        string typeName = type.Name;
                        int year = int.Parse(type.InnerText);
                        _context.Add(new TypeYear { Type = typeName, Year = year });
                    }
                }
                _context.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// 根据型号获取对应的使用年限
        /// url: '/api/TypeYear/GetYearByType'
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<int> GetYearByType(string type)
        {
            IQueryable<TypeYear> TypeIQ = _context.TypeYears;
            TypeIQ = TypeIQ.Where(s => s.Type.Equals(type));
            TypeYear typeYear = TypeIQ.First(s => s.Type.Equals(type));
            return typeYear.Year;
        }

    }
}