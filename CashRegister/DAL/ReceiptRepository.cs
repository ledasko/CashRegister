using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashRegister.Model;
using CashRegister.Repository;

namespace CashRegister.DAL
{
    class ReceiptRepository : IReceiptRepository
    {
        public void Add(Receipt receipt)
        {
            throw new NotImplementedException();
        }

        public List<Receipt> GetByDate(DateTime receiptDate)
        {
            throw new NotImplementedException();
        }

        public Receipt GetById(int receiptId)
        {
            throw new NotImplementedException();
        }

        public void Remove(Receipt receipt)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Receipt receipt)
        {
            throw new NotImplementedException();
        }
    }
}
