using TicTacToe.Core;

namespace TicTacToe.Players
{
	public class RandomBlockingWinningPlayer(Player player) : IPlayer
	{
		private readonly RandomPlayer _randomPlayer = new();

		public Move GetMove(Board board)
		{
			var validMoves = board.ValidMoves;

			// Check for a winning move
			foreach (var move in validMoves)
			{
				var simulatedBoard = board.MovePlayer(move, player);
				if (simulatedBoard.Winner == player)
				{
					return move;
				}
			}

			// Check for a blocking move
			var opponent = player == Player.X ? Player.O : Player.X;
			foreach (var move in validMoves)
			{
				var simulatedBoard = board.MovePlayer(move, player);
				var opponentMoves = simulatedBoard.ValidMoves;

				foreach (var opponentMove in opponentMoves)
				{
					var opponentSimulatedBoard = simulatedBoard.MovePlayer(opponentMove, opponent);
					if (opponentSimulatedBoard.Winner == opponent)
					{
						return opponentMove;
					}
				}
			}

			// If no winning or blocking move, return a random move
			return _randomPlayer.GetMove(board);
		}
	}
}
