using System;
using System.Collections.Generic;

namespace Runtime.GameBoard
{
     internal interface IHumanMindInterpreter
     {
          void HandleClick(byte boardPosition);
     }
}