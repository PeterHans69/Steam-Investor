using System.IO;
using System.Reflection;


namespace Steam_Investor_App

{
    public static class MyPathes
    {
        public static string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)+"..\\..\\..";// For Debugging in the 
        

        public static string MySteamItems = path+"\\SteamData\\MySteamItems.json";// For Debugging in the 
        public static string SteamItems = path+"\\SteamData\\SteamItems.json";// For Debugging in the 

        //public static string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); //For the installed app

        //public static string MySteamItems = path + "\\MySteamItems.json";//For the installed app
        //public static string SteamItems = path + "\\SteamItems.json";//For the installed app


        public static string charData = path+"\\charData.json";
        public static string date = path + "\\date.txt";
    }
}
