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
    public Piece? EnPassantVulnerability { get; private set; }

    public ChessMatch()
    {
        Board = new Board(8, 8);
        Turn = 1;
        CurrentPlayer = PieceColor.White;
        IsFinished = false;
        CapturedPieces = new HashSet<Piece>();
        PiecesInPlay = new HashSet<Piece>();
        Check = false;
        EnPassantVulnerability = null;
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
        //Small Castling Move
        if (piece is King && target.Column == origin.Column + 2)
        {
            Position rookOrigin = new(origin.Row, origin.Column + 3);
            Position rookTarget = new(origin.Row, origin.Column + 1);
            Piece rook = Board.TakePiece(rookOrigin);
            rook.IncrementMovimentQuantity();
            Board.PlacePiece(rook, rookTarget);
        }
        //Long Castling Move
        if (piece is King && target.Column == origin.Column - 2)
        {
            Position rookOrigin = new(origin.Row, origin.Column - 4);
            Position rookTarget = new(origin.Row, origin.Column - 1);
            Piece rook = Board.TakePiece(rookOrigin);
            rook.IncrementMovimentQuantity();
            Board.PlacePiece(rook, rookTarget);
        }
        //En Passant Move
        if (piece is Pawn)
        {
            if (origin.Column != target.Column && capturedPiece == null)
            {
                Position pawnPosition;
                if (piece.Color == PieceColor.White)
                {
                    pawnPosition = new(target.Row + 1, target.Column);
                }
                else
                {
                    pawnPosition = new(target.Row - 1, target.Column);
                }
                capturedPiece = Board.TakePiece(pawnPosition);
                CapturedPieces.Add(capturedPiece);
            }
        }
        return capturedPiece;
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
        //Small Castling Undo
        if (piece is King && target.Column == origin.Column + 2)
        {
            Position rookOrigin = new(origin.Row, origin.Column + 3);
            Position rookTarget = new(origin.Row, origin.Column + 1);
            Piece rook = Board.TakePiece(rookTarget);
            rook.DecrementMovementQuantity();
            Board.PlacePiece(rook, rookOrigin);
        }
        //Long Castling Undo
        if (piece is King && target.Column == origin.Column - 2)
        {
            Position rookOrigin = new(origin.Row, origin.Column - 4);
            Position rookTarget = new(origin.Row, origin.Column - 1);
            Piece rook = Board.TakePiece(rookTarget);
            rook.DecrementMovementQuantity();
            Board.PlacePiece(rook, rookOrigin);
        }
        //En Passant Undo
        if (piece is Pawn)
        {
            if (origin.Column != target.Column && capturedPiece != EnPassantVulnerability)
            {
                Piece pawn = Board.TakePiece(target);
                Position pawnPosition;
                if (piece.Color == PieceColor.White)
                {
                    pawnPosition = new(3, target.Column);
                }
                else
                {
                    pawnPosition = new(4, target.Column);
                }
                Board.PlacePiece(pawn, pawnPosition);
            }
        }
    }
    public void GamePlay(Position origin, Position target)
    {
        Piece capturedPiece = MakeMove(origin, target);
        if (IsTheKingInCheck(CurrentPlayer))
        {
            UndoMove(origin, target, capturedPiece);
            throw new BoardException("You can't put your king in check");
        }
        Piece piece = Board.Piece(target);

        //Pawn promotion
        if (piece is Pawn)
        {
            if ((piece.Color == PieceColor.White && target.Row == 0) || (piece.Color == PieceColor.Black && target.Row == 7))
            {
                piece = Board.TakePiece(target);
                PiecesInPlay.Remove(piece);
                PawnPromotion(piece.Color, target);
            }
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
        //En passant move
        if (piece is Pawn && (target.Row == origin.Row - 2 || target.Row == origin.Row + 2))
        {
            EnPassantVulnerability = piece;
        }
        else
        {
            EnPassantVulnerability = null;
        }
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
        if (!Board.Piece(origin).CheckIfItCanMoveTo(target))
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
        //White pieces
        PlaceNewPiece('a', '1', new Rook(Board, PieceColor.White));
        PlaceNewPiece('b', '1', new Knight(Board, PieceColor.White));
        PlaceNewPiece('c', '1', new Bishop(Board, PieceColor.White));
        PlaceNewPiece('d', '1', new Queen(Board, PieceColor.White));
        PlaceNewPiece('e', '1', new King(Board, PieceColor.White, this));
        PlaceNewPiece('f', '1', new Bishop(Board, PieceColor.White));
        PlaceNewPiece('g', '1', new Knight(Board, PieceColor.White));
        PlaceNewPiece('h', '1', new Rook(Board, PieceColor.White));
        for (char i = 'a'; i <= 'h'; i++)
        {
            PlaceNewPiece(i, '2', new Pawn(Board, PieceColor.White, this));
        }
        //Black pieces
        PlaceNewPiece('a', '8', new Rook(Board, PieceColor.Black));
        PlaceNewPiece('b', '8', new Knight(Board, PieceColor.Black));
        PlaceNewPiece('c', '8', new Bishop(Board, PieceColor.Black));
        PlaceNewPiece('d', '8', new Queen(Board, PieceColor.Black));
        PlaceNewPiece('e', '8', new King(Board, PieceColor.Black, this));
        PlaceNewPiece('f', '8', new Bishop(Board, PieceColor.Black));
        PlaceNewPiece('g', '8', new Knight(Board, PieceColor.Black));
        PlaceNewPiece('h', '8', new Rook(Board, PieceColor.Black));
        for (char i = 'a'; i <= 'h'; i++)
        {
            PlaceNewPiece(i, '7', new Pawn(Board, PieceColor.Black, this));
        }
    }
    private Piece PawnPromotion(PieceColor color, Position target)
    {
        Console.WriteLine("Choose which piece you want the pawn to promote to:");
        Console.WriteLine("Q for Queen, B for Bishop, K for Knight or R for Rook");
        string str = Console.ReadLine().ToUpper();
        Piece piece;
        switch (str)
        {
            case "Q":
                piece = new Queen(Board, color);
                break;
            case "B":
                piece = new Bishop(Board, color);
                break;
            case "K":
                piece = new Knight(Board, color);
                break;
            case "R":
                piece = new Rook(Board, color);
                break;
            default:
                throw new BoardException("Enter a valid character");
        }
        Board.PlacePiece(piece, target);
        PiecesInPlay.Add(piece);
        return piece;
    }
}
