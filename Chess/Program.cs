using Chess.Entities.ChessBoard;
using Chess.Entities.GameLogic;

namespace Chess;

internal class Program
{
    static void Main(string[] args)
    {
        Board board = new(8, 8);
        Screen.PrintBoard(board);
    }
}