using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Shop
{
    public partial class Layout : UserControl
    {
        bool listIsRotated = false;

        public Layout()
        {
            InitializeComponent();
        }

        private void ListByBtn(object sender, RoutedEventArgs e)
        {
            RotateListIcon();
        }
        private async void RotateListIcon()
        {
            RotateTransform rotateTransform = new RotateTransform();
            list.RenderTransformOrigin = new Point(0.5, 0.5);
            list.RenderTransform = rotateTransform;



            if (listIsRotated)
            {
                DoubleAnimation animation2 = new DoubleAnimation();
                animation2.From = 180;
                animation2.To = 0;
                animation2.Duration = TimeSpan.FromSeconds(0.25);
                rotateTransform.BeginAnimation(RotateTransform.AngleProperty, animation2);
                listIsRotated = false;
                ScaleTransform scaleTransform = new ScaleTransform();
                ProductsList.RenderTransform = scaleTransform;
                ProductsList.RenderTransformOrigin = new Point(0.5, 0);
                DoubleAnimation anim = new DoubleAnimation();
                anim.From = 1;
                anim.To = 0;
                anim.Duration = TimeSpan.FromSeconds(0.3);
                scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, anim);
                await Task.Delay(300);
                Panel.SetZIndex(ProductsList, 0);
                ProductsList.Visibility = Visibility.Collapsed;
            }
            else
            {
                DoubleAnimation animation1 = new DoubleAnimation();
                animation1.From = 0;
                animation1.To = 180;
                animation1.Duration = TimeSpan.FromSeconds(0.25);
                rotateTransform.BeginAnimation(RotateTransform.AngleProperty, animation1);
                listIsRotated = true;
                ScaleTransform scaleTransform = new ScaleTransform();
                ProductsList.RenderTransform = scaleTransform;
                ProductsList.RenderTransformOrigin = new Point(0.5, 0);
                DoubleAnimation anim = new DoubleAnimation();
                anim.From = 0;
                anim.To = 1;
                anim.Duration = TimeSpan.FromSeconds(0.3);
                Panel.SetZIndex(ProductsList, 1);
                ProductsList.Visibility = Visibility.Visible;
                scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, anim);
                await Task.Delay(300);
            }

        }
        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Search.Text != "")
            {
                var packIcon = FindChild<PackIcon>(Searchbtn, "searchIcon");
                packIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Close;
                SearchLabel.Visibility = Visibility.Collapsed;
            }
            if (Search.Text == "")
            {
                var packIcon = FindChild<PackIcon>(Searchbtn, "searchIcon");
                packIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Search;
                SearchLabel.Visibility = Visibility.Visible;
            }
        }

        private void Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MessageBox.Show("Your pruducts!!!");
            }
        }

        private void SearchBtn(object sender, RoutedEventArgs e)
        {
            var packIcon = FindChild<PackIcon>(Searchbtn, "searchIcon");
            if (packIcon.Kind == MaterialDesignThemes.Wpf.PackIconKind.Close)
            {

                Search.Text = string.Empty;
                SearchLabel.Visibility = Visibility.Visible;
                packIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Search;
            }
            else
            {
                MessageBox.Show("Nothing!!!");
            }
        }

        private void Heart_MouseEnter(object sender, MouseEventArgs e)
        {
            var heartIcon = FindChild<PackIcon>(HeartBtn, "Heart");
            ColorAnimation animation = new ColorAnimation();
            animation.From = Colors.Black;
            animation.To = Color.FromRgb(143, 143, 143);
            animation.Duration = TimeSpan.FromSeconds(0.2);
            SolidColorBrush brush = new SolidColorBrush(animation.From.Value);
            heartIcon.Foreground = brush;
            brush.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }
        private void Heart_MouseLeave(object sender, MouseEventArgs e)
        {
            var heartIcon = FindChild<PackIcon>(HeartBtn, "Heart");
            ColorAnimation animation = new ColorAnimation();
            animation.From = Color.FromRgb(143, 143, 143);
            animation.To = Colors.Black;
            animation.Duration = TimeSpan.FromSeconds(0.2);
            SolidColorBrush brush = new SolidColorBrush(animation.From.Value);
            heartIcon.Foreground = brush;
            brush.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }


        private void Person_MouseEnter(object sender, MouseEventArgs e)
        {
            var personIcon = FindChild<PackIcon>(PersonBtn, "Person");
            ColorAnimation animation = new ColorAnimation();
            animation.From = Colors.Black;
            animation.To = Color.FromRgb(143, 143, 143);
            animation.Duration = TimeSpan.FromSeconds(0.2);
            SolidColorBrush brush = new SolidColorBrush(animation.From.Value);
            personIcon.Foreground = brush;
            brush.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }

        private void Person_MouseLeave(object sender, MouseEventArgs e)
        {
            var personIcon = FindChild<PackIcon>(PersonBtn, "Person");
            ColorAnimation animation = new ColorAnimation();
            animation.From = Color.FromRgb(143, 143, 143);
            animation.To = Colors.Black;
            animation.Duration = TimeSpan.FromSeconds(0.2);
            SolidColorBrush brush = new SolidColorBrush(animation.From.Value);
            personIcon.Foreground = brush;
            brush.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }

        private T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            if (parent == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                string controlName = child.GetValue(Control.NameProperty) as string;
                if (controlName == childName)
                {
                    return child as T;
                }
                else
                {
                    T result = FindChild<T>(child, childName);
                    if (result != null)
                        return result;
                }
            }

            return null;
        }

        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;

            if (parentObject is T parent)
            {
                return parent;
            }
            else
            {
                return FindParent<T>(parentObject);
            }
        }
        private void Icon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var border = (Border)sender;
            if (border == prod)
                MessageBox.Show("Clicked!!!");
            else if (border == dprod)
                MessageBox.Show("Clicked!");
            else if (border != prod && border != dprod)
                MessageBox.Show("Clicked");
        }
    }
}
