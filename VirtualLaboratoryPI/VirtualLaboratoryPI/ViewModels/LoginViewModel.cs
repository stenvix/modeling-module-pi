using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;

namespace VirtualLaboratoryPI.ViewModels
{
    public class LoginViewModel: ViewModelBase
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public RelayCommand SignInCommand {
            get
            {
                return new RelayCommand(SignIn);
            }
        }

        private void SignIn()
        {        }
    }
}
