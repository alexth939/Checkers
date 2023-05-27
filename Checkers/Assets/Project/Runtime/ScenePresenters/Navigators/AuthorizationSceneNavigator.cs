using System;
using Popups;
using Runtime.WindowViews;

namespace Runtime.ScenePresenters
{
    internal class AuthorizationSceneNavigator
    {
        private IWelcomeWindowView _welcomeWindow;
        private IRegistrationWindowView _registrationWindow;
        private IAuthorizationWindowView _authorizationWindow;

        internal AuthorizationSceneNavigator(AuthorizationSceneWindows sceneWindows)
        {
            _welcomeWindow = sceneWindows.WelcomeWindow;
            _registrationWindow = sceneWindows.RegistrationWindow;
            _authorizationWindow = sceneWindows.AuthorizationWindow;
        }

        private void BackToWelcomeWindow(IPopupView currentWindow)
        {
            currentWindow.Hide(onDone: () =>
                 _welcomeWindow.Show(onDone: () =>
                      _welcomeWindow.UnblockInteraction()));
        }

        internal void InitBackFromRegistrationScenario()
        {
            _registrationWindow.OnGoBackRequest.AddListener(() =>
            {
                _registrationWindow.BlockInteraction();
                BackToWelcomeWindow(currentWindow: _registrationWindow);
            });
        }

        internal void InitBackFromAuthorizationScenario()
        {
            _authorizationWindow.OnGoBackRequest.AddListener(() =>
            {
                _authorizationWindow.BlockInteraction();
                BackToWelcomeWindow(currentWindow: _authorizationWindow);
            });
        }

        internal void InitRegistrationScenario(Action OnArrived = null)
        {
            _welcomeWindow.OnRegisterButtonClicked.AddListener(() =>
            {
                _welcomeWindow.BlockInteraction();
                _welcomeWindow.Hide(onDone: () =>
                        _registrationWindow.Show(onDone: () =>
                        {
                            _registrationWindow.UnblockInteraction();
                            _registrationWindow.OnRegisterRequest.AddListener(() =>
                               {
                                   _registrationWindow.BlockInteraction();
                                   OnArrived?.Invoke();
                               });
                        }));
            });
        }

        internal void InitAuthorizationScenario(Action OnArrived = null)
        {
            _welcomeWindow.OnSignInButtonClicked.AddListener(() =>
            {
                _welcomeWindow.BlockInteraction();
                _welcomeWindow.Hide(onDone: () =>
                        _authorizationWindow.Show(onDone: () =>
                        {
                            _authorizationWindow.UnblockInteraction();
                            _authorizationWindow.OnAuthorizeRequest.AddListener(() =>
                               {
                                   _authorizationWindow.BlockInteraction();
                                   OnArrived?.Invoke();
                               });
                        }));
            });
        }

        internal class AuthorizationSceneWindows
        {
            internal IWelcomeWindowView WelcomeWindow;
            internal IRegistrationWindowView RegistrationWindow;
            internal IAuthorizationWindowView AuthorizationWindow;
        }
    }
}
