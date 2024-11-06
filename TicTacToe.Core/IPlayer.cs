namespace TicTacToe.Core
{
	public interface IPlayer
	{
		(Row row, Column column) GetMove(Board board);
	}
}
