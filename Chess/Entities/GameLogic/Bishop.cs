using Chess.Entities.ChessBoard;

namespace Chess.Entities.GameLogic;

internal class Bishop : Piece
{
    public Bishop(Board board, PieceColor color) : base(board, color)
    {
    }
    public override string ToString()
    {
        return "B";
    }
}
