using AngularTest.Cache;
using AngularTest.Models;
using AngularTest.PageVeiwModels;
using AngularTest.Service;
using AngularTest.VeiwModels;

namespace AngularTest.ViewModelManage
{
    public class ReplacePhoneCheckManage : AddPhoneCheckManage
    {

        public ReplacePhoneCheckManage(PhoneContext phoneContext) : base(phoneContext)
        {
            phoneService = new PhoneService(phoneContext);
        }

        public ReplacePhoneCheckPageViewModel GetReplacePhoneCheckPageViewModel(long userId, int nowNode, int isSubmit)
        {
            ReplacePhoneCheckPageViewModel model = new ReplacePhoneCheckPageViewModel()
            {
                IsLogin = true
            };
            if (Step.stepTable[nowNode * isSubmit, Step.replacePhoneCheck] || nowNode == Step.replacePhoneCheck)
            {
                model.IsVisitLegal = true;
                model.TempNewPhone = TempPhone.GetTempNewPhoneByUserId(userId);
                model.TempOldPhone = TempPhone.GetTempOldPhoneByUserId(userId);
            }
            return model;
        }

        public new FormFeedbackViewModel SubmitMsg(long userId, int nowNode)
        {
            FormFeedbackViewModel model = new FormFeedbackViewModel()
            {
                IsLogin = true
            };
            if (Step.stepTable[nowNode, Step.replacePhoneCheckSubmit])
            {
                model.IsVisitLegal = true;
                model.IsParameterNotEmpty = true;
                if (TempPhone.IsTempNewPhoneNotEmpty(userId) && TempPhone.IsTempOldPhoneNotEmpty(userId))
                {
                    model.IsParameterLegal = true;
                    phoneService.SetTempNewPhoneToDBByUserId(userId);
                    phoneService.SetTemoOldPhoneAbandonToDBByUserId(userId);
                    model.IsSuccess = true;
                }
            }
            return model;
        }

    }
}

