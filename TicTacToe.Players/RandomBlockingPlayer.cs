using TicTacToe.Core;

namespace TicTacToe.Players
{
	public class RandomBlockingPlayer(Player player) : IPlayer
	{
		private readonly RandomPlayer _randomPlayer = new();

		public (Row row, Column column) GetMove(Board board)
		{
			var opponent = player == Player.X ? Player.O : Player.X;
			var validMoves = board.GetValidMoves();

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

			return _randomPlayer.GetMove(board);
		}
	}
}

