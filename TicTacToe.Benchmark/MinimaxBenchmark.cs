using BenchmarkDotNet.Attributes;
using TicTacToe.Core;
using TicTacToe.Players.Minimax;

namespace TicTacToe.Benchmark
{
	//[MemoryDiagnoser]
	public class MinimaxBenchmark
	{
		[Benchmark(Baseline = true)]
		public Move Standard() => new MinimaxStandard(Player.X).GetMove(Board.Empty);

		[Benchmark]
		public Move AlphaBeta() => new MinimaxAlphaBeta(Player.X).GetMove(Board.Empty);

		[Benchmark]
		public Move Memoized() => new MinimaxMemoized(Player.X).GetMove(Board.Empty);

		[Benchmark]
		public Move MemoizedVariants() => new MinimaxMemoizedVariants(Player.X).GetMove(Board.Empty);

		[Benchmark]
		public Move AlphaBetaMemoizedVariants() => new MinimaxAlphaBetaMemoizedVariants(Player.X).GetMove(Board.Empty);

		[Benchmark]
		public Move SourceGenerated() => new MinimaxSourceGenerated(Player.X).GetMove(Board.Empty);
	}
}
