using System.Threading.Tasks;
using Runtime.GameBoard;

namespace Runtime.GameFlow
{
     internal sealed class ForcingRandomMoveState: TurnState
     {
          private readonly BoardSide _activePlayer;
          private readonly GameFlowModel _flowModel;

          internal ForcingRandomMoveState(GameFlowModel flowModel)
          {
               _flowModel = flowModel;
          }

          private GameCommandsChannel CommandsChannel => _flowModel.CommandsChannel;
          private GameMovesChannel MovesChannel => _flowModel.MovesChannel;

          protected internal override TurnStateResult StateResult { get => throw new System.NotImplementedException(); protected set => throw new System.NotImplementedException(); }

          internal override Task ProcessAsync(BoardSide activePlayer)
          {
               CommandsChannel.CancelMove(target: _activePlayer);
               //todo: generate random move
               MovesChannel.PostMove(target: _activePlayer);
               return Task.CompletedTask;
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