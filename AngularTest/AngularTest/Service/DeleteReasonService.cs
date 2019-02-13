using AngularTest.Data;
using AngularTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularTest.Service
{
    public class DeleteReasonService
    {
        private readonly DeleteReasonContext _deleteReasonContext;
        private readonly IQueryable<DeleteReason> deleteReasonIQ;

        public DeleteReasonService(DeleteReasonContext deleteReasonContext)
        {
            _deleteReasonContext = deleteReasonContext;
            deleteReasonIQ = _deleteReasonContext.DeleteReasons;
        }

        public List<DeleteReason> GetDeleteReason()
        {
            return deleteReasonIQ.ToList();
        }
    }
}
