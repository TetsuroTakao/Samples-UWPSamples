namespace Processtune.Utilities
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    //using System.Resources;
    using System.Threading.Tasks;
    using Windows.ApplicationModel.Core;
    using Windows.ApplicationModel.Resources;
    using Windows.ApplicationModel.Resources.Core;
    using Windows.Globalization;
    using Windows.Graphics.Imaging;
    using Windows.UI.Core;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Media.Imaging;

    public class ResourceUtilities
    {
        private static ResourceLoader resourceLoader = ResourceLoader.GetForCurrentView("Resources");

        #region "プロパティ"

        /// <summary>
        /// テーマカラー
        /// <value>SolidColorBrush(Color.FromArgb(255, 228, 241, 250))</value>
        /// </summary>
        //public Brush ThemaColor
        //{
        //    get
        //    {
        //        return new SolidColorBrush(Color.FromArgb(255, 228, 241, 250));
        //    }
        //}

        #endregion

        public static string GetResourceString(string key)
        {
            return resourceLoader.GetString(key);
        }

        public static string GetCurrentInputMethod()
        {
            return Windows.Globalization.Language.CurrentInputMethodLanguageTag;
        }

        public static Brush GetResourceImageBrush(string path)
        {
            //BitmapImage source = getSource(path, projectType, isConpiledResource);
            Brush result = new ImageBrush();
            //if (result.CanFreze) result.Freeze();
            return result;
        }

        private static BitmapImage getSource(string path, bool isConpiledResource = true)
        {
            BitmapImage source = null;
                //MessageDataModel message = Output.CreateMessageDataModel(UI.GetResourceString("ObtainImageFail", ""));
                //Output.CreateMessageDataModel(UI.GetResourceString("ObtainImageFail", ""));
                //Output.LogoutToXML(message);
                // TODO　DMRootに公開用のモデルを用意する

            //"/Images/NCSetting/66_kyugo.png"
                if (!(path.Split('/').Count() > 0))
                {
                    if (isConpiledResource)
                    {
                        path = "pack://application:,,,/;component/Assets/" + path;
                    }
                    else
                    {
                        path = "/Images/" + path;
                    }
                }
            try
            {
                new Task(() =>
                {
                    source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
                }).Start(TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (ArgumentNullException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            catch (FileNotFoundException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return source;
        }

        public static ImageSource GetResourceImageSource(string path)
        {
            BitmapImage source = new BitmapImage(new Uri(path));
            return source;
        }

        public static void ChangeLanguageResources(string targetLanguage, SettingsFlyout targtControl)
        {
            CultureInfo cul = new CultureInfo(targetLanguage);
            RegionInfo reg = new RegionInfo(targetLanguage);
            if (cul == CultureInfo.InvariantCulture) return;

            var qualifierValues = ResourceContext.GetForCurrentView().Clone().QualifierValues;
            Task t = new Task(() => 
            {
                string text = "";
                if (targtControl != null)
                {
                    text = GetResourceString(targtControl.Name + ".Title");
                    if (string.IsNullOrEmpty(text))
                    {
                        text = GetResourceString(targtControl.Name + "Title");
                    }
                    targtControl.Title = text;
                    setChildrenResource(targtControl);
                }
            });
            qualifierValues.MapChanged += (s,e) =>
            {
                t.RunSynchronously();
            };
            ApplicationLanguages.PrimaryLanguageOverride = targetLanguage;
        }
        private static void LanguageChanged(DependencyObject sender)
        {
            ComboBox langSelector = sender as ComboBox;
            if (langSelector == null) return;
            string trgetLanguage = langSelector.SelectedValue as string;
            if (string.IsNullOrEmpty(trgetLanguage)) return;
            DependencyObject root = sender as DependencyObject;
            while (root != null)
            {
                root = VisualTreeHelper.GetParent(root);
            }
            SettingsFlyout f = root as SettingsFlyout;

            ResourceContext.GetForCurrentView().QualifierValues.MapChanged += async (s, m) => 
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, new DispatchedHandler(() =>
                {
                    string text = "";
                    if (f != null)
                    {
                        text = GetResourceString(f.Name + ".Title");
                        if (string.IsNullOrEmpty(text))
                        {
                            text = GetResourceString(f.Name + "Title");
                        }
                        f.Title = text;
                    }
                    setChildrenResource(root);
                }));
                ApplicationLanguages.PrimaryLanguageOverride = trgetLanguage;
            };
        }
        private static void setChildrenResource(DependencyObject target)
        {
            string key = "";
            string text = "";
            string tagText = "";
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(target); i++)
            {
                DependencyObject d = VisualTreeHelper.GetChild(target, i);
                TextBlock b = d as TextBlock;
                if (b != null)
                {
                    tagText = b.Tag == null ? "" : b.Tag.ToString();
                    if (tagText == "Bind") continue;
                    if (!string.IsNullOrEmpty(b.Name))
                    {
                        if (b.Name != "DropDownGlyph")
                        {
                            b.Text = GetResourceString(b.Name + ".Contents");
                            text = GetResourceString(b.Name + ".Text");
                            if (string.IsNullOrEmpty(text))
                            {
                                text = GetResourceString(b.Name + "Text");
                            }
                            b.Text = text;
                        }
                    }
                }
                TextBox t = d as TextBox;
                if (t != null)
                {
                    tagText = t.Tag == null ? "" : t.Tag.ToString();
                    if (tagText == "Bind") continue;
                    key = "Resources/" + t.Name + "/Text";
                    text = ResourceManager.Current.MainResourceMap.GetValue(key, ResourceContext.GetForCurrentView()).ValueAsString;
                    if (string.IsNullOrEmpty(text))
                    {
                        key = "Resources/" + t.Name;
                        text = ResourceManager.Current.MainResourceMap.GetValue(key, ResourceContext.GetForCurrentView()).ValueAsString;
                    }
                    t.Text = text;
                }
                setChildrenResource(d);
            }
        }
    }
}
