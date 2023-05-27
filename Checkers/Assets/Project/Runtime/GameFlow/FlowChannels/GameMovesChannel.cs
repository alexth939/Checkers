using System;
using Runtime.GameBoard;

namespace Runtime.GameFlow
{
    internal class GameMovesChannel
    {
        internal event MoveEventArgs OnPlayerMoved;

        internal void PostMove(BoardSide target)
        {
            throw new NotImplementedException();
        }
    }
}
