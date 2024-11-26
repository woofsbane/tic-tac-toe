using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Linq;
using System.Text;
using TicTacToe.Core;

namespace TicTacToe.SourceGenerators;

[Generator]
public class MinimaxMemoTableGenerator : ISourceGenerator
{
	public void Initialize(GeneratorInitializationContext context)
	{
		// No initialization required
	}

	public void Execute(GeneratorExecutionContext context)
	{
		var xMemoSwitch = GenerateMemoSwitchX();
		var oMemoSwitch = GenerateMemoSwitchO();

		var source = $@"
using System.Collections.Generic;
using TicTacToe.Core;

namespace TicTacToe.SourceGenerators
{{
    public static class PrecomputedMemos
    {{
		private static int GetX(int key) {{
			switch (key) {{
{xMemoSwitch}				default: return -1;
			}};
		}}

		private static int GetO(int key) {{
			switch (key) {{
{oMemoSwitch}				default: return -1;
			}};
		}}

		public static int GetValue(Player player, Board board)
		{{
			var key = board.GetHashCode();
			return player == Player.X ? GetX(key) : GetO(key);
		}}
    }}
}}
    ";

		// Add the source code to the compilation
		context.AddSource("PrecomputedMemos.cs", SourceText.From(source, Encoding.UTF8));
	}

	private string GenerateMemoSwitchX()
    {
        var player = new MinimaxMemoizedVariants(Player.X);
        player.BuildMemos(Board.Empty);

        var memoTable = new StringBuilder();

        foreach (var entry in player.memos.Where(x => !x.Key.Item2))
        {
            memoTable.Append($"\t\t\t\tcase {entry.Key.Item1}: return {entry.Value};\n");
        }

        return memoTable.ToString();
    }

    private string GenerateMemoSwitchO()
    {
        var player = new MinimaxMemoizedVariants(Player.O);

        foreach (var move in Board.Empty.ValidMoves)
        {
            var board = Board.Empty.MovePlayer(move, Player.X);
            player.BuildMemos(board);
        }

        var memoTable = new StringBuilder();

        foreach (var entry in player.memos.Where(x => !x.Key.Item2))
        {
            memoTable.Append($"\t\t\t\tcase {entry.Key.Item1}: return {entry.Value};\n");
        }

        return memoTable.ToString();
    }
}