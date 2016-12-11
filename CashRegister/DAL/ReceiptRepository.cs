using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashRegister.Model;
using CashRegister.Repository;
using System.IO;

namespace CashRegister.DAL
{
    class ReceiptRepository : IReceiptRepository
    {
        private static List<Receipt> _receipts = new List<Receipt>();
        private static int _nextId = 1;
        public void Add(Receipt receipt)
        {
            if (_receipts.Any(r => r.DateOfPurchase == receipt.DateOfPurchase))
            {
                throw new ReceiptAlreadyExists();
            }

            _receipts.Add(receipt);
            Save();
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
            using (Stream stream = File.Open("receipt.bin", FileMode.Create))
            {
                var bFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bFormatter.Serialize(stream, _receipts);
            }
        }

        public void Update(Receipt receipt)
        {
            throw new NotImplementedException();
        }

        public void LoadAll()
        {
            using (Stream stream = File.Open("receipt.bin", FileMode.Open))
            {
                var bFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                _receipts = (List<Receipt>)bFormatter.Deserialize(stream);

                int t = 0;
                foreach (Receipt r in _receipts)
                {
                    if (r.Id > t)
                    {
                        t = r.Id;
                    }

                    _nextId = t + 1;
                }
            }
        }

        public List<Receipt> GetReceiptList()
        {
            LoadAll();
            return _receipts;
        }

        public int GetFollowingId()
        {
            return _nextId++;
        }
    }
}
