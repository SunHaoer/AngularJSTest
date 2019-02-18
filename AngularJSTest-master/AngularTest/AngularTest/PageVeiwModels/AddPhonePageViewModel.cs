using AngularTest.Models;
using AngularTest.PageVeiwModels;
using System;
using System.Collections.Generic;

namespace AngularTest.VeiwModels
{
    public class AddPhonePageViewModel : BasePageViewModel
    {
        public Phone TempNewPhone { set; get; }
        public List<Brand> BrandList { set; get; }
        public List<BrandType> TypeList { set; get; }
    }
}
