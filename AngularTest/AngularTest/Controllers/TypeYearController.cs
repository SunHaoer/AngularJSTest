using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
<<<<<<< HEAD
using System.Xml;
=======
>>>>>>> origin/hubert
using AngularTest.Models;
using Microsoft.AspNetCore.Http;
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
<<<<<<< HEAD
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
           
=======
            if (context.TypeYears.Count() == 0)
            {
                _context.Add(new TypeYear { Type = "Mate 20", Year = 2 });
                _context.Add(new TypeYear { Type = "Mate RS", Year = 1 });
                _context.Add(new TypeYear { Type = "Mate 20Pro", Year = 3 });
                _context.Add(new TypeYear { Type = "Nova 3", Year = 3 });
                _context.Add(new TypeYear { Type = "X", Year = 2 });
                _context.Add(new TypeYear { Type = "7Plus", Year = 4 });
                _context.Add(new TypeYear { Type = "6", Year = 2 });
                _context.Add(new TypeYear { Type = "6s", Year = 3 });
                _context.Add(new TypeYear { Type = "K1", Year = 1 });
                _context.Add(new TypeYear { Type = "R17", Year = 2 });
                _context.Add(new TypeYear { Type = "A7x", Year = 2 });
                _context.Add(new TypeYear { Type = "R15x", Year = 2 });
                _context.Add(new TypeYear { Type = "Galaxy S8", Year = 1 });
                _context.Add(new TypeYear { Type = "Galaxy Note8", Year = 1 });
                _context.Add(new TypeYear { Type = "Galaxy A8s", Year = 2 });
                _context.Add(new TypeYear { Type = "Galaxy S9", Year = 3 });
                _context.SaveChanges();
            }
>>>>>>> origin/hubert
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