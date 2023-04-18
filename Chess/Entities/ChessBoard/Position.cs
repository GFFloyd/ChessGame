namespace Chess.Entities.ChessBoard;

internal class Position
{
    public int Row { get; set; }
    public int Column { get; set; }
    public Position(int row, int col)
    {
        Row = row;
        Column = col;
    }
    public void DefineValues(int row, int col)
    {
        Row = row;
        Column = col;
    }
    public override string ToString()
    {
        return $"{Row}, {Column}";
    }
}
