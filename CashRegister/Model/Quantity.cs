using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Model
{
    /// <summary>
    /// Used to define the quantity of items sold per receipt.
    /// </summary>
    [Serializable]
    class Quantity
    {
        public Quantity(Item item, float itemQuantity)
        {
            Item = item;
            ItemQuantity = itemQuantity;
        }

        Item Item { get; set; }
        float ItemQuantity { get; set; }

        public string ToString()
        {
            return Item.Name + " - " + ItemQuantity;
        }
    }
}
