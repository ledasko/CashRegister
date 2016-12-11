using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Model
{
    [Serializable]
    class Receipt : EntityBase<int>
    {
        public Receipt(DateTime dateOfPurchase, List<Quantity> receiptList) : base(0)
        {
            DateOfPurchase = dateOfPurchase;
            ReceiptList = receiptList;
        }
        DateTime DateOfPurchase { get; set; }
        List<Quantity> ReceiptList { get; set; }
        float Total { get; set; }

    }
}
