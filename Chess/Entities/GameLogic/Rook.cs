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
        bool[,] possibleMovesArray = new bool[ChessBoard.Row, ChessBoard.Column];

        Position position = new Position(0, 0);


        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i != j && i != -j)
                {
                    position.DefineValues(Position.Row + i, Position.Column + j);
                    while (ChessBoard.ValidPosition(position) && CanMove(position))
                    {
                        possibleMovesArray[position.Row, position.Column] = true;
                        //if there's a piece in a square or if it's an oppposite color piece, the loop breaks
                        if (ChessBoard.Piece(position) != null && ChessBoard.Piece(position).Color != Color)
                        {
                            break;
                        }
                        if (i < 0 && j == 0)
                        {
                            position.Row--;
                        }
                        else if (i > 0 && j == 0)
                        {
                            position.Row++;
                        }
                        else if (i == 0 && j < 0)
                        {
                            position.Column--;
                        }
                        else
                        {
                            position.Column++;
                        }
                    }
                }
            }
        }
        return possibleMovesArray;
    }
}
