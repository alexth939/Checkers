using UnityEngine.UI;
using static UnityEngine.EventSystems.PointerEventData;

namespace UnityEngine.EventSystems
{
     public delegate void PointerEvent(Vector2 pointerCoords);

     [RequireComponent(typeof(Graphic), typeof(RectTransform))]
     public class PointerControl: MonoBehaviour, IPointerControl, IPointerConfiguration, IPointerHandlers
     {
          public event PointerEvent OnLeftDown, OnLeftMoved, OnLeftUp;
          public event PointerEvent OnMiddleDown, OnMiddleMoved, OnMiddleUp;
          public event PointerEvent OnRightDown, OnRightMoved, OnRightUp;

          private RectTransform _myRectTransform;

          void IPointerConfiguration.SetPadSize(Vector2 screenSize)
          {
               _myRectTransform ??= GetComponent<RectTransform>();
               _myRectTransform.sizeDelta = screenSize;
          }

          void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
          {
               var HandleEvent = eventData.button switch
               {
                    InputButton.Left => OnLeftDown,
                    InputButton.Middle => OnMiddleDown,
                    InputButton.Right => OnRightDown,
                    _ => null
               };

               var coords = eventData.position;
               HandleEvent?.Invoke(coords);
          }

          void IPointerMoveHandler.OnPointerMove(PointerEventData eventData)
          {
               var HandleEvent = eventData.button switch
               {
                    InputButton.Left => OnLeftMoved,
                    InputButton.Middle => OnMiddleMoved,
                    InputButton.Right => OnRightMoved,
                    _ => null
               };

               var coords = eventData.position;
               HandleEvent?.Invoke(coords);
          }

          void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
          {
               var HandleEvent = eventData.button switch
               {
                    InputButton.Left => OnLeftUp,
                    InputButton.Middle => OnMiddleUp,
                    InputButton.Right => OnRightUp,
                    _ => null
               };

               var coords = eventData.position;
               HandleEvent?.Invoke(coords);
          }
     }
}
