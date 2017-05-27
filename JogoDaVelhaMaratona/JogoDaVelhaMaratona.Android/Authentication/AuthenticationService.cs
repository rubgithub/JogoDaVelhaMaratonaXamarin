using JogoDaVelhaMaratona.Droid.Authentication;
using JogoDaVelhaMaratona.Helpers;
using JogoDaVelhaMaratona.Interfaces;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(AuthenticationService))]
namespace JogoDaVelhaMaratona.Droid.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        public async Task<MobileServiceUser> Authenticate(MobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {
            try
            {
                var user = await client.LoginAsync(Forms.Context, provider);

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