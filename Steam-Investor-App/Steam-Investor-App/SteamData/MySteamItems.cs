using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            string jsonMySteamItems = "";

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
            

            jsonMySteamItems = JsonConvert.SerializeObject(mySteamItems);
            File.WriteAllText(System.IO.Path.GetFullPath(@"..\..\SteamData\MySteamItems.json"), jsonMySteamItems);
        }
    }
    public class SteamItemForJson
    {
        public  string itemName { get; set; }

        public string itemQuantity { get; set; }
        public string ItemPriceGoal { get; set; }
        public string ItemBuyPrice { get; set; }

        public string itemCondition { get; set; }
        public string itemUrl { get; set; }


    }
}

