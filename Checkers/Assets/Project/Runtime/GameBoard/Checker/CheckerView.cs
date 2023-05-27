using UnityEngine;

namespace Runtime.GameBoard
{
    internal class CheckerView: MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _backSprite;
        [SerializeField] private SpriteRenderer _frontSprite;

        internal void SetSpriteSize(Vector2 size)
        {
            _backSprite.size = size;
            _frontSprite.size = size * 0.9f;
        }

        [EasyButtons.Button]
        internal void SetColor(Color color)
        {
            _frontSprite.color = color;
            _backSprite.color = new Color()
            {
                r = 1 - color.r,
                g = 1 - color.g,
                b = 1 - color.b,
                a = 1
            };
        }
    }
}
