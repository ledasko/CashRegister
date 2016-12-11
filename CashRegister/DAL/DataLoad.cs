using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CashRegister.Accessories;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace CashRegister.DAL
{
    class DataLoad
    {
        public void Serialize(DataTypes dataType, object data)
        {
            string dir = @"c:\temp";
            string serializationFile = Path.Combine(dir, dataType + ".bin");

            using (Stream stream = File.Open(serializationFile, FileMode.OpenOrCreate))
            {
                var bFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bFormatter.Serialize(stream, data);
            }
        }

        public object Deserialize(DataTypes dataType)
        {
            string dir = @"c:\temp";
            string serializationFile = Path.Combine(dir, dataType + ".bin");

            using (Stream stream = File.Open(serializationFile, FileMode.OpenOrCreate))
            {
                var bFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                return bFormatter.Deserialize(stream);
            }
        }

    }
}
