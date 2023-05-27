using System;

namespace Runtime.GameBoard
{
    internal interface IHumanMindInterpreter
    {
        void HandleClick(byte boardPosition);
    }
}
