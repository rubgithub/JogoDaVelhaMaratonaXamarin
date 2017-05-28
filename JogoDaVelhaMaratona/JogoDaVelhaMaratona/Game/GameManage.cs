namespace JogoDaVelhaMaratona.Game
{
    public class GameManage
    {
        public const string PlayerSymbolX = "X";
        public const string PlayerSymbolO = "O";
        public const string PlayerSymbolVazio = "";

        public const int ValueSymbolX = 1;
        public const int ValueSymbolO = 2;
        public const int ValueSymbolVazio = 0;

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
            //se vazio retornar vazio
            var valueSymbol = GameBoard[line, column];
            if (valueSymbol == ValueSymbolVazio)
                return PlayerSymbolVazio;
            var symbol = valueSymbol == ValueSymbolX ? PlayerSymbolX : PlayerSymbolO;
            return symbol;
        }
    }
}
