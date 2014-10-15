using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MvvmCrudGv.Common
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private bool _IsDirty;

        public virtual bool IsDirty
        {
            get { return _IsDirty; }
            set
            {
                if (_IsDirty != value)
                {
                    _IsDirty = value;
                    OnPropertyChanged("IsDirty");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// When property is changed call this method to fire the PropertyChanged Event
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged(string propertyName)
        {
            //Fire the PropertyChanged event in case somebody subscribed to it
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                if (!propertyName.Equals("IsDirty"))
                { IsDirty = true; }
            }
        }

        public void OnPropertyChanged<T>(Expression<Func<T>> property)
        {
            if (PropertyChanged != null)
            {
                var memberExpression = property.Body as MemberExpression;
                PropertyChanged(this, new PropertyChangedEventArgs(memberExpression.Member.Name));
                if (!memberExpression.Member.Name.Equals("IsDirty"))
                { IsDirty = true; }
            }
        }
    }
}
