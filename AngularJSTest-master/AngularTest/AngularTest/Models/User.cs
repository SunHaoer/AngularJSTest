using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularTest.Models
{
    public class User
    {
        public long Id { set; get; }
        public string Username { set; get; }
        public string Password { set; get; }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string ToSessionString()
        {
            return Id + "," + Username;
        }
    }
}
