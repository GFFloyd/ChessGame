namespace Chess.Entities.ChessBoard;

internal abstract class ChessPiece
{
    public Position? Position { get; set; }
    public PieceColor? Color { get; set; }
    public int Movements { get; protected set; } = 0;
    public ChessBoard? ChessBoard { get; protected set; }

    public ChessPiece(Position position, PieceColor color, ChessBoard board)
    {
        Position = position;
        Color = color;
        ChessBoard = board;
    }
}
