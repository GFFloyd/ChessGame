using Chess.Entities.ChessBoard;

namespace Chess.Entities.GameLogic;

internal class Pawn : Piece
{
    private readonly ChessMatch _chessMatch;
    public Pawn(Board board, PieceColor color, ChessMatch chessMatch) : base(board, color)
    {
        _chessMatch = chessMatch;
    }
    public override string ToString()
    {
        return "P";
    }
    private bool ThereIsOpponentPiece(Position position)
    {
        Piece piece = ChessBoard.Piece(position);
        return piece != null && piece.Color != Color;
    }
    private bool FreeMovement(Position position)
    {
        return ChessBoard.Piece(position) == null;
    }
    public override bool[,] PossibleMovements()
    {
        bool[,] possibleMovesArray = new bool[ChessBoard.Row, ChessBoard.Column];

        Position position = new(0, 0);

        if (Color == PieceColor.White)
        {
            position.DefineValues(Position.Row - 1, Position.Column);
            if (ChessBoard.IsItAValidPosition(position) && FreeMovement(position))
            {
                possibleMovesArray[position.Row, position.Column] = true;
            }
            position.DefineValues(Position.Row - 2, Position.Column);
            Position secondPosition = new Position(position.Row - 1, position.Column);
            if (ChessBoard.IsItAValidPosition(secondPosition) && FreeMovement(secondPosition) && FreeMovement(position) && Movements == 0)
            {
                possibleMovesArray[position.Row, position.Column] = true;
            }
            position.DefineValues(Position.Row - 1, Position.Column - 1);
            if (ChessBoard.IsItAValidPosition(position) && ThereIsOpponentPiece(position))
            {
                possibleMovesArray[position.Row, position.Column] = true;
            }
            position.DefineValues(Position.Row - 1, Position.Column + 1);
            if (ChessBoard.IsItAValidPosition(position) && ThereIsOpponentPiece(position))
            {
                possibleMovesArray[position.Row, position.Column] = true;
            }
            //En Passant logic for white
            if (Position.Row == 3)
            //This looks like a magic number, but when you're playing white;
            //the only row that you can make an en passant move is the third
            //(in the boolean 2d array) in algebraic notation is the 5th row.
            {
                Position left = new(Position.Row, Position.Column - 1);
                if (ChessBoard.IsItAValidPosition(left) && ThereIsOpponentPiece(left) && ChessBoard.Piece(left) == _chessMatch.EnPassantVulnerability)
                {
                    possibleMovesArray[left.Row - 1, left.Column] = true;
                }
                Position right = new(Position.Row, Position.Column + 1);
                if (ChessBoard.IsItAValidPosition(right) && ThereIsOpponentPiece(right) && ChessBoard.Piece(right) == _chessMatch.EnPassantVulnerability)
                {
                    possibleMovesArray[right.Row - 1, right.Column] = true;
                }
            }
        }
        else
        {
            position.DefineValues(Position.Row + 1, Position.Column);
            if (ChessBoard.IsItAValidPosition(position) && FreeMovement(position))
            {
                possibleMovesArray[position.Row, position.Column] = true;
            }
            position.DefineValues(Position.Row + 2, Position.Column);
            Position secondPosition = new Position(position.Row - 1, position.Column);
            if (ChessBoard.IsItAValidPosition(secondPosition) && FreeMovement(secondPosition) && FreeMovement(position) && Movements == 0)
            {
                possibleMovesArray[position.Row, position.Column] = true;
            }
            position.DefineValues(Position.Row + 1, Position.Column - 1);
            if (ChessBoard.IsItAValidPosition(position) && ThereIsOpponentPiece(position))
            {
                possibleMovesArray[position.Row, position.Column] = true;
            }
            position.DefineValues(Position.Row + 1, Position.Column + 1);
            if (ChessBoard.IsItAValidPosition(position) && ThereIsOpponentPiece(position))
            {
                possibleMovesArray[position.Row, position.Column] = true;
            }
            //En Passant Logic for black
            if (Position.Row == 4)
            //Black's en passant can only happen in the fourth row of the boolean 2d array
            //or the fourth row in algebraic notation
            {
                Position left = new(Position.Row, Position.Column - 1);
                if (ChessBoard.IsItAValidPosition(left) && ThereIsOpponentPiece(left) && ChessBoard.Piece(left) == _chessMatch.EnPassantVulnerability)
                {
                    possibleMovesArray[left.Row + 1, left.Column] = true;
                }
                Position right = new(Position.Row, Position.Column + 1);
                if (ChessBoard.IsItAValidPosition(right) && ThereIsOpponentPiece(right) && ChessBoard.Piece(right) == _chessMatch.EnPassantVulnerability)
                {
                    possibleMovesArray[right.Row + 1, right.Column] = true;
                }
            }
        }
        return possibleMovesArray;
    }
}
