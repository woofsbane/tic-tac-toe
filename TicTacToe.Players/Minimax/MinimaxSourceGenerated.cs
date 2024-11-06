using TicTacToe.Core;
using TicTacToe.SourceGenerators;

namespace TicTacToe.Players.Minimax
{
	public class MinimaxSourceGenerated(Player player) : IPlayer
	{
		public (Row row, Column column) GetMove(Board board)
		{
            return board
                .GetValidMoves()
                .MaxBy(move => PrecomputedMemos.GetValue(player, board.Move(move.row, move.column, player)));
		}
	}
}
