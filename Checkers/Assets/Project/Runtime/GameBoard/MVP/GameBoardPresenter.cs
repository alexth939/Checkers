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
        private readonly IPointerConfiguration _pointerConfiguration;
        private readonly IPointerControl _pointerControl;
        private BoardViewMode? _currentViewMode;

        internal GameBoardPresenter(
            IGameBoardView boardView,
            IPointerControl pointerControl,
            IPointerConfiguration pointerConfiguration,
            CheckersGameType gameType)
        {
            _boardView = boardView;
            _boardModel = gameType.CreateBoardModel();
            _pointerControl = pointerControl;
            _pointerConfiguration = pointerConfiguration;

            _boardMath = new GameBoardMath(_boardModel, _boardView);
            _boardGenerators = new GameBoardGenerators(_boardModel, _boardMath);

            _boardView.Init(_boardMath, _boardGenerators);
        }

        ~GameBoardPresenter()
        {
            _boardMath.Dispose();
        }

        public event Action<Vector2Int> OnSelectedField;

        internal BoardViewMode ViewMode
        {
            get => _currentViewMode.Value;
            set
            {
                if(value == _currentViewMode)
                    return;

                UnsubscribeFromCurrentPointer();
                SubscribeToOtherPointer();
                _currentViewMode = value;

                void UnsubscribeFromCurrentPointer()
                {
                    if(_currentViewMode.HasValue)
                        _pointerControl.OnLeftDown -= RoutePointerEvent;
                }

                void SubscribeToOtherPointer()
                {
                    _pointerControl.OnLeftDown += RoutePointerEvent;

                    if(value == BoardViewMode.OrthographicTopDown)
                        _pointerConfiguration.SetPadSize(screenSize: _boardMath.BoardSizeInPixels);
                }
            }
        }

        internal void InitializeGame(out MoveEventArgs moveCheckerMethod)
        {
            _boardView.ShowBoard();

            var checkerPrefab = Resources.Load<CheckerView>(Paths.CheckerPrefabPath);

            SpawnCheckers(checkerPrefab, _boardModel.GetCheckersOf(BoardSide.BothSides));
            //SpawnCheckers(checkerPrefab, _boardModel.LowerCheckersPositions);
            //SpawnCheckers(checkerPrefab, _boardModel.UpperCheckersPositions);

            ViewMode = BoardViewMode.OrthographicTopDown;
            moveCheckerMethod = null;
        }

        private void RoutePointerEvent(Vector2 pointerCoords)
        {
            //OnSelectedField?.Invoke(_boardMath.ScreenToRawPosition(pointerCoords));
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
