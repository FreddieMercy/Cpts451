using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Json;
using System.Text.RegularExpressions;

namespace parse_yelp
{
    
    class ParseJSONObjects
    {              
        Categories category;

        public ParseJSONObjects( )
        {
            category = new Categories();
        }
        
        public void Close( )
        {
        }

        private int maxLength = 5000;
        private string cleanTextforSQL(string inStr)
        {
            String outStr = Encoding.GetEncoding("iso-8859-1").GetString(Encoding.UTF8.GetBytes(inStr));
            outStr = outStr.Replace("\"", "").Replace("'", " ").Replace(",", " ").Replace(@"\n", " ").Replace(@"\u000a", " ").Replace("\\", " ").Replace("é", "e").Replace("ê", "e").Replace("Ã¼", "A").Replace("Ã", "A").Replace("¤", "").Replace("©", "c").Replace("¶","");
            outStr = Regex.Replace(outStr,@"[^\u0020-\u007E]", "?");
            
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
            //You may extract values for certain keys by specifying the key name. 
            //Example: extract business_id
            //String business_id = my_jsonStr["business_id"].ToString();
                        
            /*To retrieve list of Keys in JSON :
                    my_jsonStr.Keys.ToArray()[0]  is the "business_id" key. */
            /*To retrieve list of Values in JSON 
                    my_jsonStr.Values.ToArray()[0]  is the value for "business_id".*/
            
            //Alternative ways to extract business_id:
            //business_id = my_jsonStr[my_jsonStr.Keys.ToArray()[0]].ToString();
            //business_id = my_jsonStr.Values.ToArray()[0].ToString();

            /* EXTRACT OTHER KEY VALUES */

            //Clean text and remove any characters that might cause errors in MySQL.
            //return ("business_id:  " + cleanTextforSQL(business_id));

            string tmp = "";



            foreach (string key in my_jsonStr.Keys.ToArray())
            {

                if (key != "hours" && key != "neighborhoods" && !(my_jsonStr[key] is JsonObject))
                {
                    tmp += (key + ": " + cleanTextforSQL(my_jsonStr[key].ToString()) + ",");
                }
                else if (key != "hours" && key != "neighborhoods" && my_jsonStr[key] is JsonObject)
                {
                    tmp += ProcessBusiness((JsonObject)my_jsonStr[key]);
                }
            }
            

            return tmp;
        }


        /* Extract review information*/
        public string ProcessReviews(JsonObject my_jsonStr)
        {
            //Example: extract business_id and reviewtext
            //String review_id = cleanTextforSQL(my_jsonStr["review_id"].ToString());
            //You may limit the text lenght for review text 
            //String reviewtext = TruncateReviewText(cleanTextforSQL(my_jsonStr["text"].ToString()));

            /* EXTRACT OTHER KEY VALUES */
            //return ("review_id:  " + review_id + "    review text:  " + reviewtext);

            string tmp = "";



            foreach (string key in my_jsonStr.Keys.ToArray())
            {

                if (!(my_jsonStr[key] is JsonObject))
                {
                    tmp += (key + ": " + cleanTextforSQL(my_jsonStr[key].ToString()) + ",");
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
            //Example: extract user_id
            //String user_id = cleanTextforSQL(my_jsonStr[my_jsonStr.Keys.ToArray()[4]].ToString());


            /* EXTRACT OTHER KEY VALUES */
            //return ("user_id:  " + user_id );

            string tmp = "";



            foreach (string key in my_jsonStr.Keys.ToArray())
            {
                
                if (key != "friends" && key != "compliments" && key != "elite" && !(my_jsonStr[key] is JsonObject))
                {
                    tmp += (key + ": " + cleanTextforSQL(my_jsonStr[key].ToString()) + ",");
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
