using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace StatefulMiddleWareUWP
{
    public class MenuItemContext
    {
        public string PageTitle { get; set; }
        public string Lead { get; set; }
        public ContentTypes CurrentType { get; set; }
    }
    public class ProfileContent: MenuItemContext
    {
        public ProfileContent()
        {
            CurrentType = ContentTypes.Profile;
            DistinationType = DistinationTypes.Undefined;
            SyncIds = new List<Tuple<DistinationTypes, string>>();
            Lead = "Aquiring from SNS or idenity provider";
            PageTitle = "Create Profile";
        }
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
            Menu = new List<MenuItemContext>();
            Current = new ProfileContent();
            Menu.Add(Current);
            AppTitle = "My App";
        }
        public MenuItemContext Current { get; set; }
        public List<MenuItemContext> Menu { get; set; }
        public string AppTitle { get; set; }
    }
}
