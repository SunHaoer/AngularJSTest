using AngularTest.Cache;
using AngularTest.Data;
using AngularTest.Models;
using System;
using System.Linq;
using System.Xml;

namespace AngularTest.VeiwModels
{
    public class LoginManage
    {
        private readonly UserContext _userContext;
        private readonly PhoneContext _phoneContext;
        private readonly BrandContext _brandContext;
        private readonly BrandTypeContext _brandTypeContext;
        private readonly BrandTypeProductNoContext _brandTypeProductNoContext;
        private readonly TypeYearContext _typeYearContext;
        private readonly DeleteReasonContext _deleteReasonContext;
        private readonly IQueryable<User> userIQ;
        private readonly IQueryable<Phone> phoneIQ;
        private readonly IQueryable<Brand> brandIQ;
        private readonly IQueryable<BrandType> brandTypeIQ;
        private readonly IQueryable<BrandTypeProductNo> brandTypeProductNoIQ;
        private readonly IQueryable<TypeYear> typeYearIQ;
        private readonly IQueryable<DeleteReason> deleteReasonIQ;

        public LoginManage(UserContext userContext, PhoneContext phoneContext, BrandContext brandContext, BrandTypeContext brandTypeContext, BrandTypeProductNoContext brandTypeProductNoContext, TypeYearContext typeYearContext, DeleteReasonContext deleteReasonContext)
        {
            _userContext = userContext;
            _phoneContext = phoneContext;
            _brandContext = brandContext;
            _brandTypeContext = brandTypeContext;
            _brandTypeProductNoContext = brandTypeProductNoContext;
            _typeYearContext = typeYearContext;
            _deleteReasonContext = deleteReasonContext;
            userIQ = _userContext.Users;
            phoneIQ = _phoneContext.Phones;
            brandIQ = _brandContext.Brands;
            brandTypeIQ = _brandTypeContext.BrandTypes;
            brandTypeProductNoIQ = _brandTypeProductNoContext.BrandTypeProductNos;
            typeYearIQ = _typeYearContext.TypeYears;
            deleteReasonIQ = _deleteReasonContext.DeleteReasons;
        }

        public void SetInitData()
        {
            if(userIQ.Count() == 0)
            {
                _userContext.Add(new User("admin", "root"));
                _userContext.Add(new User("dillon", "sunhao"));
                _userContext.SaveChanges();
            }
        }

        public LoginPageViewModel Login(string username, string password)
        {
            LoginPageViewModel model = new LoginPageViewModel
            {
                IsVisitLegal = true
            };
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                User user = GetLoginUser(username, password);
                if (user != null)
                {
                    model.IsLegal = true;
                    model.LoginUserSessionString = user.ToSessionString();
                    SetInitDataBase(user.Id);
                    model.IsLogin = true;
                }
            }
            return model;
        }

        private User GetLoginUser(string username, string password)
        {
            return userIQ.FirstOrDefault(item => item.Username.Equals(username) && item.Password.Equals(password));
        }

        private void SetInitDataBase(long userId)
        {
            InitPhoneDataBase();
            InitBrandDataBase();
            InitBrandTypeDataBase();
            InitBrandTypeProductNoDataBase();
            InitTypeYearDataBase();
            InitDeleteReasonDataBase();
            InitUserTempPhone(userId);
            InitUserStepTable(userId);
        }

        private void InitPhoneDataBase()
        {
            if (phoneIQ.Count() == 0)
            {
                _phoneContext.Phones.Add(new Phone { PhoneUser = "admin", UserId = 1, Brand = "HUAWEI", Type = "Mate20", ProductNo = "HUAWEIMate201", StartDate = new DateTime(2018, 11, 2), EndDate = new DateTime(2019, 11, 2), State = 1 });
                _phoneContext.Phones.Add(new Phone { PhoneUser = "admin", UserId = 1, Brand = "IPHONE", Type = "iphoneX", ProductNo = "IPHONEiphoneX1", StartDate = new DateTime(2018, 01, 09), EndDate = new DateTime(2019, 01, 09), State = 1 });
                _phoneContext.Phones.Add(new Phone { PhoneUser = "admin", UserId = 1, Brand = "HUAWEI", Type = "MateRS", ProductNo = "HUAWEIMateRS1", StartDate = new DateTime(2017, 11, 25), EndDate = new DateTime(2019, 11, 25), State = 1 });
                _phoneContext.Phones.Add(new Phone { PhoneUser = "admin", UserId = 1, Brand = "HUAWEI", Type = "Mate20", ProductNo = "HUAWEIMate201", StartDate = new DateTime(2018, 11, 25), EndDate = new DateTime(2019, 11, 25), State = 1 });
                _phoneContext.SaveChanges();
            }
        }

        private void InitBrandDataBase()
        {
            if (brandIQ.Count() == 0)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@".\phones\phonesDetail.xml");
                XmlNode root = doc.SelectSingleNode("Detail");
                XmlNodeList brands = root.ChildNodes;
                foreach (XmlNode brand in brands)
                {
                    string brandName = brand.Name;
                    _brandContext.Add(new Brand { BrandName = brandName });
                }
                _brandContext.SaveChanges();
            }
        }

        private void InitBrandTypeDataBase()
        {
            if (brandTypeIQ.Count() == 0)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@".\phones\phonesDetail.xml");
                XmlNode root = doc.SelectSingleNode("Detail");
                XmlNodeList brands = root.ChildNodes;
                foreach (XmlNode brand in brands)
                {
                    string brandName = brand.Name;
                    XmlNodeList types = brand.ChildNodes;
                    foreach (XmlNode type in types)
                    {
                        string typeName = type.Name;
                        _brandTypeContext.Add(new BrandType { Brand = brandName, Type = typeName });
                    }
                }
                _brandTypeContext.SaveChanges();
            }
        }

        private void InitBrandTypeProductNoDataBase()
        {
            if (_brandTypeProductNoContext.BrandTypeProductNos.Count() == 0)
            {
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("HUAWEI", "Mate20", "HUAWEIMate201"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("HUAWEI", "MateRS", "HUAWEIMateRS1"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("HUAWEI", "Mate20Pro", "HUAWEIMate20Pro1"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("HUAWEI", "Nova3", "HUAWEINova31"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("OPPO", "K1", "OPPOK11"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("OPPO", "R17", "OPPOR171"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("OPPO", "A7x", "OPPOA7x1"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("OPPO", "R15x", "OPPOR15x1"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("IPHONE", "iphone6Plus", "IPHONEiphone6Plus1"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("IPHONE", "iphone6", "IPHONEiphone61"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("IPHONE", "iphone7Plus", "IPHONEiphone7Plus1"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("IPHONE", "iphoneX", "IPHONEiphoneX1"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyS8", "SAMSUNGGalaxyS81"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyNote8", "SAMSUNGGalaxyNote81"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyA8s", "SAMSUNGGalaxyA8s1"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyS9", "SAMSUNGGalaxyS91"));

                _brandTypeProductNoContext.Add(new BrandTypeProductNo("HUAWEI", "Mate20", "HUAWEIMate202"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("HUAWEI", "MateRS", "HUAWEIMateRS2"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("HUAWEI", "Mate20Pro", "HUAWEIMate20Pro2"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("HUAWEI", "Nova3", "HUAWEINova32"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("OPPO", "K1", "OPPOK12"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("OPPO", "R17", "OPPOR172"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("OPPO", "A7x", "OPPOA7x2"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("OPPO", "R15x", "OPPOR15x2"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("IPHONE", "iphone6Plus", "IPHONEiphone6Plus2"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("IPHONE", "iphone6", "IPHONEiphone62"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("IPHONE", "iphone7Plus", "IPHONEiphone7Plus2"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("IPHONE", "iphoneX", "IPHONEiphoneX2"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyS8", "SAMSUNGGalaxyS82"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyNote8", "SAMSUNGGalaxyNote82"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyA8s", "SAMSUNGGalaxyA8s2"));
                _brandTypeProductNoContext.Add(new BrandTypeProductNo("SAMSUNG", "GalaxyS9", "SAMSUNGGalaxyS92"));
                _brandTypeProductNoContext.SaveChanges();
            }
        }

        private void InitTypeYearDataBase()
        {
            if (_typeYearContext.TypeYears.Count() == 0)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@".\phones\phonesDetail.xml");
                XmlNode root = doc.SelectSingleNode("Detail");
                XmlNodeList brands = root.ChildNodes;
                foreach (XmlNode brand in brands)
                {
                    XmlNodeList types = brand.ChildNodes;
                    foreach (XmlNode type in types)
                    {
                        string typeName = type.Name;
                        int year = int.Parse(type.InnerText);
                        _typeYearContext.Add(new TypeYear { Type = typeName, Year = year });
                    }
                }
                _typeYearContext.SaveChanges();
            }
        }

        private void InitDeleteReasonDataBase()
        {
            if(_deleteReasonContext.DeleteReasons.Count() == 0)
            {
                _deleteReasonContext.Add(new DeleteReason("lost"));
                _deleteReasonContext.Add(new DeleteReason("buy new phone"));
                _deleteReasonContext.Add(new DeleteReason("time end"));
                _deleteReasonContext.Add(new DeleteReason("not interested"));
                _deleteReasonContext.Add(new DeleteReason("other"));
                _deleteReasonContext.SaveChanges();
            }
        }

        private void InitUserTempPhone(long userId)
        {
            if(TempPhone.GetTempNewPhoneByUserId(userId) == null)
            {
                TempPhone.tempNewPhone.Add(userId, new Phone());
            }
        }

        private void InitUserStepTable(long userId)
        {
            Step.InitStepTable();
        }

    }
}
