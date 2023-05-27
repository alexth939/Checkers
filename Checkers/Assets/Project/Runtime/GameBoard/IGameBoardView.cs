using System;
using UnityEngine;

namespace Runtime.GameBoard
{
    internal interface IGameBoardView
    {
        Vector2 SpriteSize { get; }

        void Init(GameBoardMath boardMath, GameBoardGenerators boardGenerators);

        void ShowBoard();

        void GetWorldAnchors(out Vector3 minBoardWorldAnchor, out Vector3 maxBoardWorldAnchor);

        void HightlightFields(params byte[] rawPositions);

        void DimHightlightedFields();
    }
}
