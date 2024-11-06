using TicTacToe.Core;

namespace TicTacToe.Players
{
	public class RandomWinningPlayer(Player player) : IPlayer
	{
		private readonly RandomPlayer _randomPlayer = new();

		public (Row row, Column column) GetMove(Board board)
		{
			var validMoves = board.GetValidMoves();

			foreach (var move in validMoves)
			{
				var simulatedBoard = board.Move(move.row, move.column, player);
				if (simulatedBoard.Winner == player)
				{
					return move;
				}
			}

			return _randomPlayer.GetMove(board);
		}
	}
}
