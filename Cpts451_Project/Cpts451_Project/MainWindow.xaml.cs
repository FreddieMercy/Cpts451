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

namespace Cpts451_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySql_Connection mydb;
        
        // *** IMPORTANT !! ***
        //It is super werid that combo box selection is string, and by default the SelectedItem is "__blank__", which may cause the unexpected error
        // *** Very Important !!!!!!! ***


        public MainWindow()
        {
            InitializeComponent();
            mydb = new MySql_Connection();
         
            State_cb.ItemsSource = mydb.SQLSELECTExec("SELECT DISTINCT state FROM censusdata ORDER BY state; ", "state");
            State_cb.SelectionChanged += State_cb_SelectionChanged;
            city_lb.SelectionChanged += City_lb_SelectionChanged;
            zip_lb.SelectionChanged += Zip_lb_SelectionChanged;
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
                age_under_18_tb.Text = tmp[2];
                age_18_to_24_tb.Text = tmp[3];
                age_25_to_44_tb.Text = tmp[4];
                age_45_to_64_tb.Text = tmp[5];
                age_65_and_older_tb.Text = tmp[6];
                medi_age_tb.Text = tmp[7];
            }

        }

        private void City_lb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            zip_lb.ItemsSource = mydb.SQLSELECTExec("SELECT distinct zipcode FROM censusdata WHERE city='" + ((sender as ListBox).SelectedItem as string) + "' and state='" + 
                (State_cb.SelectedItem as string) + "'" + " ORDER BY Zipcode;", "Zipcode");
        }

        private void State_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            city_lb.ItemsSource = mydb.SQLSELECTExec("SELECT distinct city FROM censusdata WHERE state='" + ((sender as ComboBox).SelectedItem as string) + "' ORDER BY city;", "city");

        }



        //we can use it now



    }
}
