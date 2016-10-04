/*WSU EECS CptS 451*/
/*Instructor: Sakire Arslan Ay*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Collections.ObjectModel;

namespace parse_yelp
{
    class Parser
    {

        public static String dataDir = ".\\..\\..\\yelp\\";


        public static string CTR(string s)
        {
            string t = "";

            switch (s)
            {
                case "full_address":
                    t = "full_address tinytext,";
                    break;

                case "business_id":
                    t = "business_id char(22),";
                    break;

                case "city":
                    t = "city varchar(25),";
                    break;

                case "review_count":
                    t = "review_count integer default 0,";
                    break;

                case "name":
                    t = "name tinytext,";
                    break;

                case "longitude":
                    t = "longitude double,";
                    break;

                case "state":
                    t = "state char(2),";
                    break;

                case "stars":
                    t = "stars float default 0,";
                    break;

                case "latitude":
                    t = "latitude double,";
                    break;

                case "Noise_Level":
                    t = "Noise_Level char(9),";
                    break;

                case "Alcohol":
                    t = "Alcohol char(13),";
                    break;

                case "Attire":
                    t = "Attire char(6),";
                    break;

                case "Smoking":
                    t = "Smoking char(10),";
                    break;

                case "Wi_Fi":
                    t = "Wi_Fi char(4),";
                    break;

                case "BYOBCorkage":
                    t = "BYOBCorkage char(11),";
                    break;

                case "Price_Range":
                    t = "Price_Range float, ";
                    break;

                case "Ages_Allowed":
                    t = "Ages_Allowed char(7), ";
                    break;

                default:
                    t = s + " bool default NULL,";
                    break;

            }


            return t;

        }

        static void Main(string[] args)
        {
            List<string> del = new List<string>();
            MySql_Connection mydb2 = new MySql_Connection();
            List<KeyValuePair<string, List<string>>> meow = new List<KeyValuePair<string, List<string>>>();


            foreach (object x in mydb2.SQLSELECTExec("select distinct child from cats order by child"))
            {
                //int size = Int32.Parse(mydb2.SQLSELECTExec("select count(*) from category where category='" + x + "';")[0]);
                KeyValuePair<string, List<string>> tmp = new KeyValuePair<string, List<string>>(x.ToString(), new List<string>());
                foreach (object y in mydb2.SQLSELECTExec("select parent from cats where child='" + x + "';"))
                {
                    string st = "select count(*) from " + y.ToString() + " natural join (select business_id from Category where category = '" + x.ToString() + "') as a; ";
                    string se = "Delete from cats where parent='" + y.ToString() + "' and child='" + x + "';";
                    
                    if (Int32.Parse(mydb2.SQLSELECTExec(st)[0]) ==0)//< size)
                    {

                        del.Add(se);
                        Console.WriteLine(del.Count);

                    }
                    else
                    {
                        ObservableCollection<string> temp = mydb2.SQLSELECTExec("Describe " + y.ToString() + ";", null, false);
                        foreach (string z in temp)
                        {
                            if (!tmp.Value.Contains(z))
                            {
                                tmp.Value.Add(z);
                            }
                        }
                    }

                }

                meow.Add(tmp);

            }

            using (StreamWriter w = new StreamWriter(dataDir + "ClearUnnecessaryTables.sql"))
            {
                foreach (string k in del)
                {
                    w.WriteLine(k);
                }
            }

            string list = "";
            string drops = "drop table category;";


            using (StreamWriter w = new StreamWriter(dataDir + "CreateCategories.sql"))
            {

                foreach (KeyValuePair<string, List<string>> k in meow)
                {
           
                    string s = "create table " + k.Key + " (business_id char(22),";
                    drops += "drop table " + k.Key+";";
                    string li = k.Key + ",";
                    int b = 0;
                    foreach (string a in (k.Value as List<string>))
                    {
                        if (a != "business_id")
                        {
                            li += a + ";";
                            s += CTR(a);
                            b++;
                            
                        }
                        
                    }
                    s += " foreign key (business_id) references business(business_id), primary key (business_id));";
                    if (b>0)
                    {
                        list += li+",";
                    }
                    w.WriteLine(s);

                }

            }

            using (StreamWriter w = new StreamWriter(dataDir + "Meow.txt"))
            {
                w.WriteLine(list.Substring(0, list.Length-1));
            }

            using (StreamWriter w = new StreamWriter(dataDir + "drops.sql"))
            {
                w.WriteLine(drops);
            }

        }
    }
}