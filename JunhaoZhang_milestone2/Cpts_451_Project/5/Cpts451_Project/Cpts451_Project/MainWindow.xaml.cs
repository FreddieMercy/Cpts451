using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Backward_logic_Engine;
using System.Collections.ObjectModel;
using Microsoft.VisualBasic.FileIO;
using System.Drawing;
using System.Collections;

namespace Cpts451_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySql_Connection mydb; /* Uncomment to enable Milestone1 */
        MySql_ConnectionTwo mydb2;
        private Dictionary<string, string> all_state = new Dictionary<string, string> { };
        private Hashtable cons = new Hashtable();
        private string SelAttofBtn = "";
        private Hashtable SelAttrTracker = new Hashtable();
        private List<string> newbie = new List<string>();
        // *** IMPORTANT !! ***
        //It is super weird that combo box selection is string, and by default the SelectedItem is "__blank__", which may cause the unexpected error
        // *** Very Important !!!!!!! ***


        public MainWindow()
        {
            InitializeComponent();

            List<string> val = new List<string>();
            val.Add("21plus");
            val.Add("allages");
            cons.Add("Ages_Allowed", val);

            val = new List<string>();
            val.Add("none");
            val.Add("full_bar");
            val.Add("beer_and_wine");
            cons.Add("Alcohol", val);

            val = new List<string>();
            val.Add("casual");
            val.Add("dressy");
            val.Add("formal");
            cons.Add("Attire", val);

            val = new List<string>();
            val.Add("yes_free");
            val.Add("no");
            val.Add("yes_corkage");
            cons.Add("BYOBCorkage", val);

            val = new List<string>();
            val.Add("average");
            val.Add("loud");
            val.Add("very_loud");
            val.Add("quiet");
            cons.Add("Noise_Level", val);

            val = new List<string>();
            val.Add("no");
            val.Add("free");
            val.Add("paid");
            cons.Add("Wi_Fi", val);

            tabControl.SelectedIndex = 1;

            /**** Sync btw tab0 and tab1 ****/

            //tb2Categories.ItemsSource = Categories.Items;
            //tb2State_cb.ItemsSource = State_cb.Items;

            
            

            /* ************  tab0 *********************** */

            mydb = new MySql_Connection();

            State_cb.ItemsSource = mydb.SQLSELECTExec("SELECT DISTINCT state FROM censusdata ORDER BY state; ", "state");
            State_cb.SelectionChanged += State_cb_SelectionChanged;
            State_cb.SelectionChanged += enableUpdate;
            city_lb.SelectionChanged += City_lb_SelectionChanged;
            zip_lb.SelectionChanged += Zip_lb_SelectionChanged;

            mydb2 = new MySql_ConnectionTwo();

            //Categories.ItemsSource = onTest();

            Categories.ItemsSource = mydb2.SQLSELECTExec("select distinct category from category order by category;");
            Categories.SelectionChanged += onSelectedCat;
            Categories.SelectionChanged += enableUpdate;
            // Categories.SelectedItemChanged += onModifySel;

            //Selected_Categories.DataContextChanged +=(s,e)=> { MessageBox.Show("S"); };
            Selected_Categories.SelectionChanged += (sender, e) => { Selected_Categories.Items.Remove(Selected_Categories.SelectedItem); };
            Selected_Categories.SelectionChanged += enableUpdate;

            /**********************************************************************/



            /******************** tab 1 ********************/
            tb2State_cb.ItemsSource = mydb2.SQLSELECTExec("SELECT DISTINCT state FROM business ORDER BY state;", "state");
            tb2Categories.ItemsSource = Categories.Items;

            tb2State_cb.SelectionChanged += tb2State_cb_SelectionChanged;
            tb2city_lb.SelectionChanged += tb2City_lb_SelectionChanged;

            tb2Categories.SelectionChanged += tb2onSelectedCat;
            tb2Categories.SelectionChanged += enableSearch;
            tb2Categories.SelectionChanged += getAttrs;

            tb2Selected_Categories.SelectionChanged += (sender, e) => { tb2Selected_Categories.Items.Remove(tb2Selected_Categories.SelectedItem); };
            tb2Selected_Categories.SelectionChanged += enableSearch;
            tb2Selected_Categories.SelectionChanged += getAttrs;

            tb2Selected_Categories.SelectionChanged += AttrSetVals;

            NoState.Checked += NoState_Checked;
            NoState.Unchecked += NoState_Checked;

            //tb2State_cb.IsEnabled = false;
            //tb2city_lb.IsEnabled = false;
            //tb2zip_lb.IsEnabled = false;

            /************************************************************************/

            for (int i = 0; i < 6; i++)
            {
                MinRat.Items.Add(i);
                MaxRat.Items.Add(i);
            }

            for (int i = 0; i <= Int32.Parse(mydb2.SQLSELECTExec("select max(review_count) from business;")[0]); i++)
            {
                MinRev.Items.Add(i);
                MaxRev.Items.Add(i);
            }

            val = new List<string>();
            for (int i = 0; i <= Int32.Parse(mydb2.SQLSELECTExec("select max(val) from attributes where attr='price_range';")[0]); i++)
            {
                val.Add(i.ToString());
            }

            cons.Add("Price_Range", val);


            //US states translate btw full and abbr.
            using (TextFieldParser data = new TextFieldParser("../../usstate.txt"))
            {
                data.TextFieldType = FieldType.Delimited;
                data.SetDelimiters(" ");
                while (!data.EndOfData)
                {
                    var array = data.ReadFields();

                    string abbreviation = array[0];
                    string state_name = null;
                    if (array.Length > 2)
                    {
                        for (int i = 1; i < array.Length; i++)
                        {
                            if (i != array.Length - 1)
                            {
                                state_name += array[i] + " ";
                            }
                            else
                            {
                                state_name += array[i];
                            }
                        }
                    }
                    else
                    {
                        state_name = array[1];
                    }
                    all_state.Add(state_name, abbreviation);
                    all_state.Add(abbreviation, abbreviation);

                }
                //data.Dispose();
            }
        }

        private void tb2Selected_Categories_Search_Click(object sender, RoutedEventArgs e)
        {
            string final = "select distinct name, city, state, zipcode, stars, review_count, business_id from ";
            string bns = " (select distinct a0.business_id, name, city, state, zipcode, stars, review_count from business, ";
            string cat = "business.business_id=a0.business_id and "; //and condition
            int i = 0;
            string a = "a"; //multi-same-table
            string s = ""; //variabriazied tables making alias
            string z = ""; //keep same business_id
            string TF = ""; //trans True = 1 and False = 0

            foreach (string x in tb2Selected_Categories.Items)
            {
                cat += a + i + ".category='" + x + "' and ";
                s += "category as a" + i + ", ";
                if (i > 0)
                {
                    z += "a" + (i - 1) + ".business_id = a" + i + ".business_id and ";
                }

                i++;
            }

            cat = cat.Substring(0, cat.Length-4);

            if (NoState.IsChecked.HasValue && !NoState.IsChecked.Value)
            {
                if (tb2zip_lb.SelectedItem != null)
                {
                    cat += " and zipcode='" + tb2zip_lb.SelectedItem.ToString() + "' ";
                }
                else
                {
                    if (tb2State_cb.SelectedItem != null)
                    {
                        cat += " and state='" + tb2State_cb.SelectedItem.ToString() + "' ";
                    }

                    if (tb2State_cb.SelectedItem != null && tb2city_lb.SelectedItem != null)
                    {
                        cat += "and city='" + tb2city_lb.SelectedItem.ToString() + "' ";
                    }
                }
            }

            if (MinRat.SelectedItem != null)
            {
                cat += "and stars>=" + MinRat.SelectedItem.ToString();
            }
            if (MaxRat.SelectedItem != null)
            {
                cat += " and stars <=" + MaxRat.SelectedItem.ToString();
            }
            if (MinRev.SelectedItem != null)
            {
                cat += " and review_count>=" + MinRev.SelectedItem.ToString();
            }
            if (MaxRev.SelectedItem != null)
            {
                cat += " and review_count<=" + MaxRev.SelectedItem.ToString();
            }

            if (i > 1)
            {
                cat = bns + s.Substring(0, s.Length - 2) + " " + "where" + " " + cat + " and " + z.Substring(0, z.Length-4)+ ") as a natural join (select * from attributes ";
            }
            else
            {
                //cat = bns + s.Substring(0, s.Length - 2) + " " + "where" + " " + cat + ") as a natural join attributes group by business_id ";
                cat = bns + s.Substring(0, s.Length - 2) + " " + "where" + " " + cat + ") as a natural join (select * from attributes ";
            }

            foreach(string x in SelAttr.Items)
            {
                List<string> sep = x.Split('=').ToList<string>();
                TF += "attr='"+sep[0]+"' and ";
                if(sep[1]=="'True'")
                {
                    TF += "val ='1' and ";
                }
                else if (sep[1] == "'False'")
                {
                    TF += "val = '0' and ";
                }
                else
                {
                    TF += "val="+sep[1]+" and ";
                }
            }

            cat = final + cat;

            if (TF.Length > 0)
            {
                cat += " where "+TF.Substring(0, TF.Length - 4);
            }
            cat += ") as b;";

            

            SixSeg_DG.ItemsSource = findThe6(cat);

        }

        private ObservableCollection<Tab2The6> findThe6(string s)
        {
            ObservableCollection<Tab2The6> tmp = new ObservableCollection<Tab2The6>();
            List<string> a = mydb2.SQLSELECTExec(s).ToList();

            for (int i = 0; i < a.Count; i++)
            {
                tmp.Add(new Tab2The6() { bnsName = a[i], bnsCity = a[i + 1], bnsState = a[i + 2], bnsZip = a[i + 3], bnsAvRat = a[i + 4], bnsNumRev = a[i + 5], bnsId=a[i+6] });
                i = i + 6;
            }

            if (tmp.Count == 0)
            {
                tmp.Add(new Tab2The6() { bnsAvRat = "Not Found", bnsCity = "Not Found", bnsName = "Not Found", bnsNumRev = "Not Found", bnsState = "Not Found", bnsZip = "Not Found" });
            }

            return tmp;
        }

    }
}