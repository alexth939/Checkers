using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.GameBoard
{
    internal interface IGameBoardView
    {
        event Action<PointerEventData> OnUserClick;

        Vector2 SpriteSize { get; }

        void Init(GameBoardMath boardMath, GameBoardGenerators boardGenerators);

        void ShowBoard();

        void GetWorldAnchors(out Vector3 minBoardWorldAnchor, out Vector3 maxBoardWorldAnchor);

        void HightlightFields(params byte[] rawPositions);

        void DimHightlightedFields();
    }
}
