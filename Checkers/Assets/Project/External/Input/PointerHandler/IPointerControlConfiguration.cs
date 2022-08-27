using System;

namespace UnityEngine.EventSystems
{
     public interface IPointerConfiguration
     {
          void SetPadSize(Vector2 screenSize);

          /// <summary> By Default "PointerEvent" passes screen coords as V2 in pixels. </summary>
          void SetCoordsParseMethod(Func<Vector2, object> method);
     }
}