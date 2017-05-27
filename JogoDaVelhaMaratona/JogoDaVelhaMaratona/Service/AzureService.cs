using JogoDaVelhaMaratona.Helpers;
using JogoDaVelhaMaratona.Interfaces;
using JogoDaVelhaMaratona.Model;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(JogoDaVelhaMaratona.Service.AzureService))]
namespace JogoDaVelhaMaratona.Service
{
    public class AzureService
    {
        List<AppServiceIdentity> identities = null;
        static readonly string AppUrl = "https://rub-maratona-xamarin-intermediario.azurewebsites.net";
        public MobileServiceClient Client { get; set; } = null;

        public void Initialize()
        {
            Client = new MobileServiceClient(AppUrl);

            //if (!string.IsNullOrWhiteSpace(Settings.AuthToken) && !string.IsNullOrWhiteSpace(Settings.UserId))
            //{
            //    Client.CurrentUser = new MobileServiceUser(Settings.UserId)
            //    {
            //        MobileServiceAuthenticationToken = Settings.AuthToken
            //    };
            //}
        }

        public async Task<bool> LoginAsync()
        {
            Initialize();

            var auth = DependencyService.Get<IAuthenticationService>();
            var user = await auth.Authenticate(Client, MobileServiceAuthenticationProvider.Facebook);

            Settings.AuthToken = string.Empty;
            Settings.UserId = string.Empty;

            if (user == null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Não foi possível efetuar login, tente novamente.", "OK");
                });
                return false;
            }
            else
            {
                identities = await Client.InvokeApiAsync<List<AppServiceIdentity>>("/.auth/me");
                var identities2 = await Client.InvokeApiAsync("/.auth/me");
                var name = identities[0].UserClaims.Find(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")).Value; 

                var userToken = identities[0].AccessToken; 

                var requestUrl = $"https://graph.facebook.com/v2.9/me/?fields=picture&access_token={userToken}";

                var httpClient = new HttpClient();

                var userJson = await httpClient.GetStringAsync(requestUrl);

                var facebookProfile = JsonConvert.DeserializeObject<FacebookProfile>(userJson);

                Settings.AuthToken = user.MobileServiceAuthenticationToken;
                Settings.UserId = user.UserId;

                Settings.Player1Name = name;
                Settings.Player1Image = facebookProfile.Picture.Data.Url;

            }
            return true;
        }
    }
}
