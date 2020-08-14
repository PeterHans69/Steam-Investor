using Steam_Investor_App.SteamData;
using Steam_Investor_App.SteamData.SteamMarketJson;
using Steam_Investor_App.ViewModels;
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

namespace Steam_Investor_App
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        MainViewModel mvm = new MainViewModel();
        public MainWindow()
        {

            //Task.Run(() => GetSteamItems.LoadAllItemsAsync());
             GetSteamItems.LoadAllItemsAsync();


            InitializeComponent();
            

            DataContext = mvm;

        }

        

        private void MainView_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = mvm;
        }

        private void helpView_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new helpViewModel();
            
        }

        private void settingsView_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new settingsViewModel();
        }
        
       

        
        
    }
}
