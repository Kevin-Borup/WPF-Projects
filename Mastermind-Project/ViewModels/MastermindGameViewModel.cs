using Battleship_Project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind_Project.ViewModels
{
    internal class MastermindGameViewModel : ViewModelBase
    {

        private bool blueButton;
        public bool BlueButton 
        { 
            get { return blueButton; }
            set 
            {
                blueButton = value;
                ChangeSelectedColor(nameof(BlueButton));
                OnPropertyChanged(nameof(BlueButton));
            }
        }

        private bool cyanButton;
        public bool CyanButton
        {
            get { return cyanButton; }
            set
            {
                cyanButton = value;
                ChangeSelectedColor(nameof(CyanButton));
                OnPropertyChanged(nameof(CyanButton));
            }
        }

        private bool greenButton;
        public bool GreenButton
        {
            get { return greenButton; }
            set
            {
                greenButton = value;
                ChangeSelectedColor(nameof(GreenButton));
                OnPropertyChanged(nameof(GreenButton));
            }
        }

        private bool yellowButton;
        public bool YellowButton
        {
            get { return yellowButton; }
            set
            {
                yellowButton = value;
                ChangeSelectedColor(nameof(YellowButton));
                OnPropertyChanged(nameof(YellowButton));
            }
        }

        private bool blueButton;
        public bool BlueButton
        {
            get { return blueButton; }
            set
            {
                blueButton = value;
                ChangeSelectedColor(nameof(BlueButton));
                OnPropertyChanged(nameof(BlueButton));
            }
        }

        private bool blueButton;
        public bool BlueButton
        {
            get { return blueButton; }
            set
            {
                blueButton = value;
                ChangeSelectedColor(nameof(BlueButton));
                OnPropertyChanged(nameof(BlueButton));
            }
        }

        private bool blueButton;
        public bool BlueButton
        {
            get { return blueButton; }
            set
            {
                blueButton = value;
                ChangeSelectedColor(nameof(BlueButton));
                OnPropertyChanged(nameof(BlueButton));
            }
        }

        private bool blueButton;
        public bool BlueButton
        {
            get { return blueButton; }
            set
            {
                blueButton = value;
                ChangeSelectedColor(nameof(BlueButton));
                OnPropertyChanged(nameof(BlueButton));
            }
        }

        public string selectedColor;

        public MastermindGameViewModel()
        {

        }

        private void ChangeSelectedColor(string colorName)
        {
            switch (colorName)
            {
                case "":
                    break;
                case "":
                    break;
                case "":
                    break;
            }
        }

    }
}
