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
            Task.Run(() =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    if (GetSteamItems.Loadeditems != 0)
                    {
                        
                        reload.Content = "loading";
                        reload.IsEnabled = false;
                    }
                    
                    
                });
                do
                {
                    
                    this.Dispatcher.Invoke(() => //gives the button free, from the máin thread
                    {
                        if (GetSteamItems.ITemsInTotal != 0)
                        {
                            ProgressBar.Maximum = GetSteamItems.ITemsInTotal;
                        }
                        ProgressBar.Value = GetSteamItems.Loadeditems;

                    });

                    Thread.Sleep(7000);
                } while (GetSteamItems.Loadeditems != 0);
                
            });
            

            
        }

        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            

            new Thread(() => //creating a new thread, so that u can still interact with the UI
            {
                this.Dispatcher.Invoke(() => //gives the button free, from the máin thread
                {

                    reload.Content = "loading";
                    reload.IsEnabled = false;
                });


                Task.Run(()=> GetSteamItems.LoadAllItems());
                Task.Run(() =>
                {
                    
                    do
                    {
                        
                        this.Dispatcher.Invoke(() => //gives the button free, from the máin thread
                        {
                            if (GetSteamItems.ITemsInTotal != 0)
                            {
                                ProgressBar.Maximum = GetSteamItems.ITemsInTotal;
                            }
                            ProgressBar.Value = GetSteamItems.Loadeditems;
                            
                        });
                        
                        Thread.Sleep(7000);
                    } while (GetSteamItems.Loadeditems != 0);

                    this.Dispatcher.Invoke(() =>
                    {
                        ProgressBar.Value = 0;
                        reload.Content = "reload";
                        reload.IsEnabled = true;
                    });
                });
                

                

            }).Start();
            

        }
        
    }
}
