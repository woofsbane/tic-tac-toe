using TicTacToe.Core;

namespace TicTacToe.Players
{
	public class RandomWinningPlayer(Player player) : IPlayer
	{
		private readonly RandomPlayer _randomPlayer = new();

		public Move GetMove(Board board)
		{
			var validMoves = board.ValidMoves;

			foreach (var move in validMoves)
			{
				var simulatedBoard = board.MovePlayer(move, player);
				if (simulatedBoard.Winner == player)
				{
					return move;
				}
			}

			return _randomPlayer.GetMove(board);
		}
	}
}
