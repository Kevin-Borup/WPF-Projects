using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_VendingMachine.Models;
using WPF_VendingMachine.Views;
using System.Windows.Threading;

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

        private bool sorterOn;

        public bool SorterOn
        {
            get { return sorterOn; }
            set
            {
                if (sorterOn == value)
                    return;

                sorterOn = value;
                OnPropertyChanged(nameof(SorterOn));
            }
        }


        private bool sorterOff;

        public bool SorterOff
        {
            get { return sorterOff; }
            set
            {
                if (sorterOff == value)
                    return;

                sorterOff = value;
                OnPropertyChanged(nameof(SorterOff));
            }
        }

        private bool beerOn;

        public bool BeerOn
        {
            get { return beerOn; }
            set
            {
                if (beerOn == value)
                    return;

                beerOn = value;
                OnPropertyChanged(nameof(BeerOn));
            }
        }

        private bool beerOff;

        public bool BeerOff
        {
            get { return beerOff; }
            set
            {
                if (beerOff == value)
                    return;

                beerOff = value;
                OnPropertyChanged(nameof(BeerOff));
            }
        }

        private bool sodaOn;

        public bool SodaOn
        {
            get { return sodaOn; }
            set
            {
                if (sodaOn == value)
                    return;

                sodaOn = value;
                OnPropertyChanged(nameof(SodaOn));
            }
        }

        private bool sodaOff;

        public bool SodaOff
        {
            get { return sodaOff; }
            set
            {
                if (sodaOff == value)
                    return;

                sodaOff = value;
                OnPropertyChanged(nameof(SodaOff));
            }
        }

        private ObservableCollection<BottleViewModel> createdBottles;

        public ObservableCollection<BottleViewModel> CreatedBottles
        {
            get { return createdBottles; }
            set
            {
                if (createdBottles == value)
                    return;

                createdBottles = value;
                OnPropertyChanged(nameof(CreatedBottles));
            }
        }


        public ICommand AutomaticCommand { get; set; }
        public ICommand BeerBottleCommand { get; set; }
        public ICommand SodaBottleCommand { get; set; }
        public ICommand PowerChangeSorterCommand { get; set; }
        public ICommand PowerChangeSodaCommand { get; set; }
        public ICommand PowerChangeBeerCommand { get; set; }

        public VendingMachineViewModel()
        {
            CreatedBottles = new ObservableCollection<BottleViewModel>();
            factory = new Factory();

            AutomaticOn = false;
            AutomaticOff = true;
            SorterOn = false;
            SorterOff = true;
            SodaOn = false;
            SodaOff = true;
            BeerOn = false;
            BeerOff = true;

            AutomaticCommand = new Commands.AutomaticProductionCommand(this, factory);
            BeerBottleCommand = new Commands.SpawnBeerBottleCommand(factory);
            SodaBottleCommand = new Commands.SpawnSodaBottleCommand(factory);
            PowerChangeSorterCommand = new Commands.PowerChangeSorterCommand(this, factory);
            PowerChangeSodaCommand = new Commands.PowerChangeSodaCommand(this, factory);
            PowerChangeBeerCommand = new Commands.PowerChangeBeerCommand(this, factory);

            factory.producer.BottleCreated += BottleCreatedEvent;
            factory.splitter.BottleSent += BottleSentEvent;
        }

        private void BottleCreatedEvent(object? sender, Events.BottleEventArgs e)
        {
            Debug.WriteLine("Bottle Created Event Entered");
            BottleSpawner(e.Bottle, "Producer");
        }

        private void BottleSentEvent(object? sender, Events.BottleEventArgs e)
        {
            Debug.WriteLine("Bottle Sent Event Entered");
            BottleSpawner(e.Bottle, "Splitter");
        }

        private void BottleSpawner(Bottle bottle, string sender)
        {
            Thread movementThread = null;

            if (sender.Equals("Producer"))
            {
                bottle.BottleModel.Location = new Vector(-150, 0);
                Application.Current.Dispatcher.Invoke(() => { CreatedBottles.Add(bottle.BottleModel); });
                movementThread = new Thread(() => MoveProducedBottle(bottle));
                movementThread.Start();
            }
            else if (sender.Equals("Splitter"))
            {
                if (bottle.Type.Equals("Beer"))
                {
                    bottle.BottleModel.Location = new Vector(660, 70);
                }
                else if (bottle.Type.Equals("Soda"))
                {
                    bottle.BottleModel.Location = new Vector(660, -70);
                }

                Application.Current.Dispatcher.Invoke(() => { CreatedBottles.Add(bottle.BottleModel); });
                movementThread = new Thread(() => MoveFilteredBottle(bottle));
                movementThread.Start();
            }
        }

        private void MoveProducedBottle(Bottle bottle)
        {
            while (bottle.BottleModel.Location.X < 550)
            {
                bottle.BottleModel.Location = new Vector(bottle.BottleModel.Location.X + 1, bottle.BottleModel.Location.Y);
                Application.Current.Dispatcher.Invoke(() => { CreatedBottles[CreatedBottles.IndexOf(bottle.BottleModel)] = bottle.BottleModel; });
                Thread.Sleep(2);
            }

            Application.Current.Dispatcher.Invoke(() => { CreatedBottles.Remove(bottle.BottleModel); });
            bottle.Arrived = true;
            factory.ArrivedBottlePulseProductionQueue();
        }

        private void MoveFilteredBottle(Bottle bottle)
        {
            if (bottle.Type.Equals("Beer"))
            {
                while (bottle.BottleModel.Location.Y < 240)
                {
                    bottle.BottleModel.Location = new Vector(bottle.BottleModel.Location.X, bottle.BottleModel.Location.Y + 1);
                    Application.Current.Dispatcher.Invoke(() => { CreatedBottles[CreatedBottles.IndexOf(bottle.BottleModel)] = bottle.BottleModel; });
                    Thread.Sleep(2);
                }
            }
            else if (bottle.Type.Equals("Soda"))
            {
                while (bottle.BottleModel.Location.Y > -210)
                {
                    bottle.BottleModel.Location = new Vector(bottle.BottleModel.Location.X, bottle.BottleModel.Location.Y - 1);
                    Application.Current.Dispatcher.Invoke(() => { CreatedBottles[CreatedBottles.IndexOf(bottle.BottleModel)] = bottle.BottleModel; });
                    Thread.Sleep(2);
                }
            }

            while (bottle.BottleModel.Location.X < 1300)
            {
                bottle.BottleModel.Location = new Vector(bottle.BottleModel.Location.X + 1, bottle.BottleModel.Location.Y);
                Application.Current.Dispatcher.Invoke(() => { CreatedBottles[CreatedBottles.IndexOf(bottle.BottleModel)] = bottle.BottleModel; });
                Thread.Sleep(2);
            }

            Application.Current.Dispatcher.Invoke(() => { CreatedBottles.Remove(bottle.BottleModel); });
            bottle.Arrived = true;

            if (bottle.Type.Equals("Beer"))
            {
                factory.ArrivedBottlePulseFilteredBeerBottleQueue();
            }
            else if (bottle.Type.Equals("Soda"))
            {
                factory.ArrivedBottlePulseFilteredSodaBottleQueue();
            }
        }
    }
}
