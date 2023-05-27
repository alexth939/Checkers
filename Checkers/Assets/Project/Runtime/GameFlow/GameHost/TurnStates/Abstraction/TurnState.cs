using System;
using System.Threading.Tasks;
using Runtime.GameBoard;

namespace Runtime.GameFlow
{
    internal abstract class TurnState: ITurnState
    {
        public TurnStateResult StateResult { get; private set; } = TurnStateResult.NotFinished;
        protected GameFlowModel FlowModel { get; set; }
        protected GameCommandsChannel CommandsChannel => FlowModel.CommandsChannel;
        protected GameMovesChannel MovesChannel => FlowModel.MovesChannel;
        protected TimeSpan TimeoutDelayPeriod => TimeSpanAdapter.New(seconds: ResponseTimeoutValue);
        private float ResponseTimeoutValue => FlowModel.StateTimeoutValues[key: this.GetType()];

        public abstract Task ProcessAsync(BoardSide activePlayer);

        protected virtual void HandlePingResponse(BoardSide sender)
        { }

        protected virtual void HandleMoveAttempt(BoardSide sender, byte[] move)
        { }

        protected virtual void SummarizeResults(Task completedTask)
        {
            StateResult = completedTask.Status switch
            {
                TaskStatus.RanToCompletion => TurnStateResult.TimeoutOccurred,
                TaskStatus.Canceled => TurnStateResult.SuccessfullyCompleted,
                _ => throw new NotSupportedException()
            };
        }
    }
}
