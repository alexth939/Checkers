using System;
using System.Threading;
using System.Threading.Tasks;

namespace Runtime.GameBoard
{
     internal interface IGameBoardPlayer
     {
          void IsReady(Action positiveResponse);
          void BeginTurn(Action<byte[]> value);
          void CancelTurn();
     }
}