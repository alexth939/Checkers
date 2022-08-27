using System;
using System.Collections.Generic;
using External.DelegateExtensions;

namespace Runtime.GameBoard
{
     internal sealed class HumanMindInterpreter: IHumanMindInterpreter
     {
          private Stack<byte> _checkerPath;
          //private PathVisualizer _pathVisualizer;

          internal HumanMindInterpreter(Action<MindDependencies> argsSetter)
          {
               _checkerPath = new Stack<byte>();

               argsSetter.Extract(out var args);
          }

          private Func<byte, bool> CanSelectAtPosition;
          private Func<IEnumerable<byte>, bool> IsValidCheckerPath;

          //private Action<byte> HighlightBoardPosition;
          //private Action DimHighlightedCells;

          public void HandleClick(byte boardPosition)
          {
               if(TrySelectChecker(boardPosition))
               {
                    //DimHighlightedCells?.Invoke();
                    //HighlightBoardPosition?.Invoke(boardPosition);
               }
               else if(_checkerPath.Count > 0 && TryAppendMove(boardPosition))
               {
                    //HighlightBoardPosition?.Invoke(boardPosition);
               }
          }

          private bool TrySelectChecker(byte boardPosition)
          {
               bool result = CanSelectAtPosition(boardPosition);

               if(result == true)
               {
                    _checkerPath.Clear();
                    _checkerPath.Push(boardPosition);
               }

               return result;
          }

          private bool TryAppendMove(byte positionToAppend)
          {
               _checkerPath.Push(positionToAppend);

               bool result = IsValidCheckerPath(_checkerPath);

               if(result == false)
                    _ = _checkerPath.Pop();

               return result;
          }

          internal sealed class MindDependencies
          {

          }
     }
}
