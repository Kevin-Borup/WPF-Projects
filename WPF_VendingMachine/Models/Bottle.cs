using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_VendingMachine.ViewModels;

namespace WPF_VendingMachine.Models
{
    /// <summary>
    /// This represents a bottle in our system. It contains a Type and an ID.
    /// </summary>
    internal class Bottle
    {
        public BottleViewModel BottleModel { get; private set; }
        public string Type { get; private set; }
        public int ID { get; private set; }

        public bool Arrived { get; set; }

        /// <summary>
        /// Bottle Constructor, 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        public Bottle(string type, int id)
        {
            Type = type;
            ID = id;
            Arrived = false;

            if (Type.Equals("Soda"))
            {
                BottleModel = new BottleViewModel("../Graphics/BottleSoda.png");
            }
            else if (Type.Equals("Beer"))
            {
                BottleModel = new BottleViewModel("../Graphics/BottleBeer.png");
            }
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
