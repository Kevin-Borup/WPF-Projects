using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_VendingMachine.Commands
{
    internal class AutomaticProductionCommand : CommandBase
    {
        Models.Factory factory;
        ViewModels.VendingMachineViewModel vendingVM;

        public AutomaticProductionCommand(ViewModels.VendingMachineViewModel vendingMachineViewModel, Models.Factory mainFactory)
        {
            factory = mainFactory;
            vendingVM = vendingMachineViewModel;
        }

        public override void Execute(object parameter)
        {
            if (vendingVM.AutomaticOn)
            {
                factory.AutomaticGenerationOn();
            } else if (vendingVM.AutomaticOff)
            {
                factory.AutomaticGenerationOff();
            }
        }
    }
}
