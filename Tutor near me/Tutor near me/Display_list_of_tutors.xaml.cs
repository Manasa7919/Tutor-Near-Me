using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tutor_near_me.Helper;
using Tutor_near_me.Model;
namespace Tutor_near_me
{
    [DesignTimeVisible(false)]
    public partial class Display_list_of_tutors : ContentPage
{
    
    FirebaseHelper firebaseHelper = new FirebaseHelper();
    public Display_list_of_tutors()
    {
        InitializeComponent();
    }
    protected async override void OnAppearing()
    {

            base.OnAppearing();
            var allPersons = await firebaseHelper.GetAllPersons();
            lstPersons.ItemsSource = allPersons;
            
    }
        private void OnTutionSelected(object sender, ItemTappedEventArgs e)
        {
            var item =(Newtutor)e.Item;
            String vc = item.UserID;
            App.Current.MainPage.Navigation.PushModalAsync(new Displaytutordetails(vc));
            // Your code here
        }

        private async void BtnFilter_Clicked(object sender, EventArgs e)
        {
            String sel = (String)nos.SelectedItem;
            base.OnAppearing();
            var allPersons = await firebaseHelper.GetAllPersonsfiltered(sel);
            lstPersons.ItemsSource = allPersons;

        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            //thats all you need to make a search  
            var allPersons = await firebaseHelper.GetAllPersons();
            if (string.IsNullOrEmpty(e.NewTextValue))
            {

                lstPersons.ItemsSource = allPersons;
            }

            else
            {
                lstPersons.ItemsSource = allPersons.Where(x => x.Name.StartsWith(e.NewTextValue));
            }
        }
    }
}