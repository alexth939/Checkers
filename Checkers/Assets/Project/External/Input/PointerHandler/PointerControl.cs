using System;
using UnityEngine.UI;
using static UnityEngine.EventSystems.PointerEventData;

namespace UnityEngine.EventSystems
{
     public delegate void PointerEvent(object pointerCoords);

     [RequireComponent(typeof(Graphic), typeof(RectTransform))]
     public class PointerControl: MonoBehaviour, IPointerControl, IPointerConfiguration, IPointerHandlers
     {
          public event PointerEvent OnLeftDown, OnLeftMoved, OnLeftUp;
          public event PointerEvent OnMiddleDown, OnMiddleMoved, OnMiddleUp;
          public event PointerEvent OnRightDown, OnRightMoved, OnRightUp;

          private RectTransform _myRectTransform;
          private Func<Vector2, object> _coordsParseMethod = screenCoords => (Vector2)screenCoords;

          void IPointerConfiguration.SetPadSize(Vector2 screenSize)
          {
               _myRectTransform ??= GetComponent<RectTransform>();
               _myRectTransform.sizeDelta = screenSize;
          }

          void IPointerConfiguration.SetCoordsParseMethod(Func<Vector2, object> method)
          {
               _coordsParseMethod = method;
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

               var coords = _coordsParseMethod.Invoke(eventData.position);
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

               var coords = _coordsParseMethod.Invoke(eventData.position);
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

               var coords = _coordsParseMethod.Invoke(eventData.position);
               HandleEvent?.Invoke(coords);
          }
     }
}
