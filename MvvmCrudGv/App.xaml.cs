using MvvmCrudGv.Common.Messaging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MvvmCrudGv
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IEventAggregator eventAggregator { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            Common.BootStrapper.Instance.Bootstrap(this,e);
            eventAggregator = new EventAggregator();
            base.OnStartup(e);
        }

        

        protected override void OnExit(ExitEventArgs e)
        {
            Common.BootStrapper.Instance.ShutDown(this, e);
            base.OnExit(e);
        }
    }
}
