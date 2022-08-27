using UnityEngine;
using UnityEngine.SceneManagement;
using Runtime.GameBoard;
using Runtime.GameFlow;

namespace Runtime.ScenePresenters
{
     internal sealed class PlayScenePresenter: ScenePresenter
     {
          [SerializeField] private PlaySceneDIContainer _dependencies;

          [EasyButtons.Button]
          protected override void EnteringScene()
          {
               PlaySceneDependencyInjector.Expose(_dependencies);

               var chosedGameType = CheckersGameType.Checkers64;
               var gameBoard = new GameBoardPresenter(chosedGameType);
               gameBoard.InitializeGame(out var moveCheckerMethod);

               var flowModel = new GameFlowModel(options =>
               {
                    options.LocalPlayers = new()
                    {
                         [BoardSide.LowerSide] = default,
                         [BoardSide.UpperSide] = default
                    };
                    options.CommandsChannel = default;
                    options.MovesChannel = default;
               });

               var flowProcessor = new GameFlowProcessor(flowModel, moveCheckerMethod);
               var gameHost = new GameHost(chosedGameType, flowModel);
          }

          protected override void LeavingScene()
          {
               PlaySceneDependencyInjector.Dispose();
          }
     }
}
