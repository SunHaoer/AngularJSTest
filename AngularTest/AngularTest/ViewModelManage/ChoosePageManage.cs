using AngularTest.Cache;
using AngularTest.Models;
using AngularTest.PageVeiwModels;
using AngularTest.Dao;
using AngularTest.Utils;
using System;

namespace AngularTest.VeiwModels
{
    public class ChoosePageManage
    {
        private PhoneDao phoneDao;

        public ChoosePageManage(PhoneContext phoneContext)
        {
            phoneDao = new PhoneDao(phoneContext);
        }

        public ChoosePageViewModel GetChoosePageViewModel(string userInfo, int nowNode, int isSubmit, int pageIndex, int pageSize)
        {
            ChoosePageViewModel model = new ChoosePageViewModel
            {
                IsLogin = true
            };
            if (Step.stepTable[nowNode * isSubmit, Step.choosePage] || nowNode == Step.choosePage || nowNode == Step.addPhone || nowNode == Step.replacePhone || nowNode == Step.deletePhone)
            {
                long userId = long.Parse(userInfo.Split(",")[0]);
                model.IsVisitLegal = true;
                model.LoginUsername = userInfo.Split(",")[1];
                SetTempPhoneEmpty(userId);
                model.PhoneList = phoneDao.GetPhoneList(userId, pageIndex, pageSize);
            }
            return model;
        }

        public FormFeedbackViewModel SetUsingToAbandonById(long userId, int nowNode, long id, DateTime abandonDate)
        {
            FormFeedbackViewModel model = new FormFeedbackViewModel()
            {
                IsLogin = true
            };
            if (Step.stepTable[nowNode, Step.choosePageSubmit])
            {
                model.IsVisitLegal = true;
                if (phoneDao.ValidateIdInAbandonOrDelete(id, userId) && Validation.IsDateLegal(abandonDate))
                {
                    model.IsParameterNotEmpty = true;
                    model.IsParameterLegal = true;
                    Phone phone = phoneDao.GetPhoneById(id);
                    phone = UpdatePhoneState(phone, abandonDate, 2);
                    phoneDao.UpdatePhoneStateInDB(phone);
                    model.IsSuccess = true;
                }
            }
            return model;
        }

        public FormFeedbackViewModel SetAbanddonToUsingById(long userId, int nowNode, long id, DateTime startDate)
        {
            FormFeedbackViewModel model = new FormFeedbackViewModel()
            {
                IsLogin = true
            };
            if (Step.stepTable[nowNode, Step.choosePageSubmit])
            {
                model.IsVisitLegal = true;
                if (phoneDao.ValidateIdInAbandonOrDelete(id, userId) && Validation.IsDateLegal(startDate))
                {
                    model.IsParameterNotEmpty = true;
                    model.IsParameterLegal = true;
                    Phone phone = phoneDao.GetPhoneById(id);
                    phone = UpdatePhoneState(phone, startDate, 1);
                    phoneDao.UpdatePhoneStateInDB(phone);
                    model.IsSuccess = true;
                }
            }
            return null;
        }

        public FormFeedbackViewModel SetTempOldPhoneById(long userId, int nowNode, long id)
        {
            FormFeedbackViewModel model = new FormFeedbackViewModel()
            {
                IsLogin = true
            };
            if (Step.stepTable[nowNode, Step.choosePageSubmit])
            {
                model.IsVisitLegal = true;
                if (phoneDao.ValidateIdInReplace(id, userId))
                {
                    model.IsParameterNotEmpty = true;
                    model.IsParameterLegal = true;
                    Phone phone = phoneDao.GetPhoneById(id);
                    TempPhone.SetTempOldPhoneByUserId(userId, phone);
                    model.IsSuccess = true;
                }
            }
            return model;
        }

        public FormFeedbackViewModel SetTempNewPhoneById(long userId, int nowNode, long id)
        {
            FormFeedbackViewModel model = new FormFeedbackViewModel()
            {
                IsLogin = true
            };
            if (Step.stepTable[nowNode, Step.choosePageSubmit])
            {
                model.IsVisitLegal = true;
                if (phoneDao.ValidateIdInAbandonOrDelete(id, userId))
                {
                    model.IsParameterNotEmpty = true;
                    model.IsParameterLegal = true;
                    Phone phone = phoneDao.GetPhoneById(id);
                    TempPhone.SetTempNewPhoneByUserId(userId, phone);
                    model.IsSuccess = true;
                }
            }
            return model;
        }

        private void SetTempPhoneEmpty(long userId)
        {
            TempPhone.SetTempNewPhoneByUserId(userId, new Phone());
            TempPhone.SetTempOldPhoneByUserId(userId, new Phone()); 
        }

        private Phone UpdatePhoneState(Phone phone, DateTime date, int newState)
        {
            phone.State = newState;
            if(newState == 2)
            {
                phone.AbandonDate = date;
            }
            else
            {
                phone.StartDate = date;
                phone.AbandonDate = new DateTime();
            }
            return phone;
        }

    }
}
