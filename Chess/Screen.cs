using Chess.Entities.ChessBoard;
using Chess.Entities.GameLogic;

namespace Chess;

internal class Screen
{
    public static void PrintBoard(Board board)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        for (byte i = 0; i < board.Row; i++)
        {
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
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    if (board.Piece(i, j) != null)
                    {
                        PrintPiece(board.Piece(i, j));
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    if (board.Piece(i, j) != null)
                    {
                        PrintPiece(board.Piece(i, j));
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
        }
        Console.WriteLine("  abcdefgh");
    }
    public static void PrintPiece(Piece piece)
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
    public static AlgebraicNotation ReadPosition()
    {
        string str = Console.ReadLine();
        char col = str[0];
        char row = str[1];
        return new AlgebraicNotation(col, row);
    }
}
