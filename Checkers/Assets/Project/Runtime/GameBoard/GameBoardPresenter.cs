using UnityEngine;

namespace Runtime.GameBoard
{
    public sealed class GameBoardPresenter
    {
        private readonly IGameBoardView _boardView;

        public GameBoardPresenter(IGameBoardView boardView)
        {
            _boardView = boardView;
            Generate();
        }

        private void Generate()
        {
            int boardSize = 8;

            var boardTexture = new Texture2D(boardSize, boardSize)
            {
                filterMode = FilterMode.Point,
                wrapMode = TextureWrapMode.Clamp
            };

            for(int x = 0; x < boardSize; x++)
            {
                for(int y = 0; y < boardSize; y++)
                {
                    boardTexture.SetPixel(x, y, (x + y) % 2 == 0 ? Color.white : Color.black);
                }
            }

            boardTexture.Apply();
            _boardView.ShowBoard(boardTexture);
        }
    }
}
