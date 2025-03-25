using System.Collections.Generic;
using UnityEngine;

namespace TamQuocDefender
{
    [System.Serializable]
    public class DataPlayerController
    {
        //public int gold;
        public int level;
        public int levelPlay;
        public DataPlayerController( int level, int levelPlay)
        {
            //this.gold = gold;
            this.level = level;
            this.levelPlay = levelPlay; 
        }
    }
    [System.Serializable]
    public class Level
    {
        public int level;
        public int Star;
        public bool isOpen;
        public Level(int level)
        {
            this.level = level;
            this.Star = 0;
            this.isOpen = false;
        }
    }
    [System.Serializable]
    public class LevelDatas
    {
        public List<Level> levels;
    }

    public class UserManager : MonoBehaviour
    {
        public static UserManager instance;
        public DataPlayerController dataPlayerController;
        public LevelDatas LevelDatas;
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        private void Start()
        {
            LoadData();
            //GameManager.instance.InitUi();
        }
        public void LoadData()
        {
            string jsonDataPlayerController = PlayerPrefs.GetString("dataPlayerController");

            if (!string.IsNullOrEmpty(jsonDataPlayerController))
            {
                dataPlayerController = JsonUtility.FromJson<DataPlayerController>(jsonDataPlayerController);
            }
            else
            {
                dataPlayerController = new DataPlayerController(0,0);
               
                Debug.LogWarning("No saved player data found.");
            }
            string jsonData = PlayerPrefs.GetString("LevelDatas");
            Debug.Log(jsonData);
            if (!string.IsNullOrEmpty(jsonData))
            {

                this.LevelDatas = JsonUtility.FromJson<LevelDatas>(jsonData);
                Debug.LogWarning("No saved 1 ");
            }
            else
            {
                Debug.LogWarning("No saved ");
                for (int i = 0; i < DataAsset.instance.levelData.LevelDatas.Count; i++)
                {
                    Level level = new Level(DataAsset.instance.levelData.LevelDatas[i].Level);
                    if (DataAsset.instance.levelData.LevelDatas[i].Level <= this.dataPlayerController.level)
                    {
                        level.isOpen = true;
                    }
                    this.LevelDatas.levels.Add(level);
                }
            }
            SaveData();
        }
        public void SaveData()
        {
            string jsonDataPlayerController = JsonUtility.ToJson(dataPlayerController);
            PlayerPrefs.SetString("dataPlayerController", jsonDataPlayerController);

            string jsonData = JsonUtility.ToJson(this.LevelDatas);
            PlayerPrefs.SetString("LevelDatas", jsonData);
        }
    }
}