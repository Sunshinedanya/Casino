using Casino.DB;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Policy;
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

namespace Casino.Pages
{
    /// <summary>
    /// Логика взаимодействия для RullerPage.xaml
    /// </summary>
    public partial class RullerPage : Page
    {
        Random random;
        DispatcherTimer SpinTimer;
        public RullerPage()
        {
            InitializeComponent();
            random = new Random();
        }

        private void BetTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = BetTB.Text;
            text = new string(text.Where(c => int.TryParse(c.ToString(), out int temp)).ToArray());
            BetTB.Text = text;
            BetsButtons.IsEnabled = BetTB.Text != "" && int.Parse(BetTB.Text) < Utils.LoggedUser.CurrentBalance;
        }

        private void MakeBet(object sender, RoutedEventArgs e)
        {
            var bet = int.Parse(BetTB.Text);

            if (Utils.LoggedUser.CurrentBalance - bet > 0 == false)
                MessageBox.Show("У тебя нехватает денег гений");

            if(random.Next(100) > 50)
            {
                Utils.LoggedUser.CurrentBalance -= bet;
                var transaction = new Transaction
                {
                    TransactionId = bet,
                    User = Utils.LoggedUser,
                    Summ = -bet,
                    Date = DateTime.Now,
                    TypeId = 2
                };
                Utils.DB.Transaction.Add(transaction);
                Utils.DB.SaveChanges();
                
                qrImg.Source = GenerateQrCodeBitmapImage($"Lose {bet}");
            }
            else
            {

                Utils.LoggedUser.CurrentBalance += bet;
                var transaction = new Transaction
                {
                    TransactionId = bet,
                    User = Utils.LoggedUser,
                    Summ = +bet,
                    Date = DateTime.Now,
                    TypeId = 2
                };
                Utils.DB.Transaction.Add(transaction);
                Utils.DB.SaveChanges();
                
                qrImg.Source = GenerateQrCodeBitmapImage($"Win {bet}");
            }
        }

        private BitmapImage GenerateQrCodeBitmapImage(string text)
        {
            using (var qrGenerator = new QRCodeGenerator())
            {
                using (var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q))
                {
                    using (var qrCode = new QRCode(qrCodeData))
                    {
                        using (var qrBitmap = qrCode.GetGraphic(20))
                        {
                            using (var ms = new MemoryStream())
                            {
                                qrBitmap.Save(ms, ImageFormat.Png);
                                ms.Position = 0;
                                var bitmapImage = new BitmapImage(); 
                                
                                bitmapImage.BeginInit();
                                {
                                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                                    bitmapImage.StreamSource = ms;
                                }
                                bitmapImage.EndInit();
                                
                                return bitmapImage;
                            }
                        }
                    }
                }
            }
        }
    }
}
