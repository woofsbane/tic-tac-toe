namespace TicTacToe.Core
{
	public class BoardBuilder
	{
		public static BoardRow2Builder With(Player c1, Player c2, Player c3)
		{
			return new BoardRow2Builder(c1, c2, c3);
		}

		public class BoardRow2Builder(Player r1c1, Player r1c2, Player r1c3)
		{
			public BoardRow3Builder With(Player c1, Player c2, Player c3)
			{
				return new BoardRow3Builder(r1c1, r1c2, r1c3, c1, c2, c3);
			}
		}

		public class BoardRow3Builder(Player r1c1, Player r1c2, Player r1c3, Player r2c1, Player r2c2, Player r2c3)
		{
			public Board With(Player c1, Player c2, Player c3)
			{
				return Board.From(r1c1, r1c2, r1c3, r2c1, r2c2, r2c3, c1, c2, c3);
			}
		}
	}

}
