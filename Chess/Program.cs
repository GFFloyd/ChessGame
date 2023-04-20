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
                try
                {
                    Console.Clear();
                    Screen.PrintBoard(match.Board);
                    Console.WriteLine();
                    Console.WriteLine($"Turn: {match.Turn}");
                    Console.WriteLine($"Awaiting move by {match.CurrentPlayer}");

                    Console.Write("Origin: ");
                    Position origin = Screen.ReadPosition().ToPosition();
                    match.ValidateOriginPosition(origin);


                    bool[,] possibleMoves = match.Board.Piece(origin).PossibleMovements();
                    Console.Clear();
                    Screen.PrintBoard(match.Board, possibleMoves);

                    Console.Write("Target: ");
                    Position target = Screen.ReadPosition().ToPosition();
                    match.ValidateTargetPosition(origin, target);

                    match.GamePlay(origin, target);
                }
                catch (BoardException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }

        }
        catch (BoardException e)
        {
            Console.WriteLine(e.Message);
        }


    }
}