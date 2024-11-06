using TicTacToe.Core;

namespace TicTacToe.Stdout
{
	public class BoardPrinter
	{
		public string Print(Board board)
		{
			var positions = board.Select(x => x switch
			{
				Player.X => "X",
				Player.O => "O",
				_ => " "
			}).ToArray();

			return
@$"  0 1 2
0 {positions[0]}|{positions[1]}|{positions[2]}
  -----
1 {positions[3]}|{positions[4]}|{positions[5]}
  -----
2 {positions[6]}|{positions[7]}|{positions[8]}
";
		}
	}
}
