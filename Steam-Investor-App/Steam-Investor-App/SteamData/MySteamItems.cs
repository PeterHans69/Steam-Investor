using DocumentFormat.OpenXml.Office.CustomUI;
using Newtonsoft.Json;
using Steam_Investor_App.SteamData.SteamMarketJson;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Steam_Investor_App.SteamData
{
    public static class MySteamItems
    {

        static int currency = Properties.Settings.Default.Currency;
        static List<SteamItemJson> mySteamItems;
        
        public static void AddItemToJSON(SteamItemJson item)
        {
             currency = Properties.Settings.Default.Currency;
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


            mySteamItems.Add(item);
            //write json

            string jsonMySteamItems = JsonConvert.SerializeObject(mySteamItems);
            File.WriteAllText(System.IO.Path.GetFullPath(@"..\..\SteamData\MySteamItems.json"), jsonMySteamItems);
        }
        public static void removeJsonItem(int index)
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


            mySteamItems.RemoveAt(index);
            var jsonResult = JsonConvert.SerializeObject(mySteamItems);

            File.WriteAllText(System.IO.Path.GetFullPath(@"..\..\SteamData\MySteamItems.json"), jsonResult);

        }

        public static Task UpdateAllItems()
        {
            currency = Properties.Settings.Default.Currency;
            List<SteamItemJson> myNewSteamItems = new List<SteamItemJson>();
            var fileContent = File.ReadAllText(System.IO.Path.GetFullPath(@"..\..\SteamData\MySteamItems.json"));
            try
            {
                mySteamItems = JsonConvert.DeserializeObject<List<SteamItemJson>>(fileContent);
            }
            catch
            {

            }
            if (mySteamItems != null)
            {
                foreach (SteamItemJson item in mySteamItems)
                {
                   Task.Run(async () => item.itemPrice = await GetSteamItems.GetItemPrice(item.itemName, item.itemCondition, currency)).Wait();
                  
                }
                var jsonResult = JsonConvert.SerializeObject(mySteamItems);
                File.WriteAllText(System.IO.Path.GetFullPath(@"..\..\SteamData\MySteamItems.json"), jsonResult);
            }

            return Task.CompletedTask;
        }

              
                
    }
    public class SteamItemJson
    {
        public  string itemName { get; set; }

        public string itemPrice { get; set; }

        public string itemQuantity { get; set; }
        public string ItemPriceGoal { get; set; }
        public string ItemBuyPrice { get; set; }

        public string itemCondition { get; set; }
        


    }
}

