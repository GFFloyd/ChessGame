using Chess.Entities.ChessBoard;

namespace Chess.Entities.GameLogic;

internal class Pawn : Piece
{
    public Pawn(Board board, PieceColor color) : base(board, color)
    {
    }
    public override string ToString()
    {
        return "P";
    }
    public override bool[,] PossibleMovements()
    {
        throw new NotImplementedException();
    }
}
