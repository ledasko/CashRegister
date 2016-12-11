using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CashRegister.Accessories;

namespace CashRegister.Model
{
    class Item : EntityBase<int>
    {
        public Item(string name, float price, int taxModifier) : base(0)
        {
            Name = name;
            Price = price;
            TaxModifier = taxModifier;

        }

        string Name { get; set; }
        float Price { get; set; }
        int TaxModifier { get; set; }
        Quantify Volume { get; set; }
    }
}
