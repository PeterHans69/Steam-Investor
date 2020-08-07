using DocumentFormat.OpenXml.Spreadsheet;
using Steam_Investor_App.ViewModels;
using Steam_Investor_App.Views;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Steam_Investor_App.Windows
{
    /// <summary>
    /// Interaktionslogik für AddItem.xaml
    /// </summary>
    public partial class AddItemWindows : Window
    {
        StackPanel sp;
        public AddItemWindows(StackPanel stackpanel) //needs the stack panel to add the item
        {
            sp = stackpanel;
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        double pricePerItem;
        BrushConverter bc = new BrushConverter();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Item item = new Item();
            if (checkPrice() == false)
            {
               pricePerItemLabel.Foreground = (Brush)bc.ConvertFrom("#e84118");
            }
            sp.Children.Add(item);
            
        }
        public bool checkPrice()
        {
            try
            {
                pricePerItem= Convert.ToDouble(pricePerItemWPF);
                if (pricePerItem < 0.03){
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
