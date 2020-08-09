using DocumentFormat.OpenXml.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Diagnostics;

namespace Steam_Investor_App.SteamData
{
    class SteamItem
    {
        private static readonly byte[] s_nameUtf8 = Encoding.UTF8.GetBytes("hash_name");
        private static ReadOnlySpan<byte> Utf8Bom => new byte[] { 0xEF, 0xBB, 0xBF };

        public static bool searchforItem(string item)
        {
            // ReadAllBytes if the file encoding is UTF-8:
            string fileName = System.IO.Path.GetFullPath(@"..\..\SteamData\SteamItems.json");
            ReadOnlySpan<byte> jsonReadOnlySpan = File.ReadAllBytes(fileName);

            // Read past the UTF-8 BOM bytes if a BOM exists.
            if (jsonReadOnlySpan.StartsWith(Utf8Bom))
            {
                jsonReadOnlySpan = jsonReadOnlySpan.Slice(Utf8Bom.Length);
            }

            // Or read as UTF-16 and transcode to UTF-8 to convert to a ReadOnlySpan<byte>
            //string fileName = "Universities.json";
            //string jsonString = File.ReadAllText(fileName);
            //ReadOnlySpan<byte> jsonReadOnlySpan = Encoding.UTF8.GetBytes(jsonString);


            int count = 0;
            int total = 0;

            var reader = new Utf8JsonReader(jsonReadOnlySpan);

            while (reader.Read())
            {
                JsonTokenType tokenType = reader.TokenType;

                switch (tokenType)
                {
                    case JsonTokenType.StartObject:
                        total++;
                        break;
                    case JsonTokenType.PropertyName:
                        if (reader.ValueTextEquals(s_nameUtf8))
                        {
                            // Assume valid JSON, known schema
                            reader.Read();
                            if (reader.GetString().Equals(item))//If Item is in steams database
                            {
                                count++;
                                return true; //then return true
                            }
                            else
                            {
                                return false;//else return false
                            }
                        }
                        break;
                }
            }
            Debug.WriteLine($"{count} out of {total} have names that end with 'University'");
        }

    }
}
