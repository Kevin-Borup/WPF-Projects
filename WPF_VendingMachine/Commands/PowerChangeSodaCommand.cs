using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_VendingMachine.Commands
{
    internal class PowerChangeSodaCommand : CommandBase
    {
        ViewModels.VendingMachineViewModel vendingVM;
        Models.Factory factory;

        public PowerChangeSodaCommand(ViewModels.VendingMachineViewModel vendingMachineViewModel, Models.Factory mainFactory)
        {
            vendingVM = vendingMachineViewModel;
            factory = mainFactory;
        }
        public override void Execute(object parameter)
        {
            if (vendingVM.SodaOn)
            {
                factory.SodaOn();
            }
            else if (vendingVM.SodaOff)
            {
                factory.SodaOff();
            }
        }
    }
}
