using MvvmCrudGv.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MvvmCrudGv.ViewModels
{

    public class MainWindowViewModel : BaseViewModel
    {
        private readonly ICommand _ExitCmd;

        public ICommand ExitCmd { get { return (_ExitCmd); } }


        public MainWindowViewModel()
        {
            //Initialize the command
            _ExitCmd = new RelayCommand(ExecExit, CanExit);
        }


        private void ExecExit(object obj)
        {
            //Todo: Add the functionality for ExitCmd Here
            System.Windows.Application.Current.Shutdown();
        }

        [DebuggerStepThrough]
        private bool CanExit(object obj)
        {
            //Todo: Add the checking for CanExit Here
            return (true);
        }
    }
}
