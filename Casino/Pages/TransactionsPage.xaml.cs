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

namespace Casino.Pages
{
    /// <summary>
    /// Логика взаимодействия для TransactionsPage.xaml
    /// </summary>
    public partial class TransactionsPage : Page
    {
        public TransactionsPage()
        {
            InitializeComponent();
            DataContext = Utils.LoggedUser.Transaction.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
