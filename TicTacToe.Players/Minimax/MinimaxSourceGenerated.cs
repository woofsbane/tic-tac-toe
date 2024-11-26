using TicTacToe.Core;
using TicTacToe.SourceGenerators;

namespace TicTacToe.Players.Minimax
{
    public class MinimaxSourceGenerated(Player player) : IPlayer
    {
        public Move GetMove(Board board)
        {
            var nextBestIndex = 0;

            for (int i = 0; i < board.ValidMoves.Count; i++)
            {
                var move = board.ValidMoves[i];
                var value = PrecomputedMemos.GetValue(player, board.MovePlayer(move, player));

                if (value == 1) return move;
                if (value == 0) nextBestIndex = i;
            }

            return board.ValidMoves[nextBestIndex];
        }
    }
}
