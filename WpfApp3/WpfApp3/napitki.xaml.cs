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



namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для napitki.xaml
    /// </summary>
    public partial class napitki : Page
    {
        public napitki()
        {
            InitializeComponent();
            VendingMachinesEntities database = new VendingMachinesEntities();
            lv.ItemsSource = database.Drinks.ToList();
        }
        public VendingMachinesEntities database = new VendingMachinesEntities();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (lv.SelectedItem != null)
            {
                var a = new VendingMachineDrinks();
                int check = Convert.ToInt32((lv.SelectedItem as Drinks).Id);
                var selectdrink = database.Drinks.Where(c => c.Id == check).FirstOrDefault();
                var selectdrink2 = database.VendingMachineDrinks.Where(c => c.DrinksId == check).FirstOrDefault();
                string name = selectdrink.Name;
                int cost = Convert.ToInt32(selectdrink.Cost);
                int count = Convert.ToInt32(selectdrink2.Count);
                new napitkiDop(name, cost, count).ShowDialog();
            }
        }

        private void lv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
