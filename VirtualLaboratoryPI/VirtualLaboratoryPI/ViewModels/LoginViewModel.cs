using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace VirtualLaboratoryPI.ViewModels
{
    public class LoginViewModel : ViewModelBase, IDisposable
    {
        public void Dispose()
        {
            return;
        }

        public bool SignIn(string login, string password)
        {
            return (login == "root" && password == "root");           
        }
    }
}
