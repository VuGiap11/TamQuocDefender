using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace TamQuocDefender
{
    public class WinLevel : MonoBehaviour
    {
#if UNITY_EDITOR
        public SceneAsset sceneStart;
        public SceneAsset scenePlay;
#endif

        public string sceneStartName; // Tên của Scene Start
        public string scenePlayName;  // Tên của Scene Play
        public List<GameObject> StarOff = new List<GameObject>();

        public void ResetLevel()
        {
          
            UserManager.instance.SaveData();
            if (!string.IsNullOrEmpty(scenePlayName))
            {
                SceneManager.LoadScene(scenePlayName);
            }
            else
            {
                Debug.LogError("ScenePlayName chưa được gán!");
            }
        }
        public void NextLevel()
        {
            UserManager.instance.dataPlayerController.levelPlay++;
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

        public void SetStar(int star, Level level)
        {
            int number = 0;

            if (star < 2)
            {
                number = 3;
            }
            else if (star < 5)
            {
                number = 2;
            }
            else if (star < 7)
            {
                number = 1;
            }
            else
            {
                number = 0;
            }
            if (number >= level.Star)
            {
                level.Star = number;
            }
            if (level.level <= DataAsset.instance.levelData.LevelDatas.Count - 1)
            {
                Star(number);
            }
            Debug.Log("number" + number);
            UserManager.instance.SaveData();
        }

        void Star(int number)
        {
            if (star != null)
            {
                StopCoroutine(star);
            }
            star = StartCoroutine(DisableObjectsSequentially(number));
        }

        Coroutine star;

        IEnumerator DisableObjectsSequentially(int number)
        {
            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < this.StarOff.Count; i++)
            {
                this.StarOff[i].SetActive(i > number);
                yield return new WaitForSeconds(0.2f);
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
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