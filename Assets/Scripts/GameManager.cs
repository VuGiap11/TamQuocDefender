using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TamQuocDefender
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        [SerializeField] private Transform spawnPos;
        [SerializeField] private Transform holderEnemy;
        public int wave = 0;
        public int Turn = 0;
        public int numberEnemyPass = 0;
        public List<Enemy> ListEnemies = new List<Enemy>();
        public bool isDefeat = false;
        //public GameObject[] Enemies;
        //public GameObject EnemyPre;
        public GameObject NextLevelObj;
        public GameObject DefeatObj;
        public GameObject EndGameObj;

        [Header("Text")]
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI waveText;
        [SerializeField] private TextMeshProUGUI EnemyPassText;
        [SerializeField] private TextMeshProUGUI goldText;
        [SerializeField] private BackGround backGround;
        public List<TowerPlace> towerPlaces = new List<TowerPlace>();

        public GameObject winObj;
        public GameObject LoseObj;
        public int numberEnemy;
        [SerializeField] private TextMeshProUGUI speedGameText;
        public bool isSpawnTower;
        [SerializeField] private HpBar timeBar;
        private float timeToSpawnEnemy = 15f;
        private float timeStart = 0;
        private bool canSpawnEnemy;
        public int gold;
        public bool isEndGame;

        public int number;
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        private void Update()
        {
            if (timeStart >= this.timeToSpawnEnemy && !canSpawnEnemy)
            {
                canSpawnEnemy = true;
                SpawnEnemy();
                this.timeBar.gameObject.SetActive(false);
                return;
            }
            timeStart += Time.deltaTime;
            timeBar.UpdateTime(this.timeToSpawnEnemy, this.timeStart);

        }
        public void DoubleSpeedGame()
        {
            if (Time.timeScale == 1)

            {
                Time.timeScale = 2;

            }
            else
            {
                Time.timeScale = 1;
            }
            InitUi();
        }
        private void Start()
        {
            this.isEndGame = false;
            SoundManager.instance.AudioBgGamePlay();
            this.gold = DataAsset.instance.levelData.LevelDatas[UserManager.instance.dataPlayerController.levelPlay].Gold;
            timeStart = 0;
            timeBar.UpdateTime(this.timeToSpawnEnemy, this.timeStart);
            Time.timeScale = 1;
            //backGround.ScaleBackGround();
            SpawnBackGround();
            InitUi();
            //SpawnEnemy();
        }

        public void SpawnBackGround()
        {
            LevelDataCf levelDataCf = DataAsset.instance.GetLevelDataCfByLevel(UserManager.instance.dataPlayerController.levelPlay);
            BackGround bg = DataAsset.instance.GetBackGroundById(levelDataCf.IdBackGround);
            this.backGround = Instantiate(bg);
            backGround.ScaleBackGround();
            SetTowerPlace();
            if (this.backGround.WaysPoint != null)
            {
                this.spawnPos = this.backGround.WaysPoint[0].transform;
            }
        }
        //[ContextMenu("SpawnEnemy")]
        //public void SpawnEnemy()
        //{
        //    GameObject enemy = Instantiate(this.EnemyPre,spawnPos.position,Quaternion.identity);
        //    enemy.transform.position = spawnPos.position;
        //    enemy.transform.SetParent(this.holderEnemy, true);
        //}
        [ContextMenu("SpawnEnemy")]
        public void SpawnEnemy()
        {
            this.numberEnemy = NumberEnemies();
            Debug.Log("numberEnemy" + this.numberEnemy);
            if (Spawn != null)
            {
                StopCoroutine(Spawn);
            }
            Spawn = StartCoroutine(spawn());
        }
        //Coroutine Spawn;
        //IEnumerator spawn()
        //{
        //    bool isSpawn = false;
        //    //yield return new WaitForSeconds(2f);
        //    Enemy enemy = DataAsset.instance.GetEnemybyId(DataAsset.instance.levelData.LevelDatas[UserManager.instance.dataPlayerController.level].WaveDatas[this.wave].Id[this.State]);
        //    if (enemy == null)
        //    {
        //        Debug.LogError("Enemy is null. Check the data configuration.");
        //        yield break;
        //    }
        //    for (int i = 0; i < enemy.dataEnemy.Number; i++)
        //    {

        //        ////GameObject enemy = Instantiate(this.EnemyPre, spawnPos.position, Quaternion.identity);
        //        //Enemy enemySpawn = Instantiate(enemy, spawnPos.position, Quaternion.identity);
        //        Enemy enemySpawn = ObjectPooling.instance.GetEnemyByID(enemy);
        //        if (enemySpawn != null)
        //        {
        //            this.ListEnemies.Add(enemySpawn);
        //            enemySpawn.transform.position = spawnPos.position;
        //            enemySpawn.gameObject.SetActive(true);
        //            enemySpawn.Init();
        //            //enemySpawn.transform.SetParent(this.holderEnemy, true);
        //            yield return new WaitForSeconds(3f);
        //            if (i == enemy.dataEnemy.Number - 1)
        //            {
        //                isSpawn = true;
        //            }
        //        }else
        //        {
        //            Debug.LogError("Failed to spawn enemy. Check Object Pooling.");
        //        }

        //    }
        //    Debug.Log(isSpawn);
        //    yield return new WaitUntil(() => isSpawn);
        //    isSpawn = false;
        //    yield return new WaitForSeconds(1f);
        //    this.State++;
        //    if (this.State >= DataAsset.instance.levelData.LevelDatas[UserManager.instance.dataPlayerController.level].WaveDatas[this.wave].Id.Count)
        //    {
        //        this.State = 0;
        //        this.wave++;
        //        if (this.wave >= DataAsset.instance.levelData.LevelDatas[UserManager.instance.dataPlayerController.level].WaveDatas.Count)
        //        {
        //            yield break;
        //        } 
        //    }
        //    StartCoroutine(spawn());
        //}

        public void ReduceGold(int among)
        {
            this.gold -= among;
        }
        //public void RemoveEnemyDie(Enemy enemy)
        //{
        //    this.ListEnemies.Remove(enemy);
        //}

        //public void CheckNextLevel()
        //{
        //    for (int i = 0; i < this.ListEnemies.Count; i++)
        //    {
        //        if (!this.ListEnemies[i].isDead)
        //        {
        //            return;
        //        }
        //    }
        //    isDefeat = false;
        //    NextLevel();
        //    Debug.Log("win");
        //}

        private void Defeat()
        {
            DefeatObj.SetActive(true);
            NextLevelObj.SetActive(false);
            EndGameObj.SetActive(false);
            if (Spawn != null)
            {
                StopCoroutine(Spawn);
            }
        }

        private void NextLevel()
        {
            EndGameObj.SetActive(false);
            DefeatObj.SetActive(false);
            NextLevelObj.SetActive(true);
            if (Spawn != null)
            {
                StopCoroutine(Spawn);
            }
        }

        //Coroutine Spawn;
        //IEnumerator spawn()
        //{
        //    bool isSpawn = false;
        //    //yield return new WaitForSeconds(2f);
        //    string id = DataAsset.instance.levelData.LevelDatas
        //        [UserManager.instance.dataPlayerController.level].WaveDatas[this.wave].Id[this.State];
        //    //Enemy enemy = DataAsset.instance.GetEnemybyId(DataAsset.instance.levelData.LevelDatas[UserManager.instance.dataPlayerController.level].WaveDatas[this.wave].Id[this.State]);
        //    Enemy enemy = ObjectPooling.instance.GetEnemyByID(id);
        //    if (enemy == null)
        //    {
        //        Debug.LogError("Enemy is null. Check the data configuration.");
        //        yield break;
        //    }
        //    for (int i = 0; i < enemy.dataEnemy.Number; i++)
        //    {
        //        Enemy enemySpawn = ObjectPooling.instance.GetEnemyByID(id);
        //        if (enemySpawn != null)
        //        {
        //            this.ListEnemies.Add(enemySpawn);
        //            Debug.Log("Spawn" + enemySpawn.dataEnemy.Id);
        //            enemySpawn.transform.position = spawnPos.position;
        //            //enemySpawn.name = "enemy" + i ;
        //            enemySpawn.gameObject.SetActive(true);
        //            enemySpawn.Init(this.backGround.WaysPoint);
        //            enemySpawn.transform.SetParent(this.holderEnemy, true);
        //            yield return new WaitForSeconds(3f);
        //            if (i == enemy.dataEnemy.Number - 1)
        //            {
        //                isSpawn = true;
        //            }
        //        }
        //        else
        //        {
        //            Debug.LogError("Failed to spawn enemy. Check Object Pooling.");
        //        }

        //    }
        //    //Debug.Log(isSpawn);
        //    yield return new WaitUntil(() => isSpawn);
        //    isSpawn = false;
        //    yield return new WaitForSeconds(4f);
        //    this.State++;
        //    if (this.State >= DataAsset.instance.levelData.LevelDatas[UserManager.instance.dataPlayerController.level].WaveDatas[this.wave].Id.Count)
        //    {
        //        this.State = 0;
        //        this.wave++;
        //        if (this.wave >= DataAsset.instance.levelData.LevelDatas[UserManager.instance.dataPlayerController.level].WaveDatas.Count)
        //        {
        //            yield break;
        //        }
        //    }
        //    StartCoroutine(spawn());
        //}

        Coroutine Spawn;
        IEnumerator spawn()
        {

            bool isSpawn = false;
            LevelDataCf levelDataCf = DataAsset.instance.GetLevelDataCfByLevel(UserManager.instance.dataPlayerController.levelPlay);
            string id = levelDataCf.WaveDatas[this.wave].EnemyDatas[this.Turn].Id.ToString();
            int number = levelDataCf.WaveDatas[this.wave].EnemyDatas[this.Turn].Number;
            Enemy enemy = ObjectPooling.instance.GetEnemyByID(id);
            if (enemy == null)
            {
                Debug.LogError("Enemy is null. Check the data configuration.");
                yield break;
            }
            for (int i = 0; i < number; i++)
            {
                //numberEnemy++;
                Enemy enemySpawn = ObjectPooling.instance.GetEnemyByID(id);
                if (enemySpawn != null)
                {
                    this.ListEnemies.Add(enemySpawn);
                    //Debug.Log("Spawn" + enemySpawn.dataEnemy.Id);
                    enemySpawn.transform.position = spawnPos.position;
                    //enemySpawn.name = "enemy" + i ;
                    enemySpawn.gameObject.SetActive(true);
                    enemySpawn.Init(this.backGround.WaysPoint);
                    enemySpawn.transform.SetParent(this.holderEnemy, true);
                    float timeSpawnOneEnemy = Random.Range(1f, 3f);
                    yield return new WaitForSeconds(timeSpawnOneEnemy);
                    if (i == number - 1)
                    {
                        isSpawn = true;
                    }
                }
                else
                {
                    Debug.LogError("Failed to spawn enemy. Check Object Pooling.");
                }
            }
            yield return new WaitUntil(() => isSpawn);
            isSpawn = false;
            float timeSpawnTurnEnemy = Random.Range(2f, 5f);
            yield return new WaitForSeconds(4f);
            this.Turn++;
            InitUi();
            if (this.Turn >= DataAsset.instance.levelData.LevelDatas[UserManager.instance.dataPlayerController.levelPlay].WaveDatas[this.wave].EnemyDatas.Count)
            {
                this.Turn = 0;
                this.wave++;
                if (this.wave > (DataAsset.instance.levelData.LevelDatas[UserManager.instance.dataPlayerController.levelPlay].WaveDatas.Count - 1))
                {
                    yield break;
                }
                InitUi();
            }
            StartCoroutine(spawn());
        }


        public void SetTowerPlace()
        {
            this.towerPlaces = new List<TowerPlace>();
            for (int i = 0; i < this.backGround.towerPlaces.Count; i++)
            {
                this.towerPlaces.Add(this.backGround.towerPlaces[i]);
            }
        }
        public TowerPlace CheckNearPos(Vector2 pos)
        {
            foreach (TowerPlace tow in backGround.towerPlaces)
            {
                if (tow.Tower != null)
                {
                    continue;
                }
                else
                {
                    if (pos.x < tow.transform.position.x + 0.5f && pos.x > tow.transform.position.x - 0.5f && pos.y < tow.transform.position.y + 0.5f && pos.y > tow.transform.position.y - 0.5f)
                    {
                        return tow;
                    }
                }
            }
            return null;
        }
        public void InitUi()
        {
            this.levelText.text = "level " + (UserManager.instance.dataPlayerController.levelPlay + 1).ToString();
            this.waveText.text = (this.wave + 1).ToString() + "/" + DataAsset.instance.levelData.LevelDatas[UserManager.instance.dataPlayerController.level].WaveDatas.Count.ToString();
            this.EnemyPassText.text = this.numberEnemyPass.ToString() + "/" + DataAsset.instance.levelData.LevelDatas[UserManager.instance.dataPlayerController.levelPlay].MaxEnemyCanPass.ToString();
            this.goldText.text = this.gold.ToString();
            this.speedGameText.text = "x" + Time.timeScale.ToString();
        }

        public void AddGold(int gold)
        {
            //Debug.Log("gold1" + gold + "goldnew1" + DataAsset.instance.levelData.LevelDatas[UserManager.instance.dataPlayerController.levelPlay].Gold);
            this.gold += gold;
            UserManager.instance.SaveData();
            InitUi();
            //Debug.Log("gold" + gold + "goldnew" + DataAsset.instance.levelData.LevelDatas[UserManager.instance.dataPlayerController.levelPlay].Gold);
        }
        public void Lose()
        {
            isEndGame = true;
            SoundManager.instance.AudioDefeat();
            StopAllCoroutines();
            for (int i = 0; i < ListEnemies.Count; i++)
            {
                if (!ListEnemies[i].isDead)
                {
                    ListEnemies[i].EndGame();
                }
            }
            winObj.SetActive(false);
            LoseObj.SetActive(true);
            EndGameObj.SetActive(false);
        }
        public void Win()
        {
            isEndGame = true;
            SoundManager.instance.AudioWin();
            LoseObj.SetActive(false);
            if (UserManager.instance.dataPlayerController.levelPlay >= DataAsset.instance.levelData.LevelDatas.Count - 1)
            {
                EndGameObj.SetActive(true);
                winObj.SetActive(true);
            }
            else
            {
                if (UserManager.instance.dataPlayerController.levelPlay <= DataAsset.instance.levelData.LevelDatas.Count - 2 && UserManager.instance.dataPlayerController.levelPlay <= UserManager.instance.dataPlayerController.level)
                {
                    UserManager.instance.dataPlayerController.level++;
                    for (int i = 0; i <= UserManager.instance.dataPlayerController.level; i++)
                    {
                        UserManager.instance.LevelDatas.levels[i].isOpen = true;
                    }
                }
                winObj.SetActive(true);
            }
            winObj.GetComponent<WinLevel>().SetStar(this.numberEnemyPass, UserManager.instance.LevelDatas.levels[UserManager.instance.dataPlayerController.levelPlay]);
            UserManager.instance.SaveData();
            StopAllCoroutines();
            for (int i = 0; i < ListEnemies.Count; i++)
            {
                if (!ListEnemies[i].isDead)
                {
                    ListEnemies[i].EndGame();
                }
            }
        }

        public int NumberEnemies()
        {
            int num = 0;
            foreach (var WaveData in DataAsset.instance.levelData.LevelDatas[UserManager.instance.dataPlayerController.levelPlay].WaveDatas)
            {

                foreach (var EnemyData in WaveData.EnemyDatas)
                {
                    num += EnemyData.Number;
                }
            }
            return num;
        }
    }
}
