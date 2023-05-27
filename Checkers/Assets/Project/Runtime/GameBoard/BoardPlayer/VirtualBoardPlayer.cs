using System;

namespace Runtime.GameBoard
{
    internal sealed class VirtualBoardPlayer: IGameBoardPlayer
    {
        public void BeginTurn(Action<byte[]> value)
        {
            throw new NotImplementedException();
        }

        public void CancelTurn()
        {
            throw new NotImplementedException();
        }

        public void IsReady(Action positiveResponse)
        {
            throw new NotImplementedException();
        }
    }
}
