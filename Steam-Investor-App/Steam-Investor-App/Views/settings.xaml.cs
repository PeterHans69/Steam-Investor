
using Steam_Investor_App.SteamData;
using Steam_Investor_App.SteamData.SteamMarketJson;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Steam_Investor_App.Views
{
    /// <summary>
    /// Interaktionslogik für settings.xaml
    /// </summary>
    public partial class settings : UserControl
    {
        public settings()
        {      
            
            
            InitializeComponent();
            loadCurrencySettings();
            
           
            

            
        }

        

        
        private void saveCurrencySetting()
        {
            Debug.WriteLine(CurrencyComboBox.SelectedIndex);
            int currency = CurrencyComboBox.SelectedIndex+1;//+1 because there is no currency with the index 0 on steam, it starts with 1

            Properties.Settings.Default.Currency = currency; //Set the setings

            Properties.Settings.Default.Save();//Saves the settings
        }
        private void loadCurrencySettings()
        {
            CurrencyComboBox.SelectedIndex = Properties.Settings.Default.Currency-1;
            Debug.WriteLine(Properties.Settings.Default.Currency);
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            saveCurrencySetting();
        }
    }
}
