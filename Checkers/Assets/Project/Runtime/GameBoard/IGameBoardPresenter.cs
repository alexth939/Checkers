using System;
using UnityEngine;

namespace Runtime.GameBoard
{
    internal interface IGameBoardPresenter
    {
        event Action<Vector2Int> OnSelectedField;
    }
}
