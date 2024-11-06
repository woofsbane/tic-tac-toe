using TicTacToe.Core;

namespace TicTacToe.Test
{
	public class BoardValidMovesTest
	{
		[Fact]
		public void GetValidMoves_EmptyBoard_ReturnsAllMoves()
		{
			var board = Board.Empty;

			var validMoves = board.GetValidMoves();

			Assert.Equal(9, validMoves.Count);
			Assert.Contains((Row._1, Column._1), validMoves);
			Assert.Contains((Row._1, Column._2), validMoves);
			Assert.Contains((Row._1, Column._3), validMoves);
			Assert.Contains((Row._2, Column._1), validMoves);
			Assert.Contains((Row._2, Column._2), validMoves);
			Assert.Contains((Row._2, Column._3), validMoves);
			Assert.Contains((Row._3, Column._1), validMoves);
			Assert.Contains((Row._3, Column._2), validMoves);
			Assert.Contains((Row._3, Column._3), validMoves);
		}

		[Fact]
		public void GetValidMoves_PartiallyFilledBoard_ReturnsRemainingMoves()
		{
			var board = BoardBuilder
				.With(Player.X, Player.O, Player._)
				.With(Player._, Player.X, Player._)
				.With(Player.O, Player._, Player._);

			var validMoves = board.GetValidMoves();

			Assert.Equal(5, validMoves.Count);
			Assert.Contains((Row._1, Column._3), validMoves);
			Assert.Contains((Row._2, Column._1), validMoves);
			Assert.Contains((Row._2, Column._3), validMoves);
			Assert.Contains((Row._3, Column._2), validMoves);
			Assert.Contains((Row._3, Column._3), validMoves);
		}

		[Fact]
		public void GetValidMoves_FullBoard_ReturnsNoMoves()
		{
			var board = BoardBuilder
				.With(Player.X, Player.O, Player.X)
				.With(Player.X, Player.O, Player.O)
				.With(Player.O, Player.X, Player.X);

			var validMoves = board.GetValidMoves();

			Assert.Empty(validMoves);
		}
	}
}
