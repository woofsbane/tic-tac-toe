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
		var xMemoTable = GenerateMemoTableX();
		var oMemoTable = GenerateMemoTableO();

		var source = $@"
using System.Collections.Generic;
using TicTacToe.Core;

namespace TicTacToe.SourceGenerators
{{
    public static class PrecomputedMemos
    {{
        private static readonly Dictionary<int, int> xMemos = new Dictionary<int, int>
        {{
            {xMemoTable}
        }};

        private static readonly Dictionary<int, int> oMemos = new Dictionary<int, int>
        {{
            {oMemoTable}
        }};

		public static int GetValue(Player player, Board board)
		{{
			var memoTable = player == Player.X ? xMemos : oMemos;

			if (memoTable.TryGetValue(board.GetCanonicalHashCode(), out int value))
			{{
				return value;
			}}

			return -1;
		}}
    }}
}}
    ";

		// Add the source code to the compilation
		context.AddSource("PrecomputedMemos.cs", SourceText.From(source, Encoding.UTF8));
	}

	private string GenerateMemoTableX()
	{
		var player = new MinimaxMemoizedVariants(Player.X);
		player.BuildMemos(Board.Empty);

		var memoTable = new StringBuilder();

		foreach (var entry in player.memos.Where(x => !x.Key.Item2))
		{
			memoTable.Append($"[{entry.Key.Item1}] = {entry.Value},");
		}

		return memoTable.ToString();
	}

	private string GenerateMemoTableO()
	{
		var player = new MinimaxMemoizedVariants(Player.O);

		foreach (var (row, column) in Board.Empty.GetValidMoves())
		{
			var board = Board.Empty.Move(row, column, Player.X);
			player.BuildMemos(board);
		}

		var memoTable = new StringBuilder();

		foreach (var entry in player.memos.Where(x => !x.Key.Item2))
		{
			memoTable.Append($"[{entry.Key.Item1}] = {entry.Value},");
		}

		return memoTable.ToString();
	}
}