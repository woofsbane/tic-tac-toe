using TicTacToe.Core;
using TicTacToe.SourceGenerators;

namespace TicTacToe.Players.Minimax
{
	public class MinimaxSourceGenerated(Player player) : IPlayer
	{
		public Move GetMove(Board board)
		{
            return board
                .ValidMoves
                .MaxBy(move => PrecomputedMemos.GetValue(player, board.MovePlayer(move, player)))!;
		}
	}
}
