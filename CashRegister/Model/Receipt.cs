using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Model
{
    /// <summary>
    /// Receipt which signifies a transaction made in a store.
    /// </summary>
    [Serializable]
    class Receipt : EntityBase<int>
    {
        public Receipt(int id, DateTime dateOfPurchase, List<Quantity> receiptList) : base(id)
        {
            DateOfPurchase = dateOfPurchase;
            ReceiptList = receiptList;
        }
        public DateTime DateOfPurchase { get; set; }
        public List<Quantity> ReceiptList { get; set; }
        public float Total { get; set; }

        public string ToString()
        {
            return DateOfPurchase + " - (" + ReceiptList.Count + ") ";
        }

    }
}
