// version 9.6.2022

using System;

namespace UnityEngine.SceneManagement
{
     public enum SceneName
     {
          AuthorizationScene,
          LobbyScene,
          GameScene
     }

     public static class SceneManagementExtensions
     {
          public static string AsString(this SceneName name)
          {
               return Enum.GetName(typeof(SceneName), name);
          }
     }
}
