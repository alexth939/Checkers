using System;
using UnityEngine;

namespace Runtime.GameBoard
{
    internal delegate void AnchorsArgs(out Vector3 minAnchor, out Vector3 maxAnchor);

    internal sealed class GameBoardMath: IDisposable
    {
        private static GameBoardMath _singleInstanceHolder;

        private readonly IGameBoardModel _boardModel;
        private readonly Vector3 _minBoardWorldAnchor;
        private readonly Vector3 _maxBoardWorldAnchor;
        private readonly Vector2 _cellRatio;
        private readonly Vector2 _halfCellRatio;

        internal GameBoardMath(IGameBoardModel boardModel, IGameBoardView boardView)
        {
            if(_singleInstanceHolder is not null)
                throw new NotSupportedException("Instance of GameBoardMath is already exist.");

            _singleInstanceHolder = this;

            (_minBoardWorldAnchor, _maxBoardWorldAnchor) = boardView.GetBoardCorners();
            _boardModel = boardModel;

            _cellRatio = new()
            {
                x = 1.0f / _boardModel.BoardSize,
                y = 1.0f / _boardModel.BoardSize
            };

            _halfCellRatio = _cellRatio / 2;
        }

        internal static GameBoardMath Instance =>
             _singleInstanceHolder ?? throw new NullReferenceException("GameBoardMath not Initialized yet.");

        public void Dispose()
        {
            _singleInstanceHolder = null;
        }

        internal Vector2 BoardSizeInPixels
        {
            get
            {
                Vector2 minBoardScreenAnchor = Camera.main.WorldToScreenPoint(_minBoardWorldAnchor);
                Vector2 maxBoardScreenAnchor = Camera.main.WorldToScreenPoint(_maxBoardWorldAnchor);
                float pixelWidth = maxBoardScreenAnchor.x - minBoardScreenAnchor.x;
                float pixelHeight = maxBoardScreenAnchor.y - minBoardScreenAnchor.y;
                return new Vector2(pixelWidth, pixelHeight);
            }
        }

        internal Vector2Int RawToBoardCoords(in byte boardPosition)
        {
            return new()
            {
                x = boardPosition % _boardModel.BoardSize,
                y = boardPosition / _boardModel.BoardSize
            };
        }

        internal Vector3 BoardToWorldCoords(in Vector2Int boardCoords)
        {
            float cellXT = _halfCellRatio.x + _cellRatio.x * boardCoords.x;
            float cellZT = _halfCellRatio.y + _cellRatio.y * boardCoords.y;

            Vector3 worldCoords = new Vector3()
            {
                x = Mathf.Lerp(_minBoardWorldAnchor.x, _maxBoardWorldAnchor.x, cellXT),
                z = Mathf.Lerp(_minBoardWorldAnchor.z, _maxBoardWorldAnchor.z, cellZT)
            };

            return worldCoords;
        }

        internal byte ScreenToRawPosition(Vector2 screenCoords)
        {
            Vector2 minBoardScreenAnchor = Camera.main.WorldToScreenPoint(_minBoardWorldAnchor);
            Vector2 maxBoardScreenAnchor = Camera.main.WorldToScreenPoint(_maxBoardWorldAnchor);

            Vector2 boardCoordsT = new()
            {
                x = Mathf.InverseLerp(minBoardScreenAnchor.x, maxBoardScreenAnchor.x, screenCoords.x),
                y = Mathf.InverseLerp(minBoardScreenAnchor.y, maxBoardScreenAnchor.y, screenCoords.y)
            };

            Vector2Int boardCoords = new()
            {
                x = (int)(boardCoordsT.x * _boardModel.BoardSize),
                y = (int)(boardCoordsT.y * _boardModel.BoardSize)
            };

            boardCoords.Clamp(Vector2Int.zero, new Vector2Int(_boardModel.BoardSize - 1, _boardModel.BoardSize - 1));

            return (byte)(boardCoords.y * _boardModel.BoardSize + boardCoords.x);
        }

        public Vector2Int WorldToBoardCoords(Vector3 worldPoint)
        {
            throw new NotImplementedException();
        }
    }
}
