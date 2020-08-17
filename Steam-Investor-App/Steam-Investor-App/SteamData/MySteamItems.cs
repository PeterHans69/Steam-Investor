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
        
        static List<SteamItemForJson> mySteamItems;
        
        public static void AddItemToJSON(SteamItemForJson item)
        {
            var fileContent = File.ReadAllText(System.IO.Path.GetFullPath(@"..\..\SteamData\MySteamItems.json"));
            try
            {
                mySteamItems = JsonConvert.DeserializeObject<List<SteamItemForJson>>(fileContent);
            }
            catch
            {

            }
            if (mySteamItems == null)
            {
                mySteamItems = new List<SteamItemForJson>();
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
                mySteamItems = JsonConvert.DeserializeObject<List<SteamItemForJson>>(fileContent);
            }
            catch
            {

            }
            if (mySteamItems == null)
            {
                mySteamItems = new List<SteamItemForJson>();
            }


            mySteamItems.RemoveAt(index);
            var jsonResult = JsonConvert.SerializeObject(mySteamItems);

            File.WriteAllText(System.IO.Path.GetFullPath(@"..\..\SteamData\MySteamItems.json"), jsonResult);

        }


        static HttpClient httpClient = new HttpClient();

        private const string BASE_URL = "https://steamcommunity.com/market/search/render/?search_descriptions=0&sort_column=default&sort_dir=desc&appid=730&norender=1&count=100&start=";

        static RootObject rootObject = null;
        
        
    }
    public class SteamItemForJson
    {
        public  string itemName { get; set; }

        public string itemPrice { get; set; }

        public string itemQuantity { get; set; }
        public string ItemPriceGoal { get; set; }
        public string ItemBuyPrice { get; set; }

        public string itemCondition { get; set; }
        


    }
}

