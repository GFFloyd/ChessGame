﻿using Chess.Entities.ChessBoard;
using Chess.Entities.GameLogic;

namespace Chess;

internal class Program
{
    static void Main(string[] args)
    {
        try
        {
            ChessMatch match = new ChessMatch();
            while (!match.IsFinished)
            {
                try
                {
                    Console.Clear();
                    Screen.PrintMatch(match);
                    Console.Write("Origin: ");
                    Position origin = Screen.ReadPosition().ToPosition();
                    match.ValidateOriginPosition(origin);


                    bool[,] possibleMoves = match.Board.Piece(origin).PossibleMovements();
                    Console.Clear();
                    Screen.PrintBoard(match.Board, possibleMoves);

                    Console.Write("Target: ");
                    Position target = Screen.ReadPosition().ToPosition();
                    match.ValidateTargetPosition(origin, target);

                    match.GamePlay(origin, target);
                }
                catch (BoardException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
            Console.Clear();
            Screen.PrintMatch(match);
        }
        catch (BoardException e)
        {
            Console.WriteLine(e.Message);
        }


    }
}