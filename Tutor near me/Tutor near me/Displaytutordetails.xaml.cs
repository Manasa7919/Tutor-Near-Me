using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tutor_near_me.Helper;
using Tutor_near_me.Model;
using System.ComponentModel;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Diagnostics;
using System.IO;
namespace Tutor_near_me
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Displaytutordetails : ContentPage
{
        FirebaseStorageHelper firebaseStorageHelper = new FirebaseStorageHelper();
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public Displaytutordetails(string vc)
    {
        
        InitializeComponent();
            BtnDownload_Clicked(vc);
            BtnRetrive_Clicked(vc);
       
    }
        private async void BtnRetrive_Clicked(string vc)
        {
            var person = await firebaseHelper.GetPerson(vc);
            //await DisplayAlert("Hai", person.UserID.ToString(), "Ok");
            if (person != null)
            {
                
                NameT.Text = person.Name;
                LocationT.Text = person.Location;
                CityT.Text = person.City;
                PhnT.Text =  person.Phoneno+"\n"+ person.UserID.ToString();
                SeatsT.Text = person.Seats;
                SubT.Text = person.Subjects;
                StaffT.Text = person.Staff;
                NosT.Text = person.Nos;

            }
            else
            {
                await DisplayAlert("Success", "No Person Available", "OK");
            }

        }
        private async void BtnDownload_Clicked(string uid)
        {
            string path;
            try
            {
                String userid = uid.ToString();
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
            DisplayImageT.Source = path;
        }

    }
}