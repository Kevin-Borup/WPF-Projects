using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_VendingMachine.Commands
{
    internal class PowerChangeBeerCommand : CommandBase
    {
        ViewModels.VendingMachineViewModel vendingVM;
        Models.Factory factory;

        public PowerChangeBeerCommand(ViewModels.VendingMachineViewModel vendingMachineViewModel, Models.Factory mainFactory)
        {
            vendingVM = vendingMachineViewModel;
            factory = mainFactory;
        }
        public override void Execute(object parameter)
        {
            if (vendingVM.BeerOn)
            {
                factory.BeerOn();
            }
            else if (vendingVM.BeerOff)
            {
                factory.BeerOff();
            }
        }
    }
}
