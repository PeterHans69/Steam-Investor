using Steam_Investor_App.SteamData;
using Steam_Investor_App.SteamData.SteamMarketJson;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            quantityLabel.Foreground = (Brush)bc.ConvertFrom("#f5f6fa");
            nameLabel.Foreground = (Brush)bc.ConvertFrom("#f5f6fa");

            
            if (checkName() == false)
            {
                nameLabel.Foreground = (Brush)bc.ConvertFrom("#e84118");
                conditionLabel.Foreground = (Brush)bc.ConvertFrom("#e84118");
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
            
            if(everythingIsCorrect == true)
            {
                int currency = 3;//I made this variables, to avoid the Exception that this thread is used by another process
                string name = ItemNameWPF.Text;
                string condition = getSelectcetdCondition();
                var price = Task.Run(() => GetSteamItems.GetItemPriceForAddItem(name, condition, currency).Result);

                if (price != null)
                {
                    Item item = new Item(ItemNameWPF.Text, getSelectcetdCondition(), quantityWPF.Text, pricePerItemWPF.Text, priceGoalWPF.Text, price.Result); //Creates a new item
                    sp.Children.Add(item);

                    //save item in Array
                    SteamItemJson JsonItem = new SteamItemJson();
                    JsonItem.itemName = ItemNameWPF.Text;
                    JsonItem.itemQuantity = quantityWPF.Text;
                    JsonItem.ItemBuyPrice = pricePerItemWPF.Text;
                    JsonItem.ItemPriceGoal = priceGoalWPF.Text;
                    JsonItem.itemCondition = getSelectcetdCondition();
                    JsonItem.itemPrice = price.Result;

                    MySteamItems.AddItemToJSON(JsonItem);
                    this.Close();
                }
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
            if (ItemNameWPF.Text == "")
            {
                return false;
            }
            
            if (getSelectcetdCondition() == "No Condition")
            {
                if (SteamItem.searchforItem(ItemNameWPF.Text) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (SteamItem.searchforItem(ItemNameWPF.Text + " (" + getSelectcetdCondition() + ")") == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }


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

            if (conditionWPF.SelectedIndex.ToString() == "0")
            {
                return "No Condition";
            }
            else if (conditionWPF.SelectedIndex.ToString() == "1")
            {
                return "Field-Tested";
            }
            else if (conditionWPF.SelectedIndex.ToString() == "2")
            {
                return "Minimal Wear";
            }
            else if (conditionWPF.SelectedIndex.ToString() == "3")
            {
                return "Battle-Scarred";
            }
            else if (conditionWPF.SelectedIndex.ToString() == "4")
            {
                return "Well-Worn";
            }
            else if (conditionWPF.SelectedIndex.ToString() == "5")
            {
                return "Factory New";
            }
            else 
            {
                return "Not Painted";
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
