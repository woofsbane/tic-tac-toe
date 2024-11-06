using TicTacToe.Core;

namespace TicTacToe.Players.Minimax
{
	public class MinimaxAlphaBetaMemoizedVariants(Player player) : IPlayer
	{
		private readonly Player opponent = player == Player.X ? Player.O : Player.X;

		private readonly Dictionary<(int, bool), int> memos = [];

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

		private int Minimax(Board board, bool isMaximizing, int alpha, int beta)
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
					var moveValue = Memoize(simulatedBoard, false, alpha, beta);
					bestValue = Math.Max(bestValue, moveValue);
					alpha = Math.Max(alpha, moveValue);
					if (beta <= alpha)
						break; // Beta cut-off
				}
				return bestValue;
			}
			else
			{
				var bestValue = int.MaxValue;
				foreach (var (row, column) in validMoves)
				{
					var simulatedBoard = board.Move(row, column, opponent);
					var moveValue = Memoize(simulatedBoard, true, alpha, beta);
					bestValue = Math.Min(bestValue, moveValue);
					beta = Math.Min(beta, moveValue);
					if (beta <= alpha)
						break; // Alpha cut-off
				}
				return bestValue;
			}
		}

		private int Memoize(Board board, bool isMaximizing, int alpha, int beta)
		{
			var canonicalHash = board.GetCanonicalHashCode();

			if (memos.TryGetValue((canonicalHash, isMaximizing), out int value))
			{
				return value;
			}

			var computedValue = Minimax(board, isMaximizing, alpha, beta);

			memos[(canonicalHash, isMaximizing)] = computedValue;

			return computedValue;
		}
	}
}
