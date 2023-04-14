namespace Chess.Entities.ChessBoard;

internal class Piece
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
}
