using TicTacToe.Core;

namespace TicTacToe.Players
{
	public class RandomBlockingPlayer(Player player) : IPlayer
	{
		private readonly RandomPlayer _randomPlayer = new();

		public Move GetMove(Board board)
		{
			var opponent = player == Player.X ? Player.O : Player.X;
			var validMoves = board.ValidMoves;

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

			return _randomPlayer.GetMove(board);
		}
	}
}

