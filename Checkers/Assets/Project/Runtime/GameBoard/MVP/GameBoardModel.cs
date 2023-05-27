/// <summary>
/// terminology:
/// raw position - is the index of the cell, starting at left buttom corner
/// board coords - the v2Int cell coordinates on board, starting at left buttom corner
/// world coords - the v3 cell coordinates in world position
///
/// syntax:
/// raw position: raw origin, raw destination
/// board coords: board origin, board destination
/// world coords: world origin, world destination
///
/// !!! don't ever ever ever set same position in several {UpperCheckers+LowerCheckers} items!!!
/// </summary>

using System;
using System.Collections.Generic;

#if UNITY_EDITOR

using System.Linq;

#endif

namespace Runtime.GameBoard
{
    internal interface IGameBoardModel
    {
        byte BoardSize { get; }

        IEnumerable<CheckerModel> GetCheckersOf(BoardSide player);
    }

    internal abstract class GameBoardModel: IGameBoardModel
    {
        public abstract byte BoardSize { get; }
        protected abstract CheckerModel[] UpperCheckers { get; }
        protected abstract CheckerModel[] LowerCheckers { get; }
        protected IEnumerable<CheckerModel> AllCheckers => UpperCheckers.MergeWith(LowerCheckers);

        public virtual IEnumerable<CheckerModel> GetCheckersOf(BoardSide player)
        {
#if UNITY_EDITOR
            if(AllCheckers.GroupBy(x => x).Any(g => g.Count() > 1))
                throw new NotSupportedException("!!! don't ever ever ever " +
                     "set same position in several {UpperCheckers+LowerCheckers} items!!!");
#endif
            return player switch
            {
                var requestedCheckers when requestedCheckers == BoardSide.UpperSide => UpperCheckers,

                var requestedCheckers when requestedCheckers == BoardSide.LowerSide => LowerCheckers,

                var requestedCheckers when requestedCheckers == BoardSide.BothSides => AllCheckers,

                _ => throw new ArgumentException($"Unknown placement [{player}]."),
            };
        }
    }

    internal class Checkers64Model: GameBoardModel
    {
        public override byte BoardSize { get; } = 8;
        protected override CheckerModel[] UpperCheckers { get; } = { 41, 43, 45, 47, 48, 50, 52, 54, 57, 59, 61, 63 };
        protected override CheckerModel[] LowerCheckers { get; } = { 0, 2, 4, 6, 9, 11, 13, 15, 16, 18, 20, 22 };
    }

    internal class Checkers100Model: GameBoardModel
    {
        public override byte BoardSize { get; } = 10;
        protected override CheckerModel[] UpperCheckers { get; } = { 71, 73, 75, 77, 79, 80, 82, 84, 86, 88, 91, 93, 95, 97, 99 };
        protected override CheckerModel[] LowerCheckers { get; } = { 0, 2, 4, 6, 8, 11, 13, 15, 17, 19, 20, 22, 24, 26, 28 };
    }

    internal static class BoardModelFactory
    {
        internal static IGameBoardModel CreateBoardModel(this CheckersGameType type)
        {
            return type switch
            {
                CheckersGameType.Checkers64 => new Checkers64Model(),
                CheckersGameType.Checkers100 => new Checkers100Model(),
                _ => throw new ArgumentException("Unknown checkers type", nameof(type))
            };
        }
    }
}
