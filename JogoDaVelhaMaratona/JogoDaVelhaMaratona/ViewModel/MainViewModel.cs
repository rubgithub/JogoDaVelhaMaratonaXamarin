using Xamarin.Forms;

namespace JogoDaVelhaMaratona.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public Command ShowAboutPageCommand { get; }
        public Command ShowGamePageCommand { get; }

        public MainViewModel()
        {
            ShowAboutPageCommand = new Command(ShowAboutPage);
            ShowGamePageCommand = new Command(ShowGamePage);
        }

        private async void ShowGamePage(object obj)
        {
            await PushAsync<GameViewModel>();
        }

        private async void ShowAboutPage()
        {
            await PushAsync<AboutViewModel>();
        }
    }
}
