using UnityEngine;

[ExecuteInEditMode]
public class CameraSizeAutoFixer:MonoBehaviour
{
     [SerializeField]
     Camera MainCamera;
     [SerializeField]
     Material mat;
     [SerializeField]
     bool gizmos_visible = false;
     //[SerializeField]
     //Rect rect;

     [SerializeField]
     [Range(1.0f,4.0f)]
     float minimum_width = 1.0f;


     [SerializeField]
     float InitialCameraHeight = 6.83f;

     private void Start()
     {
          float minimum_half_width = Screen.width / 2 - MainCamera.WorldToScreenPoint(
               new Vector3(
                    MainCamera.transform.position.x - MainCamera.orthographicSize * minimum_width,
                    MainCamera.transform.position.y,
                    MainCamera.transform.position.z)).x;
          Debug.Log("minimum_half_width=" + minimum_half_width);
          Debug.Log("Screen.width/2=" + Screen.width / 2);
          if(Screen.width / 2 < minimum_half_width)
          {
               MainCamera.orthographicSize *= minimum_half_width / (Screen.width/2);
          }
     }

     private void OnDisable()
     {
          MainCamera.orthographicSize = InitialCameraHeight;
     }

     private void OnEnable()
     {
          OnPostRender();
     }

     private void OnPostRender()
     {
          if(mat == null)
               return;

          if(gizmos_visible)
          {
               Color color = Color.red;

               GL.PushMatrix();
               mat.SetPass(0);
               GL.LoadOrtho();
               GL.Begin(GL.LINES);

               //! camera size (half height)
               GL.Vertex(MainCamera.WorldToViewportPoint(MainCamera.transform.position));
               GL.Vertex(MainCamera.WorldToViewportPoint(new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z + MainCamera.orthographicSize)));

               //! minimum width left
               GL.Vertex(MainCamera.WorldToViewportPoint(MainCamera.transform.position));
               GL.Vertex(MainCamera.WorldToViewportPoint(new Vector3(MainCamera.transform.position.x - MainCamera.orthographicSize * minimum_width, MainCamera.transform.position.y, MainCamera.transform.position.z)));

               GL.End();
               GL.PopMatrix();
          }

          float minimum_half_width = Screen.width / 2 - MainCamera.WorldToScreenPoint(
               new Vector3(
                    MainCamera.transform.position.x - MainCamera.orthographicSize * minimum_width,
                    MainCamera.transform.position.y,
                    MainCamera.transform.position.z)).x;
          //Debug.Log("minimum_half_width=" + minimum_half_width);
          //Debug.Log("Screen.width/2=" + Screen.width / 2);
          if(Screen.width / 2 < minimum_half_width)
          {
               MainCamera.orthographicSize = InitialCameraHeight * minimum_half_width / (Screen.width / 2);
          }
          else
          {
               MainCamera.orthographicSize = InitialCameraHeight;
          }
     }
}