using System;
using System.Linq;
using UnityEngine;

namespace Runtime.GameBoard
{
     internal interface IGameRules
     {
          bool CanSelectChecker(BoardSide requester, byte boardPosition);

          /// <summary>
          /// <para> The collection has two parts: </para>
          /// <br> path[0] represents the board position of chosed checker. </br>
          /// <br> path[1+] represents further moves, where each pair is a move [src, dst]. </br>
          /// </summary>
          bool IsValidCheckerPath(BoardSide requester, byte[] path);
     }

     internal abstract class GameRules: IGameRules
     {
          protected abstract IGameBoardModel Board { get; }

          public virtual bool CanSelectChecker(BoardSide owner, byte rawPosition)
          {
               return Board.GetCheckersOf(owner).Any(checker => checker.Position == rawPosition);
          }

          public abstract bool IsValidCheckerPath(BoardSide requester, byte[] path);

          protected abstract bool InitTests(MoveData moveData);

          protected bool IsShortDiagonalStep(MoveData moveData)
          {
               return moveData.StepOffset switch
               {
                    { x: -1 or 1, y: 1 } when moveData.Requester == BoardSide.LowerSide => true,
                    { x: -1 or 1, y: -1 } when moveData.Requester == BoardSide.UpperSide => true,
                    _ => false
               };
          }

          protected bool IsShortDiagonalAttack(MoveData moveData)
          {
               var AbsoluteOffset = new Vector2Int()
               {
                    x = Mathf.Abs(moveData.StepOffset.x),
                    y = Mathf.Abs(moveData.StepOffset.y)
               };

               bool validAttack = AbsoluteOffset switch
               {
                    { x: 2, y: 2 } => true,
                    _ => false
               };

               validAttack &= false;

               return validAttack;
          }

          protected bool IsDestinationVacant(MoveData moveData)
          {
               //Board.GetCheckersOf(BoardSide.BothSides).
               return false;
          }

          protected sealed class MoveData
          {
               internal MoveData(byte origin, byte destination, BoardSide requester)
               {
                    Requester = requester;

                    RawOrigin = origin;
                    RawDestination = destination;
                    OriginCoords = ParseRawToCoords(RawOrigin);
                    DestinationCoords = ParseRawToCoords(RawDestination);
               }

               private MoveData(MoveData previousMove, byte destination)
               {
                    PreviousMove = previousMove;
                    Requester = previousMove.Requester;

                    RawOrigin = previousMove.RawDestination;
                    RawDestination = destination;
                    OriginCoords = previousMove.DestinationCoords;
                    DestinationCoords = ParseRawToCoords(RawDestination);

                    previousMove.Influence?.Invoke(this);
               }

               internal MoveData ContinueWith(byte destination) => new MoveData(PreviousMove, destination);

               internal CheckerType CheckerType { get; set; }
               internal byte RawOrigin { get; }
               internal byte RawDestination { get; }
               internal Vector2Int OriginCoords { get; }
               internal Vector2Int DestinationCoords { get; }
               internal Vector2Int StepOffset => DestinationCoords - OriginCoords;
               internal BoardSide Requester { get; }
               internal MoveData PreviousMove { get; }
               internal Action<MoveData> Influence { private get; set; }

               private Vector2Int ParseRawToCoords(byte rawPosition)
               {
                    return GameBoardMath.Instance.RawToBoardCoords(rawPosition);
               }
          }
     }

     internal sealed class Checkers64Rules: GameRules
     {
          internal Checkers64Rules(IGameBoardModel gameModel)
          {
               Board = gameModel;

               // check for illegal checkers on board
               // because checker capabilities is hard coded in <GameRules> classes
          }

          protected override IGameBoardModel Board { get; }

          public override bool IsValidCheckerPath(BoardSide requester, byte[] path)
          {
               var currentMove = new MoveData(origin: path[0], destination: path[1], requester)
               {
                    CheckerType = CheckerType.QueenChecker
               };
               RunTests(currentMove, out bool passedAllTests);

               for(int i = 2; i < path.Length; i++)
               {
                    currentMove = currentMove.ContinueWith(destination: path[i]);
                    RunTests(currentMove, out bool passedAllTests2);
               }

               throw new System.NotImplementedException();
          }

          protected override bool InitTests(MoveData moveData)
          {
               bool isFirstMove = moveData.PreviousMove is null;
               throw new NotImplementedException();
          }

          void RunTests(MoveData moveData, out bool passedAllTests)
          {
               bool isFirstMove = moveData.PreviousMove is null;

               if(isFirstMove)
               {
                    if(moveData.CheckerType == CheckerType.RegularChecker)
                    {

                    }
               }
               else
               {
               }

               passedAllTests = false;
          }
     }

     internal sealed class Checkers100Rules: GameRules
     {
          internal Checkers100Rules(IGameBoardModel gameModel)
          {
               Board = gameModel;
          }

          protected override IGameBoardModel Board { get; }

          public override bool IsValidCheckerPath(BoardSide requester, byte[] path)
          {
               throw new NotImplementedException();
          }

          protected override bool InitTests(MoveData move)
          {
               throw new NotImplementedException();
          }
     }

     internal static class GameRulesInitializer
     {
          internal static GameRules CreateRules(this IGameBoardModel gameModel)
          {
               return gameModel switch
               {
                    Checkers64Model => new Checkers64Rules(gameModel),
                    Checkers100Model => new Checkers100Rules(gameModel),
                    _ => throw new ArgumentException("Unknown checkers type", nameof(gameModel))
               };
          }
     }
}