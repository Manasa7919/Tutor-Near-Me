using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tutor_near_me.Helper;
using Tutor_near_me.Model;
using System.Diagnostics;
using Plugin.Media.Abstractions;
using Plugin.Media;
using Firebase.Database.Query;
using Firebase.Database;
using Xamarin.Essentials;
namespace Tutor_near_me

{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Add_tutor : ContentPage
{
        String loginid;
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        FirebaseStorageHelper firebaseStorageHelper = new FirebaseStorageHelper();
        MediaFile file;

        public Add_tutor()
    {
        InitializeComponent();
            NavigateToContent();
            BtnDownload_Clicked(loginid);
            BtnRetrive_Clicked(loginid);
        }
        private async void NavigateToContent()
        {
            loginid = await SecureStorage.GetAsync("Phone");
           
        }
        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            String sel = (String)picker.SelectedItem;
            if (String.IsNullOrWhiteSpace(Tutname.Text) || String.IsNullOrWhiteSpace(Tutadd.Text) || String.IsNullOrWhiteSpace(Tutphn.Text) || String.IsNullOrWhiteSpace(Tutsea.Text) || String.IsNullOrWhiteSpace(TutSub.Text) || String.IsNullOrWhiteSpace(TutStaff.Text) || String.IsNullOrWhiteSpace(Nos.Text) || String.IsNullOrWhiteSpace(sel))
            { await DisplayAlert("Alert", "Please enter all the fields", "Ok"); 
            }

            else
            {
                await firebaseHelper.AddPerson(loginid, Tutname.Text, Tutadd.Text, Tutphn.Text, Tutsea.Text, TutSub.Text, TutStaff.Text, sel, Nos.Text);
                Tutname.Text = string.Empty;
                Tutadd.Text = string.Empty;
                Tutphn.Text = string.Empty;
                Tutsea.Text = string.Empty;
                TutSub.Text = string.Empty;
                Nos.Text = String.Empty;
                TutStaff.Text = string.Empty;
                picker.SelectedItem = string.Empty;
                await DisplayAlert("Success", "Updated Successfully", "OK");
            }
        }

        protected override void OnAppearing()
        {

            base.OnAppearing();

        }

        private async void BtnPick_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium
                });
                if (file == null)
                    return;
                imgChoosed.Source = ImageSource.FromStream(() =>
                {
                    var imageStram = file.GetStream();
                    return imageStram;
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void BtnUpload_Clicked(object sender, EventArgs e)
        {
            await firebaseStorageHelper.UploadFile(file.GetStream(), loginid);
        }

        private async void BtnDownload_Clicked(string uid)
        {
            String path;
            try
            {
                String userid = uid;
                path = await firebaseStorageHelper.GetFile(userid + ".jpg");
                if (path != null)
                {
                    path = path.Replace("&", "&amp;");
                }
            }
            catch
            {
                path = "https://firebasestorage.googleapis.com/v0/b/tutor-near-me.appspot.com/o/Ts.jpg?alt=media&amp;token=31db6b03-0abf-4c3c-8b0c-8fda52e74685";
            }
            imgChoosed.Source = path;
        }
        private async void BtnRetrive_Clicked(string vc)
        {
            var person = await firebaseHelper.GetPerson(vc);
            //await DisplayAlert("Hai", person.UserID.ToString(), "Ok");
            if (person != null)
            {

                Tutname.Text = person.Name;
                Tutadd.Text = person.Location;
                picker.SelectedItem = person.City;
                Tutphn.Text = person.Phoneno;
                Tutsea.Text = person.Seats;
                TutSub.Text = person.Subjects;
                TutStaff.Text = person.Staff;
                Nos.Text = person.Nos;

            }
            else
            {
                Tutname.Text = "";
                Tutadd.Text = "";
                picker.SelectedItem = "";
                Tutphn.Text = "";
                Tutsea.Text = "";
                TutSub.Text = "";
                TutStaff.Text = "";
                Nos.Text = "";
            }
            
        }
        public void BtnPreview_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Displaytutordetails(loginid));
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(Tutname.Text))
            {
                await DisplayAlert("Alert", "No Data Exists to Perform Delete", "Ok");
            }

            else
            {
                await firebaseHelper.DeletePerson(loginid);
                await firebaseStorageHelper.DeleteFile(loginid);
                Tutname.Text = string.Empty;
                Tutadd.Text = string.Empty;
                Tutphn.Text = string.Empty;
                Tutsea.Text = string.Empty;
                TutSub.Text = string.Empty;
                Nos.Text = string.Empty;
                TutStaff.Text = string.Empty;
                picker.SelectedItem = string.Empty;
                imgChoosed.Source = "https://firebasestorage.googleapis.com/v0/b/tutor-near-me.appspot.com/o/Ts.jpg?alt=media&amp;token=31db6b03-0abf-4c3c-8b0c-8fda52e74685";
                await DisplayAlert("Success", "Deleted Successfully", "OK");
            }
        }

        public void Deleteenable(object sender, EventArgs e)
        {
            if (enable.IsChecked==true)
                deletetut.IsEnabled = true;
            else
                deletetut.IsEnabled = false;
        }
    }
}