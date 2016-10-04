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

        private void tb2State_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tb2city_lb.ItemsSource = mydb2.SQLSELECTExec("SELECT distinct city FROM business WHERE state='" + ((sender as ComboBox).SelectedItem as string) + "' ORDER BY city;", "city");

        }

        private void tb2City_lb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tb2zip_lb.ItemsSource = mydb2.SQLSELECTExec("SELECT distinct zipcode FROM business WHERE city='" + ((sender as ListBox).SelectedItem as string) + "' and state='" +
                (tb2State_cb.SelectedItem as string) + "'" + " ORDER BY Zipcode;", "Zipcode");
        }

        public void tb2onSelectedCat(object sender, EventArgs e)
        {
            object sel = tb2Categories.SelectedItem;
            if (!tb2Selected_Categories.Items.Contains(sel))
            {
                tb2Selected_Categories.Items.Add(sel);

            }
        }
        private void enableSearch(object sender, EventArgs e)
        {
            if (tb2Selected_Categories.Items.Count == 0)
            {
                Selected_Categories_Search.IsEnabled = false;
            }
            else
            {
                Selected_Categories_Search.IsEnabled = true;
            }
        }

        private void getAttrs(object sender, EventArgs e)
        {
            //tbSelAttr.Background = System.Windows.Media.Brushes.Black; //System.Windows.Media.Color.FromArgb(System.Drawing.Color.Gray.A, System.Drawing.Color.Gray.R, System.Drawing.Color.Gray.G, System.Drawing.Color.Gray.B);
            //AttrTree.IsEnabled = false;
            List<TreeViewList> tmp = new List<TreeViewList>();
            if (tb2Selected_Categories.Items.Count > 0)
            {
                string cat = "select distinct attr from (select * from category where ";

                foreach (string x in tb2Selected_Categories.Items)
                {
                    cat += "category='" + x + "' or ";

                }
                //select distinct attr from (select * from category where category='chinese') as a natural join attributes;
                newbie = mydb2.SQLSELECTExec(cat.Substring(0, cat.Length - 3) + ") as a natural join attributes;").ToList();

                foreach (string x in newbie)
                {
                    if ("breakfast".IndexOf(x, StringComparison.CurrentCultureIgnoreCase) >= 0 |
                        "brunch".IndexOf(x, StringComparison.CurrentCultureIgnoreCase) >= 0 |
                        "lunch".IndexOf(x, StringComparison.CurrentCultureIgnoreCase) >= 0 |
                        "dinner".IndexOf(x, StringComparison.CurrentCultureIgnoreCase) >= 0 |
                        "dessert".IndexOf(x, StringComparison.CurrentCultureIgnoreCase) >= 0 |
                        "latenight".IndexOf(x, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    {
                        bool found = true;
                        foreach (TreeViewList y in tmp)
                        {
                            if (y.Name == "Meals Served")
                            {
                                y.Items.Add(new Strings() { title = x });
                                found = false;
                            }
                        }

                        if (found)
                        {
                            List<Strings> z = new List<Strings>();
                            z.Add(new Strings() { title = x });
                            tmp.Add(new TreeViewList() { Name = "Meals Served", Items = z });
                        }
                    }

                    else if ("dj".IndexOf(x, StringComparison.CurrentCultureIgnoreCase) >= 0 |
                            "jukebox".IndexOf(x, StringComparison.CurrentCultureIgnoreCase) >= 0 |
                            "karaoke".IndexOf(x, StringComparison.CurrentCultureIgnoreCase) >= 0 |
                            "live".IndexOf(x, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    {
                        bool found = true;
                        foreach (TreeViewList y in tmp)
                        {
                            if (y.Name == "Music")
                            {
                                y.Items.Add(new Strings() { title = x });
                                found = false;
                            }
                        }

                        if (found)
                        {
                            List<Strings> z = new List<Strings>();
                            z.Add(new Strings() { title = x });
                            tmp.Add(new TreeViewList() { Name = "Music", Items = z });
                        }
                    }

                    else if ("street".IndexOf(x, StringComparison.CurrentCultureIgnoreCase) >= 0 |
                        "garage".IndexOf(x, StringComparison.CurrentCultureIgnoreCase) >= 0 |
                        "valet".IndexOf(x, StringComparison.CurrentCultureIgnoreCase) >= 0 |
                        "lot".IndexOf(x, StringComparison.CurrentCultureIgnoreCase) >= 0 |
                        "validated".IndexOf(x, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    {
                        bool found = true;
                        foreach (TreeViewList y in tmp)
                        {
                            if (y.Name == "Parking")
                            {
                                y.Items.Add(new Strings() { title = x });
                                found = false;
                            }
                        }

                        if (found)
                        {
                            List<Strings> z = new List<Strings>();
                            z.Add(new Strings() { title = x });
                            tmp.Add(new TreeViewList() { Name = "Parking", Items = z });
                        }
                    }

                    else if ("kosher".IndexOf(x, StringComparison.CurrentCultureIgnoreCase) >= 0 |
                        "gluten_free".IndexOf(x, StringComparison.CurrentCultureIgnoreCase) >= 0 |
                        "halal".IndexOf(x, StringComparison.CurrentCultureIgnoreCase) >= 0 |
                        "vegetarian".IndexOf(x, StringComparison.CurrentCultureIgnoreCase) >= 0 |
                        "dairy_free".IndexOf(x, StringComparison.CurrentCultureIgnoreCase) >= 0 |
                        "soy_free".IndexOf(x, StringComparison.CurrentCultureIgnoreCase) >= 0 |
                        "vegan".IndexOf(x, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    {
                        bool found = true;
                        foreach (TreeViewList y in tmp)
                        {
                            if (y.Name == "Dietary Restrictions")
                            {
                                y.Items.Add(new Strings() { title = x });
                                found = false;
                            }
                        }

                        if (found)
                        {
                            List<Strings> z = new List<Strings>();
                            z.Add(new Strings() { title = x });
                            tmp.Add(new TreeViewList() { Name = "Dietary Restrictions", Items = z });
                        }
                    }

                    else
                    {
                        bool found = true;
                        foreach (TreeViewList y in tmp)
                        {
                            if (y.Name == "General Features")
                            {
                                y.Items.Add(new Strings() { title = x });
                                found = false;
                            }
                        }

                        if (found)
                        {
                            List<Strings> z = new List<Strings>();
                            z.Add(new Strings() { title = x });
                            tmp.Add(new TreeViewList() { Name = "General Features", Items = z });
                        }
                    }
                }
            }

            AttrTree.Items.Clear();

            AttrTree.ItemsSource = tmp;
            //AttrTree.IsEnabled = true;
            AttrValue_cb.Items.Clear();
            //tbSelAttr.Background = System.Windows.Media.Brushes.White;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AttrValue_cb.Items.Clear();
            string s = ((sender as Button).Content as TextBlock).Text;

            if (cons.ContainsKey(s))
            {
                AttrValue_cb.ItemsSource = cons[s] as List<string>;
            }
            else
            {
                AttrValue_cb.Items.Add("True");
                AttrValue_cb.Items.Add("False");
            }

            SelAttofBtn = s;
        }

        private void AddSelBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AttrValue_cb.SelectedItem != null && !SelAttrTracker.ContainsKey(SelAttofBtn))
            {
                string tb = SelAttofBtn + "='" + AttrValue_cb.SelectedItem.ToString() + "'";
                SelAttr.Items.Add(tb);
                SelAttrTracker.Add(SelAttofBtn, tb);
            }
            else if (AttrValue_cb.SelectedItem != null)
            {
                SelAttr.Items.Remove(SelAttrTracker[SelAttofBtn]);
                SelAttrTracker.Remove(SelAttofBtn);
                string tb = SelAttofBtn + "='" + AttrValue_cb.SelectedItem.ToString() + "'";
                SelAttr.Items.Add(tb);
                SelAttrTracker.Add(SelAttofBtn, tb);
            }
        }

        private void AttrSetVals(object sender, EventArgs e)
        {
            List<string> tmp = new List<string>();

            foreach (string x in SelAttrTracker.Keys)
            {
                if (!newbie.Contains(x))
                {
                    tmp.Add(x);
                }
            }


            foreach (string x in tmp)
            {
                string tb = SelAttrTracker[x] as string;
                SelAttr.Items.Remove(tb);
                SelAttrTracker.Remove(x);
            }


        }

        private void RmBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SelAttr.SelectedItem != null)
            {
                string s = "";
                string tb = SelAttr.SelectedItem as string;
                SelAttr.Items.Remove(tb);

                foreach (string k in SelAttrTracker.Keys)
                {
                    if (SelAttrTracker[k].ToString() == tb)
                    {
                        s = k;
                        break;
                    }
                }

                SelAttrTracker.Remove(s);
            }
        }


        private void MassBtnCrush(object sender, RoutedEventArgs e)
        {
            string bsn = (sender as Button).Tag as string;
            if (bsn != null)
            {
                ObservableCollection<forRevDG> tmp = new ObservableCollection<forRevDG>();
                List<string> a = mydb2.SQLSELECTExec("select date, stars, text, user_id, useful from review where business_id = '" + bsn + "';").ToList();
                for (int i = 0; i < a.Count; i++)
                {
                    tmp.Add(new forRevDG() { revDate = a[i], Stars = a[i + 1], Text = a[i + 2], UserID = a[i + 3], Vote = a[i + 4] });
                    i = i + 4;
                }

                if (tmp.Count == 0)
                {
                    tmp.Add(new forRevDG() { revDate = "Not Found", Stars = "Not Found", Text = "Not Found", UserID = "Not Found", Vote = "Not Found" });
                }

                new ReviewOverAll(tmp).ShowDialog();

            }
        }

        private void NoState_Checked(object sender, RoutedEventArgs e)
        {
                tb2State_cb.IsEnabled = tb2city_lb.IsEnabled = tb2zip_lb.IsEnabled = !(sender as CheckBox).IsChecked.Value;
        }
    }
}
