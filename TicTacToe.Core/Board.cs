using System.Collections;

namespace TicTacToe.Core
{
    public struct Board : IEnumerable<Player>
    {
        private static readonly Dictionary<int, Board> boards = [];

        private readonly Player[] _positions;

        private Board(Player r1c1, Player r1c2, Player r1c3, Player r2c1, Player r2c2, Player r2c3, Player r3c1, Player r3c2, Player r3c3)
        {
            _positions = [r1c1, r1c2, r1c3, r2c1, r2c2, r2c3, r3c1, r3c2, r3c3];

            int xCount = CountPositions(Player.X);
            int oCount = CountPositions(Player.O);

            if (xCount < oCount || xCount > oCount + 1)
            {
                throw new InvalidOperationException();
            }

            ValidMoves = GetValidMoves();
        }

        private int CountPositions(Player player)
        {
            var count = 0;

            for (int i = 0; i < 9; i++)
            {
                if (_positions[i] == player) count++;
            }

            return count;
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
            int hashCode = GetHash(r1c1, r1c2, r1c3, r2c1, r2c2, r2c3, r3c1, r3c2, r3c3);
            if (boards.TryGetValue(hashCode, out var board))
            {
                return board;
            }

            var newBoard = new Board(r1c1, r1c2, r1c3, r2c1, r2c2, r2c3, r3c1, r3c2, r3c3);
            boards[hashCode] = newBoard;
            return newBoard;
        }

        public Board MovePlayer(Move move, Player currentPlayer)
        {
            int indexToSet = (int)move.Row * 3 + (int)move.Column;

            if (!CanMoveAt(indexToSet))
            {
                throw new InvalidOperationException();
            }

            return indexToSet switch
            {
                0 => From(currentPlayer, _positions[1], _positions[2], _positions[3], _positions[4], _positions[5], _positions[6], _positions[7], _positions[8]),
                1 => From(_positions[0], currentPlayer, _positions[2], _positions[3], _positions[4], _positions[5], _positions[6], _positions[7], _positions[8]),
                2 => From(_positions[0], _positions[1], currentPlayer, _positions[3], _positions[4], _positions[5], _positions[6], _positions[7], _positions[8]),
                3 => From(_positions[0], _positions[1], _positions[2], currentPlayer, _positions[4], _positions[5], _positions[6], _positions[7], _positions[8]),
                4 => From(_positions[0], _positions[1], _positions[2], _positions[3], currentPlayer, _positions[5], _positions[6], _positions[7], _positions[8]),
                5 => From(_positions[0], _positions[1], _positions[2], _positions[3], _positions[4], currentPlayer, _positions[6], _positions[7], _positions[8]),
                6 => From(_positions[0], _positions[1], _positions[2], _positions[3], _positions[4], _positions[5], currentPlayer, _positions[7], _positions[8]),
                7 => From(_positions[0], _positions[1], _positions[2], _positions[3], _positions[4], _positions[5], _positions[6], currentPlayer, _positions[8]),
                8 => From(_positions[0], _positions[1], _positions[2], _positions[3], _positions[4], _positions[5], _positions[6], _positions[7], currentPlayer),
                _ => throw new InvalidOperationException()
            };
        }

        public bool CanMoveAt(int index)
        {
            return _positions[index] == Player._;
        }

        public Player? Winner => GetWinner();

        private Player? GetWinner()
        {
            foreach (var combination in winningCombinations)
            {
                if (_positions[combination[0]] != Player._ &&
                    _positions[combination[0]] == _positions[combination[1]] &&
                    _positions[combination[1]] == _positions[combination[2]])
                {
                    return _positions[combination[0]];
                }
            }

            if (_positions[0] != Player._ &&
                _positions[1] != Player._ &&
                _positions[2] != Player._ &&
                _positions[3] != Player._ &&
                _positions[4] != Player._ &&
                _positions[5] != Player._ &&
                _positions[6] != Player._ &&
                _positions[7] != Player._ &&
                _positions[8] != Player._)
            {
                return Player._;
            }

            return null;
        }

        private static readonly byte[][] winningCombinations =
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

        public List<Move> ValidMoves { get; }

        private List<Move> GetValidMoves()
        {
            var ret = new List<Move>();

            for (int i = 0; i < 9; i++)
            {
                if (CanMoveAt(i))
                {
                    ret.Add(Move.At((Row)(i / 3), (Column)(i % 3)));
                }
            }

            return ret;
        }

        public override int GetHashCode() => GetHash(_positions[0], _positions[1], _positions[2], _positions[3], _positions[4], _positions[5], _positions[6], _positions[7], _positions[8]);

        private static int GetHash(Player p0, Player p1, Player p2, Player p3, Player p4, Player p5, Player p6, Player p7, Player p8)
        {
            unchecked
            {
                return
                    (int)p8
                    | (int)p7 << 2
                    | (int)p6 << 4
                    | (int)p5 << 6
                    | (int)p4 << 8
                    | (int)p3 << 10
                    | (int)p2 << 12
                    | (int)p1 << 14
                    | (int)p0 << 16;
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
    }
}
