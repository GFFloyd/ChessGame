using Chess.Entities.ChessBoard;

namespace Chess.Entities.GameLogic;

internal class ChessMatch
{
    public Board Board { get; private set; }
    public int Turn { get; private set; }
    public PieceColor CurrentPlayer { get; private set; }
    public bool IsFinished { get; private set; }

    public ChessMatch()
    {
        Board = new Board(8, 8);
        Turn = 1;
        CurrentPlayer = PieceColor.White;
        IsFinished = false;
        StartingPosition();
    }
    public void MakeMove(Position origin, Position target)
    {
        //Piece movement logic, capturedPiece variable should be stored in a "captured pieces list"
        Piece piece = Board.TakePiece(origin);
        piece.IncrementMovimentQuantity();
        Piece capturedPiece = Board.TakePiece(target);
        Board.PlacePiece(piece, target);
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
    public void StartingPosition()
    {
        //TODO: instanciate initial pieces into their respective places
        Board.PlacePiece(new Rook(Board, PieceColor.White), new Position(0, 0));
        Board.PlacePiece(new King(Board, PieceColor.White), new Position(3, 5));
        Board.PlacePiece(new Rook(Board, PieceColor.White), new Position(0, 2));
        Board.PlacePiece(new King(Board, PieceColor.Black), new Position(3, 3));
    }
}
