using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KisiRehberi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class YeniKisiPage : ContentPage
    {
        public Kisi kisi { get; set; }
        public Stream ImageStreaam { get; set; }
        public static byte[] Arry { get; set; }
        public YeniKisiPage()
        {
            InitializeComponent();
            kisi = new Kisi();
            BindingContext = this;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            kisi.Ad = string.IsNullOrEmpty(_EntryAd.Text) ? _EntryAd.Text : _EntryAd.Text.Trim();
            kisi.Soyad = string.IsNullOrEmpty(_EntrySoyad.Text) ? _EntrySoyad.Text : _EntrySoyad.Text.Trim();
            kisi.TCKimlikNo = string.IsNullOrEmpty(_EntryTCNo.Text) ? 0 : int.Parse(_EntryTCNo.Text.Trim());
            kisi.BabaAd = string.IsNullOrEmpty(_EntryBabaAd.Text) ? _EntryBabaAd.Text : _EntryBabaAd.Text.Trim();
            kisi.AnneAd = string.IsNullOrEmpty(_EntryAnneAd.Text) ? _EntryAnneAd.Text : _EntryAnneAd.Text.Trim();
            kisi.Not = string.IsNullOrEmpty(_EntryNot.Text) ? _EntryNot.Text : _EntryNot.Text.Trim();

            kisi.ImageArray = Arry;

            Task t1 = Task.Run(() =>
            {
                using (UserDialogs.Instance.Loading("İşlem gerçekleştiriliyor...", null, null, true, MaskType.Black))
                {
                    Thread.Sleep(5000);
                }
            });

            Task t2 = Task.Run(async () =>
                {
                    await App.Context.AddKisiAsync(kisi);
                });

            await t2;
            await Navigation.PopModalAsync();
        }

        private async void TakeAPhotoButton_OnClicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                Directory = "MediaPlugin",
                Name = "test.jpg",
                SaveToAlbum = false
            });

            if (file == null)
            {
                return;
            }

            image.Source = ImageSource.FromStream(() =>
            {
                ImageStreaam = file.GetStream();
                ConvertStreamtoByte(ImageStreaam);
                file.Dispose();
                return ImageStreaam;
            });
        }

        public static void ConvertStreamtoByte(Stream input)
        {
            using (var ms = new MemoryStream())
            {
                input.CopyTo(ms);
                Arry = ms.ToArray();
            }
        }

        private async void PickAPhotoButton_OnClicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }
            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            });


            if (file == null)
            {
                return;
            }

            image.Source = ImageSource.FromStream(() =>
            {
                ImageStreaam = file.GetStream();
                ConvertStreamtoByte(ImageStreaam);
                file.Dispose();
                return ImageStreaam;
            });
        }
    }
}