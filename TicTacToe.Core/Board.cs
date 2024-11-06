using System.Collections;
using System.Collections.ObjectModel;

namespace TicTacToe.Core
{
	public class Board : IEnumerable<Player>, IEquatable<Board>
	{
		private readonly ReadOnlyCollection<Player> _positions;

		private Board(Player r1c1, Player r1c2, Player r1c3, Player r2c1, Player r2c2, Player r2c3, Player r3c1, Player r3c2, Player r3c3)
		{
			_positions = new([r1c1, r1c2, r1c3, r2c1, r2c2, r2c3, r3c1, r3c2, r3c3]);

			if (_positions.Count(p => p == Player.X) < _positions.Count(p => p == Player.O))
			{
				throw new InvalidOperationException();
			}

			if (_positions.Count(p => p == Player.X) > _positions.Count(p => p == Player.O) + 1)
			{
				throw new InvalidOperationException();
			}
		}

		public static Board Empty => From();

		public static Board From(
			Player r1c1 = Player._,
			Player r1c2 = Player._,
			Player r1c3 = Player._,
			Player r2c1 = Player._,
			Player r2c2 = Player._,
			Player r2c3 = Player._,
			Player r3c1 = Player._,
			Player r3c2 = Player._,
			Player r3c3 = Player._)
		{
			return new Board(r1c1, r1c2, r1c3, r2c1, r2c2, r2c3, r3c1, r3c2, r3c3);
		}

		public Board Move(Row row, Column column, Player currentPlayer)
		{
			var newBoard = _positions.ToArray();
			int indexToSet = (int)row * 3 + (int)column;

			if (!CanMoveAt(indexToSet))
			{
				throw new InvalidOperationException();
			}

			newBoard[indexToSet] = currentPlayer;

			return new Board(newBoard[0], newBoard[1], newBoard[2], newBoard[3], newBoard[4], newBoard[5], newBoard[6], newBoard[7], newBoard[8]);
		}

		public bool CanMoveAt(int index)
		{
			return _positions[index] == Player._;
		}

		public Player? Winner => GetWinner();

		private Player? GetWinner()
		{
			int[][] winningCombinations =
			[
				[0, 1, 2], // Row 1
                [3, 4, 5], // Row 2
                [6, 7, 8], // Row 3
                [0, 3, 6], // Column 1
                [1, 4, 7], // Column 2
                [2, 5, 8], // Column 3
                [0, 4, 8], // Diagonal 1
                [2, 4, 6]  // Diagonal 2
            ];

			foreach (var combination in winningCombinations)
			{
				if (_positions[combination[0]] != Player._ &&
					_positions[combination[0]] == _positions[combination[1]] &&
					_positions[combination[1]] == _positions[combination[2]])
				{
					return _positions[combination[0]];
				}
			}

			if (_positions.All(p => p != Player._))
			{
				return Player._;
			}

			return null;
		}

		public List<(Row row, Column column)> GetValidMoves()
		{
			return Enumerable.Range(0, 9)
				.Where(CanMoveAt)
				.Select(index => ((Row)(index / 3), (Column)(index % 3)))
				.ToList();
		}

		public bool Equals(Board? other)
		{
			if (other is null)
			{
				return false;
			}

			return _positions.SequenceEqual(other._positions);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return
					(int)_positions[8]
					| (int)_positions[7] << 2
					| (int)_positions[6] << 4
					| (int)_positions[5] << 6
					| (int)_positions[4] << 8
					| (int)_positions[3] << 10
					| (int)_positions[2] << 12
					| (int)_positions[1] << 14
					| (int)_positions[0] << 16;
			};
		}

		public int GetCanonicalHashCode()
		{
			unchecked
			{
				var horizontalFlip =
					(int)_positions[2] << 16 |
					(int)_positions[1] << 14 |
					(int)_positions[0] << 12 |
					(int)_positions[5] << 10 |
					(int)_positions[4] << 8 |
					(int)_positions[3] << 6 |
					(int)_positions[8] << 4 |
					(int)_positions[7] << 2 |
					(int)_positions[6];

				var verticalFlip =
					(int)_positions[6] << 16 |
					(int)_positions[7] << 14 |
					(int)_positions[8] << 12 |
					(int)_positions[3] << 10 |
					(int)_positions[4] << 8 |
					(int)_positions[5] << 6 |
					(int)_positions[0] << 4 |
					(int)_positions[1] << 2 |
					(int)_positions[2];

				var rotate90 =
					(int)_positions[6] << 16 |
					(int)_positions[3] << 14 |
					(int)_positions[0] << 12 |
					(int)_positions[7] << 10 |
					(int)_positions[4] << 8 |
					(int)_positions[1] << 6 |
					(int)_positions[8] << 4 |
					(int)_positions[5] << 2 |
					(int)_positions[2];

				var rotate180 =
					(int)_positions[8] << 16 |
					(int)_positions[7] << 14 |
					(int)_positions[6] << 12 |
					(int)_positions[5] << 10 |
					(int)_positions[4] << 8 |
					(int)_positions[3] << 6 |
					(int)_positions[2] << 4 |
					(int)_positions[1] << 2 |
					(int)_positions[0];

				var rotate270 =
					(int)_positions[2] << 16 |
					(int)_positions[5] << 14 |
					(int)_positions[8] << 12 |
					(int)_positions[1] << 10 |
					(int)_positions[4] << 8 |
					(int)_positions[7] << 6 |
					(int)_positions[0] << 4 |
					(int)_positions[3] << 2 |
					(int)_positions[6];

				var transpose =
					(int)_positions[0] << 16 |
					(int)_positions[3] << 14 |
					(int)_positions[6] << 12 |
					(int)_positions[1] << 10 |
					(int)_positions[4] << 8 |
					(int)_positions[7] << 6 |
					(int)_positions[2] << 4 |
					(int)_positions[5] << 2 |
					(int)_positions[8];

				return new[] { horizontalFlip, verticalFlip, rotate90, rotate180, rotate270, transpose }.Min();
			}
		}

		public IEnumerator<Player> GetEnumerator()
		{
			return ((IEnumerable<Player>)_positions).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public override bool Equals(object? obj)
		{
			return Equals(obj as Board);
		}
	}
}
