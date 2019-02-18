using AngularTest.Cache;
using AngularTest.Models;
using AngularTest.PageVeiwModels;
using AngularTest.VeiwModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularTest.Utils;

namespace AngularTest.ViewModelManage
{
    public class DeletePhoneCheckManage
    {
        private PhoneContext _phoneContext;
        private IQueryable<Phone> phoneIQ;

        public DeletePhoneCheckManage()
        {
        }

        public DeletePhoneCheckPageViewModel GetDeletePhoneCheckPageViewModel(long userId, int nowNode, int isSubmit)
        {
            DeletePhoneCheckPageViewModel model = new DeletePhoneCheckPageViewModel()
            {
                IsLogin = true
            };
            if (Step.stepTable[nowNode * isSubmit, Step.deletePhoneCheck] || nowNode == Step.deletePhoneCheck)
            {
                model.IsVisitLegal = true;
                model.TempNewPhone = TempPhone.GetTempNewPhoneByUserId(userId);
            }
            return model;
        }

        public FormFeedbackViewModel SubmitMsg(long userId, int nowNode)
        {
            FormFeedbackViewModel model = new FormFeedbackViewModel();
            if (Step.stepTable[nowNode, Step.deletePhoneCheckSubmit])
            {
                model.IsVisitLegal = true;
                model.IsParameterNotEmpty = true;
                if (TempPhone.IsTempNewPhoneNotEmpty(userId))
                {
                    model.IsParameterLegal = true;
                    SetTempNewPhoneToDBByUserId(userId);
                    model.IsSuccess = true;
                }
            }
            return model;
        }

        public DeletePhoneCheckManage(PhoneContext phoneContext)
        {
            _phoneContext = phoneContext;
            phoneIQ = phoneContext.Phones;
        }

        public void SetTempNewPhoneToDBByUserId(long userId)
        {
            Phone phone = TempPhone.GetTempNewPhoneByUserId(userId);
            phone.State = Consts.DELETED;
            _phoneContext.Update(phone);
            _phoneContext.SaveChanges();
            TempPhone.SetTempNewPhoneByUserId(userId, new Phone());
        }

    }
}
