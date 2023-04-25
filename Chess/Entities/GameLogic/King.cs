using Chess.Entities.ChessBoard;

namespace Chess.Entities.GameLogic;

internal class King : Piece
{
    private ChessMatch _chessMatch;

    public King(Board board, PieceColor color, ChessMatch chessMatch) : base(board, color)
    {
        _chessMatch = chessMatch;
    }
    public override string ToString()
    {
        return "K";
    }
    private bool CastlingCheck(Position position)
    {
        Piece piece = ChessBoard.Piece(position);
        return piece != null && piece is Rook && piece.Color == Color && Movements == 0;
    }
    public override bool[,] PossibleMovements()
    {
        bool[,] possibleMovesArray = new bool[ChessBoard.Row, ChessBoard.Column];

        Position position = new(0, 0);
        //Algorithm to check if a king can move to its adjacent squares:

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                position.DefineValues(i + Position.Row, j + Position.Column);
                if (ChessBoard.IsItAValidPosition(position) && CanMove(position))
                {
                    possibleMovesArray[position.Row, position.Column] = true;
                }
            }
        }
        //Small Castling Logic
        if (Movements == 0 && !_chessMatch.Check)
        {
            Position castlingPosition = new(Position.Row, Position.Column + 3);
            if (CastlingCheck(castlingPosition))
            {
                Position firstPosition = new(Position.Row, Position.Column + 1);
                Position secondPosition = new(Position.Row, Position.Column + 2);
                if (ChessBoard.Piece(firstPosition) == null && ChessBoard.Piece(secondPosition) == null)
                {
                    possibleMovesArray[Position.Row, Position.Column + 2] = true;
                }
            }
            //Long Castling Logic   
            Position longCastle = new(Position.Row, Position.Column - 4); 
            if (CastlingCheck(longCastle))
            {
                Position firstPosition = new(Position.Row, Position.Column - 1);
                Position secondPosition = new(Position.Row, Position.Column - 2);
                Position thirdPosition = new(Position.Row, Position.Column - 3);
                if (ChessBoard.Piece(firstPosition) == null && ChessBoard.Piece(secondPosition) == null && ChessBoard.Piece(thirdPosition) == null)
                {
                    possibleMovesArray[Position.Row, Position.Column - 2] = true;
                }
            }
        }
        return possibleMovesArray;
    }
}
