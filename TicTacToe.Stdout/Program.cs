using TicTacToe.Core;
using TicTacToe.Players;
using TicTacToe.Players.Minimax;
using TicTacToe.Stdout;

Console.WriteLine(@"Player types:
    1: Human
    2: Random
    3: Random, blocks winning moves
    4: Random, plays winning moves
    5: Random, blocks and plays winning moves
    6: Perfect");

IPlayer playerX = GetPlayer("X");
IPlayer playerO = GetPlayer("O");

var printer = new BoardPrinter();
var print = (Board board) => Console.WriteLine(printer.Print(board));

new Game(playerX, playerO, print).Start();

static IPlayer GetPlayer(string playerSymbol)
{
	Player player = (Player)Enum.Parse(typeof(Player), playerSymbol);

	while (true)
	{
		Console.Write($"Select player type for {playerSymbol}: ");
		var input = Console.ReadLine();

		switch (input)
		{
			case "1":
				return new HumanPlayer(player);
			case "2":
				return new RandomPlayer();
			case "3":
				return new RandomBlockingPlayer(player);
			case "4":
				return new RandomWinningPlayer(player);
			case "5":
				return new RandomBlockingWinningPlayer(player);
			case "6":
				return new MinimaxSourceGenerated(player);
			default:
				Console.WriteLine("Invalid player type selected. Please enter a number from 1 to 6.");
				break;
		}
	}
}
