
using System.Collections.Generic;
using UnityEngine;

namespace TamQuocDefender
{
    [System.Serializable]
    public class EnemyData
    {
        public string Id;
        public int Number;
    }
    [System.Serializable]
    public class WaveData
    {
        //public List<string> Id;
        public List<EnemyData> EnemyDatas;
    }
    [System.Serializable]
    public class LevelDataCf
    {
        public List<WaveData> WaveDatas;
        public int MaxEnemyCanPass;
        public int Level;
        public int Gold;
        public int IdBackGround;
    }
    [System.Serializable]
    public class LevelDataCfs
    {
        public List<LevelDataCf> LevelDatas;
    }
    public class DataAsset : MonoBehaviour
    {
        public static DataAsset instance;
        public TextAsset levelDataText;
        public LevelDataCfs levelData;
        public List<Enemy> Enemies1;
        public List<BackGround> BackGrounds = new List<BackGround>();
        private void Awake()
        {
            if (instance == null)
                instance = this;
            LoadData();
        }
        public void LoadData()
        {
            this.levelData = JsonUtility.FromJson<LevelDataCfs>(levelDataText.text);
        }

        public Enemy GetEnemybyId(string Id)
        {
            return Enemies1.Find(a => { return a.dataEnemy.Id == Id; });
        }
        public LevelDataCf GetLevelDataCfByLevel(int level)
        {
            return levelData.LevelDatas.Find(a => { return a.Level == level; });
        }

        public BackGround GetBackGroundById(int id)
        {
            return BackGrounds.Find(a => { return a.id == id; });
        }
    }
}
