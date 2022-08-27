using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Runtime.GameBoard;

namespace Runtime.ScenePresenters
{
     [Serializable]
     internal sealed class PlaySceneDIContainer
     {
          [SerializeField] private GameBoardView _gameBoardView;
          [SerializeField] private PointerControl _pointerControl;

          internal IGameBoardView GameBoardView => _gameBoardView;
          internal IPointerControl PointerControl => _pointerControl;
          internal IPointerConfiguration PointerConfiguration => _pointerControl;
     }
}
