using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularTest.PageVeiwModels
{
    public class BasePageViewModel
    {
        public bool IsLogin { set; get; }
        public bool IsParameterNotEmpty { set; get; }
        public bool IsParameterLegal { set; get; }
    }
}
