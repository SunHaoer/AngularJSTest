using AngularTest.Models;
using AngularTest.PageVeiwModels;

namespace AngularTest.VeiwModels
{
    public class ReplacePhonePageViewModel : BasePageViewModel
    {
        public Phone TempNewPhone { set; get; }
        public Phone TempOldPhone { set; get; }
    }
}
