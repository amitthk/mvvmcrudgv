using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MvvmCrudGv.Views
{
    /// <summary>
    /// Interaction logic for TodoDetails.xaml
    /// </summary>
    public partial class TodoDetails : Page
    {
        private MvvmCrudGv.ViewModels.TodoViewModel _viewModelFromNav;

        public TodoDetails()
        {
            InitializeComponent();
        }

        //For Navigation when context is already provided
        public TodoDetails(MvvmCrudGv.ViewModels.TodoViewModel model):this()
        {
            this.Loaded += TodoDetails_Loaded;
            _viewModelFromNav=model;
        }

        private void TodoDetails_Loaded(object sender, RoutedEventArgs e)
        {
            if (_viewModelFromNav!=null)
            {
                this.DataContext = _viewModelFromNav;
            }
        }
    }
}
