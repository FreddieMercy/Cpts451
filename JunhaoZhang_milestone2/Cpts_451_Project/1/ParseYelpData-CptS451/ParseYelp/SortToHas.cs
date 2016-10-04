/*WSU EECS CptS 451*/
/*Instructor: Sakire Arslan Ay*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Json;
using System.Collections;
using System.Text.RegularExpressions;

namespace parse_yelp
{
    class SortToHas
    {
        private Stack<KeyValuePair<string, List<KeyValuePair<string, Hashtable>>>> stack;
        public static List<KeyValuePair<string, List<string>>> cates; //collection of tables with same parameters, the string is table name and 
                                                                      //each hashtable in the list of hashtable is each col of that table;
        private List<KeyValuePair<string, string>> rela = new List<KeyValuePair<string, string>>(); //save the relations between tables and kind of tables: parent_table/sub_table
        private List<string> cat;
        private Hashtable oldMeow = new Hashtable();
        private Hashtable newMeow = new Hashtable();
        private List<KeyValuePair<List<string>, List<string>>> allAttr = new List<KeyValuePair<List<string>, List<string>>>();
        private int j = 0;
        private Stack<KeyValuePair<string, KeyValuePair<string, Hashtable>>> chunk = new Stack<KeyValuePair<string, KeyValuePair<string, Hashtable>>>();

        private string rmQuo(string instr)
        {
            string s = "";
            foreach(char x in instr)
            {
                if(x != '"')
                {
                    s += x;
                }
            }

            return s;
        }

        public SortToHas()
        {
            stack = new Stack<KeyValuePair<string, List<KeyValuePair<string, Hashtable>>>>();
            cates = new List<KeyValuePair<string, List<string>>>();
            cat = new List<string>();

        }

        private ICollection findPre(string key)
        {

            //we assume stack.ToArray()[0] == stack.Peek() is true;
            foreach (KeyValuePair<string, List<KeyValuePair<string, Hashtable>>> keyss in stack)
            {
                List<KeyValuePair<string, Hashtable>> keys = keyss.Value as List<KeyValuePair<string, Hashtable>>;
                foreach (KeyValuePair<string, Hashtable> tmp in keys)
                {
                    if (tmp.Key == key)
                    {
                        return (tmp.Value as Hashtable).Keys;
                    }
                }
            }

            return null;
        }

        public void Push(List<string> tmp2, Hashtable tmp, string bns)
        {
            List<string> catt = new List<string>();
            List<KeyValuePair<string, Hashtable>> jsnObj = new List<KeyValuePair<string, Hashtable>>();
            List<string> recorded = new List<string>();

            foreach (string keys in tmp2)
            {
                string _keysValue = ParseJSONObjects.repSpac2Und(keys.ToString());
 
                if (ParseJSONObjects.Categoriess.Contains(_keysValue))
                {
                    //Console.WriteLine(_keysValue);
                    if (!catt.Contains(ParseJSONObjects.repSpac2Und(_keysValue)))
                    {
                        catt.Add(ParseJSONObjects.repSpac2Und(_keysValue));

                    }
                }

                KeyValuePair<string, Hashtable> top = new KeyValuePair<string, Hashtable>(_keysValue, new Hashtable());
                ICollection temp = findPre(_keysValue);

                if (temp != null)
                {
                    foreach (string k in temp)
                    {
                        if (!recorded.Contains(k))
                        {
                            recorded.Add(k);
                        }

                        if (tmp.ContainsKey(k))
                        {
                            if (!(top.Value as Hashtable).ContainsKey(k))
                            {
                                (top.Value as Hashtable).Add(k, tmp[k]);
                            }
                            else
                            {
                                (top.Value as Hashtable)[k] = tmp[k];
                            }

                        }
                    }

                }
                else
                {
                    //add all values to jsnObj
                    foreach (string k in tmp.Keys)
                    {
                        if (!recorded.Contains(k))
                        {
                            recorded.Add(k);
                        }

                        (top.Value as Hashtable)[k] = tmp[k];
                    }
                }

                jsnObj.Add(top);

            }


            //For the unused/un-recorded parameters:

            List<string> unused = new List<string>();

            foreach (string s in tmp.Keys)
            {
                if (!recorded.Contains(s))
                {
                    unused.Add(s);
                }
            }


            if (unused.Count > 0)
            {
                //add all unused values to jsnObj

                foreach (string k in unused)
                {
                    KeyValuePair<string, Hashtable> tops = new KeyValuePair<string, Hashtable>(k, new Hashtable());

                    if (!(tops.Value as Hashtable).ContainsKey(ParseJSONObjects.repSpac2Und(k)))
                    {
                        (tops.Value as Hashtable).Add(ParseJSONObjects.repSpac2Und(k), tmp[k]);
                    }
                    else
                    {
                        (tops.Value as Hashtable)[ParseJSONObjects.repSpac2Und(k)] = tmp[k];
                    }

                    jsnObj.Add(tops);
                }


            }

            KeyValuePair<string, Hashtable> sth;

            stack.Push(new KeyValuePair<string, List<KeyValuePair<string, Hashtable>>>(bns, jsnObj));

            if (catt.Count() == 1)
            {
                sth = new KeyValuePair<string, Hashtable>(ParseJSONObjects.repSpac2Und(catt.First()), tmp);

                if (!oldMeow.ContainsKey(ParseJSONObjects.repSpac2Und(catt.First())))
                {
                    oldMeow.Add(ParseJSONObjects.repSpac2Und(catt.First()), new List<string>());
                }

                foreach (string k in tmp.Keys)
                {
                    if (!(oldMeow[ParseJSONObjects.repSpac2Und(catt.First())] as List<string>).Contains(ParseJSONObjects.repSpac2Und(k)))
                    {
                        (oldMeow[ParseJSONObjects.repSpac2Und(catt.First())] as List<string>).Add(ParseJSONObjects.repSpac2Und(k));
                    }
                }
                chunk.Push(new KeyValuePair<string, KeyValuePair<string, Hashtable>>(bns, sth));
            }
            else if(catt.Count() > 1)
            {
                sth = new KeyValuePair<string, Hashtable>(null, tmp);
                List<string> meowAttr = new List<string>();
                foreach (string k in tmp.Keys)
                {
                    if (!meowAttr.Contains(ParseJSONObjects.repSpac2Und(k)))
                    {
                        meowAttr.Add(ParseJSONObjects.repSpac2Und(k));
                    }
                }

                KeyValuePair<List<string>, List<string>> meow = new KeyValuePair<List<string>, List<string>>(catt, meowAttr);
                allAttr.Add(meow);
                chunk.Push(new KeyValuePair<string, KeyValuePair<string, Hashtable>>(bns, sth));
            }
            

        }

        private void sortMeow()
        {
            List<string> rm = new List<string>();
            foreach (KeyValuePair<List<string>, List<string>> x in allAttr)
            {
                foreach (string y in (x.Key as List<string>))
                {
                    foreach (string z in (x.Value as List<string>))
                    {
                        if (!oldMeow.ContainsKey(ParseJSONObjects.repSpac2Und(y)))
                        {
                            oldMeow.Add(ParseJSONObjects.repSpac2Und(y), new List<string>());
                            newMeow.Add(ParseJSONObjects.repSpac2Und(y), new List<string>());
                        }

                        //You can uncomment, but I don't understand why the Cats.sql file is even more complete. Big Question Mark...

                        //if ((oldMeow[ParseJSONObjects.repSpac2Und(y)] as List<string>).Contains(ParseJSONObjects.repSpac2Und(z)))
                        //{
                        //    if (!rm.Contains(ParseJSONObjects.repSpac2Und(z)))
                        //    {

                        //        rm.Add(ParseJSONObjects.repSpac2Und(z));
                        //    }
                        //}
                    }
                }

                foreach (string k in rm)
                {
                    (x.Value as List<string>).Remove(ParseJSONObjects.repSpac2Und((k)));
                }

                Console.WriteLine("orph atters "+(x.Value as List<string>).Count);

                if ((x.Value as List<string>).Count > 0)
                {
                    foreach (string y in (x.Key as List<string>))
                    {
                        foreach (string z in (x.Value as List<string>))
                        {

                            if (!(oldMeow[ParseJSONObjects.repSpac2Und(y)] as List<string>).Contains(ParseJSONObjects.repSpac2Und(z)))
                            {
                                (oldMeow[ParseJSONObjects.repSpac2Und(y)] as List<string>).Add(ParseJSONObjects.repSpac2Und(z));
                                if (!newMeow.ContainsKey(ParseJSONObjects.repSpac2Und(y)))
                                {
                                    newMeow.Add(ParseJSONObjects.repSpac2Und(y), new List<string>());
                                }
                                
                                (newMeow[ParseJSONObjects.repSpac2Und(y)] as List<string>).Add(ParseJSONObjects.repSpac2Und(z));

                            }

                        }
                    }
                }
            }


            MeowMeow(oldMeow);


        }

        private void MeowMeow(Hashtable tb)
        {
            Console.WriteLine("rela has : "+rela.Count);
            Console.WriteLine("oldMeow has : "+ oldMeow.Count);
            Console.WriteLine("newMeow has : " + newMeow.Count);

            foreach (string m in tb.Keys)
            {
                List<string> tmp = new List<string>();

                foreach (string k in (tb[ParseJSONObjects.repSpac2Und(m)] as List<string>))
                {
                    tmp.Add(ParseJSONObjects.repSpac2Und(k));
                }

                foreach (KeyValuePair<string, List<string>> x in cates)
                {
                    //if (x.Key == m)
                    //{
                        List<string> y = (x.Value as List<string>);

                        bool ex = true;

                        foreach (string z in y)
                        {

                            if (!tmp.Contains(ParseJSONObjects.repSpac2Und(z)))
                            {
                                ex = false;
                                break;
                            }
                            /*
                            else
                            {
                                tmp.Remove(ParseJSONObjects.repSpac2Und(z));
                            }
                            */
                        }

                        if (ex)
                        {
                            Console.WriteLine("Your ex-!!");
                            rela.Add(new KeyValuePair<string, string>(ParseJSONObjects.repSpac2Und(x.Key), ParseJSONObjects.repSpac2Und(m)));
                        }
                    //}
                }


                if (tmp.Count > 0)
                {
                    Console.WriteLine("You failed");
                }

            }


            Console.WriteLine("First blood!!");
            foreach (KeyValuePair<string, string> r in rela)
            {
                string query = "Insert into Cats set ";

                query += "Parent='" + r.Key + "', ";
                query += "Child='" + r.Value + "'; ";

                using (StreamWriter w = File.AppendText(Parser.dataDir + "/sub/Cats.sql"))
                {
                    w.WriteLine(query);
                }

            }

        }

        private List<KeyValuePair<string, List<string>>> findDuc(string key) //if (!cat.Contains(t.Key)) is false
        {
            List<KeyValuePair<string, List<string>>> result = new List<KeyValuePair<string, List<string>>>();

            List<string> tbs = new List<string>();
            foreach (KeyValuePair<string, string> x in rela)
            {
                if (ParseJSONObjects.repSpac2Und((x.Value as string)) == ParseJSONObjects.repSpac2Und(key))
                {
                    tbs.Add(ParseJSONObjects.repSpac2Und(x.Key));
                }
            }

            foreach (string x in tbs)
            {
                //Assume x is Table_#
                foreach (KeyValuePair<string, List<string>> y in cates)
                {
                    if (ParseJSONObjects.repSpac2Und(x) == ParseJSONObjects.repSpac2Und(y.Key))
                    {
                        result.Add(y);
                    }
                }
            }

            return result;
        }

        private List<KeyValuePair<string, List<string>>> asemb(KeyValuePair<string, Hashtable> input) //if (!cat.Contains(t.Key)) is true
        {
            int i = 0;

            Hashtable hash = input.Value as Hashtable;
            List<KeyValuePair<string, List<string>>> t = new List<KeyValuePair<string, List<string>>>();
            List<string> tmp = new List<string>();
            List<KeyValuePair<string, List<string>>> cates_add = new List<KeyValuePair<string, List<string>>>();
            List<KeyValuePair<string, List<string>>> cates_remove = new List<KeyValuePair<string, List<string>>>();

            foreach (string x in hash.Keys)
            {
                tmp.Add(ParseJSONObjects.repSpac2Und(x));
            }

            foreach (KeyValuePair<string, List<string>> x in cates)
            {

                List<string> y = (x.Value as List<string>);
                List<string> anoProduct = new List<string>();
                bool ex = true;

                foreach (string z in y)
                {

                    if (!tmp.Contains(ParseJSONObjects.repSpac2Und(z)))
                    {
                        ex = false;
                    }
                    else
                    {
                        tmp.Remove(ParseJSONObjects.repSpac2Und(z));
                        anoProduct.Add(ParseJSONObjects.repSpac2Und(z));
                    }
                }

                if (ex)
                {
                    t.Add(x);
                    rela.Add(new KeyValuePair<string, string>(ParseJSONObjects.repSpac2Und(x.Key), ParseJSONObjects.repSpac2Und(input.Key)));
                }
                else if (anoProduct.Count > 0 & anoProduct.Count < (x.Value as List<string>).Count)
                {
                    //Breakdown the prodcut into assemb;

                    cates_remove.Add(x);
                    KeyValuePair<string, List<string>> new_x = new KeyValuePair<string, List<string>>(ParseJSONObjects.repSpac2Und(x.Key), anoProduct);
                    cates_add.Add(new_x);

                    List<string> ano = new List<string>();

                    foreach (string xs in x.Value)
                    {
                        if (!anoProduct.Contains(ParseJSONObjects.repSpac2Und(xs)))
                        {
                            ano.Add(ParseJSONObjects.repSpac2Und(xs));
                        }
                    }

                    if (ano.Count > 0)
                    {
                        string newName = "n" + ParseJSONObjects.repSpac2Und(x.Key) + j.ToString();
                        j++;

                        KeyValuePair<string, List<string>> new_s = new KeyValuePair<string, List<string>>(ParseJSONObjects.repSpac2Und(newName), ano);
                        cates_add.Add(new_s);


                        List<KeyValuePair<string, string>> tbs = new List<KeyValuePair<string, string>>();

                        foreach (KeyValuePair<string, string> xs in rela)
                        {
                            if (ParseJSONObjects.repSpac2Und((xs.Key as string)) == ParseJSONObjects.repSpac2Und(x.Key))
                            {
                                tbs.Add(new KeyValuePair<string, string>(ParseJSONObjects.repSpac2Und(newName), ParseJSONObjects.repSpac2Und(xs.Value)));
                            }
                        }

                        foreach (KeyValuePair<string, string> xs in tbs)
                        {
                            rela.Add(xs);
                        }
                    }

                }

                i++;

            }

            foreach (KeyValuePair<string, List<string>> sth in cates_remove)
            {
                cates.Remove(sth);
            }

            foreach (KeyValuePair<string, List<string>> sth in cates_add)
            {
                cates.Add(sth);
            }

            List<string> ts = new List<string>();

            foreach (string k in tmp)
            {
                ts.Add(ParseJSONObjects.repSpac2Und(k));
            }

            if (ts.Count > 0)
            {
                KeyValuePair<string, List<string>> tt = new KeyValuePair<string, List<string>>(ParseJSONObjects.repSpac2Und("Table_" + i.ToString()), ts);

                cates.Add(tt);

                rela.Add(new KeyValuePair<string, string>(ParseJSONObjects.repSpac2Und(tt.Key), ParseJSONObjects.repSpac2Und(input.Key)));


                t.Add(tt);
            }
            return t;
        }


        private List<KeyValuePair<string, List<string>>> findsDuc(KeyValuePair<string, Hashtable> keys)
        {
            List<KeyValuePair<string, List<string>>> t = new List<KeyValuePair<string, List<string>>>();
            List<string> tmp = new List<string>();

            foreach (string k in keys.Value.Keys)
            {
                tmp.Add(ParseJSONObjects.repSpac2Und(k));
            }

            foreach (KeyValuePair<string, List<string>> x in cates)
            {

                List<string> y = (x.Value as List<string>);

                bool ex = true;

                foreach (string z in y)
                {

                    if (!tmp.Contains(ParseJSONObjects.repSpac2Und(z)))
                    {
                        ex = false;
                    }
                    //else
                    //{
                        //tmp.Remove(ParseJSONObjects.repSpac2Und(z));
                    //}
                }

                if (ex)
                {
                    t.Add(x);
                    string input = keys.Key;

                    if (input != null)
                    {

                        rela.Add(new KeyValuePair<string, string>(ParseJSONObjects.repSpac2Und(x.Key), ParseJSONObjects.repSpac2Und(input)));
                    }

                }


            }
            /*
            if (tmp.Count > 0)
            {
                Console.WriteLine("You failed");
            }
            */
            return t;
        }

        public void Pop()
        {

            foreach (KeyValuePair<string, List<KeyValuePair<string, Hashtable>>> tmpp in stack)
            {
                List<KeyValuePair<string, Hashtable>> tmp = tmpp.Value as List<KeyValuePair<string, Hashtable>>;
                foreach (KeyValuePair<string, Hashtable> t in tmp)
                {
                    List<KeyValuePair<string, List<string>>> temp;

                    if (!cat.Contains(ParseJSONObjects.repSpac2Und(t.Key)))
                    {
                        cat.Add(ParseJSONObjects.repSpac2Und(t.Key));
                        temp = asemb(t);
                    }
                    else
                    {
                        temp = findDuc(ParseJSONObjects.repSpac2Und(t.Key));
                    }

                }

            }

            rela.Clear();
            

            foreach (KeyValuePair<string, KeyValuePair<string, Hashtable>> tmpp in chunk)
            {

                List<string> tables = new List<string>();
                KeyValuePair<string, Hashtable> tmp = tmpp.Value;

                //If cat does not contain the category
                List<KeyValuePair<string, List<string>>> temp = findsDuc(tmp);

                //write mysql query of saved cates and required parameters as recorded, and save those queries to local *.sql file.
                foreach (KeyValuePair<string, List<string>> product in temp)
                {
                    if (!tables.Contains(ParseJSONObjects.repSpac2Und(product.Key)))
                    {
                        tables.Add(ParseJSONObjects.repSpac2Und(product.Key));

                        string query = "Insert into " + ParseJSONObjects.repSpac2Und(product.Key.ToString()) + " set business_id='" + ParseJSONObjects.cleanTextforSQL(tmpp.Key) + "',";
                        int before = query.Length;

                        foreach (string s in (product.Value as List<string>))
                        {
                            query += ParseJSONObjects.repSpac2Und(s) + "='" + ParseJSONObjects.cleanTextforSQL((tmp.Value as Hashtable)[ParseJSONObjects.repSpac2Und(s)].ToString()) + "',";
                        }


                        query = query.Substring(0, query.Length - 1) + ";";

                        using (StreamWriter w = File.AppendText(Parser.dataDir + "/sub/insertIntoCatTables.sql"))
                        {
                            w.WriteLine(query);
                        }
                    }


                    //end inner 'foreach'
                }

                //end outter 'foreach'
            }


            this.sortMeow();
            //end func
        }


        public string CTR(string s)
        {
            string t = "";

            switch (s)
            {
                case "full_address":
                    t = "full_address tinytext,";
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

    }
}