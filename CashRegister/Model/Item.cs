using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CashRegister.Accessories;

namespace CashRegister.Model
{
    [Serializable]
    class Item : EntityBase<int>
    {
        public Item(int id, string name, float price, int taxModifier, Quantify volume) : base(id)
        {
            Name = name;
            Price = price;
            TaxModifier = taxModifier;
            Volume = volume;
        }

        public string Name { get; set; }
        public float Price { get; set; }
        public int TaxModifier { get; set; }
        public Quantify Volume { get; set; }
    }
}
