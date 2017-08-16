using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;

namespace iFramework.Util
{
    public class BinFileHelper
    {
        /// <summary>
        /// BinaryFormatter序列化
        /// </summary>
        /// <param name="obj">对象</param>
        public static void SaveToBinFile<T>(T obj, string FileName)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream ms = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                formatter.Serialize(ms, obj);
                ms.Flush();
                ms.Close();
            }
        }
        
        /// <summary>
        /// BinaryFormatter反序列化
        /// </summary>
        /// <param name="str">字符串序列</param>
        public static T LoadFromBinFile<T>(string FileName)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream ms = new FileStream(FileName, FileMode.Open, FileAccess.Read))
            {
                return (T)formatter.Deserialize(ms);
            }
        }
    }
}
