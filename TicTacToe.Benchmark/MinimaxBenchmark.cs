using BenchmarkDotNet.Attributes;
using TicTacToe.Core;
using TicTacToe.Players.Minimax;

namespace TicTacToe.Benchmark
{
	//[MemoryDiagnoser]
	public class MinimaxBenchmark
	{
		[Benchmark(Baseline = true)]
		public (Row, Column) Standard() => new MinimaxStandard(Player.X).GetMove(Board.Empty);

		[Benchmark]
		public (Row, Column) AlphaBeta() => new MinimaxAlphaBeta(Player.X).GetMove(Board.Empty);

		[Benchmark]
		public (Row, Column) Memoized() => new MinimaxMemoized(Player.X).GetMove(Board.Empty);

		[Benchmark]
		public (Row, Column) MemoizedVariants() => new MinimaxMemoizedVariants(Player.X).GetMove(Board.Empty);

		[Benchmark]
		public (Row, Column) AlphaBetaMemoizedVariants() => new MinimaxAlphaBetaMemoizedVariants(Player.X).GetMove(Board.Empty);

		[Benchmark]
		public (Row, Column) SourceGenerated() => new MinimaxSourceGenerated(Player.X).GetMove(Board.Empty);
	}
}
