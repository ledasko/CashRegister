using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Model
{
    class Quantity
    {
        public Quantity(Item item, float itemQuantity)
        {
            Item = item;
            ItemQuantity = itemQuantity;
        }

        Item Item { get; set; }
        float ItemQuantity { get; set; }
    }
}
