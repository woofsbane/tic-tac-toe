using TicTacToe.Core;
using TicTacToe.SourceGenerators;

namespace TicTacToe.Players.Minimax
{
	public partial class MinimaxSourceGenerated(Player player) : IPlayer
	{
		public (Row row, Column column) GetMove(Board board)
		{
            return board
                .GetValidMoves()
                .MaxBy(move => GetValue(board.Move(move.row, move.column, player)));
		}

		private int GetValue(Board board)
		{
            var memoTable = player == Player.X ? PrecomputedMemoTable.xMemos : PrecomputedMemoTable.oMemos;

            if (memoTable.TryGetValue((board.GetCanonicalHashCode(), false), out int value))
			{
				return value;
			}

			return -1;
		}
	}
}
