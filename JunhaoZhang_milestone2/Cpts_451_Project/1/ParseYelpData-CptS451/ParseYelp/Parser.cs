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

        //all Categories
        public static List<string> allCat = new List<string>();

        //use SortToHas to sort and merge into same kind of tables
        public static SortToHas Cates = new SortToHas();

        //find all vars, values, those things...

        public static List<string> var = new List<string>();

        public static List<string> Noise_Level = new List<string>();
        public static List<string> Attire = new List<string>();
        public static List<string> Alcohol = new List<string>();
        public static List<string> type = new List<string>();
        public static List<string> BYOB_Corkage = new List<string>();
        public static List<string> Wi_Fi = new List<string>();
        public static List<string> Ages_Allowed = new List<string>();




        static void Main(string[] args)
        {
            JSONParser my_parser = new JSONParser();

            //try
            //{
            //    File.Delete(dataDir + "/business.sql");
            //}
            //catch (Exception)
            //{

            //}

            //try
            //{
            //    File.Delete(dataDir + "/review.sql");
            //}
            //catch (Exception)
            //{

            //}

            try
            {
                File.Delete(Parser.dataDir + "/sub/req/zip.sql");
            }
            catch (Exception)
            {

            }


            try
            {
                File.Delete(dataDir + "/sub/Category.sql");
            }
            catch (Exception)
            {

            }

            try
            {
                File.Delete(dataDir + "/sub/insertIntoCatTables.sql");
            }
            catch (Exception)
            {

            }

            try
            {
                File.Delete(dataDir + "/sub/req/Create_Cats.sql");
            }
            catch (Exception)
            {

            }

            try
            {
                File.Delete(dataDir + "/sub/Cats.sql");
            }
            catch (Exception)
            {

            }



            //Parse yelp_business.json 
            my_parser.parseJSONFile(dataDir + "yelp_business.json", dataDir + "business.sql");

            //save all vars, values, those things into different local files

            using (StreamWriter w = new StreamWriter(dataDir + "/sub/vars/Ages_Allowed.txt"))
            {
                foreach (string k in Ages_Allowed)
                {
                    w.WriteLine(k);
                }
            }

            using (StreamWriter w = new StreamWriter(dataDir + "/sub/vars/allCat.txt"))
            {
                string s = "";
                foreach (string k in Parser.allCat)
                {
                    s += k + ",";
                }
                w.WriteLine(s);
            }

            using (StreamWriter w = new StreamWriter(dataDir + "/sub/vars/parameters.txt"))
            {
                foreach (string k in ParseJSONObjects.parameters)
                {
                    w.WriteLine(k);
                }
            }

            using (StreamWriter w = new StreamWriter(dataDir + "/sub/vars/vars.txt"))
            {
                foreach (string k in var)
                {
                    w.WriteLine(k);
                }
            }


            using (StreamWriter w = new StreamWriter(dataDir + "/sub/vars/Noise_Level.txt"))
            {
                foreach (string k in Noise_Level)
                {
                    w.WriteLine(k);
                }
            }

            using (StreamWriter w = new StreamWriter(dataDir + "/sub/vars/Attire.txt"))
            {
                foreach (string k in Attire)
                {
                    w.WriteLine(k);
                }
            }

            using (StreamWriter w = new StreamWriter(dataDir + "/sub/vars/Alcohol.txt"))
            {
                foreach (string k in Alcohol)
                {
                    w.WriteLine(k);
                }
            }

            using (StreamWriter w = new StreamWriter(dataDir + "/sub/vars/type.txt"))
            {
                foreach (string k in type)
                {
                    w.WriteLine(k);
                }
            }
            using (StreamWriter w = new StreamWriter(dataDir + "/sub/vars/BYOBCorkage.txt"))
            {
                foreach (string k in BYOB_Corkage)
                {
                    w.WriteLine(k);
                }
            }
            using (StreamWriter w = new StreamWriter(dataDir + "/sub/vars/Wi_Fi.txt"))
            {
                foreach (string k in Wi_Fi)
                {
                    w.WriteLine(k);
                }
            }


            Cates.Pop();

            string drop = "";

            using (StreamWriter w = File.AppendText(dataDir + "/sub/req/Create_Cats.sql"))
            {
                foreach (KeyValuePair<string, List<string>> k in SortToHas.cates)
                {
                    string s = "create table " + k.Key + " (";
                    drop += "drop table " + k.Key + ";";
                    foreach (string a in (k.Value as List<string>))
                    {

                        s += Cates.CTR(a);
                    }
                    s += "business_id char(22), foreign key (business_id) references business(business_id), primary key (business_id));";
                    //s = s.Substring(0, s.Length - 1) + ");";
                    w.Write(s);
                    w.WriteLine();
                }
            }

            using (StreamWriter w = new StreamWriter(dataDir + "/sub/req/drop.sql"))
            {
                w.WriteLine(drop);
                w.WriteLine("drop table cats;");
            }

            //Parse yelp_review.json 
            my_parser.parseJSONFile(dataDir + "yelp_review.json", dataDir + "review.sql");

            //Parse yelp_user.json 
            my_parser.parseJSONFile(dataDir + "yelp_user.json", dataDir + "User.sql");


        }

    }
}