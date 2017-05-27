namespace JogoDaVelhaMaratona.Game
{
    public class GameManage
    {
        public const string PlayerSymbolX = "X";
        public const string PlayerSymbolO = "O";

        private static int[,] GameBoard = new int[3,3];

        public static int CheckGameResult()
        {
            return 0;
        }

        public static void SetGameBoard(int line, int column, int valueSimbol)
        {
            GameBoard[line, column] = valueSimbol;
        }

        public static string GetGameBoardSymbol(int line, int column)
        {
            var valueSymbol = GameBoard[line, column];
            var symbol = valueSymbol == 1 ? PlayerSymbolX : PlayerSymbolO;
            return symbol;
        }
    }
}
