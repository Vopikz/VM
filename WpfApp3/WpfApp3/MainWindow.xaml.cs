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
using System.Windows.Threading;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //lv.ItemsSource = ;
            VendingMachinesEntities database = new VendingMachinesEntities();
            lv.ItemsSource = database.Drinks.ToList();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += buttoncheck;
            timer.Start();
        }

        public void buttoncheck(object sender, object e)
        {
            VendingMachinesEntities database = new VendingMachinesEntities();
            List<Button> coin_buttons = new List<Button>() { one, two, five, desyat };
            for (int i = 0; i < 4; i++)
            {
                var coincheck = database.VendingMachineCoins.Where(c => c.CoinsId == i + 1).FirstOrDefault();
                if (coincheck.IsActive == 0)
                {
                    coin_buttons[i].IsEnabled = false;
                }
                else
                {
                    coin_buttons[i].IsEnabled = true;
                }
            }
        }

        public VendingMachinesEntities database = new VendingMachinesEntities();
        public int[] coins_insert = new int[4];

        //public void UpdateData(object sender, object e)
        //{
        //    VendingMachinesEntities database = new VendingMachinesEntities();
        //    lv.ItemsSource = database.Drinks.ToList();
        //}

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var secretcode = database.VendingMachines.Single();
            adminsecret.Visibility = Visibility.Visible;
            if (button.Content == "Войти")
            {
                if (adminsecret.Password == secretcode.SecretCode)
                {
                    Admin w = new Admin();
                    w.Show();
                }
                else
                {
                    MessageBox.Show("Неверный код");
                }
            }
            button.Content = "Войти";
        }

        int kassa = 0;
        int odin = 0, dva = 0, pyat = 0, ten = 0;

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var moneti = database.VendingMachineCoins.Where(c => c.CoinsId == 1).FirstOrDefault();
            kassa = kassa + 1;
            chet.Content = kassa;
            odin = odin + 1;
            moneti.Count += 1;
            database.SaveChanges();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var moneti = database.VendingMachineCoins.Where(c => c.CoinsId == 2).FirstOrDefault();
            kassa = kassa + 2;
            chet.Content = kassa;
            dva = dva + 1;
            moneti.Count += 1;
            database.SaveChanges();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var moneti = database.VendingMachineCoins.Where(c => c.CoinsId == 3).FirstOrDefault();
            kassa = kassa + 5;
            chet.Content = kassa;
            pyat = pyat + 1;
            moneti.Count += 1;
            database.SaveChanges();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var moneti = database.VendingMachineCoins.Where(c => c.CoinsId == 4).FirstOrDefault();
            kassa = kassa + 10;
            chet.Content = kassa;
            ten = ten + 1;
            moneti.Count += 1;
            database.SaveChanges();
        }

        private void lv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lv.SelectedItem != null)
            {
                var a = new VendingMachineCoins();

                for (int i = 0; i < a.CoinsId; i++)
                {
                    var nowCoin = database.VendingMachineCoins.Single(id => id.CoinsId == i + 1);
                    nowCoin.Count += coins_insert[i];
                }
                int kassamoney = Convert.ToInt32(chet.Content), cost = Convert.ToInt32((lv.SelectedItem as Drinks).Cost);

                if (kassamoney >= cost)
                {
                    int check = Convert.ToInt32((lv.SelectedItem as Drinks).Id); //получение id выбранного напитка
                    var drinkcount = database.VendingMachineDrinks.Where(c => c.DrinksId == check).FirstOrDefault(); //сортировка по полученному id
                    if (drinkcount.Count != 0) //проверка на количество напитков
                    {
                        drinkcount.Count -= 1; //изменение значения в бд
                        database.SaveChanges();
                        chet.Content = kassamoney - cost;
                        kassa = Convert.ToInt32(chet.Content);
                    }
                    else
                    {
                        MessageBox.Show("Выбранный напиток отсуствует", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Недостаточно денег");
                }
                lv.SelectedItem = null;
            }
        }

        int odin1 = 0, dva1 = 0, pyat1 = 0, ten1 = 0;

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            var a = new VendingMachineCoins();
            var tencount = database.VendingMachineCoins.Single(b => b.CoinsId == 4);
            var fivecount = database.VendingMachineCoins.Single(b => b.CoinsId == 3);
            var twocount = database.VendingMachineCoins.Single(b => b.CoinsId == 2);
            var onecount = database.VendingMachineCoins.Single(b => b.CoinsId == 1);

            MessageBox.Show(chet.Content.ToString(), Convert.ToString("1:" + odin + ", 2:" + dva + ", 5:" + pyat + ", 10:" + ten));

            ten1 = kassa / 10;
            if (tencount.Count > 0)
            {
                if (tencount.Count >= ten1)
                {
                    kassa -= ten1 * 10;
                    tencount.Count -= ten1;
                    database.SaveChanges();
                }
                else
                {
                    int sdacha = ten1 - tencount.Count;
                    kassa -= sdacha * 10;
                    tencount.Count -= sdacha;
                    database.SaveChanges();
                }
            }

            pyat1 = kassa / 5;
            if (fivecount.Count > 0)
            {
                if (fivecount.Count != 0)
                {
                    kassa -= pyat1 * 5;
                    fivecount.Count -= pyat1;
                    database.SaveChanges();
                }
                else
                {
                    int sdacha = pyat1 - fivecount.Count;
                    kassa -= sdacha * 5;
                    fivecount.Count -= sdacha;
                    database.SaveChanges();
                }
            }

            dva1 = kassa / 2;
            if (twocount.Count > 0)
            {
                if (twocount.Count != 0)
                {
                    kassa -= dva1 * 2;
                    twocount.Count -= dva1;
                    database.SaveChanges();
                }
                else
                {
                    int sdacha = dva1 - twocount.Count;
                    kassa -= sdacha * 2;
                    twocount.Count -= sdacha;
                    database.SaveChanges();
                }
            }

            odin1 = kassa / 1;
            if (onecount.Count > 0)
            {
                if (onecount.Count != 0)
                {
                    kassa -= odin1 * 1;
                    onecount.Count -= odin1;
                    database.SaveChanges();
                }
                else
                {
                    int sdacha = odin1 - onecount.Count;
                    kassa -= sdacha * 1;
                    onecount.Count -= sdacha;
                    database.SaveChanges();
                }
            }

            MessageBox.Show(Convert.ToString(kassa + " " + ten1 + " " + dva1 + " " + pyat1 + " " + odin1));
        }
    }
}

