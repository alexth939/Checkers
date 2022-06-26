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

     [MenuItem("<Open a Scene>/Game Scene")]
     private static void OpenPlayingScene()
     {
          var canLeave = SaveCurrentModifiedScenesIfUserWantsTo();
          if(canLeave == false)
               return;

          OpenScene("Assets/Scenes/GameScene.unity");
     }

     [MenuItem("<Open a Scene>/Leaderboard Scene")]
     private static void OpenLeaderboardScene()
     {
          var canLeave = SaveCurrentModifiedScenesIfUserWantsTo();
          if(canLeave == false)
               return;

          OpenScene("Assets/Scenes/LeaderboardScene.unity");
     }

     [MenuItem("<Open a Scene>/Prepare Scene")]
     private static void OpenPrepareScene()
     {
          var canLeave = SaveCurrentModifiedScenesIfUserWantsTo();
          if(canLeave == false)
               return;

          OpenScene("Assets/Scenes/PrepareScene.unity");
     }
}
