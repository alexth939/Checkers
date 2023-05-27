using System;
using System.Collections.Generic;
using ProjectDefaults;
using UnityEngine;

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

        public void Show() => _boardView.ShowBoard();
    }
}
