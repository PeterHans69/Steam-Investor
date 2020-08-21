using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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
using DocumentFormat.OpenXml.Drawing.Charts;
using LiveCharts;
using LiveCharts.Wpf;
using Newtonsoft.Json;
using Steam_Investor_App.SteamData;
using Steam_Investor_App.ViewModels;
using Steam_Investor_App.Windows;

namespace Steam_Investor_App.Views
{
    /// <summary>
    /// Interaktionslogik für UserControl1.xaml
    /// </summary>
    public partial class Main : UserControl
    {
        
        public Main()
        {
            InitializeComponent();
           
            this.CartesianMonth();

            
            loadAllItems();
            loadConclusion();
        }
        //Cartesian chart

        

        public void CartesianMonth()
        {
            SeriesCollectionMonth = new SeriesCollection
            {
                new LineSeries
                {

                    Title="Profit", Values = new ChartValues<double>{ 232, 400, 600,500,300,234,232, 400, 600, 500, 300, 234, -232, 400, 600, 500, 300, 234, 232, 400, 600, 500, 300, 234, 232, 400, 600, 500, 300, 234, 232 }
                }

            };

            
            
            DataContext = this;
        }
        public Func<double, string> yFormatterMonth { get; set; }
        public SeriesCollection SeriesCollectionMonth { get; set; }
        public string[] Labels { get; set; }
        

        //adds an Item to the stack Panel
        public void addItem(UserControl userControl)
        {
            ItemList.Children.Add(userControl);
        }
        

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainClass.addItemWindow(ItemList);


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {            
            loadAllItems();
            loadConclusion();
        }
        List<SteamItemJson> mySteamItems;
        private void loadMySteamItems()
        {
            var fileContent = File.ReadAllText(System.IO.Path.GetFullPath(@"..\..\SteamData\MySteamItems.json"));
            try
            {
                mySteamItems = JsonConvert.DeserializeObject<List<SteamItemJson>>(fileContent);
            }
            catch
            {

            }
            if (mySteamItems == null)
            {
                mySteamItems = new List<SteamItemJson>();
            }

            ItemList.Children.Clear();
            foreach(SteamItemJson JsonItem in mySteamItems)
            {
                
                Item item = new Item(JsonItem.itemName,JsonItem.itemCondition,JsonItem.itemQuantity,JsonItem.ItemBuyPrice,JsonItem.ItemPriceGoal,JsonItem.itemPrice);

                ItemList.Children.Add(item);
            }
        }
        private void loadAllItems()
        {
            new Thread(() => //creating a new thread, so that u can still interact with the UI
            {
                this.Dispatcher.Invoke(() => //gives the button free, from the máin thread
                {

                    refresh.Content = "loading";
                    refresh.IsEnabled = false;
                });


                MySteamItems.UpdateAllItems();

                this.Dispatcher.Invoke(() =>
                {

                    refresh.Content = "refresh";
                    refresh.IsEnabled = true;
                });

            }).Start();
            loadMySteamItems();
        }


        double overAllProfit = 0;
        double overAllProfitWithTaxes = 0;
        double total = 0;
        private void loadConclusion()
        {
            BrushConverter bc = new BrushConverter();
            
            foreach(Item item in ItemList.Children)
            {
                overAllProfit = overAllProfit + Convert.ToDouble(item.profitXaml.Content);
                overAllProfitWithTaxes = overAllProfitWithTaxes + Convert.ToDouble(item.profitWithTaxesXaml.Content);
                total = Convert.ToDouble(item.buyPriceXaml.Content) + Convert.ToDouble(item.profitXaml.Content);
            }
            totalXaml.Content = Math.Round(total,2)+" $";
            
            profitWithTaxes.Content = Math.Round(overAllProfitWithTaxes,2)+" $";

            if (overAllProfitWithTaxes >= 0)
            {
                profitWithTaxes.Foreground = (Brush)bc.ConvertFrom("#44bd32");
            }
            else
            {
                profitWithTaxes.Foreground = (Brush)bc.ConvertFrom("#e84118");
            }


            overAllProfitXaml.Content = Math.Round( overAllProfit,2) + " $";
            overAllProfitXaml2.Content = Math.Round( overAllProfit,2) + " $";
            if (overAllProfit >= 0)
            {
                overAllProfitXaml.Foreground= (Brush)bc.ConvertFrom("#44bd32");
                overAllProfitXaml2.Foreground = (Brush)bc.ConvertFrom("#44bd32");

            }
            else
            {
                overAllProfitXaml.Foreground = (Brush)bc.ConvertFrom("#e84118");
                overAllProfitXaml2.Foreground = (Brush)bc.ConvertFrom("#e84118");
            }
        
            
        }
        

        
    }
}
