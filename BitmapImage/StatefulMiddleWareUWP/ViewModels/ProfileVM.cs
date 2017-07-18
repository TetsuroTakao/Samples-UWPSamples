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
    public class Content
    {
        public Content()
        {
            ContentType = ContentTypes.Profile;
            DistinationType = DistinationTypes.Undefined;
            SyncIds = new List<Tuple<DistinationTypes, string>>();
            var except = new string[] { "Undefined", "Goo", "Rakten", "Blogger", "Mixi", "Ameba", "Tumbler", "Reddit", "Yahoo", "Pintarest" };
            foreach (string s in Enum.GetNames(typeof(DistinationTypes)))
            {
                if (except.Where(i => i == s).Count() > 0) continue;
                DistinationType = (DistinationTypes)Enum.Parse(typeof(DistinationTypes), s);
                SyncIds.Add(new Tuple<DistinationTypes, string>(DistinationType, s));
                 SourceDistination = s;
                PhotoImagePath = "ms-appx:///Assets/" + s + "Icon.png";
            }
            PhotoImage = new BitmapImage() { UriSource = new Uri(PhotoImagePath) };
        }
        public BitmapImage PhotoImage { get; set; }
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
        Google,
        MicrosoftGraph,
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
