﻿using TicTacToe.Core;

namespace TicTacToe.Players.Minimax
{
	public class MinimaxStandard(Player player) : IPlayer
	{
		private readonly Player _player = player;
		private readonly Player _opponent = player == Player.X ? Player.O : Player.X;

		public (Row row, Column column) GetMove(Board board)
		{
			var validMoves = board.GetValidMoves();
			var bestMove = validMoves.First();
			var bestValue = int.MinValue;

			foreach (var move in validMoves)
			{
				var simulatedBoard = board.Move(move.row, move.column, _player);
				var moveValue = Minimax(simulatedBoard, false);

				if (moveValue > bestValue)
				{
					bestMove = move;
					bestValue = moveValue;
				}
			}

			return bestMove;
		}

		private int Minimax(Board board, bool isMaximizing)
		{
			var validMoves = board.GetValidMoves();

			if (board.Winner == _player)
				return 1;
			if (board.Winner == _opponent)
				return -1;
			if (validMoves.Count == 0)
				return 0;

			if (isMaximizing)
			{
				var bestValue = int.MinValue;
				foreach (var (row, column) in validMoves)
				{
					var simulatedBoard = board.Move(row, column, _player);
					var moveValue = Minimax(simulatedBoard, false);
					bestValue = Math.Max(bestValue, moveValue);
				}
				return bestValue;
			}
			else
			{
				var bestValue = int.MaxValue;
				foreach (var (row, column) in validMoves)
				{
					var simulatedBoard = board.Move(row, column, _opponent);
					var moveValue = Minimax(simulatedBoard, true);
					bestValue = Math.Min(bestValue, moveValue);
				}
				return bestValue;
			}
		}
	}
}
