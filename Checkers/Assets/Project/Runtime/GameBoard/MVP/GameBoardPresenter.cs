using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ProjectDefaults;
using static Runtime.ScenePresenters.PlaySceneDependencyInjector;

namespace Runtime.GameBoard
{
     internal delegate void MoveEventArgs(BoardSide player, byte[] move);

     internal sealed class GameBoardPresenter
     {
          private readonly GameBoardGenerators _boardGenerators;
          private readonly GameBoardMath _boardMath;
          private readonly IGameBoardModel _boardModel;
          private readonly IGameBoardView _boardView;

          internal GameBoardPresenter(CheckersGameType gameType)
          {
               _boardView = (null as IGameBoardView).FromScene();
               _boardModel = gameType.CreateBoardModel();

               _boardMath = new GameBoardMath(_boardModel, _boardView);
               _boardGenerators = new GameBoardGenerators(_boardModel, _boardMath);

               _boardView.Init(_boardMath, _boardGenerators);
               ConfigurePointerControl(_boardMath);
          }

          ~GameBoardPresenter()
          {
               _boardMath.Dispose();
          }

          internal void InitializeGame(out MoveEventArgs moveCheckerMethod)
          {
               _boardView.ShowBoard();

               var checkerPrefab = Resources.Load<CheckerView>(Paths.CheckerPrefabPath);

               SpawnCheckers(checkerPrefab, _boardModel.GetCheckersOf(BoardSide.BothSides));
               //SpawnCheckers(checkerPrefab, _boardModel.LowerCheckersPositions);
               //SpawnCheckers(checkerPrefab, _boardModel.UpperCheckersPositions);

               moveCheckerMethod = null;
          }

          private void ConfigurePointerControl(GameBoardMath boardMath)
          {
               var pointerConfiguration = (null as IPointerConfiguration).FromScene();

               pointerConfiguration.SetPadSize(screenSize: boardMath.BoardSizeInPixels);
               pointerConfiguration.SetCoordsParseMethod(screenCoords => (byte)boardMath.ScreenToRawPosition(screenCoords));
          }

          private void SpawnCheckers(CheckerView checkerPrefab, IEnumerable<CheckerModel> checkersPositions)
          {
               foreach(CheckerModel checker in checkersPositions)
               {
                    _boardView.SpawnChecker(checkerPrefab, checker);
               }
          }
     }
}