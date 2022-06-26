using System;
using UnityEngine;
//using Firebase;
using EmailRegistrationArgs = External.FirebaseWorkers.UserAuthorizator.EmailAuthorizationArgs;
using EmailAuthorizationArgs = External.FirebaseWorkers.UserAuthorizator.EmailAuthorizationArgs;

namespace External.FirebaseWorkers
{
     public class FirebaseCustomApi
     {
          public static void CheckIfAvaliable(Action<bool> resultCallback = null)
          {
               Debug.Log($"Checking firebase's dependencies.");
               //FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
               //{
               //     var dependencyStatus = task.Result;
               //     if(dependencyStatus == Firebase.DependencyStatus.Available)
               //     {
               //          Debug.Log($"Its still alive and avaliable.");
               //          resultCallback?.Invoke(true);
               //     }
               //     else
               //     {
               //          Debug.Log($"Could not resolve all Firebase dependencies: {dependencyStatus}");
               //          resultCallback?.Invoke(false);
               //     }
               //});
          }

          public static void TryRegisterEmailAsync(Action<EmailRegistrationArgs> argumentsSetter)
          {
               UserRegistrator.TryRegisterEmailAsync(argumentsSetter);
          }

          public static void TryAuthorizeEmailAsync(Action<EmailAuthorizationArgs> argumentsSetter)
          {
               UserAuthorizator.TryAuthorizeEmailAsync(argumentsSetter);
          }
     }
}
