namespace Chess.Entities.ChessBoard;

internal class ChessBoard
{
    public byte Row { get; set; }
    public byte Column { get; set; }
    private ChessPiece[,] _pieces;

    public ChessBoard(byte row, byte col)
    {
        Row = row;
        Column = col;
        _pieces = new ChessPiece[row, col];
    }
}
