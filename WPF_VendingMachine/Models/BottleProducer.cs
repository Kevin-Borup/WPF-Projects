using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace WPF_VendingMachine.Models
{
    /// <summary>
    /// This class maintains the methods needed to create a running producer of Bottle classes to the appropriate Queue.
    /// <para>It waits if productionQueue is full</para>
    /// </summary>
    internal class BottleProducer
    {
        private BottleQueue<Bottle> producedBottles;
        EventHandler<Events.BottleEventArgs> BottleCreated;

        private int beerIncrement = 1;
        private int sodaIncrement = 1;

        public bool KeepRunning { get; set; }
        private bool bottleToQueue = true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bottles"></param>
        public BottleProducer(BottleQueue<Bottle> bottles, EventHandler<Events.BottleEventArgs> bottleCreated)
        {
            producedBottles = bottles;
            BottleCreated = bottleCreated;
        }
        
        protected void OnBottleCreated(Bottle bottle)
        {
            BottleCreated?.Invoke(this, new Events.BottleEventArgs(bottle));
        }

        /// <summary>
        /// Produces a bottle object with randomized type. The bottle is then added to the Queue recieved with the Constructor.
        /// </summary>
        public void Produce()
        {
            Random numGen = new Random();
            Bottle bottle;

            while (KeepRunning)
            {
                if (numGen.Next(1, 3) == 1)
                {
                    bottle = new Bottle("Beer", beerIncrement);
                    beerIncrement++;
                    bottleToQueue = true;
                }
                else
                {
                    bottle = new Bottle("Soda", sodaIncrement);
                    sodaIncrement++;
                    bottleToQueue = true;
                }

                while (bottleToQueue)
                {
                    try
                    {
                        if (Monitor.TryEnter(producedBottles.Lock))
                        {
                            if (producedBottles.Full)
                            {
                                //Wait for the lock if the queue is full to avoid ressource waste.
                                Monitor.Wait(producedBottles.Lock);
                            }

                            producedBottles.Enqueue(bottle);
                            //Program.ConsoleWriter(bottle, producedBottles.Count);
                            OnBottleCreated(bottle);
                            Monitor.Pulse(producedBottles.Lock);
                            Monitor.Exit(producedBottles.Lock);
                            bottleToQueue = false;
                        }
                    }
                    catch (Exception e)
                    {
                        if (e is ThreadAbortException || e is ThreadInterruptedException || e is ArgumentNullException)
                        {
                            if (Monitor.IsEntered(producedBottles.Lock))
                            {
                                Monitor.Exit(producedBottles.Lock);
                            }
                            //Program.ExceptionWriter(e);
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }
        }

        public void Produce(string bottleType)
        {
            Bottle bottle = null;
            bool bottleToEnqueue = false;

            if (bottleType != null)
            {
                if (bottleType.Equals("Beer"))
                {
                    bottle = new Bottle("Beer", beerIncrement);
                    beerIncrement++;
                    bottleToEnqueue = true;
                }
                else if (bottleType.Equals("Soda"))
                {
                    bottle = new Bottle("Soda", sodaIncrement);
                    sodaIncrement++;
                    bottleToEnqueue = true;
                }

                while (bottleToEnqueue)
                {
                    try
                    {
                        if (Monitor.TryEnter(producedBottles.Lock))
                        {
                            if (producedBottles.Full)
                            {
                                //Wait for the lock if the queue is full to avoid ressource waste.
                                Monitor.Wait(producedBottles.Lock);
                            }

                            producedBottles.Enqueue(bottle);
                            //Program.ConsoleWriter(bottle, producedBottles.Count);
                            Monitor.Pulse(producedBottles.Lock);
                            Monitor.Exit(producedBottles.Lock);
                            break;
                        }
                    }
                    catch (Exception e)
                    {
                        if (e is ThreadAbortException || e is ThreadInterruptedException || e is ArgumentNullException)
                        {
                            if (Monitor.IsEntered(producedBottles.Lock))
                            {
                                Monitor.Exit(producedBottles.Lock);
                            }
                            //Program.ExceptionWriter(e);
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }
        }
    }
}
