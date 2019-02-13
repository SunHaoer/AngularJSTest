using AngularTest.Data;
using AngularTest.Models;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace AngularTest.Service
{
    public class BrandService
    {
        private readonly BrandContext _brandContext;
        private readonly IQueryable<Brand> brandIQ;

        public BrandService(BrandContext brandContext)
        {
            _brandContext = brandContext;
            brandIQ = _brandContext.Brands;
        }

        public List<Brand> GetBrandList()
        {
            return brandIQ.ToList();
        }

        public void InitBrandDataBase()
        {
            if (brandIQ.Count() == 0)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@".\phones\phonesDetail.xml");
                XmlNode root = doc.SelectSingleNode("Detail");
                XmlNodeList brands = root.ChildNodes;
                foreach (XmlNode brand in brands)
                {
                    string brandName = brand.Name;
                    _brandContext.Add(new Brand { BrandName = brandName });
                }
                _brandContext.SaveChanges();
            }
        }

    }
}
