using AngularTest.Models;
using AngularTest.PageVeiwModels;
using AngularTest.Utils;

namespace AngularTest.VeiwModels
{
    public class ChoosePageViewModel : BasePageViewModel
    {
        public long LoginUserId { set; get; }
        public string LoginUsername { set; get; }
        public PaginatedList<Phone> PhoneList { set; get; }
    }
}
