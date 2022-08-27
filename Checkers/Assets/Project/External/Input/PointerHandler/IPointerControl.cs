using UnityEngine;

namespace UnityEngine.EventSystems
{
     public interface IPointerControl
     {
          event PointerEvent OnLeftDown;
          event PointerEvent OnLeftMoved;
          event PointerEvent OnLeftUp;
     }
}