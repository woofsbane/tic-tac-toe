using TicTacToe.Core;
using TicTacToe.SourceGenerators;

namespace TicTacToe.Players.Minimax
{
	public partial class MinimaxSourceGenerated(Player player) : IPlayer
	{
		private readonly Player opponent = player == Player.X ? Player.O : Player.X;

		public (Row row, Column column) GetMove(Board board)
		{
			var validMoves = board.GetValidMoves();
			var bestMove = validMoves.First();
			var bestValue = int.MinValue;

			foreach (var move in validMoves)
			{
				var simulatedBoard = board.Move(move.row, move.column, player);
				var moveValue = Memoize(simulatedBoard, false, int.MinValue, int.MaxValue);

				if (moveValue > bestValue)
				{
					bestMove = move;
					bestValue = moveValue;
				}
			}

			return bestMove;
		}

		private int Memoize(Board board, bool isMaximizing, int alpha, int beta)
		{
			var canonicalHash = board.GetCanonicalHashCode();

			var memoTable = player == Player.X ? PrecomputedMemoTable.xMemos : PrecomputedMemoTable.oMemos;

			if (memoTable.TryGetValue((canonicalHash, isMaximizing), out int value))
			{
				return value;
			}

			return -1;
		}
	}
}
