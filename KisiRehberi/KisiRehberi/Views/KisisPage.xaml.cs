using Acr.UserDialogs;
using KisiRehberi;
using KisiRehberi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KisiRehberi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KisisPage : ContentPage
    {
        KisiViewModel viewModel;
        public KisisPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new KisiViewModel();
        }
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var kisi = args.SelectedItem as Kisi;
            if (kisi == null)
                return;

            await Navigation.PushAsync(new KisiDetailPage(new KisiDetailViewModel(kisi)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new YeniKisiPage()));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Loading("Veriler yükleniyor...");
        }

        private async Task Loading(string message, int time = 5000)
        {
            using (UserDialogs.Instance.Loading(message, null, null, true, MaskType.Black))
            {
                await Task.Delay(TimeSpan.FromMilliseconds(time));

                if (viewModel.KisiObsvList.Count == 0)
                    viewModel.LoadItemsCommand.Execute(null);
                else
                    viewModel.LoadItemsCommand.Execute(null);
            }
        }
    }
}