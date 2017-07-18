using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;

namespace StatefulMiddleWareUWP
{
    public class Facebook : ProviderBase
    {
        public Facebook()
        {
            var scopeList = new string[] { "public_profile", "user_friends", "email", "user_about_me", "user_actions.books", "user_actions.fitness", "user_actions.music", "user_actions.news", "user_actions.video", "user_actions:{app_namespace}", "user_birthday", "user_education_history", "user_events", "user_games_activity", "user_hometown", "user_likes", "user_location", "user_managed_groups", "user_photos", "user_posts", "user_relationships", "user_relationship_details", "user_religion_politics", "user_tagged_places", "user_videos", "user_website", "user_work_history", "read_custom_friendlists", "read_insights", "read_audience_network_insights", "read_page_mailboxes", "manage_pages", "publish_pages", "publish_actions", "rsvp_event", "pages_show_list", "pages_manage_cta", "pages_manage_instant_articles", "ads_read", "ads_management", "business_management", "pages_messaging", "pages_messaging_subscriptions", "pages_messaging_payments", "pages_messaging_phone_number" };
            Scope = scopeList.ToList<string>();
            appID = "1929285880642177";
            aPIVersion = "v2.9";
            scopeParameter = "&scope=";
            scopeParameter += string.Join(",", Scope.Where(s => s == "public_profile" || s == "email"));
            //if OAuth server not support "", use below as redirect URL
            //RedirectURL = "https://www.facebook.com/connect/login_success.html";
            Uri callBackUri = WebAuthenticationBroker.GetCurrentApplicationCallbackUri();
            RedirectURL = callBackUri.AbsoluteUri;
            OptionalParameters = "&display=popup&response_type=token";
            CurrentProviderTypes = ProviderTypes.FaceBook;
            Public_Profile = new PublicProfile();
            //[Obsolete]

        }
        string aPIVersion { get; set; }
        string appID { get; set; }
        string scopeParameter { get; set; }
        public string OptionalParameters { get; set; }
        public string OAuthRequestURL
        {
            get
            {
                return "https://www.facebook.com/" + aPIVersion + "/dialog/oauth?client_id=" + Uri.EscapeDataString(appID) + "&redirect_uri=" + Uri.EscapeDataString(RedirectURL) + scopeParameter + OptionalParameters;
            }
        }
        public string PublicProfileRequestURL
        {
            get
            {
                return "https://graph.facebook.com/v2.9/me?access_token=" + AccessToken;
            }
        }
        public string UserRequestURL
        {
            get
            {
                return "https://graph.facebook.com/v2.9/" + Public_Profile.id + "?access_token=" + AccessToken;
            }
        }
        public string RedirectURL { get; set; }
        string APIVersionString { get; set; }
        public User FaceBookUserInfo { get; set; }
        public PublicProfile Public_Profile { get; set; }
        public class PublicProfile
        {
            public string id { get; set; }
            public string cover { get; set; }
            public string name { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string age_range { get; set; }
            public string link { get; set; }
            public string gender { get; set; }
            public string locale { get; set; }
            public string picture { get; set; }
            public string timezone { get; set; }
            public string updated_time { get; set; }
            public string verified { get; set; }
        }

        enum install_type
        {
        }    
        public class User
        {
            public string id { get; set; }
            public string about { get; set; }
            public string admin_notes { get; set; }
            public string age_range { get; set; }
            public string birthday { get; set; }
            public string can_review_measurement_request { get; set; }
            public string context { get; set; }
            public string cover { get; set; }
            public string currency { get; set; }
            //List<UserDevice> devices { get; set; }
            //List<EducationExperience> education { get; set; }
            public string email { get; set; }
            public string employee_number { get; set; }
            //List<Experience> favorite_athletes { get; set; }
            //List<Experience> favorite_teams { get; set; }
            public string first_name { get; set; }
            public string gender { get; set; }
            public string hometown { get; set; }
            //List<Experience> inspirational_people { get; set; }
            public bool installed { get; set; }
            public List<string> interested_in { get; set; }
            public bool is_shared_login { get; set; }
            public bool is_verified { get; set; }
            //List<PageLabel> labels { get; set; }
            //List<Experience> languages { get; set; }
            public string last_name { get; set; }
            public string link { get; set; }
            public string locale { get; set; }
            //Page location { get; set; }
            public List<string> meeting_for { get; set; }
            public string middle_name { get; set; }
            public string name { get; set; }
            public string name_format { get; set; }
            //PaymentPricepoints payment_pricepoints { get; set; }
            public string political { get; set; }
            public string public_key { get; set; }
            public string quotes { get; set; }
            public string relationship_status { get; set; }
            public string religion { get; set; }
            //SecuritySettings security_settings   { get; set; }
            public DateTime shared_login_upgrade_required_by    { get; set; }
            public string short_name { get; set; }
            public User significant_other { get; set; }
            //List<Experience> sports { get; set; }
            /// <summary>
            /// unsigned int32
            /// </summary>
            public Int32 test_group { get; set; }
            public string third_party_id { get; set; }
            /// <summary>
            ///  float (min: -24) (max: 24)
            /// </summary>
            float timezone   { get; set; }
            string token_for_business { get; set; }
            public DateTime updated_time    { get; set; }
            /// <summary>
            /// time
            /// </summary>
            public DateTime Updated { get; set; }
            public bool verified    { get; set; }
            //VideoUploadLimits video_upload_limits { get; set; }
            public bool viewer_can_send_gift    { get; set; }
            public string website { get; set; }
            //List<WorkExperience> work { get; set; }

        }
    }
}
