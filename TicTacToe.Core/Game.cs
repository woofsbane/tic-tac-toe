namespace TicTacToe.Core
{
	public class Game(IPlayer playerX, IPlayer playerO, Action<Board> beforeMove)
	{
		private Board _board = Board.Empty;
		private Player _currentPlayer = Player.X;

		public void Start()
		{
			while (_board.Winner == null)
			{
				try
				{
					beforeMove(_board);

					var (row, column) = _currentPlayer == Player.X ? playerX.GetMove(_board) : playerO.GetMove(_board);

					_board = _board.Move(row, column, _currentPlayer);
					_currentPlayer = _currentPlayer == Player.X ? Player.O : Player.X;
				}
				catch (InvalidOperationException)
				{
					Console.WriteLine("Invalid move. Try again.");
				}
			}

			beforeMove(_board);
			Console.WriteLine(_board.Winner == Player._ ? "It's a draw!" : $"Player {_board.Winner} wins!");

			Console.ReadKey();
		}
	}
}
