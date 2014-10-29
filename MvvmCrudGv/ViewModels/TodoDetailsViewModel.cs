using MvvmCrudGv.Common;
using MvvmCrudGv.Common.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MvvmCrudGv.ViewModels
{
    public class TodoDetailsViewModel: BaseViewModel
    {
        private TodoViewModel _CurrentTodo;
        IEventAggregator _eventAggregator;
        public ICommand GoBackCmd { get; private set; }
        private readonly ICommand _SaveTodoCmd;

        public ICommand SaveTodoCmd { get { return (_SaveTodoCmd); } }


        public TodoViewModel CurrentTodo
        {
            get { return _CurrentTodo; }
            set
            {
                if ((null != value) && (_CurrentTodo != value))
                {
                    _CurrentTodo = value;
                    OnPropertyChanged("CurrentTodo");
                }
            }
        }

        public TodoDetailsViewModel()
        {
            _eventAggregator = App.eventAggregator;
            _eventAggregator.Subscribe<MvvmCrudGv.Views.ObjMessage>(UpdateTodo);

            //This goes in Initialization/constructor
            GoBackCmd = new RelayCommand(ExecGoBack, CanGoBack);
            _SaveTodoCmd = new RelayCommand(ExecSaveTodo, CanSaveTodo);
        }

        private void UpdateTodo(MvvmCrudGv.Views.ObjMessage message)
        {
            var td = (TodoViewModel)message.PayLoad;
            CurrentTodo = td;

        }

        #region Commands

        private void ExecGoBack(object obj)
        {
            if (IsDirty)
            {
                System.Windows.MessageBoxResult confirmRunResult = System.Windows.MessageBox.Show("If you go back the changes will be discarded. Do you want to do this? If not, select 'Cancel' and 'Save' the changes first.", "Discard Changes?", System.Windows.MessageBoxButton.OKCancel);
                if (confirmRunResult == System.Windows.MessageBoxResult.Cancel)
                {
                    return;
                }
            }
            App.eventAggregator.Publish<Views.NavMessage>(new Views.NavMessage("Home"));
        }

        private bool CanGoBack(object obj)
        {
            return (true);
        }

        private void ExecSaveTodo(object obj)
        {
            //Todo: Add the functionality for SaveTodoCmd Here
            bool isok = BootStrapper.Instance.TodoService.Update(this.CurrentTodo._todo);
            IsDirty = !isok;
        }

        [DebuggerStepThrough]
        private bool CanSaveTodo(object obj)
        {
            //Todo: Add the checking for CanSaveTodo Here
            return (IsDirty);
        }
        #endregion
    }
}
