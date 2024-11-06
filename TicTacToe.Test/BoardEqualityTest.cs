using TicTacToe.Core;

namespace TicTacToe.Test
{
	public class BoardEqualityTest
	{
		[Fact]
		public void EmptyBoards_AreEqual()
		{
			var board1 = Board.Empty;
			var board2 = Board.Empty;

			Assert.Equal(board1, board2);
		}

		[Fact]
		public void DifferentBoards_AreNotEqual()
		{
			var board1 = Board.Empty;
			var board2 = BoardBuilder
				.With(Player.O, Player.X, Player.X)
				.With(Player.X, Player.X, Player.O)
				.With(Player.O, Player.O, Player.X);

			Assert.NotEqual(board1, board2);
		}

		[Fact]
		public void SameBoards_AreEqual()
		{
			var board1 = BoardBuilder
				.With(Player.O, Player.X, Player.X)
				.With(Player.X, Player.X, Player.O)
				.With(Player.O, Player.O, Player.X);

			var board2 = BoardBuilder
				.With(Player.O, Player.X, Player.X)
				.With(Player.X, Player.X, Player.O)
				.With(Player.O, Player.O, Player.X);

			Assert.Equal(board1, board2);
		}

		[Fact]
		public void DifferentBoards_HaveDifferentHashCodes()
		{
			var board1 = Board.Empty;
			var board2 = BoardBuilder
				.With(Player.O, Player.X, Player.X)
				.With(Player.X, Player.X, Player.O)
				.With(Player.O, Player.O, Player.X);

			Assert.NotEqual(board1.GetHashCode(), board2.GetHashCode());
		}

		[Fact]
		public void SameBoards_HaveSameHashCodes()
		{
			var board1 = BoardBuilder
				.With(Player.O, Player.X, Player.X)
				.With(Player.X, Player.X, Player.O)
				.With(Player.O, Player.O, Player.X);

			var board2 = BoardBuilder
				.With(Player.O, Player.X, Player.X)
				.With(Player.X, Player.X, Player.O)
				.With(Player.O, Player.O, Player.X);

			Assert.Equal(board1.GetHashCode(), board2.GetHashCode());
		}
	}
}
