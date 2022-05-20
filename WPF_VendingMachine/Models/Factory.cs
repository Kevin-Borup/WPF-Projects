using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace WPF_VendingMachine.Models
{
    /// <summary>
    /// This class represents the entire factory as a single entity. It is the manager of all processes in the plant.
    /// </summary>
    internal class Factory
    {
        private BottleQueue<Bottle> producedBottles;
        private BottleQueue<Bottle> filteredBeerBottles;
        private BottleQueue<Bottle> filteredSodaBottles;

        public BottleProducer producer { get; private set; }
        public BottleSplitter splitter { get; private set; }
        public BottleConsumer beerExport { get; private set; }
        public BottleConsumer sodaExport { get; private set; }

        private Thread bottleProducer;
        private Thread bottleSplitter;
        private Thread beerConsumer;
        private Thread sodaConsumer;


        public Factory()
        {
            producedBottles = new BottleQueue<Bottle>(24);
            filteredBeerBottles = new BottleQueue<Bottle>(24);
            filteredSodaBottles = new BottleQueue<Bottle>(24);

            producer = new BottleProducer(producedBottles);
            splitter = new BottleSplitter(producedBottles, filteredBeerBottles, filteredSodaBottles);
            beerExport = new BottleConsumer(filteredBeerBottles);
            sodaExport = new BottleConsumer(filteredSodaBottles);
        }

        public void AutomaticGenerationOn()
        {
            producer.KeepRunning = true;
            bottleProducer = new Thread(producer.Produce) { Name = "Bottle Producer" };
            bottleProducer.Start();
            Debug.WriteLine("Automatic On");
        }

        public void AutomaticGenerationOff()
        {
            producer.KeepRunning = false;
            bottleProducer.Join();
            Debug.WriteLine("Automatic Off");
        }

        public void SorterOn()
        {
            splitter.KeepRunning = true;
            bottleSplitter = new Thread(splitter.Split) { Name = "Bottle Splitter" };
            bottleSplitter.Start();
            Debug.WriteLine("Sorter On");
        }

        public void SorterOff()
        {
            splitter.KeepRunning = true;
            bottleSplitter.Join();
            Debug.WriteLine("Sorter Off");
        }

        public void SodaOn()
        {
            sodaExport.KeepRunning = true;
            sodaConsumer = new Thread(sodaExport.Consume) { Name = "Soda Consumer" };
            sodaConsumer.Start();
            Debug.WriteLine("Soda On");
        }

        public void SodaOff()
        {
            sodaExport.KeepRunning = false;
            sodaConsumer.Join();
            Debug.WriteLine("Soda On");
        }

        public void BeerOn()
        {
            beerExport.KeepRunning = true;
            beerConsumer = new Thread(beerExport.Consume) { Name = "Beer Consumer" };
            beerConsumer.Start();
            Debug.WriteLine("Beer On");
        }

        public void BeerOff()
        {
            beerExport.KeepRunning = false;
            beerConsumer.Join();
            Debug.WriteLine("Beer Off");
        }

        public void GenerateBottle(string bottleType)
        {
            producer.Produce(bottleType);
            Debug.WriteLine("Generate Bottle: " + bottleType);
        }

        public void ArrivedBottlePulseProductionQueue()
        {
            bool waiting = true;
            while (waiting)
            {
                if (Monitor.TryEnter(producedBottles.Available))
                {
                    // Wait until the bottle arrives on the conveyorbelt
                    Monitor.Wait(producedBottles.Available);
                    Monitor.Exit(producedBottles.Available);
                    waiting = false;
                }
            }
        }

        public void ArrivedBottlePulseFilteredSodaBottleQueue()
        {
            bool waiting = true;
            while (waiting)
            {
                if (Monitor.TryEnter(filteredSodaBottles.Available))
                {
                    // Wait until the bottle arrives on the conveyorbelt
                    Monitor.Wait(filteredSodaBottles.Available);
                    Monitor.Exit(filteredSodaBottles.Available);
                    waiting = false;
                }
            }
        }

        public void ArrivedBottlePulseFilteredBeerBottleQueue()
        {
            bool waiting = true;
            while (waiting)
            {
                if (Monitor.TryEnter(filteredBeerBottles.Available))
                {
                    // Wait until the bottle arrives on the conveyorbelt
                    Monitor.Wait(filteredBeerBottles.Available);
                    Monitor.Exit(filteredBeerBottles.Available);
                    waiting = false;
                }
            }
        }

    }
}
