using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
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
		// Generate the memo table
		var xMemoTable = GenerateMemoTableX();
		var oMemoTable = GenerateMemoTableO();

		// Create the source code
		var source = $@"
    using System.Collections.Generic;

    namespace TicTacToe.SourceGenerators
    {{
        public static class PrecomputedMemoTable
        {{
            public static readonly Dictionary<(int, bool), int> xMemos = new Dictionary<(int, bool), int>
            {{
                {xMemoTable}
            }};

            public static readonly Dictionary<(int, bool), int> oMemos = new Dictionary<(int, bool), int>
            {{
                {oMemoTable}
            }};
        }}
    }}
    ";

		// Add the source code to the compilation
		context.AddSource("PrecomputedMemoTable.cs", SourceText.From(source, Encoding.UTF8));
	}

	private string GenerateMemoTableX()
	{
		var player = new MinimaxMemoizedVariants(Player.X);
		player.GetMove(Board.Empty);

		var memoTable = new StringBuilder();

		foreach (var entry in player.memos)
		{
			memoTable.Append($"[({entry.Key.Item1}, {entry.Key.Item2.ToString().ToLower()})] = {entry.Value},");
		}

		return memoTable.ToString();
	}

	private string GenerateMemoTableO()
	{
		var player = new MinimaxMemoizedVariants(Player.O);

		foreach (var (row, column) in Board.Empty.GetValidMoves())
		{
			var board = Board.Empty.Move(row, column, Player.X);
			player.GetMove(board);
		}

		var memoTable = new StringBuilder();

		foreach (var entry in player.memos)
		{
			memoTable.Append($"[({entry.Key.Item1}, {entry.Key.Item2.ToString().ToLower()})] = {entry.Value},");
		}

		return memoTable.ToString();
	}
}