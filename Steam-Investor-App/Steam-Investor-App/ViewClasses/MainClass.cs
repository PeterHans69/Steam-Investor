using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Spreadsheet;
using Steam_Investor_App.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Steam_Investor_App.ViewModels
{
    public static class MainClass
    {



        static AddItemWindows addItem=null;
        
        public static void addItemWindow(StackPanel sp)
        {
            
            if (addItem==null)
            {
                addItem = new AddItemWindows(sp);
                addItem.Closed += (sender, args) => addItem = null;
                addItem.Show();
            }
            else
            {
                addItem.Activate();
            }
        }

        
    }
}
