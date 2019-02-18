using AngularTest.Models;
using AngularTest.PageVeiwModels;

namespace AngularTest.VeiwModels
{
    public class ReplacePhonePageViewModel : AddPhonePageViewModel
    {
        public Phone TempOldPhone { set; get; }
    }
}
