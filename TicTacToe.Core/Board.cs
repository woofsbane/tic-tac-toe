using System.Collections;

namespace TicTacToe.Core
{
    public readonly struct Board : IEnumerable<Player>
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
            hashCode = GetHash(_positions[0], _positions[1], _positions[2], _positions[3], _positions[4], _positions[5], _positions[6], _positions[7], _positions[8]);
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

        public static Board Empty { get; } = From();

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

        public readonly Player? Winner => GetWinner();

        private readonly Player? GetWinner()
        {
            if (_positions[0] != Player._ && _positions[0] == _positions[1] && _positions[0] == _positions[2]) return _positions[0]; // Row 1
            if (_positions[3] != Player._ && _positions[3] == _positions[4] && _positions[3] == _positions[5]) return _positions[3]; // Row 2
            if (_positions[6] != Player._ && _positions[6] == _positions[7] && _positions[6] == _positions[8]) return _positions[6]; // Row 3
            if (_positions[0] != Player._ && _positions[0] == _positions[3] && _positions[0] == _positions[6]) return _positions[0]; // Column 1
            if (_positions[1] != Player._ && _positions[1] == _positions[4] && _positions[1] == _positions[7]) return _positions[1]; // Column 2
            if (_positions[2] != Player._ && _positions[2] == _positions[5] && _positions[2] == _positions[8]) return _positions[2]; // Column 3
            if (_positions[0] != Player._ && _positions[0] == _positions[4] && _positions[0] == _positions[8]) return _positions[0]; // Diagonal 1
            if (_positions[2] != Player._ && _positions[2] == _positions[4] && _positions[2] == _positions[6]) return _positions[2]; // Diagonal 2

            // Tie
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

        private readonly int hashCode;
        public override int GetHashCode() => hashCode;

        private static int GetHash(Player p0, Player p1, Player p2, Player p3, Player p4, Player p5, Player p6, Player p7, Player p8)
        {
            unchecked
            {
                return
                    (int)p0 << 16 |
                    (int)p1 << 14 |
                    (int)p2 << 12 |
                    (int)p3 << 10 |
                    (int)p4 << 8 |
                    (int)p5 << 6 |
                    (int)p6 << 4 |
                    (int)p7 << 2 |
                    (int)p8;
            };
        }

        public int GetCanonicalHashCode()
        {
            var p0 = (byte)_positions[0];
            var p1 = (byte)_positions[1];
            var p2 = (byte)_positions[2];
            var p3 = (byte)_positions[3];
            var p4 = (byte)_positions[4];
            var p5 = (byte)_positions[5];
            var p6 = (byte)_positions[6];
            var p7 = (byte)_positions[7];
            var p8 = (byte)_positions[8];

            unchecked
            {
                var horizontalFlip =
                    p2 << 16 |
                    p1 << 14 |
                    p0 << 12 |
                    p5 << 10 |
                    p4 << 8 |
                    p3 << 6 |
                    p8 << 4 |
                    p7 << 2 |
                    p6;

                var verticalFlip =
                    p6 << 16 |
                    p7 << 14 |
                    p8 << 12 |
                    p3 << 10 |
                    p4 << 8 |
                    p5 << 6 |
                    p0 << 4 |
                    p1 << 2 |
                    p2;

                var rotate90 =
                    p6 << 16 |
                    p3 << 14 |
                    p0 << 12 |
                    p7 << 10 |
                    p4 << 8 |
                    p1 << 6 |
                    p8 << 4 |
                    p5 << 2 |
                    p2;

                var rotate180 =
                    p8 << 16 |
                    p7 << 14 |
                    p6 << 12 |
                    p5 << 10 |
                    p4 << 8 |
                    p3 << 6 |
                    p2 << 4 |
                    p1 << 2 |
                    p0;

                var rotate270 =
                    p2 << 16 |
                    p5 << 14 |
                    p8 << 12 |
                    p1 << 10 |
                    p4 << 8 |
                    p7 << 6 |
                    p0 << 4 |
                    p3 << 2 |
                    p6;

                var transpose =
                    p0 << 16 |
                    p3 << 14 |
                    p6 << 12 |
                    p1 << 10 |
                    p4 << 8 |
                    p7 << 6 |
                    p2 << 4 |
                    p5 << 2 |
                    p8;

                var code = GetHashCode();

                if (horizontalFlip < code) code = horizontalFlip;
                if (verticalFlip < code) code = verticalFlip;
                if (rotate90 < code) code = rotate90;
                if (rotate180 < code) code = rotate180;
                if (rotate270 < code) code = rotate270;
                if (transpose < code) code = transpose;

                return code;
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
