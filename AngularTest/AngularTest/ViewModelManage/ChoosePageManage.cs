using AngularTest.Cache;
using AngularTest.Models;
using AngularTest.PageVeiwModels;
using AngularTest.Service;
using AngularTest.Utils;
using System;

namespace AngularTest.VeiwModels
{
    public class ChoosePageManage
    {
        private PhoneService phoneService;

        public ChoosePageManage(PhoneContext phoneContext)
        {
            phoneService = new PhoneService(phoneContext);
        }

        public ChoosePageViewModel GetChoosePageViewModel(string userInfo, int nowNode, int isSubmit, int pageIndex, int pageSize)
        {
            ChoosePageViewModel model = new ChoosePageViewModel
            {
                IsLogin = true
            };
            if (Step.stepTable[nowNode * isSubmit, Step.choosePage] || nowNode == Step.choosePage)
            {
                long userId = long.Parse(userInfo.Split(",")[0]);
                model.IsVisitLegal = true;
                model.LoginUsername = userInfo.Split(",")[1];
                SetTempPhoneEmpty(userId);
                model.PhoneList = phoneService.GetPhoneList(userId, pageIndex, pageSize);
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
                if (phoneService.ValidateIdInAbandonOrDelete(id, userId) && Validation.IsDateLegal(abandonDate))
                {
                    model.IsParameterNotEmpty = true;
                    model.IsParameterLegal = true;
                    Phone phone = phoneService.GetPhoneById(id);
                    phone = UpdatePhoneState(phone, abandonDate, 2);
                    phoneService.UpdatePhoneStateInDB(phone);
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
                if (phoneService.ValidateIdInAbandonOrDelete(id, userId) && Validation.IsDateLegal(startDate))
                {
                    model.IsParameterNotEmpty = true;
                    model.IsParameterLegal = true;
                    Phone phone = phoneService.GetPhoneById(id);
                    phone = UpdatePhoneState(phone, startDate, 1);
                    phoneService.UpdatePhoneStateInDB(phone);
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
                if (phoneService.ValidateIdInReplace(id, userId))
                {
                    model.IsParameterNotEmpty = true;
                    model.IsParameterLegal = true;
                    Phone phone = phoneService.GetPhoneById(id);
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
                if (phoneService.ValidateIdInAbandonOrDelete(id, userId))
                {
                    model.IsParameterNotEmpty = true;
                    model.IsParameterLegal = true;
                    Phone phone = phoneService.GetPhoneById(id);
                    TempPhone.SetTempNewPhoneByUserId(userId, phone);
                    model.IsSuccess = true;
                }
            }
            return model;
        }

        //public FormFeedbackViewModel SetIsSubmit(long userId, int nowNode)
        //{
        //    FormFeedbackViewModel model = new FormFeedbackViewModel()
        //    {
        //        IsLogin = true
        //    };
        //    if (Step.stepTable[nowNode, Step.choosePageSubmit])
        //    {
        //        model.IsVisitLegal = true;
        //        model.IsParameterNotEmpty = true;
        //        model.IsParameterLegal = true;
        //        model.IsSuccess = true;
        //    }
        //    return model;
        //}

        //private IQueryable<Phone> GetPhoneIQByUserId(long userId)
        //{
        //    return phoneIQ.Where(item => item.UserId == userId);
        //}

        //private PaginatedList<Phone> GetPhoneList(long userId, int pageIndex, int pageSize)
        //{
        //    IQueryable<Phone> phoneIQ = GetPhoneIQByUserId(userId);
        //    return PaginatedList<Phone>.Create(phoneIQ, pageIndex, pageSize);
        //}

        private void SetTempPhoneEmpty(long userId)
        {
            TempPhone.SetTempNewPhoneByUserId(userId, new Phone());
            TempPhone.SetTempOldPhoneByUserId(userId, new Phone()); 
        }

        //private bool ValidateIdInAbandonOrDelete(long id, long loginUserId)
        //{
        //    return phoneIQ.Any(item => item.Id == id && item.UserId == loginUserId && item.State != 3);
        //}

        //private bool ValidateIdInReplace(long id, long loginUserId)
        //{
        //    return phoneIQ.Any(item => item.Id == id && item.UserId == loginUserId && item.State == 1);
        //}

        //private Phone GetPhoneById(long id)
        //{
        //    return phoneIQ.FirstOrDefault(item => item.Id == id);
        //}

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

        //private void UpdatePhoneStateInDB(Phone phone)
        //{
        //    _phoneContext.Update(phone);
        //    _phoneContext.SaveChanges();
        //}

    }
}
