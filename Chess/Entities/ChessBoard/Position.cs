namespace Chess.Entities.ChessBoard;

internal struct Position
{
    public byte Row { get; set; }
    public byte Column { get; set; }
    public Position(byte row, byte col)
    {
        Row = row;
        Column = col;
    }
    public override string ToString()
    {
        return $"{Row}, {Column}";
    }
}
