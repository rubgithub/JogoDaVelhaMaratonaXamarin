using Microsoft.WindowsAzure.Messaging;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Networking.PushNotifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace JogoDaVelhaMaratona.UWP
{
    sealed partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        protected  override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                Xamarin.Forms.Forms.Init(e);

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
                //await InitNotificationsAsync();
            }

            if (rootFrame.Content == null)
            {
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();

        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        private async Task InitNotificationsAsync()
        {
            var channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();

            //var hub = new NotificationHub("<<YOUR LISTEN CONNECTION STRING>>", "<<YOUR AZURE NOTIFICATION HUB NAME>>");

            //listen and send
            var playerTag = new string[] {"user_player1"};
            var hub = new NotificationHub("maratona-xamarin-push",
                "Endpoint=sb://rub-maratona-xamarin-push.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=7W2h5nQf/n0As/slkcU7IFH0GDTBRKJc2PB384Vozps=");
            var result = await hub.RegisterNativeAsync(channel.Uri, playerTag);

            channel.PushNotificationReceived += Channel_PushNotificationReceived;

            // Displays the registration ID so you know it was successful
            if (result.RegistrationId != null)
            {
                
            }
        }

        private void Channel_PushNotificationReceived(PushNotificationChannel sender, PushNotificationReceivedEventArgs args)
        {
            if (args.NotificationType == PushNotificationType.Toast)
            {
                System.Diagnostics.Debug.WriteLine("");
            }else
            {
                var raw = args.RawNotification;
                Xamarin.Forms.MessagingCenter.Send<object, string>(this, "GamePlayerMove", raw.Content);
            }
        }
    }
}
