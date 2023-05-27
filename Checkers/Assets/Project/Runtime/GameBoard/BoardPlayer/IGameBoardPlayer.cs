using System;

namespace Runtime.GameBoard
{
    internal interface IGameBoardPlayer
    {
        void IsReady(Action positiveResponse);

        void BeginTurn(Action<byte[]> value);

        void CancelTurn();
    }
}
