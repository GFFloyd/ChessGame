using Chess.Entities.ChessBoard;

namespace Chess.Entities.GameLogic;

internal class King : Piece
{
    public King(Board board, PieceColor color) : base(board, color)
    {
    }
    public override string ToString()
    {
        return "K";
    }
}
