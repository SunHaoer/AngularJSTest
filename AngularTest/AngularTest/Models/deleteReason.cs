using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularTest.Models
{
    public class deleteReason
    {
        public long Id { get; set; }
        public string DeleteReason { get; set; }
        public deleteReason()
        {
        }
        public deleteReason(string deleteReason)
        {
            DeleteReason = deleteReason;
        }
    }
}
