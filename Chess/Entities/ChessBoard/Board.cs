namespace Chess.Entities.ChessBoard;

internal class Board
{
    public byte Row { get; set; }
    public byte Column { get; set; }
    private readonly Piece[,] _pieces;

    public Board(byte row, byte col)
    {
        Row = row;
        Column = col;
        _pieces = new Piece[row, col];
    }
    public Piece Piece(byte row, byte col)
    {
        return _pieces[row, col];
    }
    public void PlacePiece(Piece piece, Position position)
    {
        _pieces[position.Row, position.Column] = piece;
        piece.Position = position;
    }
}
