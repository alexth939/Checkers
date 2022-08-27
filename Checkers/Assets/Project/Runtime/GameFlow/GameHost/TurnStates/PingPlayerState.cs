using System;
using System.Threading;
using System.Threading.Tasks;
using Runtime.GameBoard;

namespace Runtime.GameFlow
{
     internal sealed class PingPlayerState: TurnState
     {
          private readonly GameFlowModel _flowModel;
          private readonly CancellationTokenSource _timeoutAwaitingSource;
          private BoardSide _activePlayer;

          internal PingPlayerState(GameFlowModel flowModel)
          {
               _flowModel = flowModel;
               _timeoutAwaitingSource = new CancellationTokenSource();
          }

          protected internal override TurnStateResult StateResult { get; protected set; } = TurnStateResult.StillRunning;
          private GameCommandsChannel CommandsChannel => _flowModel.CommandsChannel;
          private float ResponseTimeoutValue => _flowModel.StateTimeoutValues[key: this.GetType()];
          private TimeSpan TimeoutDelayPeriod => TimeSpanAdapter.New(seconds: ResponseTimeoutValue);

          internal override async Task ProcessAsync(BoardSide activePlayer)
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

          protected override void HandleMoveAttempt(BoardSide sender, byte[] move)
          {
               throw new NotSupportedException();
          }

          protected override void SummarizeResults(Task timeoutAwaiting)
          {
               StateResult = timeoutAwaiting.Status switch
               {
                    TaskStatus.RanToCompletion => TurnStateResult.TimeoutOccurred,
                    TaskStatus.Canceled => TurnStateResult.SuccessfullyCompleted,
                    _ => throw new NotSupportedException()
               };
          }
     }
}
