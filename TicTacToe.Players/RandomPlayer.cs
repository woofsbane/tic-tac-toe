using TicTacToe.Core;

namespace TicTacToe.Players
{
	public class RandomPlayer : IPlayer
	{
		private static readonly Random _random = new();

		public Move GetMove(Board board)
		{
			var validMoves = board.ValidMoves;
			if (validMoves.Count == 0)
			{
				throw new InvalidOperationException("No valid moves available.");
			}

			var randomMove = validMoves[_random.Next(validMoves.Count)];
			return randomMove;
		}
	}
}
