using AngularTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace AngularTest.Dao
{
    public class TypeYearDao
    {
        private readonly TypeYearContext _typeYearContext;
        private readonly IQueryable<TypeYear> typeYearIQ;

        public TypeYearDao(TypeYearContext typeYearContext)
        {
            _typeYearContext = typeYearContext;
            typeYearIQ = _typeYearContext.TypeYears;
        }

        public int GetYearByType(string type)
        {
            return typeYearIQ.FirstOrDefault(item => item.Type.Equals(type.Trim())).Year;
        }

        public void InitTypeYearDataBase()
        {
            if (_typeYearContext.TypeYears.Count() == 0)
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
                        _typeYearContext.Add(new TypeYear { Type = typeName, Year = year });
                    }
                }
                _typeYearContext.SaveChanges();
            }
        }

    }
}
