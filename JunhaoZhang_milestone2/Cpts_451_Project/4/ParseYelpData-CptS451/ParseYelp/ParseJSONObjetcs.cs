using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Json;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections;

namespace parse_yelp
{
    
    class ParseJSONObjects
    {              
        Categories category;

        private Hashtable values; //somehow make all values in the row into one hashtable
        public static Hashtable atb;
        public static Hashtable value;
        private List<string> li;
        public ParseJSONObjects( )
        {
            category = new Categories();
            atb = category.getAll;
            values = new Hashtable();
            value = new Hashtable();
            li = new List<string>();
        }
        
        public void Close( )
        {
        }

        public static int maxLength = 5000;

        
        public static string cleanTextforSQL(string inStr)
        {
            String outStr = Encoding.GetEncoding("iso-8859-1").GetString(Encoding.UTF8.GetBytes(inStr));
            outStr = outStr.Replace("\"", "").Replace("'", " ").Replace(",", " ").Replace(@"\n", " ").Replace(@"\u000a", " ").Replace("\\", " ").Replace("é", "e").Replace("ê", "e").Replace("Ã¼", "A").Replace("Ã", "A").Replace("¤", "").Replace("©", "c").Replace("¶", "");
            outStr = Regex.Replace(outStr,@"[^\u0020-\u007E]", "?");
            
            //Only get he first maxLength chars. Set maxLength to the max length of your attribute.
            return outStr.Substring(0, Math.Min(outStr.Length, maxLength));
        }

        //replace space/-/slash to underline, for making variables
        public static string repSpac2Und(string inStr)
        {
            String outStr = Encoding.GetEncoding("iso-8859-1").GetString(Encoding.UTF8.GetBytes(inStr));
            outStr = outStr.Replace("\"", "").Replace("'", "_").Replace(",", "_").Replace(@"\n", "_").Replace(@"\u000a", "_").Replace("\\", "_").Replace("é", "e").Replace("ê", "e").Replace("Ã¼", "A").Replace("Ã", "A").Replace("¤", "").Replace("©", "c").Replace("¶", "").Replace("/", "").Replace(" ", "_").Replace("&", "and").Replace("(", "_").Replace(")", "").Replace("-", "_");
            outStr = Regex.Replace(outStr, @"[^\u0020-\u007E]", "?");

            //Only get he first maxLength chars. Set maxLength to the max length of your attribute.
            return outStr.Substring(0, Math.Min(outStr.Length, maxLength));
        }

        
        private string TruncateReviewText(string longText)
        {
            int maxTextLength = 250;
            return longText.Substring(0, Math.Min(maxTextLength, longText.Length)) + "...";
        }
        
        /* Extract business information*/
        public void ProcessBusiness(JsonObject my_jsonStr)
        {
            string bns_id = "";
            foreach (string key in my_jsonStr.Keys.ToArray())
            {
                //get all values in jason for specific columns
                if (key != "hours" && key != "neighborhoods" && !(my_jsonStr[key] is JsonObject))
                {
                    
                    if (cleanTextforSQL(my_jsonStr[key].ToString()) == "false" | cleanTextforSQL(my_jsonStr[key].ToString()) == "true")
                    {

                        string s = "";

                        if (cleanTextforSQL(my_jsonStr[key].ToString()) == "false")
                        {
                            s = "0";
                        }
                        else
                        {
                            s = "1";
                        }

                        if (repSpac2Und(key) != "open")
                        {

                            if (!values.ContainsKey(repSpac2Und(key)))
                            {
                                values.Add(repSpac2Und(key), s);
                            }
                            else
                            {
                                values[repSpac2Und(key)] = s;
                            }
                        }

                    }

                    else if (key == "categories")
                    {

                        using (StreamWriter w = File.AppendText(Parser.dataDir + "sub/insertIntoTables.sql"))
                        {
            
                            foreach (KeyValuePair<string, JsonValue> keys in my_jsonStr[key].ToArray())
                            {
                                li.Add(repSpac2Und(keys.Value.ToString()));
                                //w.WriteLine("INSERT INTO " + repSpac2Und(keys.Value.ToString()) + " set business_id='" + cleanTextforSQL(my_jsonStr["business_id"].ToString()) + "';");
                            }
                        }
                    }
                    else if (key == "business_id")
                    {
                        bns_id = cleanTextforSQL(my_jsonStr["business_id"].ToString());
                    }
                    else if (key == "type")
                    {
                        value.Add(bns_id, new KeyValuePair<Hashtable, List<string>> (values, li));
                        values = new Hashtable();
                        li = new List<string>();
                    }
                    else
                    {

                        if (repSpac2Und(key) == "city" |
                            repSpac2Und(key) == "longitude" |
                            repSpac2Und(key) == "state" |
                            repSpac2Und(key) == "stars" |
                            repSpac2Und(key) == "latitude" |
                            repSpac2Und(key) == "review_count" |
                            repSpac2Und(key) == "name" |
                            repSpac2Und(key) == "full_address")
                        {

                        }
                        else
                        {
                            if (!values.ContainsKey(repSpac2Und(key)))
                            {
                                values.Add(repSpac2Und(key), cleanTextforSQL(my_jsonStr[key].ToString()));
                            }
                            else
                            {
                                values[repSpac2Und(key)] = cleanTextforSQL(my_jsonStr[key].ToString());
                            }
                        }
                    }

                }
                else if (key != "hours" && key != "neighborhoods" && my_jsonStr[key] is JsonObject)
                {
                        ProcessBusiness((JsonObject)my_jsonStr[key]);
                    
                }

            }


        }

    }
}
