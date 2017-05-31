using JogoDaVelhaMaratona.Service;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JogoDaVelhaMaratona.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly AzureService _azureService;
        private readonly NotificationService _pushNotification;

        public Command ShowAboutPageCommand { get; }
        public Command ShowGamePageCommand { get; }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set {
                SetProperty(ref _isBusy , value);
                IsLogginIn = !_isBusy;
            }
        }
        
        private bool _isLogginIn;
        public bool IsLogginIn
        {
            get { return _isLogginIn; }
            set { SetProperty(ref _isLogginIn , value); }
        }


        public MainViewModel()
        {
            IsBusy = false;
            ShowAboutPageCommand = new Command(ShowAboutPage);
            ShowGamePageCommand = new Command(ShowGamePage);

            _azureService = DependencyService.Get<AzureService>();
            _pushNotification = DependencyService.Get<NotificationService>();
        }

        private async void ShowGamePage(object obj)
        {
            IsBusy = true;
            try
            {
                if (await LoginAsync())
                {
                    IsBusy = false;
                    await PushAsync<GameViewModel>();
                    await _pushNotification.Register();
                }else
                {
                    IsBusy = false;
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
