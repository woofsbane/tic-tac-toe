using System;
using System.Collections.Generic;
using TicTacToe.Core;

namespace TicTacToe.SourceGenerators
{
    public class MinimaxMemoizedVariants(Player player)
    {
        private readonly Player opponent = player == Player.X ? Player.O : Player.X;

        public readonly Dictionary<(int, bool), int> memos = [];

        public void BuildMemos(Board board)
        {
            var validMoves = board.GetValidMoves();

            foreach (var move in validMoves)
            {
                Memoize(board.Move(move.row, move.column, player), false);
            }
        }

        private int Minimax(Board board, bool isMaximizing)
        {
            var validMoves = board.GetValidMoves();

            if (board.Winner == player)
                return 1;
            if (board.Winner == opponent)
                return -1;
            if (validMoves.Count == 0)
                return 0;

            if (isMaximizing)
            {
                var bestValue = int.MinValue;
                foreach (var (row, column) in validMoves)
                {
                    var simulatedBoard = board.Move(row, column, player);
                    var moveValue = Memoize(simulatedBoard, false);
                    bestValue = Math.Max(bestValue, moveValue);
                }
                return bestValue;
            }
            else
            {
                var bestValue = int.MaxValue;
                foreach (var (row, column) in validMoves)
                {
                    var simulatedBoard = board.Move(row, column, opponent);
                    var moveValue = Memoize(simulatedBoard, true);
                    bestValue = Math.Min(bestValue, moveValue);
                }
                return bestValue;
            }
        }

        private int Memoize(Board board, bool isMaximizing)
        {
            if (memos.TryGetValue((board.GetCanonicalHashCode(), isMaximizing), out int value))
            {
                return value;
            }

            var computedValue = Minimax(board, isMaximizing);

            memos[(board.GetCanonicalHashCode(), isMaximizing)] = computedValue;

            return computedValue;
        }
    }
}
