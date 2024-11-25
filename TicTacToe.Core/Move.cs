namespace TicTacToe.Core
{
    public struct Move
    {
        public Row Row { get; }
        public Column Column { get; }

        private Move(Row row, Column column)
        {
            Row = row;
            Column = column;
        }

        public static Move At(Row row, Column column)
        {
            return allMoves[(byte)row * 3 + (byte)column];
        }

        private static readonly Move[] allMoves =
        [
            new Move(Row._1, Column._1),
            new Move(Row._1, Column._2),
            new Move(Row._1, Column._3),
            new Move(Row._2, Column._1),
            new Move(Row._2, Column._2),
            new Move(Row._2, Column._3),
            new Move(Row._3, Column._1),
            new Move(Row._3, Column._2),
            new Move(Row._3, Column._3),
        ];
    }
}
