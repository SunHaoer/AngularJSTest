using AngularTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace AngularTest.Service
{
    public class BrandTypeService
    {
        private readonly BrandTypeContext _brandTypeContext;
        private readonly IQueryable<BrandType> brandTypeIQ;

        public BrandTypeService(BrandTypeContext brandTypeContext)
        {
            _brandTypeContext = brandTypeContext;
            brandTypeIQ = _brandTypeContext.BrandTypes;
        }

        public List<BrandType> GetBrandTypeList()
        {
            return brandTypeIQ.ToList();
        }

        public void InitBrandTypeDataBase()
        {
            if (brandTypeIQ.Count() == 0)
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
                        _brandTypeContext.Add(new BrandType { Brand = brandName, Type = typeName });
                    }
                }
                _brandTypeContext.SaveChanges();
            }
        }

    }
}
