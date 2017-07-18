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
            string result = string.Empty;
            Uri callBackUri = WebAuthenticationBroker.GetCurrentApplicationCallbackUri();
            string redirectURL = callBackUri.AbsoluteUri;
            switch ((SyncDistinationArea.SelectedValue as Provider).Caption)
            {
                case "FaceBook":
                    //if OAuth server not support "", use below as redirect URL
                    //redirectURL = "https://www.facebook.com/connect/login_success.html";
                    oauthURL = "https://www.facebook.com/v2.9/dialog/oauth?client_id=" + Uri.EscapeDataString("1929285880642177") + "&redirect_uri=" + Uri.EscapeDataString(redirectURL);
                    break;
                default:
                    oauthURL = "https://www.facebook.com/v2.9/dialog/oauth?client_id=" + Uri.EscapeDataString("1929285880642177") + "&redirect_uri=" + Uri.EscapeDataString(redirectURL);
                    //oauthURL = "https://www.facebook.com/v2.9/dialog/oauth?client_id=" + Uri.EscapeDataString("1929285880642177") + "&redirect_uri=" + Uri.EscapeDataString(redirectURL) + "&scope=read_stream&display=popup&response_type=token";
                    break;
            }
            try
            {

                System.Uri StartUri = new Uri(oauthURL);
                System.Uri EndUri = new Uri(redirectURL);


                WebAuthenticationResult WebAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, StartUri, EndUri);
                if (WebAuthenticationResult.ResponseStatus == WebAuthenticationStatus.Success)
                {
                    string aquireTakenResult = WebAuthenticationResult.ResponseData.ToString();
                    string responseData = aquireTakenResult.Substring(aquireTakenResult.IndexOf("access_token"));
                    String[] keyValPairs = responseData.Split('&');
                    string access_token = null;
                    string expires_in = null;
                    for (int i = 0; i < keyValPairs.Length; i++)
                    {
                        String[] splits = keyValPairs[i].Split('=');
                        switch (splits[0])
                        {
                            case "access_token":
                                access_token = splits[1];
                                break;
                            case "expires_in":
                                expires_in = splits[1];
                                break;
                        }
                    }
                    HttpClient httpClient = new HttpClient();
                    string response = await httpClient.GetStringAsync(new Uri("https://graph.facebook.com/me?access_token=" + access_token));
                    JsonObject value = JsonValue.Parse(response).GetObject();
                    string facebookUserName = value.GetNamedString("name");
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
    }
}
