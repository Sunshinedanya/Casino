using Casino.DB;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Casino.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditProfilePage.xaml
    /// </summary>
    public partial class EditProfilePage : Page
    {
        byte[] Avatar;
        public EditProfilePage()
        {
            InitializeComponent();
            Avatar = Utils.LoggedUser.Avatar;
            DataContext = Utils.LoggedUser;
        }

        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            var user = new User
            {
                Name = NameTB.Text,
                Surname = SurnameTB.Text,
                Login = LoginTB.Text
            };

            Utils.DB.User.Add(user);
            Utils.DB.SaveChanges();
            NavigationService.GoBack();
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
