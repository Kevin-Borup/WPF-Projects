using System;
using System.Collections.Generic;
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
        private BottleQueue<Bottle> producedBottles;
        private BottleQueue<Bottle> filteredBeerBottles;
        private BottleQueue<Bottle> filteredSodaBottles;

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

        /// <summary>
        /// This dequeues a bottle from the production Queue. The bottle is then transferen to the relevant queue.
        /// </summary>
        public void Split()
        {
            Bottle bottle;
            while (true)
            {
                while (true)
                {
                    try
                    {
                        if (Monitor.TryEnter(producedBottles.Lock))
                        {
                            if (producedBottles.Empty)
                            {
                                Monitor.Wait(producedBottles.Lock);
                            }

                            bottle = producedBottles.Dequeue();
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
            while (true)
            {
                try
                {
                    if (Monitor.TryEnter(bottleQueue.Lock))
                    {
                        if (bottleQueue.Full)
                        {
                            Monitor.Wait(bottleQueue.Lock);
                        }

                        bottleQueue.Enqueue(bottle);
                        //Program.ConsoleWriter(bottle, bottleQueue.Count);
                        Monitor.Pulse(bottleQueue.Lock);
                        Monitor.Exit(bottleQueue.Lock);
                        break;
                    }
                }
                catch (Exception e)
                {
                    if (e is ThreadAbortException || e is ThreadInterruptedException || e is ArgumentNullException)
                    {
                        if (Monitor.IsEntered(bottleQueue.Lock))
                        {
                            Monitor.Exit(bottleQueue.Lock);
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
