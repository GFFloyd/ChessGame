using Chess.Entities.ChessBoard;

namespace Chess;

internal class Screen
{
    public static void PrintBoard(Board board)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        for (byte i = 0; i < board.Row; i++)
        {
            for (byte j = 0; j < board.Column; j++)
            {
                int total = i + j;
                if (board.Piece(i, j) == null && total % 2 == 0)
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.Write(" ");

                }
                else if (board.Piece(i, j) == null && total % 2 != 0)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(" ");
                }
                else if (total % 2 == 0)
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(board.Piece(i, j));
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(board.Piece(i, j));
                }
            }
            Console.WriteLine();
        }
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Gray;
    }
}
