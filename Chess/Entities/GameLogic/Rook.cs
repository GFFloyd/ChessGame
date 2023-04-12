using Chess.Entities.ChessBoard;

namespace Chess.Entities.GameLogic;

internal class Rook : Piece
{
    public Rook(Board board, PieceColor color) : base(board, color)
    {
    }
    public override string ToString()
    {
        return "R";
    }
}
