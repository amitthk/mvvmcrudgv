using MvvmCrudGv.Common;
using MvvmCrudGv.Common.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MvvmCrudGv.ViewModels
{
     public class HomeViewModel: BaseViewModel
    {
         
        public ICommand AddTodoCmd { get; private set; }
        public ICommand ListTodosCmd { get; private set; }
        public ICommand UpdateTodoCmd { get; private set; }
        public ICommand LoadTodoCmd { get; private set; }
        public ICommand DeleteTodoCmd { get; private set; }
        public ICommand NewTodoCmd { get; private set; }
        public ICommand GoTodoDetailsCmd { get; private set; }

        public ObservableCollection<TodoViewModel> TodoList { get; set; }
        private TodoViewModel _SelectedTodo;
        private int _TodoListSelectedIndex;
        private EditMode _TodoListEditMode;
        private IEventAggregator _eventAggregator;

        public EditMode TodoListEditMode
        {
            get { return _TodoListEditMode; }
            set
            {
                if (_TodoListEditMode != value)
                {
                    _TodoListEditMode = value;
                    OnPropertyChanged("TodoListEditMode");
                }
            }
        }

        public int TodoListSelectedIndex
        {
            get { return _TodoListSelectedIndex; }
            set
            {
                if (_TodoListSelectedIndex != value)
                {
                    _TodoListSelectedIndex = value;
                    if (_TodoListSelectedIndex==-1)
                    {
                        TodoListEditMode = EditMode.Create;
                    }else
                    {
                        TodoListEditMode = EditMode.Update;
                    }
                    OnPropertyChanged("TodoListSelectedIndex");
                }
            }
        }

        public TodoViewModel SelectedTodo
        {
            get { return _SelectedTodo; }
            set
            {
                if ((null != value) && (_SelectedTodo != value))
                {
                    _SelectedTodo = value;
                    OnPropertyChanged("SelectedTodo");
                }
            }
        }

        MvvmCrudGv.Service.ITodoService _todoServiceClient;


        public HomeViewModel()
        {
            _todoServiceClient = BootStrapper.Instance.TodoService;
            _eventAggregator = App.eventAggregator;
            TodoList = new ObservableCollection<TodoViewModel>();


            loadTodoList();
            _TodoListSelectedIndex = -1;
            _SelectedTodo = new TodoViewModel();



            UpdateTodoCmd = new RelayCommand(ExecUpdateTodo, CanUpdateTodo);
            DeleteTodoCmd = new RelayCommand(ExecDeleteTodo, CanDeleteTodo);
            LoadTodoCmd = new RelayCommand(ExecLoadTodo, CanLoadTodo);
            ListTodosCmd = new RelayCommand(ExecListTodos, CanListTodos);
            AddTodoCmd = new RelayCommand(ExecAddTodo, CanAddTodo);
            NewTodoCmd = new RelayCommand(ExecNewTodo, CanNewTodo);
            GoTodoDetailsCmd = new RelayCommand(ExecGoTodoDetails,CanGoTodoDetails);
        }

        private void ExecNewTodo(object obj)
        {
            SelectedTodo = new TodoViewModel();
            TodoListSelectedIndex = -1;
        }

        [DebuggerStepThrough]
        private bool CanNewTodo(object obj)
        {
            return (true);
        }

        private void loadTodoList()
        {
            //Dummy
            if(!(_todoServiceClient.List().Count>0)){
            var tid = _todoServiceClient.Add(new Service.Entity.Todo() { AddedBy = "Amit", Title = "First todo", Text = "this is first todo" });
            }

            var lstTodos = _todoServiceClient.List();
            if ((lstTodos != null) && (lstTodos.Count > 0))
            {
                foreach (var item in lstTodos)
                {
                    TodoList.Add(new TodoViewModel(item));
                }
            }
        }


        //This goes in Initialization/constructor
        private void ExecDeleteTodo(object obj)
        {
            System.Windows.MessageBoxResult confirmRunResult = System.Windows.MessageBox.Show("Are you sure you want to delete this todo?", "Delete Item?", System.Windows.MessageBoxButton.OKCancel);
            if (confirmRunResult == System.Windows.MessageBoxResult.Cancel)
            {
                return;
            }
            _todoServiceClient.Delete(SelectedTodo.Id);
            TodoList.Remove(SelectedTodo);
            resetSelectedTodo();
        }

        private bool CanDeleteTodo(object obj)
        {
            return (true);
        }
        //This goes in Initialization/constructor
        private void ExecLoadTodo(object obj)
        {

        }

        private bool CanLoadTodo(object obj)
        {
            return (true);
        }
        //This goes in Initialization/constructor
        private void ExecUpdateTodo(object obj)
        {
            bool isok= _todoServiceClient.Update(SelectedTodo._todo);
            SelectedTodo.IsDirty = !isok;
        }

        private bool CanUpdateTodo(object obj)
        {
            return (true);
        }
        //This goes in Initialization/constructor
        private void ExecListTodos(object obj)
        {

        }

        private bool CanListTodos(object obj)
        {
            return (true);
        }
        //This goes in Initialization/constructor
        private void ExecAddTodo(object obj)
        {
            Guid addedid= _todoServiceClient.Add(SelectedTodo._todo);
            SelectedTodo.Id = addedid;
            SelectedTodo.IsDirty = false;
            TodoList.Add(SelectedTodo);
            resetSelectedTodo();
        }

        private void resetSelectedTodo()
        {
            SelectedTodo = new TodoViewModel();
            TodoListSelectedIndex = -1;
            SelectedTodo.IsDirty = false;
        }

        private bool CanAddTodo(object obj)
        {
            return (true);
        }

        private void ExecGoTodoDetails(object obj)
        {
            _eventAggregator.Publish<MvvmCrudGv.Views.NavMessage>(new Views.NavMessage(new Views.TodoDetails(), SelectedTodo));
        }

        private bool CanGoTodoDetails(object obj)
        {
            return (true);
        }
    }
}
