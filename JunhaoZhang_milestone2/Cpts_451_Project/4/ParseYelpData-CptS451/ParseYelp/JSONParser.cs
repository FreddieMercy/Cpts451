/*WSU EECS CptS 451*/
/*Instructor: Sakire Arslan Ay*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Json;
using System.IO;
using System.Text.RegularExpressions;



namespace parse_yelp
{
    class JSONParser
    {

        public JSONParser()
        {

        }


        public void parseJSONFile(string jsonInput, string sqlOutput)
        {
            int counter;
            string line;
            System.IO.StreamReader jsonfile;

            try
            {
                ParseJSONObjects json2db = new ParseJSONObjects();
                Console.WriteLine("\nCreating: " + sqlOutput);
                Console.Write("Progress:");
                // Read the json data jsonfile.
                jsonfile = new System.IO.StreamReader(jsonInput);

                counter = 0;

                while ((line = jsonfile.ReadLine()) != null)
                {
                    JsonObject my_jsonStr = (JsonObject)JsonObject.Parse(line);
                    string type = my_jsonStr["type"].ToString();

                    switch (type)
                    {

                        case "\"business\"":
                            json2db.ProcessBusiness(my_jsonStr);
                            break;
                        default:
                            Console.WriteLine("Unknown type : " + type);
                            break;
                    }
                    if ((counter % 5000) == 0)
                        Console.Write("■");
                    counter++;
                }
                jsonfile.Close();

            }
            catch (Exception e)
            {
                Console.Write("Exception:");
                Console.WriteLine(e.Message);
            }
            // Suspend the screen.
            Console.WriteLine("\n" + sqlOutput + ": created. \n\n Press a key to continue.");
            Console.ReadLine();

        }

    }

}
