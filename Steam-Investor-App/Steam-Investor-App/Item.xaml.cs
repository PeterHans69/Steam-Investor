using DocumentFormat.OpenXml.Drawing.Charts;
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
        public Item(string name, string condition,string quantity,string price, string priceGoal)
        {
            InitializeComponent();

            this.priceGoalXaml.Content = priceGoal;
            this.itemNameXaml.Content = name;
            this.conditionXaml.Content = condition;
            this.quantityXaml.Content = quantity;
            this.priceXaml.Content = price;
            if(quantity!="No Condition")
            {
                url= "https://steamcommunity.com/market/listings/730/" + name + " (" + condition+")";
                
            }
            else
            {
                url = "https://steamcommunity.com/market/listings/730/" + name;
            }
            Debug.WriteLine(url);
            Uri myUri = new Uri(url, UriKind.Absolute); //makes new uri with correct link
            Hyperlink.NavigateUri = myUri;

        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as StackPanel).Children.Remove(this);

        }
        private void OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
            e.Handled = true;
        }
    }
}
