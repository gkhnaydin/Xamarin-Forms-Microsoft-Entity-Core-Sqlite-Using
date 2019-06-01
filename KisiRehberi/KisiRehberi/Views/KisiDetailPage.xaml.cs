using KisiRehberi.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KisiRehberi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KisiDetailPage : ContentPage
    {
        KisiDetailViewModel viewModel;

        public KisiDetailPage(KisiDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }      

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            var kisi = viewModel.Kisi;
            if (kisi == null)
                return;
            await App.Context.DeleteKisiAsync(kisi.Id);
            await Navigation.PopAsync();
        }
    }
}