using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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

            loadMySteamItems();
            refresh.Content = "loading";
            Task.Run(() => MySteamItems.UpdateAllItems());
            Task.WaitAll();
            refresh.Content = "refresh";
            loadMySteamItems();

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
            
            Task.Run(() =>  MySteamItems.UpdateAllItems());
            loadMySteamItems();
            
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
    }
}
