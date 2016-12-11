using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CashRegister.Model;
using CashRegister.Accessories;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace CashRegister.DAL
{
    static class DataLoad
    {
        public static void Serialize(DataTypes dataType, object data)
        {
            string serializationFile = "";

            switch (dataType)
            {
                case DataTypes.ITEM:
                    serializationFile = "item.bin";
                    break;
                case DataTypes.RECEIPT:
                    serializationFile = "receipt.bin";
                    break;
                case DataTypes.USER:
                    serializationFile = "user.bin";
                    break;
            }

            using (Stream stream = File.Open(serializationFile, FileMode.OpenOrCreate))
            {
                var bFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                switch (dataType)
                {
                    case DataTypes.ITEM:
                        bFormatter.Serialize(stream, (List<Item>)data);
                        break;
                    case DataTypes.RECEIPT:
                        bFormatter.Serialize(stream, (List<Receipt>)data);
                        break;
                    case DataTypes.USER:
                        bFormatter.Serialize(stream, (List<User>)data);
                        break;
                }

                bFormatter.Serialize(stream, data);
            }
        }

        public static object Deserialize(DataTypes dataType)
        {
            string dir = @"c:\temp";
            string serializationFile = Path.Combine(dir, dataType + ".bin");

            using (Stream stream = File.Open(serializationFile, FileMode.Open))
            {
                var bFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                return bFormatter.Deserialize(stream);
            }
        }

    }
}
