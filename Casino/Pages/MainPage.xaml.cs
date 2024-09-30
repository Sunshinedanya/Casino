using System.Windows.Controls;
using System.Windows.Input;

namespace Casino.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            LoggedFrame.Navigate(new ProfilePage());
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var path = (sender as Image).DataContext;
            switch (path) {
                case "profile":
                    LoggedFrame.Navigate(new ProfilePage());
                    break;
                case "casino": 
                    LoggedFrame.Navigate(new RullerPage());
                    break;
            }
        }
    }
}
