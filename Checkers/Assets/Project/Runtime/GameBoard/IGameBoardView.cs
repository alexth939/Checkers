using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Runtime.GameBoard
{
     public interface IGameBoardView
     {
          void ShowBoard(Texture2D boardTexture);
     }
}
