using Android.App;
using Android.Content.PM;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;

namespace JogoDaVelhaMaratona.Droid
{
    //
    [Activity(LaunchMode = LaunchMode.SingleTop, Label = "JogoDaVelhaMaratona", Icon = "@drawable/iconapp", Theme = "@style/MainTheme", 
        MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        //Create a new instance field for this activity.
        static MainActivity instance = null;

        //Return the current activity instance.
        public static MainActivity CurrentActivity
        {
            get
            {
                return instance;
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            instance = this;

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            ImageCircleRenderer.Init();

            LoadApplication(new App());
        }        
    }
}

