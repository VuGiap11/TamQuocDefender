
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TamQuocDefender
{
    public class LoseLevel : MonoBehaviour
    {
#if UNITY_EDITOR
        public SceneAsset sceneStart;
        public SceneAsset scenePlay;
#endif

        public string sceneStartName; // Tên của Scene Start
        public string scenePlayName;  // Tên của Scene Play

        public void ResetLevel()
        {
            if (!string.IsNullOrEmpty(scenePlayName))
            {
                SceneManager.LoadScene(scenePlayName);
            }
            else
            {
                Debug.LogError("ScenePlayName chưa được gán!");
            }
        }

        public void ReturnLevel()
        {
            if (!string.IsNullOrEmpty(sceneStartName))
            {
                SceneManager.LoadScene(sceneStartName);
            }
            else
            {
                Debug.LogError("SceneStartName chưa được gán!");
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            // Tự động cập nhật tên cảnh từ SceneAsset trong Unity Editor
            if (sceneStart != null)
            {
                sceneStartName = sceneStart.name;
            }

            if (scenePlay != null)
            {
                scenePlayName = scenePlay.name;
            }
        }
#endif
    }
}