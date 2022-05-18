using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_VendingMachine.Models
{
    /// <summary>
    /// This represents a bottle in our system. It contains a Type and an ID.
    /// </summary>
    internal class Bottle
    {
        public string Type { get; private set; }
        public int ID { get; private set; }
        /// <summary>
        /// Bottle Constructor, 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        public Bottle(string type, int id)
        {
            Type = type;
            ID = id;
        }

        /// <summary>
        /// Overrided ToString
        /// </summary>
        /// <returns>"Type-ID"</returns>
        public override string ToString()
        {
            return $"{Type}-{ID}";
        }
    }
}
