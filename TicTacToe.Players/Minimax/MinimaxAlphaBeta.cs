using TicTacToe.Core;

namespace TicTacToe.Players.Minimax
{
	public class MinimaxAlphaBeta(Player player) : IPlayer
	{
		private readonly Player opponent = player == Player.X ? Player.O : Player.X;

		public Move GetMove(Board board)
		{
			var validMoves = board.ValidMoves;
			var bestMove = validMoves.First();
			var bestValue = int.MinValue;

			foreach (var move in validMoves)
			{
				var simulatedBoard = board.MovePlayer(move, player);
				var moveValue = Minimax(simulatedBoard, false, int.MinValue, int.MaxValue);

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
			var validMoves = board.ValidMoves;

			if (board.Winner == player)
				return 1;
			if (board.Winner == opponent)
				return -1;
			if (validMoves.Count == 0)
				return 0;

			if (isMaximizing)
			{
				var bestValue = int.MinValue;
				foreach (var move in validMoves)
				{
					var simulatedBoard = board.MovePlayer(move, player);
					var moveValue = Minimax(simulatedBoard, false, alpha, beta);
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
				foreach (var move in validMoves)
				{
					var simulatedBoard = board.MovePlayer(move, opponent);
					var moveValue = Minimax(simulatedBoard, true, alpha, beta);
					bestValue = Math.Min(bestValue, moveValue);
					beta = Math.Min(beta, moveValue);
					if (beta <= alpha)
						break; // Alpha cut-off
				}
				return bestValue;
			}
		}
	}
}
