using Chess.Entities.ChessBoard;

namespace Chess.Entities.GameLogic;

internal class Knight : Piece
{
    public Knight(Board board, PieceColor color) : base(board, color)
    {
    }
    public override string ToString()
    {
        return "N";
    }
}
