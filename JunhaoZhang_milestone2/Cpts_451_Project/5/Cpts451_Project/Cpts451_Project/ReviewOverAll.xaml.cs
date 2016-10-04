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
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Backward_logic_Engine;

namespace Cpts451_Project
{
    /// <summary>
    /// Interaction logic for ReviewOverAll.xaml
    /// </summary>
    public partial class ReviewOverAll : Window
    {
        public ReviewOverAll(ObservableCollection<forRevDG> T)
        {
            InitializeComponent();
            ReviewDG.ItemsSource = T;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Window1((sender as Button).Content.ToString()).ShowDialog();
        }
    }
}
