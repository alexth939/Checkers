using System;
using UnityEngine;
using ProjectDefaults;

namespace Runtime.GameBoard
{
     internal sealed class GameBoardView: MonoBehaviour, IGameBoardView
     {
          [SerializeField] private SpriteRenderer _boardSpriteRenderer;
          [SerializeField] private SpriteRenderer _highlightingSptiteRenderer;
          [SerializeField] private Transform _boardTransform;
          [SerializeField] private GameObject _upperPlayerStash;
          [SerializeField] private GameObject _lowerPlayerStash;

          private Func<Sprite> BoardSpriteGenerationMethod;
          private Func<byte[], Sprite> HighlightedMapGenerationMethod;
          private Func<byte, Vector3> RawToWorldParseMethod;

          public Vector2 SpriteSize => _boardSpriteRenderer.size;

          public void GetWorldAnchors(out Vector3 minBoardAnchor, out Vector3 maxBoardAnchor)
          {
               minBoardAnchor = _boardSpriteRenderer.bounds.min;
               maxBoardAnchor = _boardSpriteRenderer.bounds.max;
          }

          public void Init(GameBoardMath boardMath, GameBoardGenerators boardGenerators)
          {
               RawToWorldParseMethod = rawPosition =>
               {
                    Vector2Int boardCoords = boardMath.RawToBoardCoords(in rawPosition);
                    Vector3 worldCoords = boardMath.BoardToWorldCoords(in boardCoords);
                    return worldCoords;
               };

               BoardSpriteGenerationMethod = boardGenerators.GenerateBoardSprite;
               HighlightedMapGenerationMethod = boardGenerators.GenerateHighlightedFieldsMap;
          }

          public void ShowBoard()
          {
               if(BoardSpriteGenerationMethod is null)
                    throw new NullReferenceException("U must init BoardSpriteGenerationMethod first.");

               var boardSprite = BoardSpriteGenerationMethod.Invoke();

               _boardSpriteRenderer.sprite = boardSprite;
          }

          public void HightlightFields(params byte[] rawPositions)
          {
               if(HighlightedMapGenerationMethod is null)
                    throw new NullReferenceException("U must init HighlightedMapGenerationMethod first.");

               _highlightingSptiteRenderer.enabled = true;

               _highlightingSptiteRenderer.sprite = HighlightedMapGenerationMethod.Invoke(rawPositions);
          }

          public void DimHightlightedFields()
          {
               _highlightingSptiteRenderer.enabled = false;
          }

          public void SpawnChecker(CheckerView view, CheckerModel model)
          {
               if(RawToWorldParseMethod is null)
                    throw new NullReferenceException("U must init RawToWorldParseMethod first.");

               var worldPosition = RawToWorldParseMethod.Invoke(model.Position);

               Instantiate(view, worldPosition, _boardTransform.rotation, _lowerPlayerStash.transform);
          }

          public void MoveChecker(CheckerView checker, byte boardDestination, Action onDone = null)
          {
               if(RawToWorldParseMethod is null)
                    throw new NullReferenceException("U must init RawToWorldParseMethod first.");

               var worldDestination = RawToWorldParseMethod(boardDestination);

               StartCoroutine(GameBoardTweeners.MoveChecker(args =>
               {
                    args.Checker = checker;
                    args.Duration = ProjectConstants.CheckerMoveDuration;
                    args.Destination = worldDestination;
                    args.OnDone = onDone;
               }));
          }

          [ExecuteAlways]
          private void OnDrawGizmos()
          {
               Gizmos.color = Color.black;
               Gizmos.DrawWireCube(_boardSpriteRenderer.bounds.center, _boardSpriteRenderer.bounds.extents * 2);
          }
     }
}
