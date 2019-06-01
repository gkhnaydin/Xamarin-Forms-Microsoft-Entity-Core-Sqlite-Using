using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using KisiRehberi;
using KisiRehberi.ViewModels;
using KisiRehberi.Views;
using Xamarin.Forms;

namespace KisiRehberi.ViewModels
{
    public class KisiViewModel : BaseViewModel
    {
        public ObservableCollection<Kisi> KisiObsvList { get; set; }
        public Command LoadItemsCommand { get; set; }

        public KisiViewModel()
        {
            Title = "Veriler";
            KisiObsvList = new ObservableCollection<Kisi>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<YeniKisiPage, Kisi>(this, "AddKisi", async (obj, item) =>
            {
                var _item = item as Kisi;
                KisiObsvList.Add(_item);
                await DataStore.AddKisiAsync(_item);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                KisiObsvList.Clear();
                var items = await DataStore.GetKisisAsync(true);
                foreach (var item in items)
                {
                    KisiObsvList.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
