using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmCrudGv.Service.Entity
{
    public class Utility
    {
        public static string getAbsolutePath(string folder, bool createIfNoDirectory = false)
        {
            string rtrnPath = Path.Combine(getAppBasePath(), folder);
            if ((createIfNoDirectory) && (!System.IO.Directory.Exists(folder)))
            {
                Directory.CreateDirectory(rtrnPath);
            }
            return (rtrnPath);
        }

        public static string getAbsolutePath(string folder, string fileName, bool createIfNoDirectory = false)
        {
            string rtrnPath = Path.Combine(getAppBasePath(), folder);

            if ((createIfNoDirectory) && (!System.IO.Directory.Exists(folder)))
            {
                Directory.CreateDirectory(rtrnPath);
            }
            rtrnPath = Path.Combine(rtrnPath, fileName);

            return (rtrnPath);
        }

        public static string getAppBasePath()
        {
            string codeBase = System.Reflection.Assembly.GetExecutingAssembly().Location;
            return Path.GetDirectoryName(codeBase);
        }



        public static bool fileExists(string filePath)
        {
            return (System.IO.File.Exists(filePath));
        }

        public static Stream ReadFileStream(string filePath)
        {
            MemoryStream ms = new MemoryStream();
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = new byte[file.Length];
                file.Read(bytes, 0, (int)file.Length);
                ms.Write(bytes, 0, (int)file.Length);
            }
            return (ms);
        }
    }
}
