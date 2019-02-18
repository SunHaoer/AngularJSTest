using AngularTest.Cache;
using AngularTest.Data;
using AngularTest.Models;
using AngularTest.PageVeiwModels;
using AngularTest.Dao;
using AngularTest.VeiwModels;

namespace AngularTest.ViewModelManage
{
    public class AddPhoneCheckManage
    {
        protected PhoneDao phoneDao; 

        public AddPhoneCheckManage(PhoneContext phoneContext)
        {
            phoneDao = new PhoneDao(phoneContext);
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
                    phoneDao.SetTempNewPhoneToDBByUserId(userId);
                    model.IsSuccess = true;
                }
            }
            return model;
        }

    }
}

