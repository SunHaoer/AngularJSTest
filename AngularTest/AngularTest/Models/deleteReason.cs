using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularTest.Models
{
    public class DeleteReason
    {
        public long Id { set; get; }
        public string DeleteReasonName { set; get; }

        public DeleteReason()
        {
        }

        public DeleteReason(string deleteReasonName)
        {
            DeleteReasonName = deleteReasonName;
        }
    }
}
