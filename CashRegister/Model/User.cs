using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Model
{
    class User : EntityBase<int>
    {
        public User(string username, string password) : base(0)
        {
            Username = username;
            Password = password;
        }
        string Username { get; set; }
        string Password { get; set; }
    }
}
