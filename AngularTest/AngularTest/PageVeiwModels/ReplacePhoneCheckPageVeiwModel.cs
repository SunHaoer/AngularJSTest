using AngularTest.Models;
using AngularTest.PageVeiwModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularTest.VeiwModels
{
    public class ReplacePhoneCheckPageVeiwModel : BasePageViewModel
    {
        public Phone TempNewPhone { set; get; }
        public Phone TempOldPhone { set; get; }
    }
}
