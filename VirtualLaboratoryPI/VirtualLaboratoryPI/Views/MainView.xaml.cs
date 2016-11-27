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

namespace VirtualLaboratoryPI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            using (LoginView loginWindow = new LoginView())
            {
                loginWindow.ShowDialog();
                if (loginWindow.DialogResult.HasValue && loginWindow.DialogResult.Value)
                {
                    InitializeComponent();
                }
                else
                {
                    this.Close();
                }
            }


        }
    }
}
