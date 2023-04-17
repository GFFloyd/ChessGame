using Chess.Entities.ChessBoard;

namespace Chess.Entities.GameLogic;

internal class Rook : Piece
{
    public Rook(Board board, PieceColor color) : base(board, color)
    {
    }
    public override string ToString()
    {
        return "R";
    }
    public override bool[,] PossibleMovements()
    {
        bool[,] booleanArray = new bool[ChessBoard.Row, ChessBoard.Column];

        Position position = new(0, 0);


        for (int i = -1; i % 2 != 0 && i <= 1; i++)
        {
            for (int j = -1; j % 2 != 0 && j <= 1; j++)
            {
                if (i != j)
                {
                    position.DefineValues((byte)(ChessBoard.Row + i), (byte)(ChessBoard.Column + j));
                    while (ChessBoard.ValidPosition(position) && CanMove(position))
                    {
                        booleanArray[position.Row, position.Column] = true;
                        if (ChessBoard.Piece(position) != null && ChessBoard.Piece(position).Color != Color)
                        {
                            break;
                        }
                        if (i < 0 && j == 0)
                        {
                            position.Row -= 1;
                        }
                        else if (i > 0 && j == 0)
                        {
                            position.Row += 1;
                        }
                        else if (i == 0 && j < 0)
                        {
                            position.Column -= 1;
                        }
                        else
                        {
                            position.Column += 1;
                        }
                    }
                }
            }
        }
        return booleanArray;
    }
}
