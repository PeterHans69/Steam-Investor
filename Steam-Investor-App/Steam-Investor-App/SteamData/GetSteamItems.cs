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
    using DocumentFormat.OpenXml.Spreadsheet;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Net.Http;
    using System.Runtime.CompilerServices;
    using System.Text.Json;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;

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


            # region loadAllItems
            public static int Loadeditems;
            public static int ITemsInTotal;

            static HttpClient httpClient = new HttpClient();

            private const string BASE_URL = "https://steamcommunity.com/market/search/render/?search_descriptions=0&sort_column=default&sort_dir=desc&appid=730&norender=1&count=100&start=";

            public static bool isLoadingItems = false;
            public static void LoadAllItems() //async Task
            {
                
                
                if (isLoadingItems==false)
                {
                    isLoadingItems = true;
                    int errors = 0;
                    int start = 0;

                    List<Result> results = new List<Result>();

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
                            Loadeditems = start;
                            ITemsInTotal = rootObject.total_count;

                        }
                        catch (Exception ex)
                        {
                            errors++;
                            Debug.WriteLine("Error" + ex);
                            Thread.Sleep(30000);

                            continue;
                        }

                        Debug.WriteLine(start);
                        Debug.WriteLine(rootObject.total_count);
                        Thread.Sleep(7000);
                    }
                    while (start < rootObject.total_count);


                    Loadeditems = 0;
                    // write to file

                    var jsonResult = JsonConvert.SerializeObject(results);

                    File.WriteAllText(System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName+@"..\..\SteamData\SteamItems.json"), jsonResult);
                    // read and deserialize it back

                    var fileContent = File.ReadAllText(System.IO.Path.GetFullPath(@"..\..\SteamData\SteamItems.json"));

                    var items = JsonConvert.DeserializeObject<List<Result>>(fileContent);
                    isLoadingItems = false;
                }



               
            }
            #endregion

            public static async Task<string> GetItemPriceForAddItem(string name, string condition, int currency) //This funktion is only for the AddItem Window! Because of MessageBox.Show();
            {
                
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


                    Debug.WriteLine("loading        " + rootObject.median_price + "/" + rootObject.lowest_price);

                    if (rootObject.median_price != null)
                    {
                        return rootObject.median_price;
                    }
                    else if(rootObject.lowest_price!=null)
                    {
                        return rootObject.lowest_price;
                    }
                    else
                    {
                        return "0";
                    }
                }
                catch
                {
                    MessageBox.Show("Too many API request try it in a minute again");
                    Debug.WriteLine("Too many API request try it in a minute again");
                    return null;
                }


            }


            static int counter = 0;//debug
            public static async Task<string> GetItemPrice(string name, string condition, int currency) //This funktion is only for MySteamItems Class! Because of the sleep(3000) instead of ;
            {

                HttpResponseMessage responseData = null;
                GetItemRoot rootObject = null;
                while (true)
                {
                    counter++;

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

                        var body = responseData.Content.ReadAsStringAsync().Result;
                        rootObject = JsonConvert.DeserializeObject<GetItemRoot>(body);
                    }
                    catch
                    {

                    }

                    if (rootObject != null)
                    {
                        Debug.WriteLine("loading        "+rootObject.median_price+"/"+rootObject.lowest_price);

                        if (rootObject.median_price != null)
                        {
                            return rootObject.median_price;
                        }
                        else if(rootObject.lowest_price!=null)
                        {
                            return rootObject.lowest_price;
                        }
                        else
                        {
                            return "0";
                        }
                        
                    }
                    else
                    {
                        Debug.WriteLine("null"+counter);

                        Thread.Sleep(20000);
                    }
                                        
                }


            }
            public class GetItemRoot
            {
                public string median_price { get; set; }
                public string lowest_price { get; set; }
            }

            private static ReadOnlySpan<byte> Utf8Bom => new byte[] { 0xEF, 0xBB, 0xBF };
            public static bool checkForNewItems()
            {
                RootObject rootObject = null;
                int total = 0;
                try // I Used try catch,because I dont exactly know the request limit
                {
                    //Get Number of Available items
                    var response = httpClient.GetAsync(BASE_URL+1).Result;
                    var body = response.Content.ReadAsStringAsync().Result;
                    

                    rootObject = JsonConvert.DeserializeObject<RootObject>(body);

                    //Number of items i have dowloaded
                    string fileName =MyPathes.SteamItems;
                    ReadOnlySpan<byte> jsonReadOnlySpan = File.ReadAllBytes(MyPathes.SteamItems);

                    // Read past the UTF-8 BOM bytes if a BOM exists.
                    if (jsonReadOnlySpan.StartsWith(Utf8Bom))
                    {
                        jsonReadOnlySpan = jsonReadOnlySpan.Slice(Utf8Bom.Length);
                    }

                    


                    
                    

                    var reader = new Utf8JsonReader(jsonReadOnlySpan);

                    while (reader.Read())
                    {
                        JsonTokenType tokenType = reader.TokenType;

                        if(tokenType== JsonTokenType.StartObject)
                        {
                            total++;
                        }
                    }
                    
                    

                }
                catch (Exception ex)
                {
                    
                    Debug.WriteLine("Error(GetSteamItems.CheckForNewItems" + ex);

                    return false;
                    
                }
                Debug.WriteLine("total items on steam :" + rootObject.total_count);
                Debug.WriteLine("total items I have :" + total);
                if (rootObject.total_count > total)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }

}
