using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Steam_Investor_App.SteamData
{
    public static class GetSteamItems
    {
        public static void LoadAllItems()
        {
            using (WebClient w = new WebClient())
            {
                var responseData = w.DownloadString("https://steamcommunity.com/market/search/render/?search_descriptions=0&sort_column=default&sort_dir=desc&appid=730&norender=1&count=500");
                //StatTrak™ FAMAS | Sergeant (Battle-Scarred)
                dynamic parsedJson = JsonConvert.DeserializeObject(responseData);
                string jsonData = JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
                System.IO.File.WriteAllText(System.IO.Path.GetFullPath(@"..\..\SteamData\SteamItems.json"), jsonData);


            }



        }
        public static float GetItemPrice(string name , string condition,int currency)
        {
            using (WebClient w = new WebClient())
            {
                var responseData = w.DownloadString("https://steamcommunity.com/market/priceoverview/?appid=730&currency=" + currency + "&market_hash_name=" + name + " " + condition);
                float price=0; //= price aus responseData
                return price;
            }
        }


    }
}
