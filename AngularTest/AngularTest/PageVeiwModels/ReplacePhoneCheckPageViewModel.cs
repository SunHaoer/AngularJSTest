using AngularTest.Models;
using AngularTest.PageVeiwModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularTest.VeiwModels
{
    public class ReplacePhoneCheckPageViewModel : AddPhoneCheckPageViewModel
    {
        public Phone TempOldPhone { set; get; }
    }
}
