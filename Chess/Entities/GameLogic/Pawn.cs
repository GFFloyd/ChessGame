using Chess.Entities.ChessBoard;

namespace Chess.Entities.GameLogic;

internal class Pawn : Piece
{
    public Pawn(Board board, PieceColor color) : base(board, color)
    {
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
        }
        return possibleMovesArray;
    }
}
