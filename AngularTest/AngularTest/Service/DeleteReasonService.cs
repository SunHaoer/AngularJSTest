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

        public void InitDeleteReasonDataBase()
        {
            if (_deleteReasonContext.DeleteReasons.Count() == 0)
            {
                _deleteReasonContext.Add(new DeleteReason("lost"));
                _deleteReasonContext.Add(new DeleteReason("buy new phone"));
                _deleteReasonContext.Add(new DeleteReason("time end"));
                _deleteReasonContext.Add(new DeleteReason("not interested"));
                _deleteReasonContext.Add(new DeleteReason("other"));
                _deleteReasonContext.SaveChanges();
            }
        }
    }
}
