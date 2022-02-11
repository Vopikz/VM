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
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
            List<CheckBox> CoinCh = new List<CheckBox>() { cb1, cb2, cb3, cb4 };
            List<TextBox> textcheck = new List<TextBox>() { text1, text2, text3, text4 };
            for (int i = 0; i < 4; i++)
            {
                var coincheck = database.VendingMachineCoins.Where(c => c.CoinsId == i + 1).FirstOrDefault();
                if (coincheck.IsActive == 0)
                {
                    CoinCh[i].IsChecked = false;
                }
                else
                {
                    CoinCh[i].IsChecked = true;
                }
                textcheck[i].Text = Convert.ToString(coincheck.Count);
            }
        }
        public VendingMachinesEntities database = new VendingMachinesEntities();
        private void save_Click(object sender, RoutedEventArgs e)
        {
            List<CheckBox> CoinCh = new List<CheckBox>() { cb1, cb2, cb3, cb4 };
            List<TextBox> textcheck = new List<TextBox>() { text1, text2, text3, text4 };
            for (int i = 0; i < 4; i++)
            {
                var coincheck = database.VendingMachineCoins.Where(c => c.CoinsId == i + 1).FirstOrDefault();
                if (CoinCh[i].IsChecked == true)
                {
                    coincheck.IsActive = 1;
                }
                else
                {
                    coincheck.IsActive = 0;
                }

                coincheck.Count = Convert.ToInt32(textcheck[i].Text);
                database.SaveChanges();
            }
        }

        private void cb1_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void cb2_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
