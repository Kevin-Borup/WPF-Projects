using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_VendingMachine.Models;

namespace WPF_VendingMachine.ViewModels
{
    internal class VendingMachineViewModel : ViewModelBase
    {
        Factory factory;

        private bool automaticOn;
        public bool AutomaticOn
        {
            get { return automaticOn; }
            set
            {
                if (automaticOn == value)
                    return;

                automaticOn = value;
                OnPropertyChanged(nameof(AutomaticOn));
            }
        }

        private bool automaticOff;
        public bool AutomaticOff
        {
            get { return automaticOff; }
            set
            {
                if (automaticOff == value)
                    return;

                automaticOff = value;
                OnPropertyChanged(nameof(AutomaticOff));
            }
        }

        public ICommand AutomaticCommand { get; set; }
        public ICommand BeerBottleCommand { get; set; }
        public ICommand SodaBottleCommand { get; set; }

        public event EventHandler<Events.BottleEventArgs> BottleCreated;

        public VendingMachineViewModel()
        {
            factory = new Factory(BottleCreated);

            AutomaticOn = false;
            AutomaticOff = true;

            AutomaticCommand = new Commands.AutomaticProductionCommand(this, factory);
            BeerBottleCommand = new Commands.SpawnBeerBottleCommand(factory);
            SodaBottleCommand = new Commands.SpawnSodaBottleCommand(factory);

            BottleCreated += BottleCreatedHandler;
        }

        private void BottleCreatedHandler(object? sender, Events.BottleEventArgs e)
        {
            switch (e.Bottle.Type)
            {
                case "Beer":
                    BottleSpawner("beer");
                    break;
                case "Soda":
                    BottleSpawner("soda");
                    break;
            }
        }

        private void BottleSpawner(string bottle)
        {

        }
    }
}
