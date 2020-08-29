using Steam_Investor_App.SteamData.SteamMarketJson;
using Steam_Investor_App.ViewModels;
using System.Threading.Tasks;
using System.Windows;

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
            Task.Run(() => //CHeck for any new items
            {
                if (GetSteamItems.checkForNewItems() == true)
                {
                    GetSteamItems.LoadAllItems();//reload all items if there is a new one
                }
            });
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
