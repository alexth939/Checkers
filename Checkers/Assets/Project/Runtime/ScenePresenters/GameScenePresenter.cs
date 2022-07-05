using System;
using UnityEngine;
using Runtime.GameBoard;
using UnityEngine.SceneManagement;

namespace Runtime.ScenePresenters
{
     public sealed class GameScenePresenter: ScenePresenter
     {
          [SerializeField] private SceneDependencies _dependencies;

          protected override void EnteringScene()
          {
               new GameBoardPresenter(_dependencies.GameBoardView);
          }

          [Serializable]
          private sealed class SceneDependencies
          {
               public IGameBoardView GameBoardView => _gameBoardView;

               [SerializeField] private GameBoardView _gameBoardView;
          }
     }
}
