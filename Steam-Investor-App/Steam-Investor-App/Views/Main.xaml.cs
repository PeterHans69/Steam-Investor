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
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Math;


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
            loadAllItems();
            loadConclusion();
            updateCharData();
            CartesianMonth();                       
        }
        //Cartesian chart

        

        
        

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

            InitializeComponent(); 
            loadAllItems();
            loadConclusion();
            updateCharData();
            CartesianMonth();

        }
        List<SteamItemJson> mySteamItems;
        private void loadMySteamItems()
        {
            var fileContent = File.ReadAllText(MyPathes.MySteamItems);
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
            overAllProfit = 0;
            overAllProfitWithTaxes = 0;
            total = 0;
            BrushConverter bc = new BrushConverter();
            
            foreach(Item item in ItemList.Children)
            {
                overAllProfit = overAllProfit + Convert.ToDouble(item.profitXaml.Content);
                overAllProfitWithTaxes = overAllProfitWithTaxes + Convert.ToDouble(item.profitWithTaxesXaml.Content);
                total = total + Convert.ToDouble(FormatDouble(item.itemPrice));


            }
            if (ItemList.Children.Count == 0)//if there are no items then:
            {
                overAllProfit = 0;
                overAllProfitWithTaxes = 0;
                total = 0;
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
        public string FormatDouble(string entrance)
        {
            string exit = "";

            foreach (char myChar in entrance)
            {
                if (myChar == '0' || myChar == '1' || myChar == '2' || myChar == '3' || myChar == '4' || myChar == '5' || myChar == '6' || myChar == '7' || myChar == '8' || myChar == '9' || myChar == ',' )
                {
                    exit = exit + myChar;
                }
                if (myChar == '.')
                {
                    exit = exit + ',';
                }
            }





            return exit;
        }

        #region Char;
        double[] CharData = new double[30];
            
        
        
        private void CartesianMonth()
        {
            //string data = JsonConvert.SerializeObject(CharData);
            string data = File.ReadAllText(MyPathes .charData);
            CharData = JsonConvert.DeserializeObject<double[]>(data);//every number

            int counter = 0;
            foreach(double d in CharData)
            {
                if (d != 0)
                {
                    counter++;
                }
            }
            
            double[] CharData_2 = new double[counter];//every number except for 0
            counter = 0;
            foreach (double d in CharData)
            {
                if (d != 0)
                {
                    CharData_2[counter] = d;
                    counter++; 
                }
            }


            SeriesCollectionMonth = new SeriesCollection
            {
                new LineSeries
                {

                    Title="Profit", Values = new ChartValues<double>(CharData_2)
                }

            };



            DataContext = this;
        }//loads Char

        private void updateCharData()//Updates Char data
        {
            string data = File.ReadAllText(MyPathes.charData);
            CharData = JsonConvert.DeserializeObject<double[]>(data);
            string currentDate = DateTime.Today.ToString();


            if (CharData != null)
            {
                
                string lastDate = File.ReadAllText(MyPathes.date);

                if (lastDate == currentDate)//If its the same day
                {
                    CharData[30] = overAllProfit;//Just change the profit from today
                }
                else
                {
                    for (int i = 0; i <= 29; i++)//if its another day
                    {
                        Debug.WriteLine("Index :" + i);
                        CharData[i] = CharData[i + 1];  //backshift  every number and add the profit fromthe current day              
                    }
                    CharData[30] = overAllProfit;
                }
            }
            else
            {
                CharData = new double[31];
            }
            var data_Safe = JsonConvert.SerializeObject(CharData);
            File.WriteAllText(MyPathes.charData, data_Safe);
            File.WriteAllText(MyPathes.date, currentDate);
            
        }
        public Func<double, string> yFormatterMonth { get; set; }
        public SeriesCollection SeriesCollectionMonth { get; set; }
        public string[] Labels { get; set; }




        #endregion
    }
}
