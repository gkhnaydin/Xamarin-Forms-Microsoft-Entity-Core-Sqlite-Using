namespace KisiRehberi.ViewModels
{
    public class KisiDetailViewModel : BaseViewModel
    {
        public Kisi Kisi { get; set; }
        public KisiDetailViewModel(Kisi kisi = null)
        {
            Title = kisi?.Ad;
            Kisi = kisi;
        }
    }
}
