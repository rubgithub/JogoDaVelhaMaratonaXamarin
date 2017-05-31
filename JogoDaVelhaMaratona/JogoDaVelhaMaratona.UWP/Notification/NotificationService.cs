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

            //listen and send
            var playerTag = new string[] { "user_player1" };
            var hub = new NotificationHub(AppConections.NotificationHubPath, AppConections.ConnectionStringPush);

            //var result = await hub.RegisterNativeAsync(channel.Uri, playerTag);

            var register = await RegisterPush();
            async Task<string> RegisterPush()
            {
                var result = await hub.RegisterNativeAsync(channel.Uri, playerTag);
                register = result.RegistrationId;
                return register;
            }

            if (register == null)
                return "Falha ao registrar push";
            else
                return $"Push registrado! Tag: {playerTag[0]}";
        }

        private void Channel_PushNotificationReceived(PushNotificationChannel sender, PushNotificationReceivedEventArgs args)
        {
            if (args.NotificationType == PushNotificationType.Toast)
            {
                System.Diagnostics.Debug.WriteLine("");
            }
            else
            {
                var raw = args.RawNotification;
                Xamarin.Forms.MessagingCenter.Send<object, string>(this, "GamePlayerMove", raw.Content);
            }
        }
    }
}
