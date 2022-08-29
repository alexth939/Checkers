using Popups;
using UnityEngine.Events;

namespace Runtime.WindowViews
{
     internal interface IWelcomeWindowView: IPopupView
     {
          UnityEvent OnRegisterButtonClicked { get; }
          UnityEvent OnSignInButtonClicked { get; }
     }
}
