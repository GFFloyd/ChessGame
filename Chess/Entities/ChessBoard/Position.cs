namespace Chess.Entities.ChessBoard;

internal class Position
{
    public byte Row { get; set; }
    public byte Column { get; set; }
    public Position()
    {
    }
    public Position(byte row, byte col)
    {
        Row = row;
        Column = col;
    }
}
