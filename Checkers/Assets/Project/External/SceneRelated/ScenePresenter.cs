// Version 27.05.2023

namespace UnityEngine.SceneManagement
{
    [DisallowMultipleComponent]
    public abstract class ScenePresenter: MonoBehaviour
    {
        protected void Start()
        {
            OnEnteringScene();
        }

        protected void OnApplicationFocus(bool focus)
        {
            if(focus)
                OnApplicationAcquiredFocus();
            else
                OnApplicationLostFocus();
        }

        protected void SwitchScene(SceneName nextScene)
        {
            OnLeavingScene();
            SceneManager.LoadScene(nextScene.AsString(), LoadSceneMode.Single);
        }

        /// <summary>
        /// Auto-invoked at base.Start().
        /// </summary>
        protected abstract void OnEnteringScene();
        protected virtual void OnApplicationAcquiredFocus() { }
        protected virtual void OnApplicationLostFocus() { }
        protected virtual void OnLeavingScene() { }
    }
}
