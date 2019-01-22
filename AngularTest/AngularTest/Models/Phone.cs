using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularTest.Models
{
    public class Phone
    {
        public long Id { get; set; }
        public string PhoneUser { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public long ProductNo { get; set; }
        public DateTime InputDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DeleteDate { get; set; }
        public string AbandonReason { get; set; }
    }
}
