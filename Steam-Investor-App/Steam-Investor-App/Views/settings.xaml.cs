
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
            int currency = 0;
            Debug.WriteLine(CurrencyComboBox.SelectedIndex);
            switch (CurrencyComboBox.SelectedIndex)
            {
                case 0:
                    currency = 1;
                    break;
                case 1:
                     currency = 2;
                    break;
                case 2:
                     currency = 3;
                    break;
                case 3:
                     currency = 4;
                    break;
                case 4:
                     currency = 5;
                    break;
                case 5:
                     currency = 6;
                    break;
                case 6:
                    currency = 7;
                    break;
                case 7:
                    currency = 8;
                    break;
                case 8:
                    currency = 15;
                    break;
                





            }
            

            

            Properties.Settings.Default.Currency = currency; //Set the setings

            Properties.Settings.Default.Save();//Saves the settings
        }
        private void loadCurrencySettings()
        {
            switch (Properties.Settings.Default.Currency)
            {
                case 1:
                    CurrencyComboBox.SelectedIndex =0 ;
                    break;
                case 2:
                    CurrencyComboBox.SelectedIndex = 1;
                    break;
                case 3:
                    CurrencyComboBox.SelectedIndex = 2;
                    break;
                case 4:
                    CurrencyComboBox.SelectedIndex = 3;
                    break;
                case 5:
                    CurrencyComboBox.SelectedIndex = 4;
                    break;
                case 6:
                    CurrencyComboBox.SelectedIndex = 5;
                    break;
                case 7:
                    CurrencyComboBox.SelectedIndex = 6;
                    break;
                case 8:
                    CurrencyComboBox.SelectedIndex = 7;
                    break;
                case 15:
                    CurrencyComboBox.SelectedIndex = 8;
                    break;






            }
            
            Debug.WriteLine(Properties.Settings.Default.Currency);
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            saveCurrencySetting();
        }
    }
}
