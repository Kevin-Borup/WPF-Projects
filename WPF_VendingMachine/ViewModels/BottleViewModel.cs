using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_VendingMachine.ViewModels
{
    public class BottleViewModel : ViewModelBase
    {
        public string ImageSource { get; private set; }
        public int MaxWidth { get; private set; }
        public int MaxHeight { get; private set; }

        private Vector location;
        public Vector Location
        {
            get { return location; }
            set
            {
                if (location == value)
                    return;

                location = value;
                OnPropertyChanged(nameof(Location));
            }
        }



        public BottleViewModel(string source)
        {
            ImageSource = source;
            MaxWidth = 50;
            MaxHeight = 50;
            Location = new Vector(0,0);
        }

    }
}
