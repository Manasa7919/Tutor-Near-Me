using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutor_near_me.View;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tutor_near_me.Helper;
namespace Tutor_near_me
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class TutorProfile : ContentPage
{
        

        String userid;
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public TutorProfile()
    {

            InitializeComponent();
            NavigateToContent();
            ProfileRetrive(userid);
        }
        private async void NavigateToContent()
        {
             userid = await SecureStorage.GetAsync("Phone");
            
        }
        private async void ProfileRetrive(string vc)
        {
            var person = await firebaseHelper.GetTutorProfile(vc);
            //await DisplayAlert("Hai", person.UserID.ToString(), "Ok");
            try
            {
                if (person != null)
                {
                    NameTutor.Text = person.Name;
                    PhoneTutor.Text = person.Phone;
                    EmailTutor.Text = person.Email;
                    AddressTutor.Text = person.Address;

                }
            }
            catch
            {
                await DisplayAlert("Alert", "Please check your internet connectivity", "OK");
            }

        }

        public void BtnViewTC_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushModalAsync(new Display_list_of_tutors());
        } 
        public void BtnAddorUpdate_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushModalAsync(new Add_tutor());
        }

        private void LogoutClicked(object sender, EventArgs e)
        {
            SecureStorage.SetAsync("userIsLoggedIn", "false");
            App.Current.MainPage = new LoginPage();

        }
    }
}