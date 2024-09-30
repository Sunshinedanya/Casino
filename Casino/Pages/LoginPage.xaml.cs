using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Casino.Pages
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void RegisterClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new RegisterPage());
        }

        private void EnterClick(object sender, RoutedEventArgs e)
        {
            var login = LoginTB.Text;
            var pass = PasswordPB.Password;

            var user = Utils.DB.User.FirstOrDefault(i => i.Password == pass && i.Login == login);

            if (user != null) {
                Utils.LoggedUser = user;
                NavigationService.Navigate(new MainPage());

            }
            else
            {
                MessageBox.Show("Проверьте правильность введеных данных");
            }
        }
    }
}