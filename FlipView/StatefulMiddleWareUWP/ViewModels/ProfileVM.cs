using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace StatefulMiddleWareUWP
{
    public class Provider
    {
        public BitmapImage Image { get; set; }
        public string Caption { get; set; }
    }
    public class Content
    {
        public Content()
        {
            ContentType = ContentTypes.Profile;
            DistinationType = DistinationTypes.Undefined;
            PhotoImages = new List<Provider>();
            var except = new string[] { "Undefined", "Goo", "Rakten", "Blogger", "Mixi", "Ameba", "Tumbler", "Reddit", "Yahoo", "Pintarest" };
            BitmapImage photoImage = null;
            foreach (string s in Enum.GetNames(typeof(DistinationTypes)))
            {
                if (except.Where(i => i == s).Count() > 0) continue;
                PhotoImagePath = "ms-appx:///Assets/" + s + "Icon.png";
                photoImage = new BitmapImage() { UriSource = new Uri(PhotoImagePath) };
                PhotoImages.Add(new Provider() { Image = photoImage, Caption = s });
                 SourceDistination = s;
            }
        }
        public List<Provider> PhotoImages { get; set; }
        public string SourceDistination { get; set; }
        public ContentTypes ContentType { get; set; }
        public DistinationTypes DistinationType { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string PhotoImagePath { get; set; }
        public string PhoneNumber { get; set; }
        public string MailAddress { get; set; }
        public string ID { get; set; }
        public List<Tuple<DistinationTypes, string>> SyncIds { get; set; }
        //private static async Task<object> FileExist(string folder, string file)
        //{
        //    StorageFolder localfolder = ApplicationData.Current.LocalFolder;
        //    return await localfolder.TryGetItemAsync(folder + Path.PathSeparator + file);
        //}
    }
    public enum DistinationTypes
    {
        Undefined,
        FaceBook,
        Instagram,
        Google,
        MicrosoftGraph,
        MSAccount,
        OWAccount,
        Linkedin,
        Twitter,
        Goo,
        Rakten,
        Blogger,
        Mixi,
        Ameba,
        Tumbler,
        Reddit,
        Yahoo,
        Pintarest
    }
}
