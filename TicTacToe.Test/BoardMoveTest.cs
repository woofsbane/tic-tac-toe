using TicTacToe.Core;

namespace TicTacToe.Test
{
	public class BoardMoveTest
	{
		[Fact]
		public void Move_UpdatesBoard()
		{
			var board = Board.Empty;
			var expectedBoard = BoardBuilder
				.With(Player._, Player._, Player._)
				.With(Player._, Player.X, Player._)
				.With(Player._, Player._, Player._);

			var newBoard = board.Move(Row._2, Column._2, Player.X);

			Assert.Equal(expectedBoard, newBoard);
		}

		[Fact]
		public void InvalidMove_Throws()
		{
			var board = BoardBuilder
				.With(Player._, Player._, Player._)
				.With(Player._, Player._, Player._)
				.With(Player._, Player._, Player.X);

			Assert.Throws<InvalidOperationException>(() => board.Move(Row._3, Column._3, Player.O));
		}

		[Fact]
		public void DuplicateMove_Throws()
		{
			var board = BoardBuilder
				.With(Player._, Player._, Player._)
				.With(Player._, Player._, Player._)
				.With(Player._, Player._, Player.X);

			Assert.Throws<InvalidOperationException>(() => board.Move(Row._3, Column._3, Player.X));
		}

		[Fact]
		public void MoveX_OnWrongTurn_Throws()
		{
			var board = BoardBuilder
				.With(Player._, Player._, Player._)
				.With(Player._, Player._, Player._)
				.With(Player._, Player._, Player.X);

			Assert.Throws<InvalidOperationException>(() => board.Move(Row._1, Column._1, Player.X));
		}

		[Fact]
		public void MoveO_OnWrongTurn_Throws()
		{
			var board = BoardBuilder
				.With(Player._, Player._, Player._)
				.With(Player._, Player.O, Player._)
				.With(Player._, Player._, Player.X);

			Assert.Throws<InvalidOperationException>(() => board.Move(Row._1, Column._1, Player.O));
		}

		[Fact]
		public void MoveO_OnEmptyBoard_Throws()
		{
			var board = Board.Empty;

			Assert.Throws<InvalidOperationException>(() => board.Move(Row._1, Column._1, Player.O));
		}
	}
}
