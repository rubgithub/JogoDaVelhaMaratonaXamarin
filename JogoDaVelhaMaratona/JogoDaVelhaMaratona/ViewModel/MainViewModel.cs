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
            try
            {
                if (await LoginAsync())
                {
                    await PushAsync<GameViewModel>();
                }
            }
            catch (System.Exception)
            {
                throw;
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
            try
            {
                return _azureService.LoginAsync();
            }
            catch (System.Exception)
            {

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Não foi possível efetuar login, tente novamente.", "OK");
                });
                return Task.FromResult(false);
            }
        }
    }
}
