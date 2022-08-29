using Popups;
using UnityEngine.Events;

namespace Runtime.WindowViews
{
     internal interface IAuthorizationWindowView: IPopupView
     {
          string GetEmail();

          string GetPassword();

          UnityEvent OnGoBackRequest { get; }
          UnityEvent OnAuthorizeRequest { get; }
     }
}