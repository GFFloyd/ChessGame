using Chess.Entities.ChessBoard;

namespace Chess.Entities.GameLogic;

internal class Knight : Piece
{
    public Knight(Board board, PieceColor color) : base(board, color)
    {
    }
    public override string ToString()
    {
        return "N";
    }
    public override bool[,] PossibleMovements()
    {
        bool[,] possibleMovesArray = new bool[ChessBoard.Row, ChessBoard.Column];

        Position position = new(0, 0);

        for (int i = -2; i <= 2; i++)
        {
            for (int j = -2; j <= 2; j++)
            {
                if ((i != 0 && j != 0) && (i != j && i != -j))
                {
                    position.DefineValues(i + Position.Row, j + Position.Column);
                    if (ChessBoard.IsItAValidPosition(position) && CanMove(position))
                    {
                        possibleMovesArray[position.Row, position.Column] = true;
                    }
                }
            }
        }
        return possibleMovesArray;
    }
}
