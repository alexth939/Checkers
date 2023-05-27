using UnityEngine;
using UnityEngine.SceneManagement;
using Runtime.GameBoard;
using Runtime.GameFlow;

namespace Runtime.ScenePresenters
{
    internal sealed class PlayScenePresenter: ScenePresenter
    {
        [SerializeField] private PlaySceneDIContainer _dependencies;
        private IGameHost _gameHost;

        protected override void OnEnteringScene()
        {
            PlaySceneDependencyInjector.Expose(_dependencies);

            var chosedGameType = CheckersGameType.Checkers64;
            var gameBoard = new GameBoardPresenter(chosedGameType);
            gameBoard.InitializeGame(out var moveCheckerMethod);

            var flowModel = new GameFlowModel(options =>
            {
                options.LocalPlayers = new()
                {
                    [BoardSide.LowerSide] = new HumanBoardPlayer(gameBoard),
                };
            });

            var flowProcessor = new GameFlowProcessor(flowModel, moveCheckerMethod);
            _gameHost = new GameHost(chosedGameType, flowModel);
        }

        protected override void OnLeavingScene()
        {
            PlaySceneDependencyInjector.Dispose();
        }

        [EasyButtons.Button(Mode = EasyButtons.ButtonMode.EnabledInPlayMode)]
        private void StartGame()
        {
            _gameHost.BeginGame();
        }
    }
}
