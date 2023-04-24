using Chess.Entities.ChessBoard;

namespace Chess.Entities.GameLogic;

internal class ChessMatch
{
    public Board Board { get; private set; }
    public int Turn { get; private set; }
    public PieceColor CurrentPlayer { get; private set; }
    public bool IsFinished { get; private set; }
    private HashSet<Piece> PiecesInPlay { get; set; }
    private HashSet<Piece> CapturedPieces { get; set; }
    public bool Check { get; private set; }

    public ChessMatch()
    {
        Board = new Board(8, 8);
        Turn = 1;
        CurrentPlayer = PieceColor.White;
        IsFinished = false;
        CapturedPieces = new HashSet<Piece>();
        PiecesInPlay = new HashSet<Piece>();
        Check = false;
        StartingPosition();
    }
    public Piece MakeMove(Position origin, Position target)
    {
        Piece piece = Board.TakePiece(origin);
        piece.IncrementMovimentQuantity();
        Piece capturedPiece = Board.TakePiece(target);
        Board.PlacePiece(piece, target);
        if (capturedPiece != null)
        {
            CapturedPieces.Add(capturedPiece);
        }
        return capturedPiece;
    }
    public void GamePlay(Position origin, Position target)
    {
        Piece capturedPiece = MakeMove(origin, target);
        if (IsTheKingInCheck(CurrentPlayer))
        {
            UndoMove(origin, target, capturedPiece);
            throw new BoardException("You can't put your king in check");
        }
        if (IsTheKingInCheck(Opponent(CurrentPlayer)))
        {
            Check = true;
        }
        else
        {
            Check = false;
        }

        if (IsItCheckMate(Opponent(CurrentPlayer)))
        {
            IsFinished = true;
        }
        else
        {
            Turn++;
            ChangePlayer();
        }
    }

    private void UndoMove(Position origin, Position target, Piece capturedPiece)
    {
        Piece piece = Board.TakePiece(target);
        piece.DecrementMovementQuantity();
        if (capturedPiece != null)
        {
            Board.PlacePiece(capturedPiece, target);
            CapturedPieces.Remove(capturedPiece);
        }
        Board.PlacePiece(piece, origin);
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
        foreach (Piece piece in PiecesInPlay)
        {
            if (piece.Color == color)
            {
                result.Add(piece);
            }
        }
        result.ExceptWith(CapturedPiecesSet(color));
        return result;
    }
    private PieceColor Opponent(PieceColor color)
    {
        //this method is used to check which pieces are the opponent's or not
        return color == PieceColor.White ? PieceColor.Black : PieceColor.White;
    }
    private Piece King(PieceColor color)
    {
        //this method is used to check if king is in check
        foreach (Piece piece in PiecesInPlaySet(color))
        {
            if (piece is King)
            {
                return piece;
            }
        }
        return null;
    }
    public bool IsTheKingInCheck(PieceColor color)
    {
        Piece king = King(color);
        foreach (Piece piece in PiecesInPlaySet(Opponent(color)))
        {
            bool[,] possibleMoves = piece.PossibleMovements();
            if (possibleMoves[king.Position.Row, king.Position.Column])
            {
                return true;
            }
        }
        return false;
    }
    public bool IsItCheckMate(PieceColor color)
    {
        if (!IsTheKingInCheck(color))
        {
            return false;
        }
        foreach (Piece piece in PiecesInPlaySet(color))
        {
            bool[,] booleanArray = piece.PossibleMovements();
            for (int i = 0; i < Board.Row; i++)
            {
                for (int j = 0; j < Board.Column; j++)
                {
                    if (booleanArray[i, j])
                    {
                        Position origin = piece.Position;
                        Position target = new Position(i, j);
                        Piece capturedPiece = MakeMove(origin, target);
                        bool testCheck = IsTheKingInCheck(color);
                        UndoMove(origin, target, capturedPiece);
                        if (!testCheck)
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }
    public void PlaceNewPiece(char row, char col, Piece piece)
    {
        Board.PlacePiece(piece, new AlgebraicNotation(row, col).ToPosition());
        PiecesInPlay.Add(piece);
    }
    public void StartingPosition()
    {
        //TODO: instanciate initial pieces into their respective places
        PlaceNewPiece('a', '8', new King(Board, PieceColor.Black));
        PlaceNewPiece('b', '8', new Rook(Board, PieceColor.Black));
        PlaceNewPiece('h', '7', new Rook(Board, PieceColor.White));
        PlaceNewPiece('d', '1', new King(Board, PieceColor.White));
        PlaceNewPiece('c', '1', new Rook(Board, PieceColor.White));
    }
}
