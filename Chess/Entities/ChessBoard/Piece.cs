namespace Chess.Entities.ChessBoard;

internal abstract class Piece
{
    public Position? Position { get; set; }
    public PieceColor? Color { get; set; }
    public int Movements { get; protected set; } = 0;
    public Board? ChessBoard { get; protected set; }

    public Piece(Board board, PieceColor color)
    {
        Position = null;
        ChessBoard = board;
        Color = color;
    }
    public void IncrementMovimentQuantity()
    {
        Movements++;
    }
    public bool CanMove(Position position)
    {
        //it checks if a square has a opposite color piece or if it's empty
        Piece piece = ChessBoard.Piece(position);
        return piece != null || piece.Color != Color;
    }
    public abstract bool[,] PossibleMovements();
}
