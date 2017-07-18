using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace StatefulMiddleWareUWP
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainVM ViewModel { get; set; }
        public MainPage()
        {
            this.InitializeComponent();
            ViewModel = new MainVM();
            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (MenuList.Items.Count > 0) MenuList.SelectedIndex = 0;
        }
        private void MenuList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox menuListBox = sender as ListBox;
            MenuContext s = menuListBox.SelectedItem as MenuContext;
            if (s != null)
            {
                ContentsFrame.Navigate(s.Body);
                //if (Window.Current.Bounds.Width < 640)
                //{
                //    Splitter.IsPaneOpen = false;
                //}
            }
        }
        async void Footer_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri(((HyperlinkButton)sender).Tag.ToString()));
        }
        private void Hamburger_Click(object sender, RoutedEventArgs e)
        {
            Splitter.IsPaneOpen = (Splitter.IsPaneOpen == true) ? false : true;
            //StatusBorder.Visibility = Visibility.Collapsed;
        }
    }
}
