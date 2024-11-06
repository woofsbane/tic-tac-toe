using TicTacToe.Core;

namespace TicTacToe.Players
{
	public class RandomBlockingWinningPlayer(Player player) : IPlayer
	{
		private readonly RandomPlayer _randomPlayer = new();

		public (Row row, Column column) GetMove(Board board)
		{
			var validMoves = board.GetValidMoves();

			// Check for a winning move
			foreach (var move in validMoves)
			{
				var simulatedBoard = board.Move(move.row, move.column, player);
				if (simulatedBoard.Winner == player)
				{
					return move;
				}
			}

			// Check for a blocking move
			var opponent = player == Player.X ? Player.O : Player.X;
			foreach (var move in validMoves)
			{
				var simulatedBoard = board.Move(move.row, move.column, player);
				var opponentMoves = simulatedBoard.GetValidMoves();

				foreach (var opponentMove in opponentMoves)
				{
					var opponentSimulatedBoard = simulatedBoard.Move(opponentMove.row, opponentMove.column, opponent);
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
