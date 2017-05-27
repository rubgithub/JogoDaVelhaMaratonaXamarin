using JogoDaVelhaMaratona.Helpers;
using JogoDaVelhaMaratona.Interfaces;
using JogoDaVelhaMaratona.UWP.Authentication;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(AuthenticationService))]
namespace JogoDaVelhaMaratona.UWP.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        public async Task<MobileServiceUser> Authenticate(MobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {
            try
            {
                var user = await client.LoginAsync(provider);

                Settings.AuthToken = user?.MobileServiceAuthenticationToken ?? string.Empty;
                Settings.UserId = user?.UserId;

                return user;
            }
            catch (Exception)
            {
                //TODO: LogError
                return null;
            }
        }
    }
}
