using System;
using Runtime.GameBoard;

namespace Runtime.GameFlow
{
     internal delegate void PingRequestArgs(BoardSide target);
     internal delegate void PingResponseArgs(BoardSide sender);
     internal delegate void TurnCancellationArgs(BoardSide target);

     internal class GameCommandsChannel
     {
          internal event PingRequestArgs OnPingRequest;
          internal event PingResponseArgs OnPingResponse;
          internal event TurnCancellationArgs OnCancelMove;

          internal virtual void SendPingRequest(BoardSide target)
          {
               OnPingRequest?.Invoke(target);
          }

          internal virtual void SendPingResponse(BoardSide sender)
          {
               OnPingResponse?.Invoke(sender);
          }

          internal virtual void TryMove(BoardSide sender, byte[] move)
          {
               throw new NotImplementedException();
          }

          internal virtual void CancelMove(BoardSide target)
          {
               throw new NotImplementedException();
          }
     }
}