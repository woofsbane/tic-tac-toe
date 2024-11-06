using TicTacToe.Core;
using TicTacToe.Stdout;

namespace TicTacToe.Test
{
	public class BoardPrinterTest
	{
		[Fact]
		public void Prints_EmptyBoard()
		{
			var printer = new BoardPrinter();
			var board = Board.Empty;

			var printedBoard = printer.Print(board);

			Assert.Equal(
				"  0 1 2" + Environment.NewLine +
				"0  | | " + Environment.NewLine +
				"  -----" + Environment.NewLine +
				"1  | | " + Environment.NewLine +
				"  -----" + Environment.NewLine +
				"2  | | " + Environment.NewLine,
				printedBoard
			);
		}

		[Fact]
		public void Prints_FirstMove()
		{
			var printer = new BoardPrinter();
			var board = BoardBuilder
				.With(Player._, Player._, Player._)
				.With(Player._, Player.X, Player._)
				.With(Player._, Player._, Player._);

			var printedBoard = printer.Print(board);

			Assert.Equal(
				"  0 1 2" + Environment.NewLine +
				"0  | | " + Environment.NewLine +
				"  -----" + Environment.NewLine +
				"1  |X| " + Environment.NewLine +
				"  -----" + Environment.NewLine +
				"2  | | " + Environment.NewLine,
				printedBoard
			);
		}

		[Fact]
		public void Prints_AllSquares()
		{
			var printer = new BoardPrinter();
			var board = BoardBuilder
				.With(Player.O, Player.X, Player.X)
				.With(Player.X, Player.X, Player.O)
				.With(Player.O, Player.O, Player.X);

			var printedBoard = printer.Print(board);

			Assert.Equal(
				"  0 1 2" + Environment.NewLine +
				"0 O|X|X" + Environment.NewLine +
				"  -----" + Environment.NewLine +
				"1 X|X|O" + Environment.NewLine +
				"  -----" + Environment.NewLine +
				"2 O|O|X" + Environment.NewLine,
				printedBoard
			);
		}
	}
}