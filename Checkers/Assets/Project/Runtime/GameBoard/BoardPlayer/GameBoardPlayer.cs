using System;

namespace Runtime.GameBoard
{
     internal abstract class GameBoardPlayer: IGameBoardPlayer
     {
          public void IsReady(Action positiveResponse)
          {
               throw new NotImplementedException();
          }

          public void BeginTurn(Action<byte[]> value)
          {
               throw new NotImplementedException();
          }

          public void CancelTurn()
          {
               throw new NotImplementedException();
          }
     }
}
