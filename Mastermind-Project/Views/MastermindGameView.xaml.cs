using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mastermind_Project.Views
{
    /// <summary>
    /// Interaction logic for MastermindGameView.xaml
    /// </summary>
    public partial class MastermindGameView : UserControl
    {
        public MastermindGameView()
        {
            InitializeComponent();
        }

        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Ellipse pin = (Ellipse)sender;

            pin.Fill = new SolidColorBrush(Colors.Blue);
            e.Handled = true;
        }
    }
}
