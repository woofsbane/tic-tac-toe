using TicTacToe.Core;

namespace TicTacToe.Players
{
	public class HumanPlayer(Player player) : IPlayer
	{
		public (Row row, Column column) GetMove(Board board)
		{
			Console.WriteLine($"Player {player}, make your move (row and column, e.g. 00): ");
			var input = Console.ReadLine();

			if (TryParseInput(input, out Row row, out Column column))
			{
				return (row, column);
			}

			throw new InvalidOperationException();
		}

		private static bool TryParseInput(string? input, out Row row, out Column column)
		{
			row = Row._1;
			column = Column._1;

			if (string.IsNullOrEmpty(input) || input.Length != 2)
			{
				return false;
			}

			return Enum.TryParse(input[0].ToString(), out row) && Enum.TryParse(input[1].ToString(), out column);
		}
	}
}
