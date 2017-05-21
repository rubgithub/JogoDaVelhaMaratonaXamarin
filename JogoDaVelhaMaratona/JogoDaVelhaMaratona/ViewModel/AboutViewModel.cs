using Xamarin.Forms;

namespace JogoDaVelhaMaratona.ViewModel
{
    public class AboutViewModel : BaseViewModel
    {
        public Command GoHomeCommand { get; }

        public AboutViewModel()
        {
            GoHomeCommand = new Command(GoHome);
        }

        private async void GoHome(object obj)
        {
            await PopAsync();
        }
    }
}
