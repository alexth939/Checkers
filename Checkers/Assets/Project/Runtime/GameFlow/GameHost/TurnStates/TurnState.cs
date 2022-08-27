using System.Threading.Tasks;
using Runtime.GameBoard;

namespace Runtime.GameFlow
{
     internal abstract class TurnState
     {
          protected internal abstract TurnStateResult StateResult { get; protected set; }

          internal abstract Task ProcessAsync(BoardSide activePlayer);
          protected abstract void HandlePingResponse(BoardSide sender);
          protected abstract void HandleMoveAttempt(BoardSide sender, byte[] move);
          protected abstract void SummarizeResults(Task completedTask);
     }
}
