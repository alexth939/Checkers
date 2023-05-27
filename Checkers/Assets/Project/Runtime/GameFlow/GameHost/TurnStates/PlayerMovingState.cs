using System.Threading;
using System.Threading.Tasks;
using Runtime.GameBoard;

namespace Runtime.GameFlow
{
    internal sealed class PlayerMovingState: TurnState
    {
        private readonly CancellationTokenSource _timeoutAwaitingSource;
        private BoardSide _activePlayer;

        internal PlayerMovingState(GameFlowModel flowModel)
        {
            FlowModel = flowModel;
            _timeoutAwaitingSource = new CancellationTokenSource();
        }

        public override async Task ProcessAsync(BoardSide activePlayer)
        {
            _activePlayer = activePlayer;
            CommandsChannel.OnMoveAttempt += HandleMoveAttempt;

            CommandsChannel.SendTurnStarted(activePlayer);
            await Task.Delay(TimeoutDelayPeriod, _timeoutAwaitingSource.Token).ContinueWith(SummarizeResults);

            CommandsChannel.OnMoveAttempt -= HandleMoveAttempt;
        }

        protected override void HandleMoveAttempt(BoardSide sender, byte[] move)
        {
            throw new System.NotImplementedException();
        }
    }
}
