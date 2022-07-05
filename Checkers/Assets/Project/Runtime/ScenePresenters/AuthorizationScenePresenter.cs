using System;
using Runtime.WindowViews;
using External.Views;
//using FirebaseWorkers;
//using ProjectDefaults;
using UnityEngine;
using UnityEngine.SceneManagement;
//using FirebaseApi = External.FirebaseWorkers.FirebaseCustomApi;

namespace Runtime.ScenePresenters
{
     public sealed class AuthorizationScenePresenter: ScenePresenter
     {
          [SerializeField] private AuthorizationSceneContainer _dependencies;

          protected override void EnteringScene()
          {
               var _sceneNavigator = new AuthorizationSceneNavigator(sceneWindows: new()
               {
                    WelcomeWindow = _dependencies.WelcomeWindow,
                    RegistrationWindow = _dependencies.RegistrationWindow,
                    AuthorizationWindow = _dependencies.AuthorizationWindow
               });

               _dependencies.TransitionsView.FadeInAsync();
               //FirebaseApi.CheckIfAvaliable(isAvaliable =>
               //{
               //     if(isAvaliable)
               //     {
               //          _dependencies.TransitionsView.FadeInAsync(onDone: () =>
               //          {
               _sceneNavigator.InitBackFromRegistrationScenario();
               _sceneNavigator.InitBackFromAuthorizationScenario();
               _sceneNavigator.InitRegistrationScenario(OnArrived: TryRegister);
               _sceneNavigator.InitAuthorizationScenario(OnArrived: TryAuthorize);
               //          });
               //     }
               //     else
               //     {
               //          DialogPopup.ShowDialog("Something is wrong with firebase");
               //     }
               //});
          }

          private void TryRegister()
          {
               //FirebaseApi.TryRegisterEmailAsync(registerArgs =>
               //{
               //     registerArgs.Email = registrationWindow.GetEmail();
               //     registerArgs.Password = registrationWindow.GetPassword();
               //     registerArgs.OnFailed = message =>
               //          DialogPopup.ShowDialog(message, onOK: () =>
               //                registrationWindow.UnblockInteraction());
               //     //registerArgs.OnSucceed = myUser =>
               //     //{
               //     //     CredentialsSaver.RememberMe(registerArgs.Email, registerArgs.Password);
               //     //     FirebaseApi.WriteScoreEntryAsync(writeArgs =>
               //     //     {
               //     //          writeArgs.ScoreEntry = new ScoreEntryModel(myUser.UserId, registrationWindow.GetNickname());
               //     //          writeArgs.OnFailed = message =>
               //     //               DialogPopup.ShowDialog(message, onOK: () =>
               //     //                    registrationWindow.UnblockInteraction());
               //     //          writeArgs.OnSucceed = () =>
               //     //          {
               //     //               ProjectStatics.CachedScoreEntry = writeArgs.ScoreEntry;
               //     //               transitionsView.FadeOutAsync(onDone: () =>
               //     //                    SwitchScene(SceneName.PlayingScene));
               //     //          };
               //     //     });
               //     //};
               //});
          }

          private void TryAuthorize()
          {
               //FirebaseApi.TryAuthorizeEmailAsync(loginArgs =>
               //{
               //     loginArgs.Email = authorizationWindow.GetEmail();
               //     loginArgs.Password = authorizationWindow.GetPassword();
               //     loginArgs.OnFailed = message =>
               //          DialogPopup.ShowDialog(message, onOK: () =>
               //                authorizationWindow.UnblockInteraction());
               //     //loginArgs.OnSucceed = myUser =>
               //     //{
               //     //     CredentialsSaver.RememberMe(loginArgs.Email, loginArgs.Password);
               //     //     FirebaseApi.ReadScoreEntryAsync(readArgs =>
               //     //     {
               //     //          readArgs.WithID = myUser.UserId;
               //     //          readArgs.OnFailed = message =>
               //     //               DialogPopup.ShowDialog(message, onOK: () =>
               //     //                    authorizationWindow.UnblockInteraction());
               //     //          readArgs.OnSucceed = snapshot =>
               //     //          {
               //     //               ProjectStatics.CachedScoreEntry = (ScoreEntryModel)snapshot;
               //     //               transitionsView.FadeOutAsync(onDone: () =>
               //     //                    SwitchScene(SceneName.PlayingScene));
               //     //          };
               //     //     });
               //     //};
               //});
          }

          [Serializable]
          public sealed class AuthorizationSceneContainer
          {
               public ITransitionsView TransitionsView => _transitionsView;
               public IWelcomeWindowView WelcomeWindow => _welcomeWindow;
               public IRegistrationWindowView RegistrationWindow => _registrationWindow;
               public IAuthorizationWindowView AuthorizationWindow => _authorizationWindow;

               [SerializeField] private WelcomeWindowView _welcomeWindow;
               [SerializeField] private RegistrationWindowView _registrationWindow;
               [SerializeField] private AuthorizationWindowView _authorizationWindow;
               [SerializeField] private TransitionsView _transitionsView;
          }
     }
}
