using Chess.Entities.ChessBoard;

namespace Chess.Entities.GameLogic;

internal class AlgebraicNotation
{
    public char Column { get; set; }
    public char Row { get; set; }
    public AlgebraicNotation(char col, char row)
    {
        Column = col;
        Row = row;
    }
    public Position ToPosition()
    {
        //Converts algebraic notation into 2D array coordinates
        return new Position((byte)('8' - Row), (byte)(Column - 'a'));
    }
    public override string ToString()
    {
        return $"{Column}{Row}";
    }
}
