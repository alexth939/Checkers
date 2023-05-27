using Runtime.GameBoard;

namespace Runtime.GameFlow
{
    internal delegate void PingRequestArgs(BoardSide target);

    internal delegate void PingResponseArgs(BoardSide sender);

    internal delegate void TurnBeginningArgs(BoardSide target);

    internal delegate void TurnCancellationArgs(BoardSide target);

    internal class GameCommandsChannel
    {
        internal event PingRequestArgs OnPingRequest;

        internal event PingResponseArgs OnPingResponse;

        internal event TurnBeginningArgs OnTurnStarted;

        internal event TurnCancellationArgs OnTurnCancelled;

        internal event MoveEventArgs OnMoveAttempt;

        internal virtual void SendPingRequest(BoardSide target)
        {
            OnPingRequest?.Invoke(target);
        }

        internal virtual void SendPingResponse(BoardSide sender)
        {
            OnPingResponse?.Invoke(sender);
        }

        internal virtual void SendTurnStarted(BoardSide target)
        {
            OnTurnStarted?.Invoke(target);
        }

        internal virtual void TryMove(BoardSide sender, byte[] move)
        {
            OnMoveAttempt?.Invoke(sender, move);
        }

        internal virtual void SendTurnCancelled(BoardSide target)
        {
            OnTurnCancelled?.Invoke(target);
        }
    }
}
