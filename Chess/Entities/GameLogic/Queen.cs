using Chess.Entities.ChessBoard;

namespace Chess.Entities.GameLogic;

internal class Queen : Piece
{
    public Queen(Board board, PieceColor color) : base(board, color)
    {
    }
    public override string ToString()
    {
        return "Q";
    }
}
