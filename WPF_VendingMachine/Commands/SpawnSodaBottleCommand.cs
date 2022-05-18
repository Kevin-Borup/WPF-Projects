using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_VendingMachine.Commands
{
    internal class SpawnSodaBottleCommand : CommandBase
    {
        Models.Factory factory;
        public SpawnSodaBottleCommand(Models.Factory mainFactory)
        {
            factory = mainFactory;
        }

        public override void Execute(object parameter)
        {
            factory.GenerateBottle("Soda");
        }
    }
}
