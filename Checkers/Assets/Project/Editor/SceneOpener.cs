using UnityEditor;
using UnityEngine;
using static UnityEditor.SceneManagement.EditorSceneManager;

public class MenuTest: MonoBehaviour
{
     [MenuItem("<Open a Scene>/Authorization Scene")]
     private static void OpenAuthorizationScene()
     {
          var canLeave = SaveCurrentModifiedScenesIfUserWantsTo();
          if(canLeave == false)
               return;

          OpenScene("Assets/Scenes/AuthorizationScene.unity");
     }

     [MenuItem("<Open a Scene>/Play Scene")]
     private static void OpenGameScene()
     {
          var canLeave = SaveCurrentModifiedScenesIfUserWantsTo();
          if(canLeave == false)
               return;

          OpenScene("Assets/Scenes/PlayScene.unity");
     }

     [MenuItem("<Open a Scene>/Lobby Scene")]
     private static void OpenLobbyScene()
     {
          var canLeave = SaveCurrentModifiedScenesIfUserWantsTo();
          if(canLeave == false)
               return;

          OpenScene("Assets/Scenes/LobbyScene.unity");
     }
}
