using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmCrudGv.Service.MvvmCrudGv.Persistence
{
    abstract class AbstractCrudDB
    {
        protected AbstractCrudDB(string basePath,string fileExtension)
        {
            BasePath = basePath;
        }

        public string BasePath { get; private set; }
        public string FileExtension { get; private set; }

        //Create
        public abstract string Write<T>(T row, string id);

        //Read
        public abstract T Read<T>(string id);
        public abstract T[] Read<T>();

        //Delete
        public abstract void Delete<T>(string id);
        public abstract void Delete(Type type, string id);

        //Update
        //No update operation right now

        public virtual string CreateID()
        {
            return Guid.NewGuid().ToString("D");
        }

        protected string CreateFilename(Type type, string id)
        {
            return System.IO.Path.Combine(BasePath, string.Format("{0}-{1}.{2}", type.Name, id, FileExtension));
        }
    }
}
