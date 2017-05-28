﻿using Microsoft.WindowsAzure.Messaging;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Networking.PushNotifications;using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace JogoDaVelhaMaratona.UWP
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected async override void OnLaunched(LaunchActivatedEventArgs e)
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
                await InitNotificationsAsync();
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();

        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        //private async Task InitNotificationsAsync()
        //{
        //    var channel = await PushNotificationChannelManager
        //        .CreatePushNotificationChannelForApplicationAsync();
        //    const string templateBodyWNS =
        //    "<toast><visual><binding template=\"ToastText01\"><textid =\"1\">$(messageParam)</text></binding></visual></toast>";

        //    JObject headers = new JObject
        //    {
        //        ["X-WNS-Type"] = "wns/toast"
        //    };

        //    JObject templates = new JObject
        //    {
        //        ["genericMessage"] = new JObject
        //        {
        //            {"body", templateBodyWNS},
        //            {"headers", headers} // Needed for WNS.
        //        }
        //    };

        //    MobileServiceClient client = new MobileServiceClient("https://rub-maratona-xamarin-intermediario.azurewebsites.net/");
        //    System.Diagnostics.Debug.Write(client.InstallationId);
        //    await client.GetPush()
        //        .RegisterAsync(channel.Uri, templates);
        //}

        private async Task InitNotificationsAsync()
        {
            var channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();

            var hub = new NotificationHub("<<YOUR LISTEN CONNECTION STRING>>", "<<YOUR AZURE NOTIFICATION HUB NAME>>");
            var result = await hub.RegisterNativeAsync(channel.Uri);
            channel.PushNotificationReceived += Channel_PushNotificationReceived;

            // Displays the registration ID so you know it was successful
            if (result.RegistrationId != null)
            {
                var dialog = new MessageDialog("Registration successful: " + result.RegistrationId);
                dialog.Commands.Add(new UICommand("OK"));
                await dialog.ShowAsync();
            }
        }

        private void Channel_PushNotificationReceived(PushNotificationChannel sender, PushNotificationReceivedEventArgs args)
        {
            var raw = args.RawNotification;
            System.Diagnostics.Debug.WriteLine(raw.Content);
            //var ToastNotifier = ToastNotificationManager.CreateToastNotifier();
            //ToastNotifier.Show(toast);
            Xamarin.Forms.MessagingCenter.Send<object, string>(this, "ShowAlertMessage", raw.Content);
        }
    }
}
