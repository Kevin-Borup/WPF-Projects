using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_VendingMachine.Commands
{
    internal class PowerChangeSorterCommand : CommandBase
    {
        ViewModels.VendingMachineViewModel vendingVM;
        Models.Factory factory;

        public PowerChangeSorterCommand(ViewModels.VendingMachineViewModel vendingMachineViewModel, Models.Factory mainFactory)
        {
            vendingVM = vendingMachineViewModel;
            factory = mainFactory;
        }
        public override void Execute(object parameter)
        {
            if (vendingVM.SorterOn)
            {
                factory.SorterOn();
            }
            else if (vendingVM.SorterOff)
            {
                factory.SorterOff();
            }
        }
    }
}
