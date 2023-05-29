using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.GameBoard
{
    internal interface IGameBoardView
    {
        event Action<PointerEventData> OnBoardClick;

        Vector2 SpriteSize { get; }

        void Init(GameBoardMath boardMath, GameBoardGenerators boardGenerators);

        void ShowBoard();

        /// <summary>
        ///     In world coordinates.
        /// </summary>
        (Vector3 LowerLeft, Vector3 UpperRight) GetBoardCorners();

        void HightlightFields(params byte[] rawPositions);

        void DimHightlightedFields();
    }
}
