using ProjectDefaults;
using Runtime.GameBoard;
using Runtime.GameFlow;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.ScenePresenters
{
    internal sealed class PlayScenePresenter: ScenePresenter
    {
        [SerializeField] private GameBoardView _gameBoardView;
        private IGameHost _gameHost;

        protected override void OnEnteringScene()
        {
            var chosedGameType = CheckersGameType.Checkers64;
            var gameBoard = new GameBoardPresenter(
                _gameBoardView,
                chosedGameType);

            gameBoard.Show();
            gameBoard.OnClickedField += new System.Action<Vector2Int>(b => { Debug.Log($"last: {b}"); });
            gameBoard.IsClickingEnabled = true;

            // spawn checkers.
            //var checkerPrefab = Resources.Load<CheckerView>(Paths.CheckerPrefabPath);

            var flowModel = new GameFlowModel(options =>
            {
                options.LocalPlayers = new()
                {
                    [BoardSide.LowerSide] = new HumanBoardPlayer(gameBoard),
                };
            });

            var flowProcessor = new GameFlowProcessor(flowModel);
            _gameHost = new GameHost(chosedGameType, flowModel);
        }

        private void StartGame()
        {
            _gameHost.BeginGame();
        }
    }
}
