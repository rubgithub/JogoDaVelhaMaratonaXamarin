using JogoDaVelhaMaratona.ConnectionsString;
using JogoDaVelhaMaratona.Interfaces;
using JogoDaVelhaMaratona.UWP.Notification;
using Microsoft.WindowsAzure.Messaging;
using System;
using System.Threading.Tasks;
using Windows.Networking.PushNotifications;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(NotificationService))]
namespace JogoDaVelhaMaratona.UWP.Notification
{
    public class NotificationService : INotificationService
    {
        public async Task RegisterNotificationAsync()
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
                SendStatusGame("Falha ao registrar push");
            else
                SendStatusGame($"PUSH OK, TAG: {playerTag[0]}");
        }

        private void SendStatusGame(string message)
        {
            MessagingCenter.Send<object, string>(this, "GameStatus", message);
        }

        private void Channel_PushNotificationReceived(PushNotificationChannel sender, PushNotificationReceivedEventArgs args)
        {
            /*
             
                var playerMoveMessage = intent.Extras.GetString("args");
                var playerNameMessage = intent.Extras.GetString("message");
                CreateNotification(playerNameMessage, playerMoveMessage);

                var playerMove = playerMoveMessage.Substring(playerMoveMessage.Length - 3);
                var playerName = playerNameMessage.Replace("Player: ", string.Empty);

                 MessagingCenter.Send<object, string[]>(this, "GamePlayerMove", new[] {playerMove, playerName});
             */

            var playerNameMessage = args.ToastNotification.Content.SelectSingleNode("/toast/visual/binding").SelectNodes("text")[0].InnerText;
            var playerMoveMessage = args.ToastNotification.Content.SelectSingleNode("/toast/visual/binding").SelectNodes("text")[1].InnerText;

            var playerMove = playerMoveMessage.Substring(playerMoveMessage.Length - 3);
            var playerName = playerNameMessage.Replace("Player: ", string.Empty);

            MessagingCenter.Send<object, string[]>(this, "GamePlayerMove", new[] { playerMove, playerName });

            //var playerMove = args.ToastNotification.Content.SelectSingleNode("/toast/visual/binding").SelectNodes("text")[1].InnerText;
            //MessagingCenter.Send<object, string>(this, "GamePlayerMove", playerMove.Substring(playerMove.Length - 3));
        }
    }
}
