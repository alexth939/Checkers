using UnityEngine;
using UnityEditor;

public class ContextMenuInjector: MonoBehaviour
{
     [SerializeField] private ScriptableObject FolderIcons;

     [MenuItem("Assets/<Open Folder Icon Editor>")]
     private static void OpenFolderIconEditor()
     {
          EditorApplication.ExecuteMenuItem("Window/General/Inspector");
          Object folderIcons = AssetDatabase.LoadMainAssetAtPath("Assets/FolderIcons/FolderIcons.asset");
          AssetDatabase.OpenAsset(folderIcons);
     }
}
