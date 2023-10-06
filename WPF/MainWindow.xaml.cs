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
using System.Data;
using System.Diagnostics.SymbolStore;
using System.Xml.Linq;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;
using System.ComponentModel.Design;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : UserControl
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            StartAnimation();
        }
        private async void StartAnimation()
        {
            if (el.Visibility == Visibility.Collapsed)
            {
                ScaleTransform scaleTransform = new ScaleTransform();
                el.RenderTransform = scaleTransform;
                el.RenderTransformOrigin = new Point(0.5, 0);
                DoubleAnimation anim = new DoubleAnimation();
                anim.From = 0;
                anim.To = 1;
                anim.Duration = TimeSpan.FromSeconds(0.3);
                Panel.SetZIndex(el, 1);
                el.Visibility = Visibility.Visible;
                scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, anim);
                await Task.Delay(300);
            }
            else
            {
                ScaleTransform scaleTransform = new ScaleTransform();
                el.RenderTransform = scaleTransform;
                el.RenderTransformOrigin = new Point(0.5, 0);
                DoubleAnimation anim = new DoubleAnimation();
                anim.From = 1;
                anim.To = 0;
                anim.Duration = TimeSpan.FromSeconds(0.3);

                // Запуск анімації для масштабу
                scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, anim);
                await Task.Delay(300);
                Panel.SetZIndex(el, 0);
                el.Visibility = Visibility.Collapsed;
            }
        }

       
    }
}
