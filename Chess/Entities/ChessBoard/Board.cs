namespace Chess.Entities.ChessBoard;

internal class Board
{
    public int Row { get; set; }
    public int Column { get; set; }
    private readonly Piece[,] _pieces;

    public Board(int row, int col)
    {
        Row = row;
        Column = col;
        _pieces = new Piece[row, col];
    }
    public Piece Piece(int row, int col)
    {
        return _pieces[row, col];
    }
    public Piece Piece(Position position)
    {
        return _pieces[position.Row, position.Column];
    }
    public bool CheckIfThereIsPiece(Position position)
    {
        //Checks if position is valid before checking if there's a piece there
        PositionValidation(position);
        return Piece(position) != null;
    }
    public void PlacePiece(Piece piece, Position position)
    {
        //Checks for a valid piece placement before placing it onto the matrix
        if (CheckIfThereIsPiece(position))
        {
            throw new BoardException("Invalid piece placement, a piece already exists there.");
        }
        _pieces[position.Row, position.Column] = piece;
        piece.Position = position;
    }
    public Piece? TakePiece(Position position)
    {
        //Takes piece if it has a valid piece in the square
        if (Piece(position) == null)
        {
            return null;
        }
        Piece piece = Piece(position);
        piece.Position = null;
        _pieces[position.Row, position.Column] = null;
        return piece;
    }
    public bool IsItAValidPosition(Position position)
    {
        //Checks for a existing row and column
        if (position.Row < 0 || position.Row >= Row || position.Column < 0 || position.Column >= Column)
        {
            return false;
        }
        return true;
    }
    public void PositionValidation(Position position)
    {
        if (!IsItAValidPosition(position))
        {
            throw new BoardException("Invalid position");
        }
    }
}
