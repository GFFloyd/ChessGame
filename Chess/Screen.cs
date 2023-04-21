using Chess.Entities.ChessBoard;
using Chess.Entities.GameLogic;

namespace Chess;

internal class Screen
{
    public static void PrintMatch(ChessMatch match)
    {
        Screen.PrintBoard(match.Board);
        Console.WriteLine();
        PrintCapturedPieces(match);
        Console.WriteLine($"Turn: {match.Turn}");
        Console.WriteLine($"Awaiting move by {match.CurrentPlayer}");
        if (match.Check)
        {
            Console.WriteLine("YOU ARE IN CHECK");
        }
    }
    private static void PrintCapturedPieces(ChessMatch match)
    {
        Console.WriteLine("Captured pieces:");
        Console.Write("White:");
        PrintSet(match.CapturedPiecesSet(PieceColor.White));
        Console.WriteLine();
        Console.Write("Black:");
        PrintSet(match.CapturedPiecesSet(PieceColor.Black));
        Console.WriteLine();
    }

    private static void PrintSet(HashSet<Piece> pieces)
    {
        Console.Write("[");
        foreach (Piece piece in pieces)
        {
            Console.Write($"{piece}");
        }
        Console.Write("]");
    }

    public static void PrintBoard(Board board)
    {
        ConsoleColor whiteSquares = ConsoleColor.DarkYellow;
        ConsoleColor blackSquares = ConsoleColor.DarkBlue;
        for (byte i = 0; i < board.Row; i++)
        {
            //Prints the row numbers on the left
            Console.Write($"{8 - i} ");
            for (byte j = 0; j < board.Column; j++)
            {
                int total = i + j;
                //this loop prints the board in a chessboard-like pattern
                if (total % 2 == 0)
                {
                    //if total is even, it will print in a different color than its odd counterpart
                    //and it checks if the square has a piece, then it prints the piece instead
                    //of a blank square
                    Console.BackgroundColor = whiteSquares;
                    PrintPiece(board.Piece(i, j));
                }
                else
                {
                    Console.BackgroundColor = blackSquares;
                    PrintPiece(board.Piece(i, j));
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
        }
        Console.WriteLine("  abcdefgh");
    }
    public static void PrintBoard(Board board, bool[,] possibleMoves)
    {
        ConsoleColor whiteSquares = ConsoleColor.DarkYellow;
        ConsoleColor blackSquares = ConsoleColor.DarkBlue;
        ConsoleColor movementSquares = ConsoleColor.Gray;
        for (byte i = 0; i < board.Row; i++)
        {
            //Prints the row numbers on the left
            Console.Write($"{8 - i} ");
            for (byte j = 0; j < board.Column; j++)
            {
                int total = i + j;
                //this loop prints the board in a chessboard-like pattern
                if (total % 2 == 0 && !possibleMoves[i, j])
                {
                    //if total is even, it will print in a different color than its odd counterpart
                    //and it checks if the square has a piece, then it prints the piece instead
                    //of a blank square
                    Console.BackgroundColor = whiteSquares;
                }
                else if (total % 2 != 0 && !possibleMoves[i, j])
                {
                    Console.BackgroundColor = blackSquares;
                }
                else if (possibleMoves[i, j])
                {
                    Console.BackgroundColor = movementSquares;
                }
                PrintPiece(board.Piece(i, j));
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.WriteLine();
        }
        Console.WriteLine("  abcdefgh");
    }
    public static void PrintPiece(Piece piece)
    {
        if (piece == null)
        {
            Console.Write(" ");
        }
        else
        {
            if (piece.Color == PieceColor.White)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(piece);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(piece);
            }
        }
    }
    public static AlgebraicNotation ReadPosition()
    {
        string str = Console.ReadLine() ?? string.Empty;
        if (str == string.Empty)
        {
            throw new BoardException("You must type a valid Origin/Target");
        }
        char col = str[0];
        char row = str[1];
        return new AlgebraicNotation(col, row);
    }
}
