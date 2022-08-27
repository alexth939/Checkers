using System;
using UnityEngine;

namespace Runtime.GameBoard
{
     internal interface IGameBoardView
     {
          Vector2 SpriteSize { get; }

          void Init(GameBoardMath boardMath, GameBoardGenerators boardGenerators);
          void ShowBoard();
          void SpawnChecker(CheckerView view, CheckerModel model);
          void GetWorldAnchors(out Vector3 minBoardWorldAnchor, out Vector3 maxBoardWorldAnchor);
          void MoveChecker(CheckerView checker, byte boardDestination, Action onDone = null);
          void HightlightFields(params byte[] rawPositions);
          void DimHightlightedFields();
     }
}
