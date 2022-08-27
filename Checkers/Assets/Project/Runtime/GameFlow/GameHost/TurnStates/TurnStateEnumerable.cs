using System.Collections;
using System.Collections.Generic;

namespace Runtime.GameFlow
{
     internal sealed class TurnStateEnumerable: IEnumerable<TurnState>
     {
          private readonly ITurnStatesCollection _statesCollection;

          internal TurnStateEnumerable(ITurnStatesCollection statesCollection)
          {
               _statesCollection = statesCollection;
          }

          public IEnumerator<TurnState> GetEnumerator()
          {
               TurnState currentState = _statesCollection.First;
               yield return currentState;

               while(_statesCollection.TryGetNext(currentState, out currentState))
               {
                    yield return currentState;
               }
          }

          IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
     }
}
