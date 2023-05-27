using System;
using System.Threading;
using System.Threading.Tasks;
using Runtime.GameBoard;

namespace Runtime.GameFlow
{
    internal sealed class PingPlayerState: TurnState
    {
        private readonly CancellationTokenSource _timeoutAwaitingSource;
        private BoardSide _activePlayer;

        internal PingPlayerState(GameFlowModel flowModel)
        {
            FlowModel = flowModel;
            _timeoutAwaitingSource = new CancellationTokenSource();
        }

        public override async Task ProcessAsync(BoardSide activePlayer)
        {
            _activePlayer = activePlayer;
            CommandsChannel.OnPingResponse += HandlePingResponse;

            CommandsChannel.SendPingRequest(_activePlayer);
            await Task.Delay(TimeoutDelayPeriod, _timeoutAwaitingSource.Token).ContinueWith(SummarizeResults);

            CommandsChannel.OnPingResponse -= HandlePingResponse;
        }

        protected override void HandlePingResponse(BoardSide sender)
        {
            if(sender == _activePlayer)
                _timeoutAwaitingSource.Cancel();
        }
    }
}
