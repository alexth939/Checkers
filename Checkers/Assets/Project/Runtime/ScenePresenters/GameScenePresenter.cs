using Runtime.GameBoard;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.ScenePresenters
{
    public sealed class GameScenePresenter: ScenePresenter
    {
        [SerializeField] private GameBoardView _gameBoardView;

        protected override void OnEnteringScene()
        {
            new GameBoardPresenter(_gameBoardView);
        }
    }
}
