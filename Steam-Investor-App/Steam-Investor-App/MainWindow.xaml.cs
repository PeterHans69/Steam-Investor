using NLog;
using Steam_Investor_App.SteamData.SteamMarketJson;
using Steam_Investor_App.ViewModels;
using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.IO;
using System.Reflection;

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
