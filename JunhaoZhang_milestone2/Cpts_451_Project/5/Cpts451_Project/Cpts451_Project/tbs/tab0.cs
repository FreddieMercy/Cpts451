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
    public partial class MainWindow : Window
    {

        private void enableUpdate(object sender, EventArgs e)
        {
            if (State_cb.SelectedIndex == 0 | Selected_Categories.Items.Count == 0 | State_cb.SelectedItem==null)
            {
                Selected_Categories_Update.IsEnabled = false;
            }
            else
            {
                Selected_Categories_Update.IsEnabled = true;
            }
        }

        private void Zip_lb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string s = (sender as ListBox).SelectedItem as string;
            if (s != null)
            {
                ObservableCollection<string> tmp = mydb.SQLSELECTExec("SELECT population,avg_income,under18years,18_to_24years,25_to_44years, 45_to_64years, 65_and_over, median_age FROM censusdata WHERE zipcode = '"
                    + s + "';");

                population_tb.Text = tmp[0];
                aver_inc_tb.Text = tmp[1];
                age_under_18_tb.Text = tmp[2] + "%";
                age_18_to_24_tb.Text = tmp[3] + "%";
                age_25_to_44_tb.Text = tmp[4] + "%";
                age_45_to_64_tb.Text = tmp[5] + "%";
                age_65_and_older_tb.Text = tmp[6] + "%";
                medi_age_tb.Text = tmp[7];
            }

        }

        private void City_lb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            zip_lb.ItemsSource = mydb.SQLSELECTExec("SELECT distinct zipcode FROM censusdata WHERE city='" + ((sender as ListBox).SelectedItem as string) + "' and state='" +
                (State_cb.SelectedItem as string) + "'" + " ORDER BY Zipcode;", "Zipcode");

            string s = (sender as ListBox).SelectedItem as string;
            string z = State_cb.SelectedItem as string;
            if (s != null && z != null)
            {
                ObservableCollection<string> tmp = mydb.SQLSELECTExec("SELECT population,avg_income,under18years,18_to_24years,25_to_44years, 45_to_64years, 65_and_over, median_age FROM censusdata WHERE state = '" + z + "' and city='"
                    + s + "';");

                Col2population_tb.Text = tmp[0];
                Col2aver_inc_tb.Text = tmp[1];
                Col2age_under_18_tb.Text = tmp[2] + "%";
                Col2age_18_to_24_tb.Text = tmp[3] + "%";
                Col2age_25_to_44_tb.Text = tmp[4] + "%";
                Col2age_45_to_64_tb.Text = tmp[5] + "%";
                Col2age_65_and_older_tb.Text = tmp[6] + "%";
                Col2medi_age_tb.Text = tmp[7];
            }

            population_tb.Text = "";
            aver_inc_tb.Text = "";
            age_under_18_tb.Text = "";
            age_18_to_24_tb.Text = "";
            age_25_to_44_tb.Text = "";
            age_45_to_64_tb.Text = "";
            age_65_and_older_tb.Text = "";
            medi_age_tb.Text = "";
        }

        private void State_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            city_lb.ItemsSource = mydb.SQLSELECTExec("SELECT distinct city FROM censusdata WHERE state='" + ((sender as ComboBox).SelectedItem as string) + "' ORDER BY city;", "city");

            string s = (sender as ComboBox).SelectedItem as string;
            if (s != null)
            {
                ObservableCollection<string> tmp = mydb.SQLSELECTExec("SELECT population,avg_income,under18years,18_to_24years,25_to_44years, 45_to_64years, 65_and_over, median_age FROM censusdata WHERE state = '"
                    + s + "';");

                Col3population_tb.Text = tmp[0];
                Col3aver_inc_tb.Text = tmp[1];
                Col3age_under_18_tb.Text = tmp[2] + "%";
                Col3age_18_to_24_tb.Text = tmp[3] + "%";
                Col3age_25_to_44_tb.Text = tmp[4] + "%";
                Col3age_45_to_64_tb.Text = tmp[5] + "%";
                Col3age_65_and_older_tb.Text = tmp[6] + "%";
                Col3medi_age_tb.Text = tmp[7];
            }

            population_tb.Text = "";
            aver_inc_tb.Text = "";
            age_under_18_tb.Text = "";
            age_18_to_24_tb.Text = "";
            age_25_to_44_tb.Text = "";
            age_45_to_64_tb.Text = "";
            age_65_and_older_tb.Text = "";
            medi_age_tb.Text = "";

            Col2population_tb.Text = "";
            Col2aver_inc_tb.Text = "";
            Col2age_under_18_tb.Text = "";
            Col2age_18_to_24_tb.Text = "";
            Col2age_25_to_44_tb.Text = "";
            Col2age_45_to_64_tb.Text = "";
            Col2age_65_and_older_tb.Text = "";
            Col2medi_age_tb.Text = "";
        }

        private void onSelectedCat(object sender, EventArgs e)
        {
            object sel = Categories.SelectedItem;
            if (!Selected_Categories.Items.Contains(sel))
            {
                Selected_Categories.Items.Add(sel);

            }
        }

        private void Selected_Categories_Update_Click(object sender, RoutedEventArgs e)
        {
            string state = "select category, count(*), avg(stars), avg(review_count) from (select * from category where ";
            string city = "select category, count(*), avg(stars), avg(review_count) from (select * from category where ";
            string zip = "select category, count(*), avg(stars), avg(review_count) from (select * from category where ";
            string cat = "";

            foreach (string x in Selected_Categories.Items)
            {
                cat += " category='" + x + "' or";
            }


            cat = cat.Substring(0, cat.Length - 3) + ") as a natural join business ";
            state += cat + "where state='" + all_state[State_cb.SelectedItem.ToString()] + "' group by category;";
            city += cat + "where state='" + all_state[State_cb.SelectedItem.ToString()] + "' and city='" + city_lb.SelectedItem + "' group by category;";
            zip += cat + "where zipcode='" + zip_lb.SelectedItem + "' group by category;";
            StateDG.ItemsSource = findThe3(state);
            CityDG.ItemsSource = findThe3(city);
            ZipDG.ItemsSource = findThe3(zip);

        }

        private ObservableCollection<Tab1The3> findThe3(string s)
        {
            ObservableCollection<Tab1The3> tmp = new ObservableCollection<Tab1The3>();
            List<string> a = mydb2.SQLSELECTExec(s).ToList();
            for (int i = 0; i < a.Count; i++)
            {
                tmp.Add(new Tab1The3() { cat = a[i], CatNum = a[i + 1], avRev = a[i + 2], avStar = a[i + 3] });
                i = i + 3;
            }

            if (tmp.Count == 0)
            {
                tmp.Add(new Tab1The3() { cat = "Not Found", avRev = "Not Found", avStar = "Not Found", CatNum = "Not Found" });
            }

            return tmp;
        }

    }
}
