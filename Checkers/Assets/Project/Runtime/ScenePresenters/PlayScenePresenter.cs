using Runtime.GameBoard;
using Runtime.GameFlow;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.ScenePresenters
{
    internal sealed class PlayScenePresenter: ScenePresenter
    {
        [SerializeField] private UnityEngine.EventSystems.PointerControl _pointerControl;
        [SerializeField] private GameBoardView _gameBoardView;
        private IGameHost _gameHost;

        protected override void OnEnteringScene()
        {
            var chosedGameType = CheckersGameType.Checkers64;
            var gameBoard = new GameBoardPresenter(
                _gameBoardView,
                _pointerControl,
                _pointerControl,
                chosedGameType);

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

        [EasyButtons.Button(Mode = EasyButtons.ButtonMode.EnabledInPlayMode)]
        private void StartGame()
        {
            _gameHost.BeginGame();
        }
    }
}
