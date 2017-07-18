using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace StatefulMiddleWareUWP
{
    public class MenuContext
    {
        public string Title { get; set; }
        public Content Body { get; set; }
    }
    public class Content
    {
        public Content()
        {
            ContentType = ContentTypes.Profile;
            DistinationType = DistinationTypes.Undefined;
            SyncIds = new List<Tuple<DistinationTypes, string>>();
        }
        public ContentTypes ContentType { get; set; }
        public DistinationTypes DistinationType { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public Image PhotoImage { get; set; }
        public string PhoneNumber { get; set; }
        public string MailAddress { get; set; }
        public string ID { get; set; }
        public List<Tuple<DistinationTypes, string>> SyncIds { get; set; }
    }
    public enum ContentTypes : int
    {
        Profile = 0,
        Other
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
    public class MainVM
    {
        public MainVM()
        {
            Menu = new List<MenuContext>();
            Current = new MenuContext() { Title = "Create Profile", Body = new Content() { } };
            Menu.Add(Current);
            Title = "Profile creation";
            Lead = "Aquiring from SNS or idenity provider";
        }
        public MenuContext Current { get; set; }
        public List<MenuContext> Menu { get; set; }
        public string Title { get; set; }
        public string Lead { get; set; }
        public Type ClassType { get; set; }
    }
}
