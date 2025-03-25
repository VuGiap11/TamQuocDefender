using System.Collections.Generic;
using UnityEngine;

namespace TamQuocDefender
{
    public class ObjectPooling : MonoBehaviour
    {
        public static ObjectPooling instance;
        //[SerializeField] private Queue<Enemy> pool = new Queue<Enemy>();
        [SerializeField] private List<Enemy> pool;
        [SerializeField] private Transform Holder;
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        private void Start()
        {
            SpawnMonsters();
        }
        public void SpawnMonsters()
        {
            pool = new List<Enemy>();
            if (DataAsset.instance.Enemies1 == null || DataAsset.instance.Enemies1.Count == 0)
            {
                Debug.LogWarning("Chưa thêm prefab quái nào vào danh sách!");
                return;
            }
            foreach (var monsterPrefab in DataAsset.instance.Enemies1)
            {
                for (int i = 0; i < 4; i++)
                {
                    Enemy spawnedMonster = Instantiate(monsterPrefab);
                    spawnedMonster.transform.SetParent(this.Holder,false);
                    spawnedMonster.gameObject.SetActive(false);
                    pool.Add(spawnedMonster);
                }
            }
        }

        //public Enemy GetEnemyByID(string id)
        //{
        //    Enemy enemy = DataAsset.instance.GetEnemybyId(id);
        //    for (int i = 0; i < pool.Count; i++)
        //    {
        //        if (pool[i].dataEnemy.Id == id && !pool[i].gameObject.activeInHierarchy) // Kiểm tra ID và trạng thái
        //        {
        //            //Enemy enemy = pool[i];
        //            pool.RemoveAt(i); // Loại bỏ khỏi danh sách
        //            return enemy;
        //        }
        //    }

        //    Debug.LogWarning($"Không tìm thấy đối tượng với ID: {id}. Đang tạo mới...");
        //    return SpawnNewEnemy(id); // Sinh thêm đối tượng nếu không tìm thấy
        //}
        public Enemy GetEnemyByID(string id)
        {
            Enemy enemy;
            for (int i = 0; i < pool.Count; i++)
            {
                if (pool[i].dataEnemy.Id == id && !pool[i].gameObject.activeInHierarchy) // Kiểm tra ID và trạng thái
                {
                    enemy = pool[i];
                    //pool.RemoveAt(i); // Loại bỏ khỏi danh sách
                    pool.Remove(enemy);
                    return enemy;
                }
            }

            //Debug.LogWarning($"Không tìm thấy đối tượng với ID: {enemy.dataEnemy.Id}. Đang tạo mới...");
            return SpawnNewEnemy(id); // Sinh thêm đối tượng nếu không tìm thấy
        }
        ///// <summary>
        ///// Tạo một đối tượng mới dựa trên ID.
        ///// </summary>
        private Enemy SpawnNewEnemy(string id)
        {
            // Tìm prefab tương ứng với ID trong danh sách prefab
            Enemy prefab = DataAsset.instance.GetEnemybyId(id);
            if (prefab == null)
            {
                Debug.LogError($"Không tìm thấy prefab nào với ID: {id}");
                return null;
            }
            // Tạo đối tượng mới
            Enemy newEnemy = Instantiate(prefab);
            newEnemy.transform.SetParent(this.Holder, false);
            newEnemy.gameObject.SetActive(false);
            //this.pool.Add(newEnemy);
            //pool.Add(newEnemy); // Thêm đối tượng mới vào pool
            return newEnemy;
        }
        public void ReturnEnemy(Enemy enemy)
        {
            if (enemy == null) return;
            //GameManager.instance.ListEnemies.Remove(enemy);
         


            enemy.transform.position = Vector3.zero;
            enemy.transform.rotation = Quaternion.identity;
            enemy.isDead = false;
            enemy.transform.SetParent(this.Holder, false);
            pool.Add(enemy);
            enemy.gameObject.SetActive(false);
            //GameManager.instance.ListEnemies.Remove(enemy);
            //for (int i = GameManager.instance.ListEnemies.Count - 1; i >= 0; i--)
            //{
            //    if (GameManager.instance.ListEnemies[i] == enemy)
            //    {
            //        GameManager.instance.ListEnemies.Remove(enemy);
            //        return;
            //    }
            //}
        }
    }
}