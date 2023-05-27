using System;
using Runtime.GameBoard;

namespace Runtime.GameFlow
{
    internal static class TurnStatesCollectionFactory
    {
        internal static ITurnStatesCollection InitTurnStates(this CheckersGameType gameType, GameFlowModel flowModel)
        {
            return gameType switch
            {
                CheckersGameType.Checkers64 => new Checkers64TurnStates(flowModel),
                _ => throw new ArgumentException("Unknown checkers type", nameof(gameType))
            };
        }
    }
}
