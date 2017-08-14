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
    /// <summary>
    /// zip文件压缩与解压方法，压缩与解压对象必须保持一致
    /// </summary>
    public class ZipFileHelper
    {
        /// <summary>
        /// 导出对象到zip文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Obj"></param>
        /// <param name="FileName"></param>
        public static void SaveToZipFile<T>(T Obj, string FileName)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream ms = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                GZipStream theStream = new GZipStream(ms,CompressionLevel.Optimal);
                formatter.Serialize(theStream, Obj);
                theStream.Flush();
                theStream.Close();
            }
        }

        /// <summary>
        /// 从zip文件导入对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static T LoadFromZipFile<T>(string FileName)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream ms = new FileStream(FileName, FileMode.Open, FileAccess.Read))
            {
                GZipStream theStream = new GZipStream(ms,CompressionMode.Decompress,false);
                var theObjT = (T)formatter.Deserialize(theStream);
                theStream.Close();
                return theObjT;
            }
        }

        /// <summary>
        /// 解压zip文件到目录
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="destinationDir"></param>
        /// <param name="overWrite"></param>
        public static void ExtractZipFile(string fileName, string destinationDir,bool overWrite)
        {
            using (ZipArchive archive = ZipFile.Open(fileName, ZipArchiveMode.Update))
            {
                ZipArchiveExtensions.ExtractToDirectory(archive, destinationDir, overWrite);
            }
        }
    }

    public static class ZipArchiveExtensions
    {
        public static void ExtractToDirectory(this ZipArchive archive, string destinationDirectory, bool overwrite)
        {
            if (!overwrite)
            {
                archive.ExtractToDirectory(destinationDirectory);
                return;
            }
            foreach (ZipArchiveEntry file in archive.Entries)
            {
                string completeFileName = Path.Combine(destinationDirectory, file.FullName);
                string directory = Path.GetDirectoryName(completeFileName);

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                if (file.Name != "")
                    file.ExtractToFile(completeFileName, true);
            }
        }
    }

}
