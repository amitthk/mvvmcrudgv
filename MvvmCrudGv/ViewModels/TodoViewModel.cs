using MvvmCrudGv.Common;
using MvvmCrudGv.Common.Messaging;
using MvvmCrudGv.Service.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MvvmCrudGv.ViewModels
{
    public class TodoViewModel:BaseViewModel
    {
        public Todo _todo { get; private set; }



        public TodoViewModel():this(new Todo())
        {

        }

        public TodoViewModel(Todo todo)
        {
            _todo = todo;

        }


        #region Properties
        public override bool IsDirty
        {
            get { return base.IsDirty; }
            set
            {
                if (base.IsDirty != value)
                {
                    base.IsDirty = value;
                    OnPropertyChanged("IsDirty");
                }
            }
        }

        public Guid Id
        {
            get { return _todo.Id; }
            set
            {
                _todo.Id = value;
                OnPropertyChanged("Id");
            }
        }



        public string Title
        {
            get { return _todo.Title; }
            set
            {
                _todo.Title = value;
                OnPropertyChanged("Title");
            }
        }




        public string Text
        {
            get { return _todo.Text; }
            set
            {
                _todo.Text = value;
                OnPropertyChanged("Text");
            }
        }



        public DateTime CreateDt
        {
            get { return _todo.CreateDt; }
            //set { _todo.CreateDt = value;
            //OnPropertyChanged("CreateDt");
            //}
        }



        public DateTime DueDt
        {
            get { return _todo.DueDt; }
            set
            {
                _todo.DueDt = value;
                OnPropertyChanged("DueDt");
            }
        }


        public int EstimatedPomodori
        {
            get { return _todo.EstimatedPomodori; }
            set
            {
                _todo.EstimatedPomodori = value;
                OnPropertyChanged("EstimatedPomodori");
            }
        }



        public int CompletedPomodori
        {
            get { return _todo.CompletedPomodori; }
            set
            {
                _todo.CompletedPomodori = value;
                OnPropertyChanged("CompletedPomodori");
            }
        }





        public string AddedBy
        {
            get { return _todo.AddedBy; }
            set
            {
                _todo.AddedBy = value;
                OnPropertyChanged("AddedBy");
            }
        } 
        #endregion

    }
}
