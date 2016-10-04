/*WSU EECS CptS 451*/
/*Instructor: Sakire Arslan Ay*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Json;
using System.Collections;

namespace parse_yelp
{
    class Parser
    {
        //initialize the input/output data directory. Currently set to execution folder. 
        public static String dataDir = ".\\..\\..\\yelp\\";

        static void Main(string[] args)
        {
            JSONParser my_parser = new JSONParser();

            try
            {
                File.Delete(dataDir+ "sub/insertIntoTables.sql");
            }
            catch(Exception)
            {

            }

            //Parse yelp_business.json 
            my_parser.parseJSONFile(dataDir + "yelp_business.json", dataDir + "business.sql");

            using (StreamWriter w = File.AppendText(Parser.dataDir + "sub/insertIntoTables.sql"))
            {
                foreach (string k in ParseJSONObjects.value.Keys)// k = business_id
                {
                    KeyValuePair<Hashtable, List<string>> tmp = (KeyValuePair<Hashtable, List<string>>)ParseJSONObjects.value[k];
                    
                    foreach(string s in tmp.Value)// s = category
                    {
                        if (ParseJSONObjects.atb.ContainsKey(s))
                        {
                            //string com = "update " + s + " set ";
                            foreach (string z in (ParseJSONObjects.atb[s] as List<string>))// z = attributes
                            {
                                if (tmp.Key.ContainsKey(z))
                                {
                                    //com += z + "='" + tmp.Key[z] + "', ";
                                    w.WriteLine("insert into attributes values ('"+k+"', '"+z+"', "+ "'" + tmp.Key[z] + "'" + ");");
                                    w.WriteLine("insert into Categories values ('" + k + "', '" + s + "', " + "'" + z + "'" + ");");
                                }
                            }
                            //w.WriteLine(com.Substring(0, com.Length - 2) + " where business_id='" + k + "';");
                        }
                    }
                }
            }


        }

    }
}
