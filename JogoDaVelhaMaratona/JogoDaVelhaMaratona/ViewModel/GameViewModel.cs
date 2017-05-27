using JogoDaVelhaMaratona.Game;
using JogoDaVelhaMaratona.Helpers;
using Xamarin.Forms;

namespace JogoDaVelhaMaratona.ViewModel
{
    public class GameViewModel : BaseViewModel
    {
        private string _playerSimbol;

        public Command GoHomeCommand { get; }
        public Command GetFacebookUserInfoCommand { get; }

        //private string GameBoard { get; set; }

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

        #region Game Enters
        private string _l1C1;
        public string L1C1
        {
            get { return _l1C1; }
            set {
                    SetProperty(ref _l1C1 , value);
                }
        }

        private string _l1C2;
        public string L1C2
        {
            get { return _l1C1; }
            set {
                SetProperty(ref _l1C1, value);
            }
        }

        private string _l1C3;
        public string L1C3
        {
            get { return _l1C1; }
            set { SetProperty(ref _l1C1, value); }
        }

        #endregion

        public GameViewModel()
        {
            GoHomeCommand = new Command(GoHome);
            Player1Name = Settings.Player1Name;
            Player1Image = Settings.Player1Image;
            _playerSimbol = GameManage.PlayerSymbolO;

            //GetFacebookUserInfoCommand = new Command(async () => await GetFacebookUserInfo());
        }

        private async void GoHome()
        {
            await PopAsync();
        }

        private void SetGameBoard(int line, int column, string playerSimbol)
        {
            var valueSymbol = playerSimbol == "X" ? 0 : 1;

            GameManage.SetGameBoard(line, column, valueSymbol);
            MarkGameBoard();
        }

        private void MarkGameBoard()
        {
            L1C1 = GameManage.GetGameBoardSymbol(1,1);
            L1C2 = GameManage.GetGameBoardSymbol(1,2);
            L1C3 = GameManage.GetGameBoardSymbol(1,3);

        }
    }
}
