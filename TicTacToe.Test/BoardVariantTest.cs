using TicTacToe.Core;

namespace TicTacToe.Test
{
	public class BoardVariantTest
	{
		[Fact]
		public void FlippedHorizontalBoards_HaveSameCanonicalHashCode()
		{
			var firstBoard = BoardBuilder
				.With(Player.X, Player.O, Player._)
				.With(Player._, Player.X, Player.O)
				.With(Player._, Player._, Player.X);

			var secondBoard = BoardBuilder
				.With(Player._, Player.O, Player.X)
				.With(Player.O, Player.X, Player._)
				.With(Player.X, Player._, Player._);

			Assert.Equal(firstBoard.GetCanonicalHashCode(), secondBoard.GetCanonicalHashCode());
		}

		[Fact]
		public void FlippedVerticalBoards_HaveSameCanonicalHashCode()
		{
			var firstBoard = BoardBuilder
				.With(Player.X, Player.O, Player._)
				.With(Player._, Player.X, Player.O)
				.With(Player._, Player._, Player.X);

			var secondBoard = BoardBuilder
				.With(Player._, Player._, Player.X)
				.With(Player._, Player.X, Player.O)
				.With(Player.X, Player.O, Player._);

			Assert.Equal(firstBoard.GetCanonicalHashCode(), secondBoard.GetCanonicalHashCode());
		}

		[Fact]
		public void Rotated90Boards_HaveSameCanonicalHashCode()
		{
			var firstBoard = BoardBuilder
				.With(Player.X, Player.O, Player._)
				.With(Player._, Player.X, Player.O)
				.With(Player._, Player._, Player.X);

			var secondBoard = BoardBuilder
				.With(Player._, Player._, Player.X)
				.With(Player._, Player.X, Player.O)
				.With(Player.X, Player.O, Player._);

			Assert.Equal(firstBoard.GetCanonicalHashCode(), secondBoard.GetCanonicalHashCode());
		}

		[Fact]
		public void Rotated180Boards_HaveSameCanonicalHashCode()
		{
			var firstBoard = BoardBuilder
				.With(Player.X, Player.O, Player._)
				.With(Player._, Player.X, Player.O)
				.With(Player._, Player._, Player.X);

			var secondBoard = BoardBuilder
				.With(Player.X, Player._, Player._)
				.With(Player.O, Player.X, Player._)
				.With(Player._, Player.O, Player.X);

			Assert.Equal(firstBoard.GetCanonicalHashCode(), secondBoard.GetCanonicalHashCode());
		}

		[Fact]
		public void Rotated270Boards_HaveSameCanonicalHashCode()
		{
			var firstBoard = BoardBuilder
				.With(Player.X, Player.O, Player._)
				.With(Player._, Player.X, Player.O)
				.With(Player._, Player._, Player.X);

			var secondBoard = BoardBuilder
				.With(Player._, Player.O, Player.X)
				.With(Player.O, Player.X, Player._)
				.With(Player.X, Player._, Player._);

			Assert.Equal(firstBoard.GetCanonicalHashCode(), secondBoard.GetCanonicalHashCode());
		}

		[Fact]
		public void TransposedBoards_HaveSameCanonicalHashCode()
		{
			var firstBoard = BoardBuilder
				.With(Player.X, Player.O, Player._)
				.With(Player._, Player.X, Player.O)
				.With(Player._, Player._, Player.X);

			var secondBoard = BoardBuilder
				.With(Player.X, Player._, Player._)
				.With(Player.O, Player.X, Player._)
				.With(Player._, Player.O, Player.X);

			Assert.Equal(firstBoard.GetCanonicalHashCode(), secondBoard.GetCanonicalHashCode());
		}
	}
}
