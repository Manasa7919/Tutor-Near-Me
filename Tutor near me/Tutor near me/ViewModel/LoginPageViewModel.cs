using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Tutor_near_me.Helper;



namespace Tutor_near_me.ViewModel
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public LoginPageViewModel()
        {

        }
        private string phone;
        public string Phone
        {
            get { return phone; }
            set
            {
                if (value != null)
                {
                    phone = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Phone"));
                }
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                if (value != null)
                {
                    password = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Password"));
                }
            }
        }
        private ICommand _loginButtonCommand;
        public ICommand ViewDetails =>
            _loginButtonCommand ?? (_loginButtonCommand = new Command(async () => await ExcuteLoginButtonCommandAsync()));

        private async System.Threading.Tasks.Task ExcuteLoginButtonCommandAsync()
        {
            if (string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(Password))
                await App.Current.MainPage.DisplayAlert("Empty values", "Please enter Username and password", "Ok");
            else
            {
                var user = await FirebaseHelper.GetUser(phone);

                if (user != null)
                    if (phone == user.Phone && password == user.Password)
                    {
                        await App.Current.MainPage.DisplayAlert("Successful", "Logged in Successfully", "Ok");


                        //Save the property userIsLoggedIn to true

                        await SecureStorage.SetAsync("userIsLoggedIn", "true");
                        await SecureStorage.SetAsync("Phone", phone);

                        App.Current.MainPage= new TutorProfile();

                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("UnSuccessful", "Either username or password is incorrect", "Ok");
                    }
                else
                    await App.Current.MainPage.DisplayAlert("Login Fail", "User not found", "OK");

            }
        }
        private ICommand listtutorsCommand;
        public ICommand ListTutorsPage =>
            listtutorsCommand ?? (listtutorsCommand = new Command(() => ExcuteListCommand()));

        private void ExcuteListCommand()
        {
            App.Current.MainPage = new Display_list_of_tutors();
        }
        private ICommand signupCommand;
        public ICommand SignupCommand =>
            signupCommand ?? (signupCommand = new Command(() => ExcuteSignUpCommand()));

        private void ExcuteSignUpCommand()
        {
            App.Current.MainPage = new SignUpPage();
        }
    }

}
