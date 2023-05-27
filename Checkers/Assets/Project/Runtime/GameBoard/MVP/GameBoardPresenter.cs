using System;
using System.Collections.Generic;
using ProjectDefaults;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.GameBoard
{
    internal delegate void MoveEventArgs(BoardSide player, byte[] move);

    internal sealed class GameBoardPresenter: IGameBoardPresenter
    {
        private readonly GameBoardGenerators _boardGenerators;
        private readonly GameBoardMath _boardMath;
        private readonly IGameBoardModel _boardModel;
        private readonly IGameBoardView _boardView;

        internal GameBoardPresenter(
            IGameBoardView boardView,
            CheckersGameType gameType)
        {
            _boardView = boardView;
            _boardModel = gameType.CreateBoardModel();

            _boardMath = new GameBoardMath(_boardModel, _boardView);
            _boardGenerators = new GameBoardGenerators(_boardModel, _boardMath);

            _boardView.Init(_boardMath, _boardGenerators);
        }

        ~GameBoardPresenter()
        {
            _boardMath.Dispose();
        }

        public event Action<Vector2Int> OnSelectedField;

        internal void InitializeGame(out MoveEventArgs moveCheckerMethod)
        {
            _boardView.ShowBoard();

            var checkerPrefab = Resources.Load<CheckerView>(Paths.CheckerPrefabPath);

            SpawnCheckers(checkerPrefab, _boardModel.GetCheckersOf(BoardSide.BothSides));
            //SpawnCheckers(checkerPrefab, _boardModel.LowerCheckersPositions);
            //SpawnCheckers(checkerPrefab, _boardModel.UpperCheckersPositions);

            moveCheckerMethod = null;
        }

        private void SpawnCheckers(CheckerView checkerPrefab, IEnumerable<CheckerModel> checkersPositions)
        {
            foreach(CheckerModel checker in checkersPositions)
            {
                _boardView.SpawnChecker(checkerPrefab, checker, BoardSide.LowerSide);
            }
        }
    }
}
