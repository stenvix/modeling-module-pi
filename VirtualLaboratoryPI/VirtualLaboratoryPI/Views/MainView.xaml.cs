using GraphX.Controls;
using GraphX.PCL.Common.Enums;
using GraphX.PCL.Logic.Algorithms.LayoutAlgorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VirtualLaboratoryPI.Graph.Area;
using VirtualLaboratoryPI.Graph.Data;
using VirtualLaboratoryPI.Graph.Data.Vertex;
using VirtualLaboratoryPI.Graph.Logic;
using VirtualLaboratoryPI.ViewModels;

namespace VirtualLaboratoryPI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        MainViewModel Context { get; set; }
        public MainView()
        {

            //using (LoginView loginWindow = new LoginView())
            //{
            //    loginWindow.ShowDialog();
            //    if (loginWindow.DialogResult.HasValue && loginWindow.DialogResult.Value)
            //    {
            //        InitializeComponent();
            //    }
            //    else
            //    {
            //        this.Close();
            //    }
            //}

            InitializeComponent();

            ZoomControl.SetViewFinderVisibility(zoomctrl, Visibility.Visible);

            zoomctrl.ZoomToFill();

            Context = (MainViewModel)this.DataContext;

            Context.GraphAreaExampleSetup(Area, zoomctrl);
            
        }

        private void toggleButton_checked(object sender, RoutedEventArgs e)
        {
            var button = (ToggleButton)sender;

            textBox.IsEnabled = false;
            textBox.IsReadOnly = true;

            Area.SetVerticesDrag(false, true);

            if (moveButton.IsChecked == true && sender == moveButton)
            {
                Area.SetVerticesDrag(true, true);
                selectButton.IsChecked = false;
                blockButton.IsChecked = false;
                pointButton.IsChecked = false;
                rhombButton.IsChecked = false;
                deleteButton.IsChecked = false;
            }
            if (selectButton.IsChecked == true && sender == selectButton)
            {
                blockButton.IsChecked = false;
                pointButton.IsChecked = false;
                rhombButton.IsChecked = false;
                moveButton.IsChecked = false;
                deleteButton.IsChecked = false;
            }
            if (deleteButton.IsChecked == true && sender == deleteButton)
            {
                blockButton.IsChecked = false;
                pointButton.IsChecked = false;
                rhombButton.IsChecked = false;
                moveButton.IsChecked = false;
                selectButton.IsChecked = false;
            }
            if (blockButton.IsChecked == true && sender == blockButton)
            {
                selectButton.IsChecked = false;
                pointButton.IsChecked = false;
                moveButton.IsChecked = false;
                rhombButton.IsChecked = false;
                deleteButton.IsChecked = false;
                textBox.IsEnabled = true;
                textBox.IsReadOnly = false;
            }
            if (pointButton.IsChecked == true && sender == pointButton)
            {
                selectButton.IsChecked = false;
                blockButton.IsChecked = false;
                moveButton.IsChecked = false;
                rhombButton.IsChecked = false;
                deleteButton.IsChecked = false;
            }
            if (rhombButton.IsChecked == true && sender == rhombButton)
            {
                selectButton.IsChecked = false;
                blockButton.IsChecked = false;
                moveButton.IsChecked = false;
                pointButton.IsChecked = false;
                deleteButton.IsChecked = false;
            }

            Context.ButtonChanged(button.Name);
        }
    }
}
