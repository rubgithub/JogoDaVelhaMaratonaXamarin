
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
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<object>(this, "GamePlayerMove");
            MessagingCenter.Unsubscribe<object>(this, "GameStatus");
        }
    }
}