using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutor_near_me.Helper;
using Tutor_near_me.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tutor_near_me
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public SignUpPage()
        {
            InitializeComponent();
        }

        private async void SignUp_Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(email.Text) || string.IsNullOrWhiteSpace(Password.Text) || string.IsNullOrWhiteSpace(Name.Text) || string.IsNullOrWhiteSpace(Password1.Text) || string.IsNullOrWhiteSpace(address.Text) || string.IsNullOrWhiteSpace(phone.Text))
                await DisplayAlert("Empty Values", "Please fill all the details", "OK");
            else
            if (Password.Text != Password1.Text)
               await DisplayAlert("Error", "Passwords do not match", "OK");
            else
            {
                await firebaseHelper.AddUser(Name.Text, phone.Text, Password.Text, email.Text, address.Text);
                Name.Text = String.Empty;
                phone.Text = String.Empty;
                Password.Text = String.Empty;
                Password1.Text = String.Empty;
                email.Text = String.Empty;
                address.Text = String.Empty;
                await DisplayAlert("Sign up successful", "", "OK");
                App.Current.MainPage = new View.LoginPage();
            }
        }
    }
}