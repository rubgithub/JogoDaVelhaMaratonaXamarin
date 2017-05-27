using JogoDaVelhaMaratona.Service;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JogoDaVelhaMaratona.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly AzureService _azureService;

        public Command ShowAboutPageCommand { get; }
        public Command ShowGamePageCommand { get; }

        public MainViewModel()
        {
            ShowAboutPageCommand = new Command(ShowAboutPage);
            ShowGamePageCommand = new Command(ShowGamePage);

            _azureService = DependencyService.Get<AzureService>();
        }

        private async void ShowGamePage(object obj)
        {
            if(await LoginAsync())
            {
                await PushAsync<GameViewModel>();
            }
        }

        private async void ShowAboutPage()
        {
            await PushAsync<AboutViewModel>();
        }

        private Task<bool> LoginAsync()
        {
            //_isBusy = true;
            //if (Settings.IsLoggedIn)
            //    return Task.FromResult(true);

            return _azureService.LoginAsync();
        }
    }
}
