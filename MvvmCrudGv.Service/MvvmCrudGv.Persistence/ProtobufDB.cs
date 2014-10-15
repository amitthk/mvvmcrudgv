using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmCrudGv.Service.MvvmCrudGv.Persistence
{
    class ProtobufDB:AbstractCrudDB
    {
        private static readonly object syncLock = new object();

        public ProtobufDB(string basePath,string fileExtension):base(basePath,fileExtension)
        {

        }

        public override string Write<T>(T row, string id)
        {
            string filename = CreateFilename(typeof(T), id);

            lock (syncLock)
            {
                using (var file = File.Create(filename))
                {
                    Serializer.Serialize(file, row);
                }
            }

            return filename;
        }

        public override T Read<T>(string id)
        {
            string filename = CreateFilename(typeof(T), id);
            return readFileToType<T>(filename);
        }

        public override T[] Read<T>()
        {
            string filePattern = string.Format("{0}-*.{1}", typeof(T).Name, FileExtension);
            string[] files = Directory.GetFiles(BasePath, filePattern, SearchOption.TopDirectoryOnly);
            List<T> list = new List<T>();
            foreach (string filename in files)
            {
                list.Add(readFileToType<T>(filename));
            }
            return list.ToArray();
        }

        public override void Delete<T>(string id)
        {
            Delete(typeof(T), id);
        }

        public override void Delete(Type type, string id)
        {
            string filename = CreateFilename(type, id);
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
        }

        private T readFileToType<T>(string filename)
        {
            if (File.Exists(filename))
            {
                using (var file = File.OpenRead(filename))
                {
                    return (T)Serializer.Deserialize<T>(file);
                }
            }
            return default(T);
        }
    }
}
