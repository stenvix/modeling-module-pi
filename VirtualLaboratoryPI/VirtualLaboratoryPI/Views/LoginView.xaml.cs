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
using GalaSoft.MvvmLight.Views;
using VirtualLaboratoryPI.ViewModels;
using GalaSoft.MvvmLight;

namespace VirtualLaboratoryPI
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginView : Window, IDisposable
    {
        private LoginViewModel ViewModel { get; set; }
        public LoginView()
        {
            InitializeComponent();
            ViewModel = (LoginViewModel)DataContext;
        }

        public void Dispose()
        {
            if (ViewModel != null)
            {
                ViewModel.Dispose();
            }
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SignIn(loginBox.Text, passwordBox.Password))
            {
                this.DialogResult = true;
                this.Close();
            }
        }
    }
}
