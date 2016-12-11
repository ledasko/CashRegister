using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CashRegister.Model;
using CashRegister.Repository;
using CashRegister.Accessories;
using System.IO;

namespace CashRegister.DAL
{
    class ItemRepository : IItemRepository
    {
        private static List<Item> _items = new List<Item>();
        private static int _nextId = 1;
        public void Add(Item item)
        {
            GetAll();

            if (_items.Any(i => i.Name == item.Name))
            {
                throw new ItemAlreadyExists();
            }

            _items.Add(item);
            Save();
        }

        public Item GetById(int itemId)
        {
            throw new NotImplementedException();
        }

        public Item GetByName(string itemName)
        {
            throw new NotImplementedException();
        }

        public void Remove(Item item)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            Console.WriteLine("Saving..");
            using (Stream stream = File.Open("item.bin", FileMode.OpenOrCreate))
            {
                var bFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bFormatter.Serialize(stream, _items);
            }
        }

        public void Update(Item item)
        {
            throw new NotImplementedException();
        }

        public void GetAll()
        {
            using (Stream stream = File.Open("item.bin", FileMode.Open))
            {
                var bFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                _items = (List<Item>)bFormatter.Deserialize(stream);

                int t = 0;
                foreach (Item i in _items)
                {
                    if (i.Id > t)
                    {
                        t = i.Id;
                    }

                    _nextId = t + 1;
                }
            }
        }

        public int GetFollowingId()
        {
            return _nextId++;
        }
    }
}
