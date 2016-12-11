using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CashRegister.Model;

namespace CashRegister.Repository
{
    interface IUserRepository
    {
        void Add(User user);
        void Save();

        User GetByUsername(string username);
        List<User> GetAllUser();
    }
}
