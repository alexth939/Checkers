using Popups;
using UnityEngine.Events;

namespace Runtime.WindowViews
{
    internal interface IRegistrationWindowView: IPopupView
    {
        UnityEvent OnGoBackRequest { get; }
        UnityEvent OnRegisterRequest { get; }

        string GetNickname();

        string GetEmail();

        string GetPassword();
    }
}
