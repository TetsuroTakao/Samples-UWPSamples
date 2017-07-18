using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace StatefulMiddleWareUWP
{
    public class MenuContext
    {
        public string Title { get; set; }
        public Type Body { get; set; }
    }
    public enum ContentTypes : int
    {
        Profile = 0,
        Other
    }
    public class MainVM
    {
        public MainVM()
        {
            Menu = new List<MenuContext>();
            Current = new MenuContext() { Title = "Create Profile", Body = typeof(Profile) };
            Menu.Add(Current);
            Title = "Profile creation";
            Lead = "Aquiring your profile information from SNS or idenity provider";
        }
        public MenuContext Current { get; set; }
        public List<MenuContext> Menu { get; set; }
        public string Title { get; set; }
        public string Lead { get; set; }
        public Type ClassType { get; set; }
    }
}
