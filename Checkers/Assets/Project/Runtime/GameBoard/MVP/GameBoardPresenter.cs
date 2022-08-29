using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ProjectDefaults;
using static Runtime.ScenePresenters.PlaySceneDependencyInjector;

namespace Runtime.GameBoard
{
     internal delegate void MoveEventArgs(BoardSide player, byte[] move);

     internal sealed class GameBoardPresenter: IGameBoardPresenter
     {
          private readonly GameBoardGenerators _boardGenerators;
          private readonly GameBoardMath _boardMath;
          private readonly IGameBoardModel _boardModel;
          private readonly IGameBoardView _boardView;
          private BoardViewMode? _currentViewMode;

          internal GameBoardPresenter(CheckersGameType gameType)
          {
               _boardView = (null as IGameBoardView).FromScene();
               _boardModel = gameType.CreateBoardModel();

               _boardMath = new GameBoardMath(_boardModel, _boardView);
               _boardGenerators = new GameBoardGenerators(_boardModel, _boardMath);

               _boardView.Init(_boardMath, _boardGenerators);
          }

          ~GameBoardPresenter()
          {
               _boardMath.Dispose();
          }

          public event Action<byte> OnBoardFieldClicked;

          internal BoardViewMode ViewMode
          {
               get => _currentViewMode.Value;
               set
               {
                    if(value == _currentViewMode)
                         return;

                    UnsubscribeFromCurrentPointer();
                    SubscribeToOtherPointer();
                    _currentViewMode = value;

                    void UnsubscribeFromCurrentPointer()
                    {
                         if(_currentViewMode.HasValue)
                              (null as IPointerControl).FromScene(_currentViewMode.Value).OnLeftDown -= RoutePointerEvent;
                    }

                    void SubscribeToOtherPointer()
                    {
                         (null as IPointerControl).FromScene(value).OnLeftDown += RoutePointerEvent;

                         if(value == BoardViewMode.OrthographicTopDown)
                              (null as IPointerConfiguration).FromScene(/*value*/).
                                   SetPadSize(screenSize: _boardMath.BoardSizeInPixels);
                    }
               }
          }

          internal void InitializeGame(out MoveEventArgs moveCheckerMethod)
          {
               _boardView.ShowBoard();

               var checkerPrefab = Resources.Load<CheckerView>(Paths.CheckerPrefabPath);

               SpawnCheckers(checkerPrefab, _boardModel.GetCheckersOf(BoardSide.BothSides));
               //SpawnCheckers(checkerPrefab, _boardModel.LowerCheckersPositions);
               //SpawnCheckers(checkerPrefab, _boardModel.UpperCheckersPositions);

               ViewMode = BoardViewMode.OrthographicTopDown;
               moveCheckerMethod = null;
          }

          private void SpawnCheckers(CheckerView checkerPrefab, IEnumerable<CheckerModel> checkersPositions)
          {
               foreach(CheckerModel checker in checkersPositions)
               {
                    _boardView.SpawnChecker(checkerPrefab, checker, BoardSide.LowerSide);
               }
          }

          private void RoutePointerEvent(Vector2 pointerCoords)
          {
               OnBoardFieldClicked?.Invoke(_boardMath.ScreenToRawPosition(pointerCoords));
          }
     }
}