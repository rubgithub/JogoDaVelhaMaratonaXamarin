using Android.App;
using Android.Content;
using Android.Media;
using Android.Support.V4.App;
using Gcm.Client;
using JogoDaVelhaMaratona.ConnectionsString;
using JogoDaVelhaMaratona.Droid.Notification;
using JogoDaVelhaMaratona.Interfaces;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using WindowsAzure.Messaging;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(NotificationService))]
namespace JogoDaVelhaMaratona.Droid.Notification
{
    [BroadcastReceiver(Permission = Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new[] { Intent.ActionBootCompleted })] // Allow GCM on boot and when app is closed   
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_MESSAGE },
    Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK },
    Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_LIBRARY_RETRY },
    Categories = new string[] { "@PACKAGE_NAME@" })]

    public class SampleGcmBroadcastReceiver : GcmBroadcastReceiverBase<NotificationService>
    {
        //IMPORTANT: Change this to your own Sender ID!
        //The SENDER_ID is your Google API Console App Project Number
        public static string[] SENDER_IDS = { AppConnections.Sender_Id };
    }

    [Service] //Must use the service tag
    public class NotificationService : GcmServiceBase, INotificationService
    {
        static NotificationHub hub;
        //static MobileServiceClient client { get; set; }

        //MobileServiceClient client = new MobileServiceClient("MOBILE_APP_URL");

        public static void Initialize(Context context)
        {
            // Call this from our main activity
            var cs = ConnectionString.CreateUsingSharedAccessKeyWithListenAccess(
                new Java.Net.URI(AppConnections.PushUrl),
                AppConnections.PushKey);            
            

            var hubName = AppConnections.NotificationHubPath;

            try
            {
                hub = new NotificationHub(hubName, cs, context);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.Message);
                throw;
            }
        }

        public static void Register(Context Context)
        {
            // Makes this easier to call from our Activity
            GcmClient.Register(Context, SampleGcmBroadcastReceiver.SENDER_IDS);
        }

        public async Task RegisterNotificationAsync()
        {
            MainActivity.CurrentActivity.RunOnUiThread
                 (
                     () => Initialize(MainActivity.CurrentActivity)
                 );

            MainActivity.CurrentActivity.RunOnUiThread
               (
                   () => Register(MainActivity.CurrentActivity)
               ); 
        }

        public NotificationService() : base(SampleGcmBroadcastReceiver.SENDER_IDS)
	    {
        }

        protected override void OnRegistered(Context context, string registrationId)
        {
            //Receive registration Id for sending GCM Push Notifications to

            const string templateGCM = "{\"data\":" +
                                            "{\"message\":\"{'Player: ' + $(player_name)}\"," +
                                            "\"args\":\"{'Jogada: ' + $(player_move)}\"}" +
                                         "}";

            var templates = new JObject
            {
                ["genericMessage"] = new JObject
                {
                    {
                        "body", templateGCM
                    }
                }
            };
            var playerTag = new string[] { "user_player2" };
            try
            {
                if (hub != null)
                {
                    var register = hub.RegisterTemplate(registrationId, "TemplateGCM", templateGCM, playerTag);
                    //hub.Register(registrationId, "TEST2");
                    if (register == null)
                        SendStatusGame("Falha ao registrar push");
                    else
                        SendStatusGame($"PUSH OK, TAG: {playerTag[0]}");
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.Message);
                throw;
            }
        }

        private void SendStatusGame(string message)
        {
            MessagingCenter.Send<object, string>(this, "GameStatus", message);
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            if (hub != null)
                hub.Unregister();
        }

        protected override void OnMessage(Context context, Intent intent)
        {
            //System.Console.WriteLine("Received Notification");
            //playerTag = new string[] { "user_player2" };
            if (intent != null || intent.Extras != null)
            {
                var playerMoveMessage = intent.Extras.GetString("args");
                var playerNameMessage = intent.Extras.GetString("message");
                CreateNotification(playerNameMessage, playerMoveMessage);

                var playerMove = playerMoveMessage.Substring(playerMoveMessage.Length - 3);
                var playerName = playerNameMessage.Replace("Player: ", string.Empty);

                MessagingCenter.Send<object, string[]>(this, "GamePlayerMove", new[] {playerMove, playerName});
            }
        }

        void CreateNotification(string title, string desc)
        {
            //Create notification
            var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;

            //Create an intent to show ui
            var uiIntent = new Intent(this, typeof(MainActivity));

            //Use Notification Builder
            var builder = new NotificationCompat.Builder(this);

            //Create the notification
            //we use the pending intent, passing our ui intent over which will get called
            //when the notification is tapped.
            var notification = builder.SetContentIntent(PendingIntent.GetActivity(this, 0, uiIntent, 0))
                    //.SetSmallIcon(Android.Resource.Drawable.SymDefAppIcon)
                    .SetSmallIcon(Resource.Drawable.iconapp)
                    //.SetLargeIcon(Resource.Drawable.iconapp)
                    .SetTicker(title)
                    .SetContentTitle(title)
                    .SetContentText(desc)

                    //Set the notification sound
                    .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))

                    //Auto cancel will remove the notification once the user touches it
                    .SetAutoCancel(true).Build();

            //Show the notification
            notificationManager.Notify(1, notification);
        }

        protected override bool OnRecoverableError(Context context, string errorId)
        {
            //Some recoverable error happened
            return true;
        }

        protected override void OnError(Context context, string errorId)
        {
            System.Console.WriteLine("Error");
            //Some more serious error happened
        }        
    }
}