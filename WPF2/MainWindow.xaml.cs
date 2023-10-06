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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WPF2;
using WPF2.Models;

namespace WPF2
{
    public partial class MainWindow : Window
    {
        private readonly MyDbContext _context = new MyDbContext();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginValid(object sender, TextChangedEventArgs e)
        {

            if (Login.Text.Length<5)
            {
                Login.ToolTip = "Too short login...";
                Login.Background = Brushes.DarkRed;
            }
            else {
                 Login.Background = Brushes.LightGreen;
                Login.ToolTip = "Nice login!";
            }
            if (Login.Text.Length == 0)
                Login.Background = Brushes.Transparent;

        }
        private void PwdValid(object sender, RoutedEventArgs e)
        {

            if (pwd.Password.Length < 5)
            {
                pwd.ToolTip = "Too short password...";
                pwd.Background = Brushes.DarkRed;
            }
            else
            {
                pwd.Background = Brushes.LightGreen;
                pwd.ToolTip = "Nice password!";
            }
            if (pwd.Password.Length == 0)
            {
                pwd.Background = Brushes.Transparent;
                confirmPwd.Background = Brushes.Transparent;
            }
            if (confirmPwd.Password == pwd.Password && pwd.Password.Length!=0)
            {
                confirmPwd.ToolTip = "Correct!";
                confirmPwd.Background = Brushes.LightGreen;
            }
            if (confirmPwd.Password != pwd.Password)
            {
                confirmPwd.ToolTip = "Incorrect password confirmation...";
                confirmPwd.Background = Brushes.DarkRed;
            }
        }
        private void ConfirmPwdValid(object sender, RoutedEventArgs e)
        {
            
            if (confirmPwd.Password.Length < 5)
            {
                confirmPwd.ToolTip = "Too short password...";
                confirmPwd.Background = Brushes.DarkRed;
            }
            else confirmPwd.Background = Brushes.LightGreen;
            if (confirmPwd.Password != pwd.Password)
            {
                confirmPwd.ToolTip = "Incorrect password confirmation...";
                confirmPwd.Background = Brushes.DarkRed;
            }
            else 
            {
                confirmPwd.Background = Brushes.LightGreen;
                confirmPwd.ToolTip = "Correct!";
            }
            if (confirmPwd.Password.Length == 0)
                confirmPwd.Background = Brushes.Transparent;
        }
        private void EmailValid(object sender, TextChangedEventArgs e)
        {
            
            if (Email.Text.Length < 6 || !Email.Text.Contains('@') || !Email.Text.Contains('.'))
            {
                Email.ToolTip = "Incorrect email address";
                Email.Background = Brushes.DarkRed;
            }
            else
            {
                Email.Background = Brushes.LightGreen;
                Email.ToolTip = "Correct email!";
            }
            if (Email.Text.Length == 0)
                Email.Background = Brushes.Transparent;
        }

        private void RegisterFunc(object sender, RoutedEventArgs e)
        {
            if (Login.Background == Brushes.LightGreen && pwd.Background == Brushes.LightGreen && confirmPwd.Background == Brushes.LightGreen && Email.Background == Brushes.LightGreen)
            {
                User _user = new User();
                _user.UserLogin = Login.Text;
                _user.UserPassword = pwd.Password;
                _user.Email = Email.Text;
                _context.Users.Add(_user);
                if (_context.SaveChanges() == 1)
                    MessageBox.Show("success");
                else
                    MessageBox.Show("Error");
            }
            else
                MessageBox.Show("Incorrect fields...");
        }
        private async void LoginView(object sender, RoutedEventArgs e)
        { 
            DoubleAnimation faseAnimation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.2));
            DoubleAnimation fadeAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.2));
            RegisterPanel.BeginAnimation(OpacityProperty, faseAnimation);
            Loginbtn.Style = (Style)this.FindResource("MaterialDesignRaisedButton");
            Registerbtn.Style = (Style)this.FindResource("MaterialDesignFlatButton");
            await Task.Delay(200);
            RegisterPanel.Visibility = Visibility.Collapsed;
            LoginPanel.Visibility = Visibility.Visible;
            LoginPanel.BeginAnimation(OpacityProperty, fadeAnimation);
            await Task.Delay(200);
        }

        private async void RegisterView(object sender, RoutedEventArgs e)
        {
            DoubleAnimation faseAnimation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.2));
            DoubleAnimation fadeAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.2));
            LoginPanel.BeginAnimation(OpacityProperty, faseAnimation);
            Registerbtn.Style = (Style)this.FindResource("MaterialDesignRaisedButton");
            Loginbtn.Style = (Style)this.FindResource("MaterialDesignFlatButton");
            await Task.Delay(200);
            LoginPanel.Visibility = Visibility.Collapsed;
            RegisterPanel.Visibility = Visibility.Visible;
            RegisterPanel.BeginAnimation(OpacityProperty, fadeAnimation);
            await Task.Delay(200);
        }

        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            var User = _context.Users.Where(b => b.UserLogin == Login_Login.Text && b.UserPassword == pwd_Login.Password).FirstOrDefault();
            if(User != null)
            {
                Hide();
                FirstWindow window = new FirstWindow();
                window.Show();
            }
            else
            {
                DoubleAnimation faseAnimation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(1));
                DoubleAnimation fadeAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(1));
                errorLogin.Text = "Incorrect login or password...";
                errorLogin.BeginAnimation(OpacityProperty, fadeAnimation);
                await Task.Delay(3000);
                errorLogin.BeginAnimation(OpacityProperty, faseAnimation);
                await Task.Delay(1000);
                errorLogin.Text = string.Empty;
            }
        }
    }
}
