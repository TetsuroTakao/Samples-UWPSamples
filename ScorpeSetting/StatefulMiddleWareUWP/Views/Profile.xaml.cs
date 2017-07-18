using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace StatefulMiddleWareUWP
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class Profile : Page
    {
        public Content ViewModel { get; set; }
        public Profile()
        {
            this.InitializeComponent();
            ViewModel = new Content();
        }
        private async void AuthLauncher_Click(object sender, RoutedEventArgs e)
        {
            if (SyncDistinationArea.SelectedValue == null) return;
            string oauthURL = string.Empty;
            string redirectURL = string.Empty;
            string result = string.Empty;
            string scope = string.Empty;
            ProviderBase provider = (SyncDistinationArea.SelectedValue as ProviderBase).CurrentProvider;
            ViewModel.CurrentProvider = provider;
            #region Set provider user selected
            switch ((SyncDistinationArea.SelectedValue as ProviderBase).CurrentProviderTypes)
            {
                case ProviderBase.ProviderTypes.FaceBook:
                    oauthURL = (provider as Facebook).OAuthRequestURL;
                    redirectURL = (provider as Facebook).RedirectURL;
                    break;
                default:
                    oauthURL = (provider as Facebook).OAuthRequestURL;
                    redirectURL = (provider as Facebook).RedirectURL;
                    break;
            }
            #endregion
            try
            {
                System.Uri StartUri = new Uri(oauthURL);
                System.Uri EndUri = new Uri(redirectURL);
                WebAuthenticationResult WebAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, StartUri, EndUri);
                if (WebAuthenticationResult.ResponseStatus == WebAuthenticationStatus.Success)
                {
                    #region Get access token from request result
                    string aquireTakenResult = WebAuthenticationResult.ResponseData.ToString();
                    string responseData = aquireTakenResult.Substring(aquireTakenResult.IndexOf("access_token"));
                    String[] keyValPairs = responseData.Split('&');
                    provider.AccessToken = null;
                    string expires_in = null;
                    for (int i = 0; i < keyValPairs.Length; i++)
                    {
                        String[] splits = keyValPairs[i].Split('=');
                        switch (splits[0])
                        {
                            case "access_token":
                                provider.AccessToken = splits[1];
                                break;
                            case "expires_in":
                                expires_in = splits[1];
                                break;
                        }
                    }
                    #endregion
                    SetDisplayElements(provider);
                    NotifyUser(ViewModel.FirstName);
                }
                else if (WebAuthenticationResult.ResponseStatus == WebAuthenticationStatus.ErrorHttp)
                {
                    result = "HTTP Error returned by AuthenticateAsync() : " + WebAuthenticationResult.ResponseErrorDetail.ToString();
                }
                else
                {
                    result = "Error returned by AuthenticateAsync() : " + WebAuthenticationResult.ResponseStatus.ToString();
                }
            }
            catch (Exception Error)
            {
                result = "ErrorMessage" + Error.Message;
            }
        }
        public void NotifyUser(string strMessage)
        {
            if (Dispatcher.HasThreadAccess)
            {
                FirstName.Text = strMessage;
            }
            else
            {
                var task = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                    FirstName.Text = strMessage;
                });
            }
        }

        private async void SetDisplayElements(ProviderBase provider)
        {
            HttpClient httpClient = new HttpClient();
            switch (provider.CurrentProviderTypes)
            {
                case ProviderBase.ProviderTypes.FaceBook:
                    string response = await httpClient.GetStringAsync(new Uri((provider as Facebook).PublicProfileRequestURL));
                    JsonObject value = JsonValue.Parse(response).GetObject();
                    if (value.Keys.Where(k => k == "age_range").Count() > 0)
                    {
                        (provider as Facebook).Public_Profile.age_range = value.GetNamedString("age_range");
                    }
                    if (value.Keys.Where(k => k == "cover").Count() > 0)
                    {
                        (provider as Facebook).Public_Profile.cover = value.GetNamedString("cover");
                    }
                    if (value.Keys.Where(k => k == "first_name").Count() > 0)
                    {
                        (provider as Facebook).Public_Profile.first_name = value.GetNamedString("first_name");
                        ViewModel.FirstName = value.GetNamedString("first_name");
                    }
                    if (value.Keys.Where(k => k == "gender").Count() > 0)
                    {
                        (provider as Facebook).Public_Profile.gender = value.GetNamedString("gender");
                    }
                    if (value.Keys.Where(k => k == "id").Count() > 0)
                    {
                        (provider as Facebook).Public_Profile.id = value.GetNamedString("id");
                        ViewModel.ID = value.GetNamedString("id");
                    }
                    if (value.Keys.Where(k => k == "last_name").Count() > 0)
                    {
                        (provider as Facebook).Public_Profile.last_name = value.GetNamedString("last_name");
                        ViewModel.LastName = value.GetNamedString("last_name");
                    }
                    if (value.Keys.Where(k => k == "link").Count() > 0)
                    {
                        (provider as Facebook).Public_Profile.link = value.GetNamedString("link");
                    }
                    if (value.Keys.Where(k => k == "locale").Count() > 0)
                    {
                        (provider as Facebook).Public_Profile.locale = value.GetNamedString("locale");
                    }
                    if (value.Keys.Where(k => k == "name").Count() > 0)
                    {
                        (provider as Facebook).Public_Profile.name = value.GetNamedString("name");
                        if (string.IsNullOrEmpty((provider as Facebook).Public_Profile.first_name))
                        {
                            if (value.GetNamedString("name").Split(' ').Count() == 1)
                            {
                                (provider as Facebook).Public_Profile.first_name = value.GetNamedString("name");
                                ViewModel.FirstName = value.GetNamedString("name");
                            }
                            if (value.GetNamedString("name").Split(' ').Count() == 2)
                            {
                                (provider as Facebook).Public_Profile.first_name = value.GetNamedString("name").Split(' ')[0];
                                ViewModel.FirstName = value.GetNamedString("name").Split(' ')[0];
                                if (string.IsNullOrEmpty((provider as Facebook).Public_Profile.last_name))
                                {
                                    (provider as Facebook).Public_Profile.last_name = value.GetNamedString("name").Split(' ')[1];
                                    ViewModel.LastName = value.GetNamedString("name").Split(' ')[1];
                                }
                            }
                        }
                    }
                    if (value.Keys.Where(k => k == "picture").Count() > 0)
                    {
                        (provider as Facebook).Public_Profile.picture = value.GetNamedString("picture");
                    }
                    if (value.Keys.Where(k => k == "timezone").Count() > 0)
                    {
                        (provider as Facebook).Public_Profile.timezone = value.GetNamedString("timezone");
                    }
                    if (value.Keys.Where(k => k == "updated_time").Count() > 0)
                    {
                        (provider as Facebook).Public_Profile.updated_time = value.GetNamedString("updated_time");
                    }
                    if (value.Keys.Where(k => k == "verified").Count() > 0)
                    {
                        (provider as Facebook).Public_Profile.verified = value.GetNamedString("verified");
                    }

                    response = await httpClient.GetStringAsync(new Uri((provider as Facebook).UserRequestURL));
                    value = JsonValue.Parse(response).GetObject();
                    foreach (string k in value.Keys)
                    {
                        if (k == "email")
                        {
                            (provider as Facebook).FaceBookUserInfo.email = value.GetNamedString("email");
                            ViewModel.MailAddress = value.GetNamedString("email");
                        }
                        if (k == "middle_name")
                        {
                            (provider as Facebook).FaceBookUserInfo.middle_name = value.GetNamedString("middle_name");
                            ViewModel.MiddleName = value.GetNamedString("middle_name");
                        }
                    }
                    ViewModel.PhoneNumber = "";
                    break;
                default:
                    break;
            }
        }
    }
}
