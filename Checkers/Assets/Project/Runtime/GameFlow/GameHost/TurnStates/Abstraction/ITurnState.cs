using System.Threading.Tasks;
using Runtime.GameBoard;

namespace Runtime.GameFlow
{
    internal interface ITurnState
    {
        TurnStateResult StateResult { get; }

        Task ProcessAsync(BoardSide activePlayer);
    }
}
