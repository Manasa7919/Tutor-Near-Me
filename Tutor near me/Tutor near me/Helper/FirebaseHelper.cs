using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;
using Firebase.Database.Query;
using Tutor_near_me.Model;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;
namespace Tutor_near_me.Helper
{
    class FirebaseHelper
{
        FirebaseClient firebase = new FirebaseClient("https://tutor-near-me.firebaseio.com/");
        public static FirebaseClient firebaselogin = new FirebaseClient("https://tutor-near-me.firebaseio.com/");
        public async Task<List<Newtutor>> GetAllPersons()
        {

            return (await firebase
              .Child("NewTutors")
              .OnceAsync<Newtutor>()).Select(item => new Newtutor
              {
                  Name = item.Object.Name,
                  Location = item.Object.Location,
                  UserID=item.Object.UserID,
                  City=item.Object.City,
                  Phoneno=item.Object.Phoneno,
                  Seats=item.Object.Seats,
                  Subjects=item.Object.Subjects,
                  Staff=item.Object.Staff,
                  Nos=item.Object.Nos
              }).ToList();
        }
        public async Task<List<Newtutor>> GetAllPersonsfiltered(String filval)
        {

            return (await firebase
              .Child("NewTutors")
              .OnceAsync<Newtutor>()).Select(items => new Newtutor
              {
                  Name = items.Object.Name,
                  Location = items.Object.Location,
                  UserID = items.Object.UserID,
                  City = items.Object.City,
                  Phoneno = items.Object.Phoneno,
                  Seats = items.Object.Seats,
                  Subjects = items.Object.Subjects,
                  Staff = items.Object.Staff,
                  Nos = items.Object.Nos
              }).Where(a => a.City == filval).ToList();
        }
        public async Task<List<Users>> GetAllTutors()
        {

            return (await firebase
              .Child("Users")
              .OnceAsync<Users>()).Select(item => new Users
              {
                  Name = item.Object.Name,
                  Phone = item.Object.Phone,
                  Email = item.Object.Email,
                  Address = item.Object.Address
              }).ToList();
        }
        public async Task AddPerson(String uid, String name, String loc, String phone, String seats, String subjects, String staff, String city, String nos)
        {

            await firebase
              .Child("NewTutors/"+uid)
              .PutAsync(new Newtutor() { UserID = uid, Name = name, Location = loc, Phoneno=phone, Seats=seats, Subjects=subjects, Staff=staff, City=city, Nos=nos});
        }

        public async Task<Newtutor> GetPerson(string UserID)
        {
            var allPersons = await GetAllPersons();
            await firebase
              .Child("NewTutors")
              .OnceAsync<Newtutor>();
            return allPersons.Where(a => a.UserID == UserID).FirstOrDefault();
        }



        public async Task<Users> GetTutorProfile(string UserID)
        {
            var allPersons = await GetAllTutors();
            await firebase
              .Child("Users")
              .OnceAsync<Users>();
            return allPersons.Where(a => a.Phone == UserID).FirstOrDefault();
        }

        public async Task AddUser(string name, string phone, string password, string mail, string address)
        {
            {
                try
                {

                    await firebase
                    .Child("Users/" + phone)
                    .PutAsync(new Users() { Name = name, Phone = phone, Password = password, Email = mail, Address = address });
                   
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Error:{e}");
                    }
            }


        }

        public static async Task<List<Users>> GetAllUser()
        {
            try
            {
                var userlist = (await firebaselogin
                .Child("Users")
                
                .OnceAsync<Users>()).Select(item =>
                new Users
                {
                    Phone = item.Object.Phone,
                    Password = item.Object.Password
                }).ToList();
                return userlist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }
        //Read     
        public static async Task<Users> GetUser(string phone)
        {
            try
            {
                var allUsers = await GetAllUser();
                await firebaselogin
                .Child("Users")
               
                .OnceAsync<Users>();
                return allUsers.Where(a => a.Phone == phone).FirstOrDefault();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }
        public async Task DeletePerson(String Deleteid)
        {
            var toDeletePerson = (await firebase
              .Child("NewTutors")
              .OnceAsync<Newtutor>()).Where(a => a.Object.UserID == Deleteid).FirstOrDefault();
            await firebase.Child("NewTutors").Child(toDeletePerson.Key).DeleteAsync();

        }
        
    }
}
