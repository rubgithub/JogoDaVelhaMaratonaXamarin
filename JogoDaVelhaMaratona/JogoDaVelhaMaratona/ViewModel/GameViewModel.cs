using JogoDaVelhaMaratona.Game;
using JogoDaVelhaMaratona.Helpers;
using System;
using Xamarin.Forms;
//TODO player pelo nome e posicionar nome e imagem 
// Logica para cada jogador X ou O não permitir duas jogadas info: jogada: [1,1] aguardando jogador2
// Push android
namespace JogoDaVelhaMaratona.ViewModel
{
    public class GameViewModel : BaseViewModel
    {
        private string _playerSimbol;

        #region Command
        public Command GoHomeCommand { get; }
        public Command GetFacebookUserInfoCommand { get; }
        public Command PlayerMoveCommand { get; }
        #endregion


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

        private string _player2Name;
        public string Player2Name
        {
            get { return _player2Name; }
            set { SetProperty(ref _player2Name, value); }
        }

        private string _player2Image;

        public string Player2Image
        {
            get { return _player2Image; }
            set { SetProperty(ref _player2Image, value); }
        }

        private string _gameStatus;

        public string GameStatus
        {
            get { return _gameStatus; }
            set { SetProperty(ref _gameStatus, value); }
        }


        #region Game Enters
        private string _l0C0;
        public string L0C0
        {
            get { return _l0C0; }
            set {
                    SetProperty(ref _l0C0 , value);
                }
        }

        private string _l0C1;
        public string L0C1
        {
            get { return _l0C1; }
            set {
                SetProperty(ref _l0C1, value);
            }
        }

        private string _l0C2;
        public string L0C2
        {
            get { return _l0C2; }
            set { SetProperty(ref _l0C2, value); }
        }

        private string _l1C0;
        public string L1C0
        {
            get { return _l1C0; }
            set
            {
                SetProperty(ref _l1C0, value);
            }
        }

        private string _l1C1;
        public string L1C1
        {
            get { return _l1C1; }
            set
            {
                SetProperty(ref _l1C1, value);
            }
        }

        private string _l1C2;
        public string L1C2
        {
            get { return _l1C2; }
            set { SetProperty(ref _l1C2, value); }
        }

        private string _l2C0;
        public string L2C0
        {
            get { return _l2C0; }
            set
            {
                SetProperty(ref _l2C0, value);
            }
        }

        private string _l2C1;
        public string L2C1
        {
            get { return _l2C1; }
            set
            {
                SetProperty(ref _l2C1, value);
            }
        }

        private string _l2C2;
        public string L2C2
        {
            get { return _l2C2; }
            set { SetProperty(ref _l2C2, value); }
        }

        #endregion

        //private readonly NotificationService _pushNotification;
        public GameViewModel()
        {
            GoHomeCommand = new Command(GoHome);
            PlayerMoveCommand = new Command<string>((playerMove) => PlayerMoveExecute(playerMove));
            Player1Name = Settings.Player1Name;
            Player1Image = Settings.Player1Image;
            Player2Name = Settings.Player2Name;
            Player2Image = Settings.Player2Image;

            if (!string.IsNullOrWhiteSpace(Player1Name))
                _playerSimbol = GameManage.PlayerSymbolO;
            else
                _playerSimbol = GameManage.PlayerSymbolX;

            GameStatus = "Registrando push notifications...";

            //_pushNotification = DependencyService.Get<NotificationService>();
            //_pushNotification.Register();

            SubscribeMessagingCenter();
        }

        private void SubscribeMessagingCenter()
        {
            MessagingCenter.Subscribe<object, string[]>(this, "GamePlayerMove", (sender, msg) =>
            {
                var playerMove = msg[0];
                var playerName = msg[1];
                PlayerMoveExecute(playerMove, playerName);
            });
            MessagingCenter.Subscribe<object, string>(this, "GameStatus", (sender, msg) =>
            {
                SetGameStatus(msg);
            });
        }

        private void SetGameStatus(string msg)
        {
            GameStatus = msg;
        }

        private void PlayerMoveExecute(string playerMove, string playerName = "")
        {
            var move = playerMove.Split(',');

            var playerSymbol = _playerSimbol;
            if (!string.IsNullOrWhiteSpace(playerName)) //jogada local
            {
                playerSymbol = _playerSimbol == GameManage.PlayerSymbolO ? GameManage.PlayerSymbolX : GameManage.PlayerSymbolO;
            }else
            {
                playerName = "Sua: ";
            }

            var lineMove = Int16.Parse(move[0]);
            var colMove = Int16.Parse(move[1]);
            SetGameBoard(lineMove, colMove, playerSymbol);
            SetGameStatus($"{playerName} Jogada: [{lineMove},{colMove}]");
        }

        private async void GoHome()
        {
            await PopAsync();
        }

        private void SetGameBoard(int line, int column, string playerSimbol)
        {
            var valueSymbol = playerSimbol == GameManage.PlayerSymbolX ? GameManage.ValueSymbolX : GameManage.ValueSymbolO;

            GameManage.SetGameBoard(line, column, valueSymbol);
            MarkGameBoard();
        }

        private void MarkGameBoard()
        {
            L0C0 = GameManage.GetGameBoardSymbol(0,0);
            L0C1 = GameManage.GetGameBoardSymbol(0,1);
            L0C2 = GameManage.GetGameBoardSymbol(0,2);

            L1C0 = GameManage.GetGameBoardSymbol(1, 0);
            L1C1 = GameManage.GetGameBoardSymbol(1, 1);
            L1C2 = GameManage.GetGameBoardSymbol(1, 2);

            L2C0 = GameManage.GetGameBoardSymbol(2, 0);
            L2C1 = GameManage.GetGameBoardSymbol(2, 1);
            L2C2 = GameManage.GetGameBoardSymbol(2, 2);
        }
    }
}
