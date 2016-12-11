using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CashRegister.Model;

namespace CashRegister.Repository
{
    interface IReceiptRepository
    {
        void Add(Receipt receipt);
        void Update(Receipt receipt);
        void Remove(Receipt receipt);
        void Save();

        Receipt GetById(int receiptId);
        List<Receipt> GetByDate(DateTime receiptDate);
    }
}
