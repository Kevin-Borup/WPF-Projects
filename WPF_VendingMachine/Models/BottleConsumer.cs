using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WPF_VendingMachine.Models
{
    /// <summary>
    /// This class maintains the methods needed to create a running consumer of Bottle classes to the appropriate Queue.
    /// <para>It waits if the filtered Queue is empty</para>
    /// </summary>
    internal class BottleConsumer
    {
        private BottleQueue<Bottle> filteredBottles;

        /// <summary>
        /// The constructor appoints the appropriate queue as the queue to consume.
        /// </summary>
        /// <param name="bottleQueue"></param>
        public BottleConsumer(BottleQueue<Bottle> bottleQueue)
        {
            filteredBottles = bottleQueue;
        }

        /// <summary>
        /// Consume a bottle when available from the Queue recieved in the constructor. It waits if the queue is empty.
        /// </summary>
        public void Consume()
        {
            while (true)
            {
                try
                {
                    if (Monitor.TryEnter(filteredBottles.Lock))
                    {
                        if (filteredBottles.Empty)
                        {
                            Monitor.Wait(filteredBottles.Lock);
                        }

                        //Program.ConsoleWriter(filteredBottles.Dequeue(), filteredBottles.Count);
                        Monitor.Pulse(filteredBottles.Lock);
                        Monitor.Exit(filteredBottles.Lock);
                    }
                }
                catch (Exception e)
                {
                    if (e is ThreadAbortException || e is ThreadInterruptedException || e is ArgumentNullException)
                    {
                        if (Monitor.IsEntered(filteredBottles.Lock))
                        {
                            Monitor.Exit(filteredBottles.Lock);
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
