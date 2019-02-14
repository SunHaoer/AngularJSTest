using AngularTest.Cache;
using AngularTest.Data;
using AngularTest.Models;
using AngularTest.PageVeiwModels;
using AngularTest.Dao;
using AngularTest.Utils;
using System;
using System.Linq;

namespace AngularTest.VeiwModels
{
    public class DeletePhoneManage
    {
        private DeleteReasonDao deleteReasonDao;

        public DeletePhoneManage(DeleteReasonContext deleteReasonContext)
        {
            deleteReasonDao = new DeleteReasonDao(deleteReasonContext);
        }

        public DeletePhonePageViewModel GetDeletePhonePageViewModel(long userId, int nowNode, int isSubmit)
        {
            DeletePhonePageViewModel model = new DeletePhonePageViewModel()
            {
                IsLogin = true
            };
            if (Step.stepTable[nowNode * isSubmit, Step.deletePhone] || nowNode == Step.deletePhone || nowNode == Step.deletePhoneCheck)
            {
                model.IsVisitLegal = true;
                model.TempNewPhone = TempPhone.GetTempNewPhoneByUserId(userId);
                model.DeleteReasonList = deleteReasonDao.GetDeleteReason();
            }
            return model;
        }

        public FormFeedbackViewModel SubmitMsg(long userId, int nowNode, string deleteReason, DateTime deleteDate, int state)
        {
            FormFeedbackViewModel model = new FormFeedbackViewModel()
            {
                IsLogin = true
            };
            if (Step.stepTable[nowNode, Step.deletePhoneSubmit])
            {
                model.IsVisitLegal = true;
                if (!string.IsNullOrEmpty(deleteReason) && (state == 1 || state == 2))
                {
                    model.IsParameterNotEmpty = true;
                    if (Validation.IsDateNotBeforeToday(deleteDate))
                    {
                        model.IsParameterLegal = true;
                        deleteReason = deleteReason.Trim();
                        SetTempNewPhoneDeleteByUserId(userId, deleteReason, deleteDate, state);
                        model.IsSuccess = true;
                    }
                }
            }
            return model;
        }

        private void SetTempNewPhoneDeleteByUserId(long userId, string deleteReason, DateTime deleteDate, int state)
        {
            Phone phone = TempPhone.GetTempNewPhoneByUserId(userId);
            phone.DeleteReason = deleteReason;
            phone.DeleteDate = deleteDate;
            phone.State = state;
            TempPhone.SetTempNewPhoneByUserId(userId, phone);
        }

    }
}
