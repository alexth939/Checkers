using System;
using UnityEngine.EventSystems;
using External.DelegateExtensions;
using static Runtime.ScenePresenters.PlaySceneDependencyInjector;
using System.Threading;
using System.Threading.Tasks;

namespace Runtime.GameBoard
{
     internal sealed class LocalBoardPlayer: GameBoardPlayer
     {
          private IPointerControl _cursorControl;
          private IHumanMindInterpreter _humanInterpreter;

          private Action<byte[]> MoveChecker;

          internal LocalBoardPlayer(Action<PlayerDataPort> argsSetter)
          {
               var args = new PlayerDataPort();
               argsSetter.Invoke(args);

               argsSetter.Extract(out var results);

               _humanInterpreter = new HumanMindInterpreter(args =>
               {

               });

               _cursorControl = (null as IPointerControl).FromScene();
          }

          internal void MakeMove(Action<byte[]> move)
          {
               
          }

          internal class PlayerDataPort
          {

          }
     }
}
