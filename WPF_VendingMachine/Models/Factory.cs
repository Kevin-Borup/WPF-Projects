using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace WPF_VendingMachine.Models
{
    internal class Factory
    {
        BottleQueue<Bottle> producedBottles;
        BottleQueue<Bottle> beerBottles;
        BottleQueue<Bottle> sodaBottles;

        BottleProducer producer;
        BottleSplitter splitter;
        BottleConsumer beerExport;
        BottleConsumer sodaExport;

        Thread bottleProducer;
        Thread bottleSplitter;
        Thread beerConsumer;
        Thread sodaConsumer;


        public Factory(EventHandler<Events.BottleEventArgs> bottleCreated)
        {
            producedBottles = new BottleQueue<Bottle>(24);
            beerBottles = new BottleQueue<Bottle>(24);
            sodaBottles = new BottleQueue<Bottle>(24);

            producer = new BottleProducer(producedBottles, bottleCreated);
            splitter = new BottleSplitter(producedBottles, beerBottles, sodaBottles);
            beerExport = new BottleConsumer(beerBottles);
            sodaExport = new BottleConsumer(sodaBottles);

            bottleProducer = new Thread(producer.Produce) { Name = "Bottle Producer" };
            bottleSplitter = new Thread(splitter.Split) { Name = "Bottle Splitter" };
            beerConsumer = new Thread(beerExport.Consume) { Name = "Beer Consumer" };
            sodaConsumer = new Thread(sodaExport.Consume) { Name = "Soda Consumer" };

            bottleSplitter.Start();
            beerConsumer.Start();
            sodaConsumer.Start();
        }

        public void AutomaticGenerationOn()
        {
            producer.KeepRunning = true;
            bottleProducer.Start();
        }

        public void AutomaticGenerationOff()
        {
            producer.KeepRunning = false;
            bottleProducer.Join();
        }

        public void GenerateBottle(string bottleType)
        {
            producer.Produce(bottleType);
        }

    }
}
