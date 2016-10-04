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

        //sort subCategories into mainCategories
        //public static Hashtable Categories;
        public static List<string> Categoriess;
        private Hashtable values; //somehow make all values in the row into one hashtable
        private List<string> obj;

        public static List<string> parameters; //record all parameters

        public ParseJSONObjects( )
        {
            category = new Categories();
            values = new Hashtable();
            obj = new List<string>();
            parameters = new List<string>();
            //Categories = category.getAll;
            Categoriess = category.getAllCc;
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
        public string ProcessBusiness(JsonObject my_jsonStr)
        {

            string tmp = "";
            string bns_id = "";

            foreach (string key in my_jsonStr.Keys.ToArray())
            {
                //Console.WriteLine(values.Count);

                //find all vars, values, those things...
                if (!Parser.var.Contains(repSpac2Und(key)))
                {
                    Parser.var.Add(repSpac2Und(key));
                }
                
                /*-----  same below  ------*/

                switch (repSpac2Und(key))
                {

                    case "Ages_Allowed":

                        if (!Parser.Ages_Allowed.Contains(cleanTextforSQL(my_jsonStr[key].ToString())))
                        {
                            Parser.Ages_Allowed.Add(cleanTextforSQL(my_jsonStr[key].ToString()));
                        }

                        break;

                    case "Noise_Level":

                        if (!Parser.Noise_Level.Contains(cleanTextforSQL(my_jsonStr[key].ToString())))
                        {
                            Parser.Noise_Level.Add(cleanTextforSQL(my_jsonStr[key].ToString()));
                        }

                        break;
                    case "Attire":

                        if (!Parser.Attire.Contains(cleanTextforSQL(my_jsonStr[key].ToString())))
                        {
                            Parser.Attire.Add(cleanTextforSQL(my_jsonStr[key].ToString()));
                        }

                        break;
                    case "Alcohol":

                        if (!Parser.Alcohol.Contains(cleanTextforSQL(my_jsonStr[key].ToString())))
                        {
                            Parser.Alcohol.Add(cleanTextforSQL(my_jsonStr[key].ToString()));
                        }

                        break;
                    case "type":

                        if (!Parser.type.Contains(cleanTextforSQL(my_jsonStr[key].ToString())))
                        {
                            Parser.type.Add(cleanTextforSQL(my_jsonStr[key].ToString()));
                        }

                        break;
                    case "BYOBCorkage":

                        if (!Parser.BYOB_Corkage.Contains(cleanTextforSQL(my_jsonStr[key].ToString())))
                        {
                            Parser.BYOB_Corkage.Add(cleanTextforSQL(my_jsonStr[key].ToString()));
                        }

                        break;
                    case "Wi_Fi":

                        if (!Parser.Wi_Fi.Contains(cleanTextforSQL(my_jsonStr[key].ToString())))
                        {
                            Parser.Wi_Fi.Add(cleanTextforSQL(my_jsonStr[key].ToString()));
                        }

                        break;
                }


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
                            //save the parameters
                            if (!parameters.Contains(repSpac2Und(key)))
                            {
                                parameters.Add(repSpac2Und(key));
                            }

                            if (!values.ContainsKey(repSpac2Und(key)))
                            {
                                values.Add(repSpac2Und(key), s);
                            }
                            else
                            {
                                values[repSpac2Und(key)] = s;
                            }
                        }
                        else
                        {
                            tmp += (repSpac2Und(key) + "= '" + s + "',");
                        }
                    }

                    else if (key == "categories")
                    {
                        //Categories ...... need another table

                        //Categories... need more 'insert'
                        //...
                        // dataDir/sub/??.sql

                        using (StreamWriter w = File.AppendText(parse_yelp.Parser.dataDir + "/sub/Category.sql"))
                        {

                            foreach (KeyValuePair<string, JsonValue> keys in my_jsonStr[key].ToArray())
                            {

                                if (!Parser.allCat.Contains(repSpac2Und(keys.Value.ToString())))
                                {
                                    Parser.allCat.Add(repSpac2Und(keys.Value.ToString()));
                                }

                                //Save only mainCat? IDK revise later, but now saving both subCat & mainCat
                                w.WriteLine("INSERT INTO Category values ('" + bns_id + "', '" + cleanTextforSQL(keys.Value.ToString()) + "');");
                                obj.Add(repSpac2Und(keys.Value.ToString()));
                            }

                        }

                    }
                    else if (key == "business_id")
                    {
                        bns_id = cleanTextforSQL(my_jsonStr[key].ToString());
                        tmp += "INSERT INTO business set ";
                        tmp += (key + "= '" + bns_id + "',");
                    }
                    else if (key == "type")
                    {
                        tmp += (key + "= '" + cleanTextforSQL(my_jsonStr[key].ToString()) + "';");
                        
                        if (obj.Count>0)
                        {   
                            parse_yelp.Parser.Cates.Push(obj, values, bns_id.ToString());

                        }

                        obj = new List<string> ();
                        values = new Hashtable();
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
                            tmp += (repSpac2Und(key) + "= '" + cleanTextforSQL(my_jsonStr[key].ToString()) + "',");

                            if(repSpac2Und(key) == "full_address")
                            {
                                using (StreamWriter w = File.AppendText(Parser.dataDir + "/sub/req/zip.sql"))
                                {
                                    string zip = cleanTextforSQL(my_jsonStr[key].ToString());
                                    w.WriteLine("update business set zipcode='"+ zip.Substring(zip.Length-5, 5) + "' where business_id = '"+bns_id+"';");
                                }
                            }
                        }
                        else
                        {
                            if (!parameters.Contains(repSpac2Und(key)))
                            {
                                parameters.Add(repSpac2Und(key));
                            }

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
                    if (my_jsonStr[key].Count>0 & repSpac2Und(key)!= "attributes")
                    {
                        obj.Add(repSpac2Und(key));
                    }
                        tmp += ProcessBusiness((JsonObject)my_jsonStr[key]);
                    
                }

            }


            return tmp;
        }


        /* Extract review information*/
        public string ProcessReviews(JsonObject my_jsonStr)
        {

            string tmp = "";
            string bns_id = "";
            string rev_id = "";
            string usr_id = "";
            string txt = "";

            foreach (string key in my_jsonStr.Keys.ToArray())
            {
                

                if (!(my_jsonStr[key] is JsonObject))
                {
                    if (key == "funny")
                    {
                        tmp += "INSERT INTO review set ";
                        tmp += (key + "= '" + cleanTextforSQL(my_jsonStr[key].ToString()) + "',");
                    }
                    else if(key=="date")
                    {
                        tmp += (key + "= STR_TO_DATE('" + cleanTextforSQL(my_jsonStr[key].ToString()) + "','%Y-%m-%d'),");
                    }
                    else if(key=="text")
                    {
                        txt = cleanTextforSQL(my_jsonStr[key].ToString());
                    }
                    else if(key == "business_id")
                    {
                        bns_id = cleanTextforSQL(my_jsonStr[key].ToString());
                        tmp += (key + "= '" + bns_id + "',");

                        //Comment if don't wanna save text into DB
                        tmp += "text='"+txt+"';";

                        //Uncomment to save texts into files
                        /*
                        string dir = parse_yelp.Parser.dataDir + "/sub/text/" + bns_id + usr_id + rev_id + ".txt";
                        using (StreamWriter w = new StreamWriter(dir))
                        {
                            w.WriteLine(txt);
                        }
                        */
                       // tmp += "text="+dir+";";
                    }
                    else if(key == "user_id")
                    {
                        usr_id = cleanTextforSQL(my_jsonStr[key].ToString());
                        tmp += (key + "= '" + usr_id + "',");
                    }
                    else if(key=="review_id")
                    {
                        rev_id = cleanTextforSQL(my_jsonStr[key].ToString());
                        tmp += (key + "= '" + rev_id + "',");
                    }
                    else
                    {
                        tmp += (key + "= '" + cleanTextforSQL(my_jsonStr[key].ToString()) + "',");
                    }
                }
                else
                {
                    tmp += ProcessReviews((JsonObject)my_jsonStr[key]);
                }
            }


            return tmp;

        }

        /* Extract review information*/
        public string ProcessUsers(JsonObject my_jsonStr)
        {

            string tmp = "";

            foreach (string key in my_jsonStr.Keys.ToArray())
            {

                if (key != "friends" && key != "compliments" && key != "elite" && !(my_jsonStr[key] is JsonObject))
                {
                    if (key == "yelping_since")
                    {
                        tmp += "INSERT INTO user set ";
                        tmp += (key + "= STR_TO_DATE('" + cleanTextforSQL(my_jsonStr[key].ToString()) + "', '%Y-%m'),");
                    }
                    else if (key == "type")
                    {
                        tmp += (key + "= '" + cleanTextforSQL(my_jsonStr[key].ToString()) + "';");

                    }
                    else
                    {
                        tmp += (key + "= '" + cleanTextforSQL(my_jsonStr[key].ToString()) + "',");
                    }
                }
                else if (key != "friends" && key != "compliments" && key != "elite" && my_jsonStr[key] is JsonObject)
                {
                    tmp += ProcessUsers((JsonObject)my_jsonStr[key]);
                }
                
            }


            return tmp;

        }


        /* The INSERT statement for category tuples*/
        public string ProcessBusinessCategories(JsonObject my_jsonStr)
        {
            String insertString = "";
            JsonArray categories = (JsonArray)my_jsonStr["categories"];
            //append an INSERT statement to insertString for each category of the business 
            for (int i = 0; i < categories.Count; i++)
                insertString = insertString + "INSERT INTO businessCategory (business_id, category) VALUES ("
                                + "'" + my_jsonStr["business_id"].ToString().Replace("\"", "") + "' , "
                                + "'" + cleanTextforSQL(categories[i].ToString()) + "'"
                                + ");"
                                + "\n"; //append a new line
            return insertString;
        }
                               

    }
}
