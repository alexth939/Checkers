using System;
using System.Collections.Generic;
using Runtime.GameBoard;

namespace Runtime.GameFlow
{
     internal sealed class GameFlowProcessor: IDisposable
     {
          private readonly GameFlowModel _flowModel;

          internal GameFlowProcessor(GameFlowModel flowModel, MoveEventArgs moveCheckerMethod)
          {
               _flowModel = flowModel;
               MoveChecker = moveCheckerMethod;

               CommandsChannel.OnPingRequest += HandlePingRequest;
               MovesChannel.OnPlayerMoved += HandlePlayerMoved;
          }

          private MoveEventArgs MoveChecker;

          private IReadOnlyDictionary<BoardSide, IGameBoardPlayer> LocalPlayers => _flowModel.LocalPlayers;
          private GameCommandsChannel CommandsChannel => _flowModel.CommandsChannel;
          private GameMovesChannel MovesChannel => _flowModel.MovesChannel;

          public void Dispose()
          {
               CommandsChannel.OnPingRequest -= HandlePingRequest;
               MovesChannel.OnPlayerMoved -= HandlePlayerMoved;
          }

          private void HandlePingRequest(BoardSide target)
          {
               LocalPlayers.TryGetValue(target, out IGameBoardPlayer player);
               player?.IsReady(positiveResponse: () => CommandsChannel.SendPingResponse(sender: target));
          }

          private void HandleBeginTurnCommand(BoardSide target)
          {
               LocalPlayers.TryGetValue(target, out IGameBoardPlayer player);
               player.BeginTurn(move => CommandsChannel.TryMove(target, move));
          }

          private void HandleTurnCancellationCommand(BoardSide target)
          {
               LocalPlayers.TryGetValue(target, out IGameBoardPlayer player);
               player.CancelTurn();
          }

          private void HandlePlayerMoved(BoardSide player, byte[] move)
          {
               MoveChecker(player, move);
          }
     }
}
