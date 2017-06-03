using JogoDaVelhaMaratona.ConnectionsString;
using JogoDaVelhaMaratona.Interfaces;
using JogoDaVelhaMaratona.UWP.Notification;
using Microsoft.WindowsAzure.Messaging;
using System;
using System.Threading.Tasks;
using Windows.Networking.PushNotifications;

[assembly: Xamarin.Forms.Dependency(typeof(NotificationService))]
namespace JogoDaVelhaMaratona.UWP.Notification
{
    public class NotificationService : INotificationService
    {
        public async Task<string> RegisterNotificationAsync()
        {
            //var hub = new NotificationHub("<<YOUR LISTEN CONNECTION STRING>>", "<<YOUR AZURE NOTIFICATION HUB NAME>>");
            var channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
            channel.PushNotificationReceived += Channel_PushNotificationReceived;

            var playerTag = new string[] { "user_player1" };
            var hub = new NotificationHub(AppConnections.NotificationHubPath, AppConnections.PushUrlFullPath);

            TemplateRegistration reg;

            try
            {
                reg = new TemplateRegistration(
                channel.Uri,
                "<toast>" +
                    "<visual>" +
                        "<binding template=\"ToastGeneric\">" +
                            "<text id=\"1\">{'Player: ' + $(player_name)}</text>" +
                            "<text id=\"2\">{'Jogada: ' + $(player_move)}</text>" +
                        "</binding>" +
                    "</visual>" +
                "</toast>"
                ,
                "GameData",
                playerTag
                );
                //new Dictionary<string, string> { { "X-WNS-Type", "wns/raw" } });
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e.Message);
                throw;
            }
            

            var register = await RegisterPush();

            async Task<string> RegisterPush()
            {
                //var result = await hub.RegisterNativeAsync(channel.Uri, playerTag); sem template
                try
                {
                    var result = await hub.RegisterAsync(reg);
                    register = result.RegistrationId;
                    return register;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.Write(e.Message);
                    throw;
                }

            }

            if (register == null)
                return "Falha ao registrar push";
            else
                return $"Push registrado! Tag: {playerTag[0]}";
        }

        private void Channel_PushNotificationReceived(PushNotificationChannel sender, PushNotificationReceivedEventArgs args)
        {
            var playerMove = args.ToastNotification.Content.SelectSingleNode("/toast/visual/binding").SelectNodes("text")[1].InnerText;
            Xamarin.Forms.MessagingCenter.Send<object, string>(this, "GamePlayerMove", playerMove.Substring(playerMove.Length - 3));
        }
    }
}
