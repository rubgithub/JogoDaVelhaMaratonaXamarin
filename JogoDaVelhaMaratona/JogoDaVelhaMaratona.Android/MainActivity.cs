using Android.App;
using Android.Content.PM;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;
using Java.Lang;

namespace JogoDaVelhaMaratona.Droid
{
    //
    [Activity(LaunchMode = LaunchMode.SingleTop, Label = "JogoDaVelhaMaratona", Icon = "@drawable/iconapp", Theme = "@style/MainTheme", 
        MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        // Create a new instance field for this activity.
        //static MainActivity instance = null;

        // Return the current activity instance.
        //public static MainActivity CurrentActivity
        //{
        //    get
        //    {
        //        return instance;
        //    }
        //}

        protected override void OnCreate(Bundle bundle)
        {
            //instance = this;

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            ImageCircleRenderer.Init();

            LoadApplication(new App());

            try
            {
                /*
                // Check to ensure everything's set up right
                GcmClient.CheckDevice(this);
                GcmClient.CheckManifest(this);

                // Register for push notifications
                System.Diagnostics.Debug.WriteLine("Registering...");
                GcmClient.Register(this, AppConnections.Sender_Id);
                */

                // Initialize our Gcm Service Hub
                Notification.NotificationService.Initialize(this);

                // Register for GCM
                Notification.NotificationService.Register(this);
            }
            catch (Java.Net.MalformedURLException)
            {
                CreateAndShowDialog("There was an error creating the client. Verify the URL.", "Error");
            }
            catch (Exception e)
            {
                CreateAndShowDialog(e.Message, "Error");
            }
        }

        private void CreateAndShowDialog(string message, string title)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);

            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.Create().Show();
        }
    }
}

