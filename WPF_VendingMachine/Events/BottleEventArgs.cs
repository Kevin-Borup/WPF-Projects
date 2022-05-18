using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_VendingMachine.Events
{
    internal class BottleEventArgs : EventArgs
    {
        public Models.Bottle Bottle { get; private set; }

        public BottleEventArgs(Models.Bottle bottle)
        {
            Bottle = bottle;
        }
    }
}
