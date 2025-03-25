using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TamQuocDefender
{
    public class MainMenuController : MonoBehaviour
    {
        public static MainMenuController Instance;
        public string StartGameName;
        public GameObject LevelObj;
        public GameObject Door;
        [SerializeField] private Transform holderLevel;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }
        private void Start()
        {
            SoundManager.instance.AudioBgStartGame();  
        }

        public void StartGame()
        {
            SoundManager.instance.AudioButton();
            //SceneManager.LoadScene(StartGameName);
            LevelObj.SetActive(true);
            InitLevel();
        }
        public void OnApplicationQuit()
        {
            SoundManager.instance.AudioButton();
            Application.Quit();
            Debug.Log("quit");
        }
        public void InitLevel()
        {
            MyFunction.ClearChild(this.holderLevel);
            //for (int i = 0; i < DataAsset.instance.levelData.LevelDatas.Count; i++)
            //{
            //    GameObject door = Instantiate(Door);
            //    door.transform.SetParent(this.holderLevel, false);
            //    door.GetComponent<DoorSelect>().Level = i;
            //    door.GetComponent<DoorSelect>().Init();
            //}
            for (int i = 0; i < UserManager.instance.LevelDatas.levels.Count; i++)
            {
                GameObject door = Instantiate(Door);
                door.transform.SetParent(this.holderLevel, false);
                door.GetComponent<DoorSelect>().level = UserManager.instance.LevelDatas.levels[i];
                door.GetComponent<DoorSelect>().Init();
            }
        }
        public void SelectLevel(int level)
        {
            SoundManager.instance.AudioButton();
            UserManager.instance.dataPlayerController.levelPlay = level;
            UserManager.instance.SaveData();
            SceneManager.LoadScene(StartGameName);
        }
    }
}