using AngularTest.Models;
using AngularTest.PageVeiwModels;
using System.Collections.Generic;

namespace AngularTest.VeiwModels
{
    public class DeletePhonePageViewModel : BasePageViewModel
    {
        public Phone TempNewPhone { set; get; }
        public List<DeleteReason> DeleteReasonList { set; get; }
    }
}
