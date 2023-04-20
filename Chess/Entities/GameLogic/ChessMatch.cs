using Chess.Entities.ChessBoard;

namespace Chess.Entities.GameLogic;

internal class ChessMatch
{
    public Board Board { get; private set; }
    public int Turn { get; private set; }
    public PieceColor CurrentPlayer { get; private set; }
    public bool IsFinished { get; private set; }
    private HashSet<Piece> Pieces { get; set; }
    private HashSet<Piece> CapturedPieces { get; set; }

    public ChessMatch()
    {
        Board = new Board(8, 8);
        Turn = 1;
        CurrentPlayer = PieceColor.White;
        IsFinished = false;
        CapturedPieces = new HashSet<Piece>();
        Pieces = new HashSet<Piece>();
        StartingPosition();
    }
    public void MakeMove(Position origin, Position target)
    {
        //Piece movement logic, capturedPiece variable should be stored in a "captured pieces list"
        Piece piece = Board.TakePiece(origin);
        piece.IncrementMovimentQuantity();
        Piece capturedPiece = Board.TakePiece(target);
        Board.PlacePiece(piece, target);
        if (capturedPiece != null)
        {
            CapturedPieces.Add(capturedPiece);
        }
    }
    public void GamePlay(Position origin, Position target)
    {
        MakeMove(origin, target);
        Turn++;
        ChangePlayer();
    }
    public void ValidateOriginPosition(Position origin)
    {
        if (Board.Piece(origin) == null)
        {
            throw new BoardException("There's no piece at this position");
        }
        if (CurrentPlayer != Board.Piece(origin).Color)
        {
            throw new BoardException("The piece in this position does not belong to you");
        }
        if (!Board.Piece(origin).CheckIfThereArePossibleMoves())
        {
            throw new BoardException("This piece does not have valid moves");
        }
    }
    public void ValidateTargetPosition(Position origin, Position target)
    {
        if (!Board.Piece(origin).CanMoveTo(target))
        {
            throw new BoardException("Target position is invalid");
        }
    }
    public void ChangePlayer()
    {
        CurrentPlayer = CurrentPlayer == PieceColor.White ? PieceColor.Black : PieceColor.White;
    }
    public HashSet<Piece> CapturedPiecesSet(PieceColor color)
    {
        HashSet<Piece> result = new HashSet<Piece>();
        foreach (Piece piece in CapturedPieces)
        {
            if (piece.Color == color)
            {
                result.Add(piece);
            }
        }
        return result;
    }
    public HashSet<Piece> PiecesInPlaySet(PieceColor color)
    {
        HashSet<Piece> result = new HashSet<Piece>();
        foreach (Piece piece in Pieces)
        {
            if (piece.Color == color)
            {
                result.Add(piece);
            }
        }
        result.ExceptWith(CapturedPiecesSet(color));
        return result;
    }
    public void PlaceNewPiece(char row, char col, Piece piece)
    {
        Board.PlacePiece(piece, new AlgebraicNotation(row, col).ToPosition());
        Pieces.Add(piece);
    }
    public void StartingPosition()
    {
        //TODO: instanciate initial pieces into their respective places
        PlaceNewPiece('a', '8', new Rook(Board, PieceColor.White));
        PlaceNewPiece('c', '8', new Rook(Board, PieceColor.White));
        PlaceNewPiece('d', '5', new King(Board, PieceColor.Black));
        PlaceNewPiece('f', '5', new King(Board, PieceColor.White));
    }
}
