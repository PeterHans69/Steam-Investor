using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Steam_Investor_App.SteamData
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Threading;
    using System.Windows;

    namespace SteamMarketJson
    {
        public class Searchdata
        {
            public string query { get; set; }
            public bool search_descriptions { get; set; }
            public int total_count { get; set; }
            public int pagesize { get; set; }
            public string prefix { get; set; }
            public string class_prefix { get; set; }
        }

        public class AssetDescription
        {
            //public int appid { get; set; }
            // public string classid { get; set; }
            //public string instanceid { get; set; }
            //public string background_color { get; set; }
            // public string icon_url { get; set; }
            //public int tradable { get; set; }
            //public string name { get; set; }
            // public string name_color { get; set; }
            // public string type { get; set; }
            // public string market_name { get; set; }
            // public string market_hash_name { get; set; }
            // public int commodity { get; set; }
        }

        public class Result
        {
            //public string name { get; set; }
            public string hash_name { get; set; }
            //public int sell_listings { get; set; }
            //public int sell_price { get; set; }
            //public string sell_price_text { get; set; }
            //public string app_icon { get; set; }
            //public string app_name { get; set; }
            //public AssetDescription asset_description { get; set; }
            //public string sale_price_text { get; set; }
        }

        public class RootObject
        {
            public bool success { get; set; }
            public int start { get; set; }
            public int pagesize { get; set; }
            public int total_count { get; set; }
            public Searchdata searchdata { get; set; }
            public List<Result> results { get; set; }
        }

        public class GetSteamItems
        {
            public GetSteamItems()
            {

            }

            static HttpClient httpClient = new HttpClient();

            private const string BASE_URL = "https://steamcommunity.com/market/search/render/?search_descriptions=0&sort_column=default&sort_dir=desc&appid=730&norender=1&count=100&start=";

            public static void LoadAllItemsAsync() //async Task
            {
                int errors = 0;
                int start = 0;

                List<Result> results = new List<Result>(); // you probably want to store results only

                RootObject rootObject = null;
                do
                {
                    try // I Used try catch,because I dont exactly know the request limit
                    {
                        //var response = httpClient.GetAsync(BASE_URL + start).Result; // use await instead of .Result when used in methods
                        //var response = await httpClient.GetAsync(BASE_URL + start);
                        //var body = await response.Content.ReadAsStringAsync();
                        var response = httpClient.GetAsync(BASE_URL + start).Result;
                        var body = response.Content.ReadAsStringAsync().Result;
                        Debug.WriteLine(BASE_URL + start);

                        rootObject = JsonConvert.DeserializeObject<RootObject>(body);

                        if (rootObject.results != null)
                        {
                            results.AddRange(rootObject.results);
                        }
                        start += 100;
                    }
                    catch (Exception ex)
                    {
                        errors++;
                        Debug.WriteLine("Error" + ex);
                        Thread.Sleep(30000);
                        if (errors > 100)
                        {
                            Debug.WriteLine("quitted while loop");
                            break;
                        }
                        continue;
                    }
                    
                    Debug.WriteLine(start);
                    Debug.WriteLine(rootObject.total_count);
                    Thread.Sleep(7000);
                }
                while (start < rootObject.total_count);

                // read and deserialize it back

                var fileContent = File.ReadAllText(System.IO.Path.GetFullPath(@"..\..\SteamData\SteamItems.json"));

                var items = JsonConvert.DeserializeObject<List<Result>>(fileContent);

                // write to file

                var jsonResult = JsonConvert.SerializeObject(results);

                File.WriteAllText(System.IO.Path.GetFullPath(@"..\..\SteamData\SteamItems.json"), jsonResult);





            }

            public static async Task<string> GetItemPrice(string name, string condition, int currency)
            {
                Debug.WriteLine(currency);
                HttpResponseMessage responseData = null;
                GetItemRoot rootObject = null;
                try
                {

                    if (condition != "No Condition")
                    {
                        responseData = httpClient.GetAsync("https://steamcommunity.com/market/priceoverview/?appid=730&currency=" + currency + "&market_hash_name=" + name + " " + "(" + condition + ")").Result;
                    }
                    else
                    {
                        responseData = httpClient.GetAsync("https://steamcommunity.com/market/priceoverview/?appid=730&currency=" + currency + "&market_hash_name=" + name).Result;

                    }
                    Debug.WriteLine("https://steamcommunity.com/market/priceoverview/?appid=730&currency=" + currency + "&market_hash_name=" + name);
                    Debug.WriteLine(currency);
                    var body = responseData.Content.ReadAsStringAsync().Result;
                    rootObject = JsonConvert.DeserializeObject<GetItemRoot>(body);


                    return rootObject.median_price;
                }
                catch
                {
                    MessageBox.Show("Too many API request try it in a minute again");
                    return null;
                }


            }
            public class GetItemRoot
            {
                public string median_price { get; set; }
            }
        }
    }

}
