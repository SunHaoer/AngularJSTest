using AngularTest.Cache;
using AngularTest.Data;
using AngularTest.Models;
using System;
using System.Linq;
using System.Xml;

namespace AngularTest.Service
{
    public class LoginService
    {
        private readonly UserContext _userContext;
        private readonly PhoneContext _phoneContext;
        private readonly BrandContext _brandContext;
        private readonly BrandTypeContext _brandTypeContext;
        private readonly BrandTypeProductNoContext _brandTypeProductNoContext;
        private readonly IQueryable<User> userIQ;
        private readonly IQueryable<Phone> phoneIQ;
        private readonly IQueryable<Brand> brandIQ;
        private readonly IQueryable<BrandType> brandTypeIQ;
        private readonly IQueryable<BrandTypeProductNo> brandTypeProductNoIQ;

        public LoginService(UserContext userContext, PhoneContext phoneContext, BrandContext brandContext, BrandTypeContext brandTypeContext, BrandTypeProductNoContext brandTypeProductNoContext,IQueryable<User> userIQ, IQueryable<Phone> phoneIQ, IQueryable<Brand> brandIQ, IQueryable<BrandType> brandTypeIQ, IQueryable<BrandTypeProductNo> brandTypeProductNoIQ)
        {
            _userContext = userContext;
            _phoneContext = phoneContext;
            _brandContext = brandContext;
            _brandTypeContext = brandTypeContext;
            _brandTypeProductNoContext = brandTypeProductNoContext;
            this.userIQ = userIQ;
            this.phoneIQ = phoneIQ;
            this.brandIQ = brandIQ;
            this.brandTypeIQ = brandTypeIQ;
            this.brandTypeProductNoIQ = brandTypeProductNoIQ;
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

        public User GetLoginUser(string username, string password)
        {
            return userIQ.FirstOrDefault(item => item.Username.Equals(username) && item.Password.Equals(password));
        }

        public void SetInitDataBase(long userId)
        {
            InitPhoneDataBase();
            InitBrandDataBase();
            InitBrandTypeDataBase();
            InitBrandTypeProductNoDataBase();
            InitUserTempPhone(userId);
        }

        private void InitPhoneDataBase()
        {
            if (phoneIQ.Count() == 0)
            {
                _phoneContext.Phones.Add(new Phone { PhoneUser = "admin", UserId = 1, Brand = "HUAWEI", Type = "Mate20", ProductNo = "HUAWEIMate201", StartDate = new DateTime(2018, 11, 2), EndDate = new DateTime(2019, 11, 25), State = 1 });
                _phoneContext.Phones.Add(new Phone { PhoneUser = "admin", UserId = 1, Brand = "IPHONE", Type = "X", ProductNo = "IPHONEX1", StartDate = new DateTime(2018, 01, 09), EndDate = new DateTime(2019, 01, 09), State = 3 });
                _phoneContext.Phones.Add(new Phone { PhoneUser = "admin", UserId = 1, Brand = "HUAWEI", Type = "MateRS", ProductNo = "HUAWEIMateRS1", StartDate = new DateTime(2017, 11, 25), EndDate = new DateTime(2019, 11, 25), State = 2 });
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

        public void InitBrandTypeDataBase()
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

        private void InitUserTempPhone(long userId)
        {
            if(TempPhone.GetTempNewPhoneByUserId(userId) == null)
            {
                TempPhone.tempNewPhone.Add(userId, new Phone());
            }
        }
    }
}
