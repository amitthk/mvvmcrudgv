using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using MvvmCrudGv.Service;
using System.ServiceModel.Description;

namespace MvvmCrudGv.Common
{
    class BootStrapper
    {
        private static BootStrapper _instance;
        private static ITodoService _todoService;

        public ITodoService TodoService { get { return (_todoService); } }

        private BootStrapper()
        {
            _todoService = new TodoService();
        }

        public static BootStrapper Instance { get {
            if (_instance==null)
            {
                _instance = new BootStrapper();
            }
            return (_instance);
        } 
        }

        public void Bootstrap(App app, System.Windows.StartupEventArgs e)
        {
            //Do bootstap here
        }

        public void ShutDown(App app, System.Windows.ExitEventArgs e)
        {
            //Do shutdown cleanup here
        }
    }
}
