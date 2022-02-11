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

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для napitkiDop.xaml
    /// </summary>
    public partial class napitkiDop : Window
    {
        public napitkiDop(string name, int coust, int count)
        {
            InitializeComponent();
            VendingMachinesEntities database = new VendingMachinesEntities();
            Name.Content = name;
            Tsena.Text = Convert.ToString(coust);
            Kolvo.Text = Convert.ToString(count);
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
