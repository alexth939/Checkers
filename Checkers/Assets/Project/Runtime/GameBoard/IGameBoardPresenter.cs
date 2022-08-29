using System;

namespace Runtime.GameBoard
{
     internal interface IGameBoardPresenter
     {
          event Action<byte> OnBoardFieldClicked;
     }
}
