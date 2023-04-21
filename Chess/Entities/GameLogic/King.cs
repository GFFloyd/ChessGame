using Chess.Entities.ChessBoard;

namespace Chess.Entities.GameLogic;

internal class King : Piece
{

    public King(Board board, PieceColor color) : base(board, color)
    {
    }
    public override string ToString()
    {
        return "K";
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
        return possibleMovesArray;
    }
}
