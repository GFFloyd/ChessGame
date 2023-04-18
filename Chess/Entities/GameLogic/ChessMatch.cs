using Chess.Entities.ChessBoard;

namespace Chess.Entities.GameLogic;

internal class ChessMatch
{
    public Board Board { get; set; }
    private readonly int Turn;
    private readonly PieceColor CurrentPlayer;
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
    public void StartingPosition()
    {
        //TODO: instanciate initial pieces into their respective places
        Board.PlacePiece(new Rook(Board, PieceColor.White), new Position(0, 0));
        Board.PlacePiece(new King(Board, PieceColor.White), new Position(3, 5));
        Board.PlacePiece(new Rook(Board, PieceColor.White), new Position(0, 2));
        Board.PlacePiece(new King(Board, PieceColor.Black), new Position(3, 3));
    }
}
