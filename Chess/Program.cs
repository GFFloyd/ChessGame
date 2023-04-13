using Chess.Entities.ChessBoard;
using Chess.Entities.GameLogic;

namespace Chess;

internal class Program
{
    static void Main(string[] args)
    {
        Board board = new(8, 8);
        board.PlacePiece(new Rook(board, PieceColor.White), new Position(0, 2));
        board.PlacePiece(new Rook(board, PieceColor.White), new Position(0, 0));
        board.PlacePiece(new Queen(board, PieceColor.Black), new Position(3, 5));
        board.PlacePiece(new Queen(board, PieceColor.Black), new Position(3, 4));
        Screen.PrintBoard(board);

    }
}