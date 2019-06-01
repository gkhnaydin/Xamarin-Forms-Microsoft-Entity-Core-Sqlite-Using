using System;
using System.Diagnostics;
using KisiRehberi.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace KisiRehberi
{
    public partial class App : Application
    {
        public static Context Context;

        public App(string path)
        {
            InitializeComponent();
            Debug.WriteLine($"Database located at: {path}");
            Context = new Context(path);
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
