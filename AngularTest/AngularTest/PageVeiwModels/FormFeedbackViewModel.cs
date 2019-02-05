using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularTest.PageVeiwModels
{
    public class FormFeedbackViewModel : BasePageViewModel
    {
        public bool IsParameterNotEmpty { set; get; }
        public bool IsParameterLegal { set; get; }
        public bool IsSuccess { set; get; }

        public  FormFeedbackViewModel()
        {
            IsParameterNotEmpty = false;
            IsParameterLegal = false;
            IsSuccess = false;
        }
    }
}
