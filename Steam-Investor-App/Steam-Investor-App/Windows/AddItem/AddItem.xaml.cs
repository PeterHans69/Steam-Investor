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
        double priceGoal;
        BrushConverter bc = new BrushConverter();
        bool everythingIsCorrect;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            everythingIsCorrect = true;
            nameLabel.Foreground = (Brush)bc.ConvertFrom("#f5f6fa");
            quantityLabel.Foreground = (Brush)bc.ConvertFrom("#f5f6fa"); 
            pricePerItemLabel.Foreground= (Brush)bc.ConvertFrom("#f5f6fa");
            priceGoalLabel.Foreground = (Brush)bc.ConvertFrom("#f5f6fa");
            conditionLabel.Foreground = (Brush)bc.ConvertFrom("#f5f6fa");
            quantityLabel.Foreground = (Brush)bc.ConvertFrom("#f5f6fa");
            nameLabel.Foreground = (Brush)bc.ConvertFrom("#f5f6fa");

            
            if (checkName() == false)
            {
                nameLabel.Foreground = (Brush)bc.ConvertFrom("#e84118");
                everythingIsCorrect = false;
            }
            if (quantityCheck() == false)
            {
                quantityLabel.Foreground = (Brush)bc.ConvertFrom("#e84118");
                everythingIsCorrect = false;
            }
            if (checkPrice() == false)
            {
               pricePerItemLabel.Foreground = (Brush)bc.ConvertFrom("#e84118");
                everythingIsCorrect = false;
            }
            if (checkPriceGoal() == false)
            {
                priceGoalLabel.Foreground = (Brush)bc.ConvertFrom("#e84118");
                everythingIsCorrect = false;
            }
            if (getSelectcetdCondition() == "")
            {
                conditionLabel.Foreground = (Brush)bc.ConvertFrom("#e84118");
                everythingIsCorrect = false;
            }
            if (everythingIsCorrect == true)
            {
                Item item = new Item();
                sp.Children.Add(item);
            }
            

        }
        public bool checkPrice()
        {
            try
            {
                pricePerItem= Convert.ToDouble(pricePerItemWPF.Text);
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
        public bool checkName()
        {
            return true;
        }
        public bool checkPriceGoal()
        {
            try
            {
                priceGoal = Convert.ToDouble(priceGoalWPF.Text);
                if (priceGoal < 0.03)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public string getSelectcetdCondition()
        {
            if (conditionWPF.SelectedItem == null)
            {
                return "";
            }
            else
            {
                return conditionWPF.SelectedItem.ToString();
            }
            
        }

        public bool quantityCheck()
        {
            try
            {
               var quantity = Convert.ToInt32(quantityWPF.Text);
                if (quantity == 0)
                {
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
