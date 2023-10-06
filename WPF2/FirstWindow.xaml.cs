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
using System.Windows.Shapes;

namespace WPF2
{
    public partial class FirstWindow : Window
    {
        public FirstWindow()
        {
            InitializeComponent();
           //. LogoHeight();

        }
        private void LogoHeight()
        {
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double desiredButtonHeight = (3.0 / 15) * screenHeight;
           // Logo.Height = desiredButtonHeight;
        }
    }
}
