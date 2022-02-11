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
    /// Логика взаимодействия для otshet.xaml
    /// </summary>
    public partial class otshet : Page
    {
        public otshet()
        {
            InitializeComponent();
            VendingMachinesEntities database = new VendingMachinesEntities();
            table.ItemsSource = database.Drinks.ToList();
        }

        private void table_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
