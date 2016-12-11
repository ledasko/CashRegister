using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CashRegister.Accessories;

namespace CashRegister.Model
{
    [Serializable]
    class User : EntityBase<int>
    {
        public User(int id, string username, string password, UserType userType) : base(id)
        {
            Username = username;
            Password = password;
            Type = userType;
        }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserType Type { get; set; }
    }
}
