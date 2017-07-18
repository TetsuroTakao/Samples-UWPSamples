using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace StatefulMiddleWareUWP
{
    public class ProviderBase
    {
        public ProviderBase()
        {
            CurrentProvider = this;
        }
        public enum ProviderTypes
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
        public ProviderTypes CurrentProviderTypes { get; set; }
        public BitmapImage Image { get; set; }
        public string Caption { get; set; }
        public ProviderBase CurrentProvider { get; set; }

        public string AccessToken { get; set; }
        public List<string> Scope { get; set; }
        public void SetImage(string s)
        {
            string photoImagePath = "ms-appx:///Assets/" + s + "Icon.png";
            Image = new BitmapImage() { UriSource = new Uri(photoImagePath) };
        }
    }
}
