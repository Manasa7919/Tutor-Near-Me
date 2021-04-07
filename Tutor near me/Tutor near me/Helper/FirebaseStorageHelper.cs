using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Storage;
using System.Threading.Tasks;
using System.IO;

namespace Tutor_near_me.Helper
{
    public class FirebaseStorageHelper
{
        FirebaseStorage firebaseStorage = new FirebaseStorage("tutor-near-me.appspot.com");
        
        public async Task<string> UploadFile(Stream fileStream, String fileName)
        {
            var imageUrl = await firebaseStorage
                .Child(fileName+".jpg")
                .PutAsync(fileStream);
            return imageUrl;
        }

        internal Task UploadFile(object p, string v)
        {
            throw new NotImplementedException();
        }
        public async Task<string> GetFile(string fileName)
        {
            return await firebaseStorage
                .Child(fileName)
                .GetDownloadUrlAsync();
        }

        public async Task DeleteFile(string fileNamedel)
        {
            try
            {
                await firebaseStorage
                     .Child(fileNamedel + ".jpg")
                     .DeleteAsync();
            }
            catch
            {
                
            }
        }
    }
}
