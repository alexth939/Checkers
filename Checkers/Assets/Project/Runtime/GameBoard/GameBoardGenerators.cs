using System;
using System.Linq;
using UnityEngine;

namespace Runtime.GameBoard
{
    internal sealed class GameBoardGenerators
    {
        private readonly IGameBoardModel _boardModel;
        private readonly GameBoardMath _boardMath;

        internal GameBoardGenerators(IGameBoardModel board, GameBoardMath boardMath)
        {
            _boardModel = board;
            _boardMath = boardMath;
        }

        internal Sprite GenerateBoardSprite()
        {
            GenerateBoardTexture(out var boardTexture);

            var textureSize = new Vector2(boardTexture.width, boardTexture.height);
            var textureRect = new Rect(position: Vector2.zero, textureSize);
            var centeredPivot = Vector2.one / 2;

            return Sprite.Create(boardTexture, textureRect, centeredPivot);
        }

        private void GenerateBoardTexture(out Texture2D boardTexture)
        {
            byte boardSize = _boardModel.BoardSize;

            boardTexture = new Texture2D(boardSize, boardSize)
            {
                filterMode = FilterMode.Point,
                wrapMode = TextureWrapMode.Clamp
            };

            for(byte x = 0; x < boardSize; x++)
            {
                for(byte y = 0; y < boardSize; y++)
                {
                    boardTexture.SetPixel(x, y, (x + y) % 2 == 0 ? Color.white : Color.black);
                }
            }

            boardTexture.Apply();
        }

        internal Sprite GenerateHighlightedFieldsMap(byte[] rawPositions)
        {
            GenerateHighlightedFields(out var highlightedMap, rawPositions);

            var textureSize = new Vector2(highlightedMap.width, highlightedMap.height);
            var textureRect = new Rect(position: Vector2.zero, textureSize);
            var centeredPivot = Vector2.one / 2;

            return Sprite.Create(highlightedMap, textureRect, centeredPivot);
        }

        private void GenerateTransparentMap(out Texture2D transparentTexture)
        {
            byte boardSize = _boardModel.BoardSize;

            transparentTexture = new Texture2D(boardSize, boardSize)
            {
                filterMode = FilterMode.Point,
                wrapMode = TextureWrapMode.Clamp
            };

            var transparentPixels = Enumerable.Repeat(new Color(0, 0, 0, 0), count: boardSize * boardSize);

            transparentTexture.SetPixels(transparentPixels.ToArray());

            transparentTexture.Apply();
        }

        private void GenerateHighlightedFields(out Texture2D highlightedMap, byte[] rawPositions)
        {
            GenerateTransparentMap(out highlightedMap);

            for(int i = 0; i < rawPositions.Length; i++)
            {
                Vector2Int fieldCoords = _boardMath.RawToBoardCoords(rawPositions[i]);
                highlightedMap.SetPixel(fieldCoords.x, fieldCoords.y, Color.yellow);
            }

            highlightedMap.Apply();
        }
    }
}
