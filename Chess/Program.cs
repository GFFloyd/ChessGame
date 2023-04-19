using Chess.Entities.ChessBoard;
using Chess.Entities.GameLogic;

namespace Chess;

internal class Program
{
    static void Main(string[] args)
    {
        try
        {
            ChessMatch match = new ChessMatch();
            while (!match.IsFinished)
            {
                Console.Clear();
                Screen.PrintBoard(match.Board);

                Console.Write("Origin: ");
                Position origin = Screen.ReadPosition().ToPosition();
                

                bool[,] possibleMoves = match.Board.Piece(origin).PossibleMovements();
                Console.Clear();
                Screen.PrintBoard(match.Board, possibleMoves);

                Console.Write("Target: ");
                Position target = Screen.ReadPosition().ToPosition();

                match.MakeMove(origin, target);
            }

        }
        catch (BoardException e)
        {
            Console.WriteLine(e.Message);
        }


    }
}