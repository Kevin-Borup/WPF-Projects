using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_VendingMachine.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private FrameworkElement contentControlView;
        public FrameworkElement ContentControlView
        {
            get { return contentControlView; }
            set 
            { 
                contentControlView = value; 
                OnPropertyChanged(nameof(ContentControlView));
            }
        }

        public MainWindowViewModel()
        {
            ContentControlView = new Views.VendingMachine();
            ContentControlView.DataContext = new VendingMachineViewModel();
        }
    }
}
