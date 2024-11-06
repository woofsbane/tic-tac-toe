using TicTacToe.Core;

namespace TicTacToe.Test
{
	public class BoardWinTest
	{
		[Fact]
		public void IncompleteBoard_ReturnsNull()
		{
			var board = BoardBuilder
				.With(Player.X, Player.O, Player.X)
				.With(Player._, Player._, Player._)
				.With(Player._, Player._, Player._);

			Assert.Null(board.Winner);
		}
		[Fact]
		public void Tie_ReturnsEmpty()
		{
			var board = BoardBuilder
				.With(Player.X, Player.O, Player.X)
				.With(Player.X, Player.X, Player.O)
				.With(Player.O, Player.X, Player.O);

			Assert.Equal(Player._, board.Winner);
		}

		[Fact]
		public void XWinsHorizontally_ReturnsX()
		{
			var board = BoardBuilder
				.With(Player.X, Player.X, Player.X)
				.With(Player.O, Player.O, Player._)
				.With(Player._, Player._, Player._);

			Assert.Equal(Player.X, board.Winner);
		}

		[Fact]
		public void OWinsVertically_ReturnsO()
		{
			var board = BoardBuilder
				.With(Player.O, Player.X, Player.X)
				.With(Player.O, Player.X, Player._)
				.With(Player.O, Player._, Player._);

			Assert.Equal(Player.O, board.Winner);
		}

		[Fact]
		public void XWinsDiagonally_ReturnsX()
		{
			var board = BoardBuilder
				.With(Player.X, Player.O, Player._)
				.With(Player.O, Player.X, Player._)
				.With(Player._, Player._, Player.X);

			Assert.Equal(Player.X, board.Winner);
		}

		[Fact]
		public void OWinsDiagonally_ReturnsO()
		{
			var board = BoardBuilder
				.With(Player.O, Player.X, Player.X)
				.With(Player.X, Player.O, Player._)
				.With(Player._, Player._, Player.O);

			Assert.Equal(Player.O, board.Winner);
		}
	}
}
