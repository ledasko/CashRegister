using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CashRegister.Accessories;

namespace CashRegister.Model
{
    /// <summary>
    /// Model representing an item to be sold in the store. Each item has a name, price, tax and volume which is used to categorize how the item is sold.
    /// </summary>
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

        public string ToString()
        {
            return Volume.Equals(Quantify.KG)
                ? (Id + ". " + Name + " - " + Price + "$ (Tax: " + TaxModifier + "%) Measured in kilograms.")
                : (Id + ". " + Name + " - " + Price + "$ (Tax: " + TaxModifier + "%) Measured in pieces.");
        }
    }
}
