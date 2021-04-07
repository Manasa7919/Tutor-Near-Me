using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tutor_near_me.View;
using Xamarin.Essentials;
namespace Tutor_near_me
{
    public partial class App : Application
    {
        public App()
        {
           
            InitializeComponent();
            NavigateToMainPage();
            
        }
        private async void NavigateToMainPage()
        {
            string isLoggedIn = await SecureStorage.GetAsync("userIsLoggedIn");
           
            
            //When the key is not set  when the app runs first time  value will be null
            if (isLoggedIn == null || isLoggedIn == "false")
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {   
                MainPage = new NavigationPage(new TutorProfile());
                
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
