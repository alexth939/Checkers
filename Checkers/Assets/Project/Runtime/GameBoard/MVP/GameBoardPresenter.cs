using System;
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

        public event Action<Vector2Int> OnClickedField;

        public bool IsClickingEnabled
        {
            set
            {
                if(value == true)
                    _boardView.OnBoardClick += HandleBoardClicked;
                else
                    _boardView.OnBoardClick -= HandleBoardClicked;
            }
        }

        private void HandleBoardClicked(PointerEventData eventData)
        {
            Vector3 clickWorldPosition = eventData.pointerCurrentRaycast.worldPosition;
            var corners = _boardView.GetBoardCorners();

            // Clamped to board coords: from 0 to plane.width.
            var clampedBoardCoords = new Vector2()
            {
                x = Mathf.InverseLerp(corners.LowerLeft.x, corners.UpperRight.x, clickWorldPosition.x),
                y = Mathf.InverseLerp(corners.LowerLeft.y, corners.UpperRight.y, clickWorldPosition.y)
            };

            // Field coords: x: 0 to fieldsCount in row.
            //              y: 0 to fieldsCount in column.
            var fieldBoardCoords = new Vector2Int()
            {
                x = Mathf.FloorToInt(clampedBoardCoords.x * _boardModel.BoardSize),
                y = Mathf.FloorToInt(clampedBoardCoords.y * _boardModel.BoardSize)
            };

            OnClickedField?.Invoke(fieldBoardCoords);
        }

        public void Show() => _boardView.ShowBoard();
    }
}
