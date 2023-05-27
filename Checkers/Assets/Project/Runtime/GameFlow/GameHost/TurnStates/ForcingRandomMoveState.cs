using System.Threading.Tasks;
using Runtime.GameBoard;

namespace Runtime.GameFlow
{
    internal sealed class ForcingRandomMoveState: TurnState
    {
        private readonly BoardSide _activePlayer;

        internal ForcingRandomMoveState(GameFlowModel flowModel)
        {
            FlowModel = flowModel;
        }

        public override Task ProcessAsync(BoardSide activePlayer)
        {
            CommandsChannel.SendTurnCancelled(target: _activePlayer);
            //todo: generate random move
            MovesChannel.PostMove(target: _activePlayer);
            return Task.CompletedTask;
        }
    }
}
