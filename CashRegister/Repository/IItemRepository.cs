using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CashRegister.Model;

namespace CashRegister.Repository
{
    interface IItemRepository
    {
        void Add(Item item);
        void Update(Item item);
        void Remove(Item item);

        Item GetById(int itemId);
        Item GetByName(string itemName);
    }
}
