using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Casino.DB;
using Microsoft.Win32;

namespace Casino.Pages
{
    public partial class RegisterPage : Page
    {

        private byte[] Avatar;

        public RegisterPage()
        {
            InitializeComponent();
        }
    
        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            if(Utils.DB.User.Any(x => x.Login == LoginTB.Text))
            {
                MessageBox.Show("Пользователь с таким логином уже существует");
                return;
            }

            var user = new User()
            {
                Name = NameTB.Text,     
                Surname = SurnameTB.Text,
                Login = LoginTB.Text,
                Password = PassPB.Password,
                CurrentBalance = 0
            };
            
            if(Avatar != null)
            {
                user.Avatar = Avatar;
            }
            
            Utils.DB.User.Add(user);
            Utils.LoggedUser = user;
            Utils.DB.SaveChanges();
            NavigationService.Navigate(new MainPage());
        }

        private void AvatarClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.ShowDialog();
            var file = openFile.FileName;
            Avatar = File.ReadAllBytes(file);
            BitmapImage bitmapImage = new BitmapImage();

            // Create a MemoryStream and write the byte array to it
            using (MemoryStream memoryStream = new MemoryStream(Avatar))
            {
                // Set the BitmapImage's source to the MemoryStream
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }

            // Assign the BitmapImage to an Image control to display it
            UserIcon.Source = bitmapImage;
        }
    }
}