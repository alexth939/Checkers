using System.Collections.Generic;
using Runtime.GameBoard;

namespace Runtime.GameFlow
{
     internal sealed class GameHost: IGameHost
     {
          private readonly ITurnStatesCollection _statesCollection;

          internal GameHost(CheckersGameType gameType, GameFlowModel flowModel)
          {
               _statesCollection = gameType.InitTurnStates(flowModel);
          }

          public void BeginGame()
          {
               IterateTurnsAsync();
          }

          private async void IterateTurnsAsync()
          {
               var turnEnumerator = new[] { BoardSide.LowerSide, BoardSide.UpperSide }.GetLoopedEnumerator();

               while(turnEnumerator.MoveNext())
               {
                    var activePlayer = turnEnumerator.Current;
                    var turnStates = CreateTurnStateEnumerator();

                    while(turnStates.MoveNext())
                    {
                         await turnStates.Current.ProcessAsync(activePlayer);
                    }
               }
          }

          private IEnumerator<TurnState> CreateTurnStateEnumerator()
          {
               return new TurnStateEnumerable(_statesCollection).GetEnumerator();
          }
     }
}