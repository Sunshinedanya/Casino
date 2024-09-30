using Casino.DB;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Casino.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
            DataContext = Utils.LoggedUser;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditProfilePage());
        }


        private void MoneyMove(int money)
        {
            if (money == 0)
                return;
            Utils.LoggedUser.CurrentBalance += money;
            var transaction = new Transaction
            {
                User = Utils.LoggedUser,
                Summ = money,
                Date = DateTime.Now
            };
            if (money < 0)
                transaction.TypeId = 2;
            else
                transaction.TypeId = 1;

            Utils.DB.Transaction.Add(transaction);
            Utils.DB.SaveChanges();
            DataContext = null;
            DataContext = Utils.LoggedUser;

        }

        private void MoneyOut(object sender, RoutedEventArgs e)
        {
            var delta = DeltaTB.Text;
            if(int.TryParse(delta, out int money))
            {
                MoneyMove(-money);
            }
        }
        private void MoneyIn(object sender, RoutedEventArgs e)
        {
            var delta = DeltaTB.Text;
            if (int.TryParse(delta, out int money))
            {
                MoneyMove(money);
            }
        }

        private void DeltaTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = DeltaTB.Text;
            text =new string(text.Where(c => int.TryParse(c.ToString(), out int temp)).ToArray());
            DeltaTB.Text = text;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TransactionsPage());
        }
    }
}
