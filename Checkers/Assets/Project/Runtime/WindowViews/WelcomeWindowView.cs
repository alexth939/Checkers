using Popups;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Runtime.WindowViews
{
    [RequireComponent(typeof(CanvasGroup))]
    internal sealed class WelcomeWindowView: PopupView, IWelcomeWindowView
    {
        [SerializeField] private Button _registerButton;
        [SerializeField] private Button _signInButton;

        public UnityEvent OnRegisterButtonClicked => _registerButton.onClick;
        public UnityEvent OnSignInButtonClicked => _signInButton.onClick;

        private void OnDestroy()
        {
            OnRegisterButtonClicked.RemoveAllListeners();
            OnSignInButtonClicked.RemoveAllListeners();
        }
    }
}
