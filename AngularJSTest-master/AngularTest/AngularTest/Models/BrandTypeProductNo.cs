using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularTest.Models
{
    public class BrandTypeProductNo
    {
        public long Id { set; get; }
        public string Brand { set; get; }
        public string Type { set; get; }
        public string ProductNo { set; get; }

        public BrandTypeProductNo(string brand, string type, string productNo)
        {
            Brand = brand;
            Type = type;
            ProductNo = productNo;
        }
    }
}
