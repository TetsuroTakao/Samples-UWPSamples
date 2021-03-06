﻿using Windows.UI.Xaml.Controls;

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
        }
        private void MenuList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox menuListBox = sender as ListBox;

        }
    }
}
