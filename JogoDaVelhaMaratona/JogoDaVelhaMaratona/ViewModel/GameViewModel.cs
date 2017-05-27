using JogoDaVelhaMaratona.Helpers;
using Xamarin.Forms;

namespace JogoDaVelhaMaratona.ViewModel
{
    public class GameViewModel : BaseViewModel
    {
        public Command GoHomeCommand { get; }
        public Command GetFacebookUserInfoCommand { get; }

        private string _player1Name;
        public string Player1Name
        {
            get { return _player1Name; }
            set { SetProperty (ref _player1Name , value); }
        }

        private string _player1Image;

        public string Player1Image
        {
            get { return _player1Image; }
            set { SetProperty (ref _player1Image , value); }
        }


        public GameViewModel()
        {
            GoHomeCommand = new Command(GoHome);
            Player1Name = Settings.Player1Name;
            Player1Image = Settings.Player1Image;
            //GetFacebookUserInfoCommand = new Command(async () => await GetFacebookUserInfo());
        }

        private async void GoHome()
        {
            await PopAsync();
        }
    }
}
