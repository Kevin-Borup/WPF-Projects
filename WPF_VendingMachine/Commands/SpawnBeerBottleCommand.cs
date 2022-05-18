using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_VendingMachine.Commands
{
    internal class SpawnBeerBottleCommand : CommandBase
    {
        Models.Factory factory;
        public SpawnBeerBottleCommand(Models.Factory mainFactory)
        {
            factory = mainFactory;
        }

        public override void Execute(object parameter)
        {
            factory.GenerateBottle("Beer");
        }
    }
}
