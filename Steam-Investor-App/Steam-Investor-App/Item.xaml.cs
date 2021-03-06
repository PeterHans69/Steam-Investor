﻿using DocumentFormat.OpenXml.Drawing.Charts;
using Steam_Investor_App.SteamData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaktionslogik für Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {

        string url;
        public string itemPrice;
        public int itemQuantity;
        public Item(string name, string condition,string quantity,string _buyPrice, string priceGoal, string price)
        {
            double buyPrice = Convert.ToDouble(_buyPrice);
            InitializeComponent();
            double currentPrice;
            priceGoalXaml.Content = priceGoal;
            itemNameXaml.Content = name;
            conditionXaml.Content = "";
            if(condition != "No Condition")
            {
                conditionXaml.Content = condition;
            }
            quantityXaml.Content = quantity;
            itemQuantity = Convert.ToInt32(quantity);
            buyPriceXaml.Content = _buyPrice;
            currrentPriceXaml.Content = price;
            itemPrice = price;
            double profit = 0;
            
            if (price.Contains("pуб."))//if its russian
            {
                currentPrice = Convert.ToDouble(FormatDoubleRUB(price));
            }
            if (price.Contains("₫"))
            {
                currentPrice = Convert.ToDouble(FormatDoubleVND(price));
            }
            else
            {

                currentPrice = Convert.ToDouble(FormatDouble(price));
            }
            profit = currentPrice - Convert.ToDouble(buyPrice);
            double d_quantity = Convert.ToDouble(quantity);
            profitXaml.Content = Math.Round(profit*d_quantity, 2) ;
            double taxes = currentPrice  / 100 * 15;
            double profitWithTaxes;
            
            profitWithTaxes = Math.Round((Math.Round(currentPrice - taxes, 2) - buyPrice) * d_quantity,2);
            
            
            
            profitWithTaxesXaml.Content = profitWithTaxes ;
            
            //Set colors of the Labels
            BrushConverter bc = new BrushConverter();
            if (profit > 0)
            {
                profitXaml.Foreground = (Brush)bc.ConvertFrom("#44bd32");
            }
            else
            {
                profitXaml.Foreground = (Brush)bc.ConvertFrom("#e84118");
            }

            if (profitWithTaxes > 0)
            {
               profitWithTaxesXaml.Foreground = (Brush)bc.ConvertFrom("#44bd32");
            }
            else
            {
                profitWithTaxesXaml.Foreground = (Brush)bc.ConvertFrom("#e84118");
            }






            if (condition!="No Condition")
            {
                url= "https://steamcommunity.com/market/listings/730/" + name + " (" + condition+")";
                
            }
            else
            {
                url = "https://steamcommunity.com/market/listings/730/" + name;
            }
           
            Uri myUri = new Uri(url, UriKind.Absolute); //makes new uri with correct link
            Hyperlink.NavigateUri = myUri;

            

        }
        public string FormatDouble(string entrance)
        {
            string exit="";

            foreach (char myChar in entrance)
            {
                if (myChar == '0' || myChar == '1' || myChar == '2' || myChar == '3' || myChar == '4' || myChar == '5' || myChar == '6' || myChar == '7' || myChar == '8' || myChar == '9' || myChar == ',')
                {
                    exit = exit + myChar;
                }
                if (myChar == '.')
                {
                    exit = exit + ',';
                }
            }
            Debug.WriteLine("Self Converted Number" + exit);
            return exit;
        }
        public string FormatDoubleRUB(string entrance)
        {
            string exit = "";

            foreach (char myChar in entrance)
            {
                if (myChar == '0' || myChar == '1' || myChar == '2' || myChar == '3' || myChar == '4' || myChar == '5' || myChar == '6' || myChar == '7' || myChar == '8' || myChar == '9' || myChar == ',')
                {
                    exit = exit + myChar;
                }

            }





            return exit;
        }
        public string FormatDoubleVND(string entrance)
        {
            string exit = "";

            foreach (char myChar in entrance)
            {
                if (myChar == '0' || myChar == '1' || myChar == '2' || myChar == '3' || myChar == '4' || myChar == '5' || myChar == '6' || myChar == '7' || myChar == '8' || myChar == '9' || myChar == ',')
                {
                    exit = exit + myChar;
                }

            }

            return exit;
        }



        public double FormatProfitWithTaxes(double _entrance) //So I dont have 3 	position after decimal point
        {
            string entrance = Convert.ToString(_entrance);
            int counter = 0;
            string exit = "";
            bool commaAppeared = false;
            foreach (char myChar in entrance)
            {
                if ( myChar == '.')
                {
                    commaAppeared = true;
                }
                if (commaAppeared==true)
                {
                    counter++;
                }
                exit = exit + myChar;
                if (counter <= 2)
                {
                    Debug.WriteLine(exit+"fsdfsf");
                    return Convert.ToDouble(exit);
                }
                
            }

            return Convert.ToDouble(exit);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            MySteamItems.removeJsonItem((this.Parent as StackPanel).Children.IndexOf(this));//removes item from MySteamItems.json list

            
            (this.Parent as StackPanel).Children.Remove(this);// removes the item froom the usercontrol

        }
        private void OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
            e.Handled = true;
        }
    }
}
