using AngularTest.Models;
using AngularTest.Utils;
using System.Linq;

namespace AngularTest.Service
{
    public class ChoosePageService
    {
        private readonly PhoneContext _context;
        private readonly IQueryable<Phone> phoneIQ;

        public ChoosePageService(PhoneContext context, IQueryable<Phone> phoneIQ)
        {
            _context = context;
            this.phoneIQ = phoneIQ;
        }

        public PaginatedList<Phone> GetPhoneList(IQueryable<Phone> phoneIQ, int pageIndex, int pageSize)
        {
            return PaginatedList<Phone>.Create(phoneIQ, pageIndex, pageSize);
        }
    }
}
