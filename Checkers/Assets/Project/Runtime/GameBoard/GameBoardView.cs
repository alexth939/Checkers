using System;
using UnityEngine;

namespace Runtime.GameBoard
{
     public sealed class GameBoardView: MonoBehaviour, IGameBoardView
     {
          [SerializeField] private SpriteRenderer _renderer;

          [EasyButtons.Button]
          public void ShowBoard(Texture2D boardTexture)
          {
               Vector2 textureSize = new Vector2(boardTexture.width, boardTexture.height);
               var textureRect = new Rect(position: Vector2.zero, textureSize);
               Vector2 centeredPivot = Vector2.one / 2;

               _renderer.sprite = Sprite.Create(boardTexture, textureRect, centeredPivot);
          }
     }
}
