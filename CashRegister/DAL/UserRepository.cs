using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashRegister.Model;
using CashRegister.Repository;
using System.IO;

namespace CashRegister.DAL
{
    class UserRepository : IUserRepository
    {
        private static List<User> _users = new List<User>();
        private static int _nextId = 1;

        public void Add(User user)
        {
            if (_users.Any(u => u.Username == user.Username))
            {
                throw new UsernameAlreadyExists();
            }

            _users.Add(user);
            Save();
        }

        public void GetAllUser()
        {
            using (Stream stream = File.Open("user.bin", FileMode.Open))
            {
                var bFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                _users = (List<User>)bFormatter.Deserialize(stream);

                int t = 0;
                foreach (User u in _users)
                {
                    if (u.Id > t)
                    {
                        t = u.Id;
                    }

                    _nextId = t + 1;
                }
            }
        }

        public User GetByUsername(string username)
        {
            GetAllUser();

            foreach (User u in _users)
            {
                if (u.Username.Equals(username))
                {
                    return u;
                }
            }

            return null;
        }

        public void Save()
        {
            using (Stream stream = File.Open("user.bin", FileMode.OpenOrCreate))
            {
                var bFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bFormatter.Serialize(stream, _users);
            }
        }

        public int GetFollowingId()
        {
            return _nextId++;
        }
    }
}
