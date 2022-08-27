namespace UnityEngine
{
     [System.Obsolete("For educational purposes only." ,true)]
     public struct Vector2Byte
     {
          public byte X;
          public byte Y;

          public Vector2Byte(byte x, byte y)
          {
               this.X = x;
               this.Y = y;
          }

          public static Vector2Byte Zero => new Vector2Byte(0, 0);
          public static Vector2Byte One => new Vector2Byte(1, 1);

          public static implicit operator Vector2Byte(Vector2 source)
          {
               checked
               {
                    return new((byte)source.x, (byte)source.y);
               }
          }

          public static implicit operator Vector2Byte(Vector2Int source)
          {
               checked
               {
                    return new((byte)source.x, (byte)source.y);
               }
          }

          public static implicit operator Vector2Int(Vector2Byte source)
          {
               checked
               {
                    return new Vector2Int(source.X, source.Y);
               }
          }

          public static Vector2Int operator +(Vector2Int v1, Vector2Byte v2)
          {
               return new Vector2Int()
               {
                    x = v1.x + v2.X,
                    y = v1.y + v2.Y
               };
          }

          public static Vector2Int operator +(Vector2Byte v2, Vector2Int v1)
          {
               return new Vector2Int()
               {
                    x = v1.x + v2.X,
                    y = v1.y + v2.Y
               };
          }

          public override string ToString()
          {
               return $"({X}, {Y})";
          }
     }
}
