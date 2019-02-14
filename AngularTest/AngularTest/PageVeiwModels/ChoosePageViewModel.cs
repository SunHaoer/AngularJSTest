using AngularTest.Models;
using AngularTest.PageVeiwModels;
using AngularTest.Utils;

namespace AngularTest.VeiwModels
{
    public class ChoosePageViewModel : BasePageViewModel
    {
        public string LoginUsername { set; get; }
        public PaginatedList<Phone> PhoneList { set; get; }
    }
}
