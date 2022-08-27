using System;

namespace Runtime.GameFlow
{
     internal class Checkers64TurnStates: ITurnStatesCollection
     {
          GameFlowModel _flowModel;

          public Checkers64TurnStates(GameFlowModel flowModel)
          {
               _flowModel = flowModel;
          }

          public TurnState First => new PingPlayerState(_flowModel);

          public bool TryGetNext(TurnState completedState, out TurnState nextState)
          {
               nextState = completedState switch
               {
                    PingPlayerState state => HandleResults(state),
                    PlayerMovingState state => HandleResults(state),
                    _ => null
               };

               bool hasContinuation = nextState is not null;
               return hasContinuation;
          }

          private TurnState HandleResults(PingPlayerState completedState)
          {
               return completedState.StateResult switch
               {
                    TurnStateResult.SuccessfullyCompleted => new PlayerMovingState(_flowModel),
                    TurnStateResult.TimeoutOccurred => new ForcingRandomMoveState(_flowModel),
                    TurnStateResult.StillRunning => throw new NotSupportedException("Not finished state!"),
                    _ => throw new NotImplementedException()
               };
          }

          private TurnState HandleResults(PlayerMovingState completedState)
          {
               throw new NotImplementedException();
          }
     }
}
