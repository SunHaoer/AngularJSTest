using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularTest.PageVeiwModels
{
    public class FormFeedbackViewModel
    {
        public bool IsLogin { set; get; }
        public bool IsParameterNotEmpty { set; get; }
        public bool IsParameterLegal { set; get; }
        public bool IsSuccess { set; get; }

        public  FormFeedbackViewModel()
        {
            IsLogin = false;
            IsParameterNotEmpty = false;
            IsParameterLegal = false;
            IsSuccess = false;
        }
    }
}
