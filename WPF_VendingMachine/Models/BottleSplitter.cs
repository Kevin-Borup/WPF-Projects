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
    /// This class maintains the methods needed to create a running splitter of Bottle classes to the appropriate Queues.
    /// <para>It waits if productionQueue is empty</para>
    /// <para>It waits if the filtered Queue is full</para>
    /// </summary>
    internal class BottleSplitter
    {
        public event EventHandler<Events.BottleEventArgs> BottleSent;

        private BottleQueue<Bottle> producedBottles;
        private BottleQueue<Bottle> filteredBeerBottles;
        private BottleQueue<Bottle> filteredSodaBottles;

        public bool KeepRunning { get; set; }

        /// <summary>
        /// The constructor needs the productionQueue to take from, and the 2 queue types to transfer to.
        /// </summary>
        /// <param name="producedQueue"></param>
        /// <param name="beerQueue"></param>
        /// <param name="sodaQueue"></param>
        public BottleSplitter(BottleQueue<Bottle> producedQueue, BottleQueue<Bottle> beerQueue, BottleQueue<Bottle> sodaQueue)
        {
            producedBottles = producedQueue;
            filteredBeerBottles = beerQueue;
            filteredSodaBottles = sodaQueue;
        }

        protected void OnBottleSent(Bottle bottle)
        {
            Debug.WriteLine("Bottle Sent Event Called");
            BottleSent?.Invoke(this, new Events.BottleEventArgs(bottle));
        }

        /// <summary>
        /// This dequeues a bottle from the production Queue. The bottle is then transferen to the relevant queue.
        /// </summary>
        public void Split()
        {
            Bottle bottle = null;
            bool bottleToGet;
            while (KeepRunning)
            {
                bottleToGet = true;
                while (bottleToGet)
                {
                    try
                    {
                        if (Monitor.TryEnter(producedBottles.Available))
                        {
                            if (producedBottles.Empty)
                            {
                                Monitor.Wait(producedBottles.Available);
                            }

                            while (!producedBottles.Peek().Arrived)
                            {
                                Monitor.Wait(producedBottles.Available);
                            }

                            if (producedBottles.TryDequeue(out bottle))
                            {
                                Monitor.Pulse(producedBottles.Available);
                                bottleToGet = false;
                            }

                            Monitor.Exit(producedBottles.Available);
                        }
                    }
                    catch (Exception e)
                    {
                        if (e is ThreadAbortException || e is ThreadInterruptedException || e is ArgumentNullException)
                        {
                            if (Monitor.IsEntered(producedBottles.Available))
                            {
                                Monitor.Exit(producedBottles.Available);
                            }
                            //Program.ExceptionWriter(e);
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                if (bottle.Type.Equals("Beer"))
                {
                    BottleTransfer(filteredBeerBottles, bottle);
                }
                else if (bottle.Type.Equals("Soda"))
                {
                    BottleTransfer(filteredSodaBottles, bottle);
                }
            }
        }

        /// <summary>
        /// This transfers the provided bottle to the provided queue, with appropriate monitor functions.
        /// </summary>
        /// <param name="bottleQueue"></param>
        /// <param name="bottle"></param>
        private void BottleTransfer(BottleQueue<Bottle> bottleQueue, Bottle bottle)
        {
            while (KeepRunning)
            {
                try
                {
                    if (Monitor.TryEnter(bottleQueue.Available))
                    {
                        if (bottleQueue.Full)
                        {
                            Monitor.Wait(bottleQueue.Available);
                        }

                        bottleQueue.Enqueue(bottle);
                        //Program.ConsoleWriter(bottle, bottleQueue.Count);
                        OnBottleSent(bottle);
                        Monitor.Pulse(bottleQueue.Available);
                        Monitor.Exit(bottleQueue.Available);
                        break;
                    }
                }
                catch (Exception e)
                {
                    if (e is ThreadAbortException || e is ThreadInterruptedException || e is ArgumentNullException)
                    {
                        if (Monitor.IsEntered(bottleQueue.Available))
                        {
                            Monitor.Exit(bottleQueue.Available);
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
