using AngularTest.Cache;
using AngularTest.PageVeiwModels;

namespace AngularTest.ViewModelManage
{
    public class SuccessPageManage
    {
        public SuccessPageViewModel GetSuccessPageViewModel(long userId, int nowNode, int isSubmit)
        {
            SuccessPageViewModel model = new SuccessPageViewModel()
            {
                IsLogin = true
            };
            if (Step.stepTable[nowNode * isSubmit, Step.successPage] || nowNode == Step.successPage)
            {
                model.IsVisitLegal = true;
                TempPhone.SetTempPhoneEmpty(userId);
            }
            return model;
        }
    }
}
