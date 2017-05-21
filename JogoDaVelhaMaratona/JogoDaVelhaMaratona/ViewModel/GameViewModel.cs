using Xamarin.Forms;

namespace JogoDaVelhaMaratona.ViewModel
{
    public class GameViewModel : BaseViewModel
    {
        public Command GoHomeCommand { get; }

        public GameViewModel()
        {
            GoHomeCommand = new Command(GoHome);
        }

        private async void GoHome(object obj)
        {
            await PopAsync();
        }
    }
}
