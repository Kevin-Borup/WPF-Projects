using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_VendingMachine.Models
{
    // Inspired by Stackoverflow post
    // https://stackoverflow.com/a/1305
    /// <summary>
    /// The queue administrating bottles and maximum size allowed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class BottleQueue<T> : Queue<T>
    {
        public object Lock;
        public int MaxLength { get; private set; }
        public bool Full { get; private set; }
        public bool Empty { get; private set; }

        /// <summary>
        /// The Constructor is used to define the MaxLength of the queue.
        /// </summary>
        /// <param name="maxLength"></param>
        public BottleQueue(int maxLength) : base(maxLength)
        {
            MaxLength = maxLength;
            Lock = new object();
            Empty = true;
            Full = false;
        }

        /// <summary>
        /// An overided Enqueue, it runs CountCheck() after enqueuing item. To update the Queue variables.
        /// </summary>
        /// <param name="item"></param>
        public new void Enqueue(T item)
        {
            while (Count >= MaxLength)
            {
                Dequeue();
            }
            base.Enqueue(item);
            CountCheck();
        }

        /// <summary>
        /// An overided Dequeue, it runs CountCheck() before returning the item. To update the Queue variables.
        /// </summary>
        /// <returns>The dequeued item</returns>
        public new dynamic Dequeue()
        {
            var item = base.Dequeue();
            CountCheck();
            return item;
        }

        /// <summary>
        /// It compares the count of the queue, to update Full or Empty appropriately. 
        /// </summary>
        private void CountCheck()
        {
            if (Count == MaxLength)
            {
                Full = true;
                Empty = false;
            }
            else if (Count < 1)
            {
                Empty = true;
                Full = false;
            }
            else
            {
                Empty = false;
                Full = false;
            }
        }
    }
}
