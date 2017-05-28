
using JogoDaVelhaMaratona.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JogoDaVelhaMaratona.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage
    {
        public GamePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new GameViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<object, string>(this, "ShowAlertMessage", (sender, msg) =>
            {
                //Device.BeginInvokeOnMainThread(() => {
                //    DisplayAlert("Push message", msg, "OK");
                //});
                var resp = (GameViewModel)BindingContext;
                resp.PlayerMoveCommand.Execute(msg);
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<object>(this, "ShowAlertMessage");
        }
    }
}