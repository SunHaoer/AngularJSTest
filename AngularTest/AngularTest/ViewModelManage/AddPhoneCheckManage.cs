using AngularTest.Cache;
using AngularTest.Data;
using AngularTest.Models;
using AngularTest.PageVeiwModels;
using AngularTest.Service;
using AngularTest.VeiwModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularTest.ViewModelManage
{
    public class AddPhoneCheckManage
    {
        protected PhoneService phoneService; 

        public AddPhoneCheckManage(PhoneContext phoneContext)
        {
            phoneService = new PhoneService(phoneContext);
        }

        public AddPhoneCheckPageViewModel GetAddPhoneCheckPageViewModel(long userId, int nowNode, int isSubmit)
        {
            AddPhoneCheckPageViewModel model = new AddPhoneCheckPageViewModel
            {
                IsLogin = true
            };
            if (Step.stepTable[nowNode * isSubmit, Step.addPhoneCheck] || nowNode == Step.addPhoneCheck)
            {
                model.IsVisitLegal = true;
                model.TempNewPhone = TempPhone.GetTempNewPhoneByUserId(userId);
            }
            return model;
        }

        public FormFeedbackViewModel SubmitMsg(long userId, int nowNode)
        {
            FormFeedbackViewModel model = new FormFeedbackViewModel()
            {
                IsLogin = true
            };
            if (Step.stepTable[nowNode, Step.addPhoneCheckSubmit])
            {
                model.IsVisitLegal = true;
                model.IsParameterNotEmpty = true;
                if (TempPhone.IsTempNewPhoneNotEmpty(userId))
                {
                    model.IsParameterLegal = true;
                    phoneService.SetTempNewPhoneToDBByUserId(userId);
                    model.IsSuccess = true;
                }
            }
            return model;
        }

    }
}

