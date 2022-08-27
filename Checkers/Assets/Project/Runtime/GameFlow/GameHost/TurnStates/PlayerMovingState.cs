using System.Threading.Tasks;
using Runtime.GameBoard;

namespace Runtime.GameFlow
{
     internal sealed class PlayerMovingState: TurnState
     {
          protected internal override TurnStateResult StateResult { get => throw new System.NotImplementedException(); protected set => throw new System.NotImplementedException(); }

          internal PlayerMovingState(GameFlowModel _flowModel)
          {
          }

          internal override Task ProcessAsync(BoardSide activePlayer)
          {
               throw new System.NotImplementedException();
          }

          protected override void HandlePingResponse(BoardSide sender)
          {
               throw new System.NotImplementedException();
          }

          protected override void HandleMoveAttempt(BoardSide sender, byte[] move)
          {
               throw new System.NotImplementedException();
          }

          protected override void SummarizeResults(Task completedTask)
          {
               throw new System.NotImplementedException();
          }
     }
}