
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TamQuocDefender
{
    public class Tower : MonoBehaviour
    {
        public Projectile projectilepre;
        public Transform firePoint;
        public float shotPerSecond;
        public float nextShotTime;
        public int towerPrice;

        public Transform closetEnemy;
        public List<GameObject> multiEnemies = new List<GameObject>();
        //public GameObject[] multiEnemies;
        private bool shouldShoot;

        [Header("Upgrade")]

        public int level;
        public int maxlevel;
        public int upgradeCost;
        //public int upgradeSPS;
        public Animator anim;
        public bool isPlay = false;
        [SerializeField] private GameObject effect;
        [SerializeField] private Transform holderEffect;
        public ParticleSystem effectA;
        private bool canUpdate;
        [SerializeField] private GameObject ObjButtonUpdate;
        [SerializeField] private TextMeshProUGUI priceUpdateText;
       

        private void Start()
        {
            anim = GetComponent<Animator>();
            priceUpdateText.text = this.upgradeCost.ToString();
        }
        public void AddLevel()
        {
            if (upgradeCost <= GameManager.instance.gold && level < maxlevel && !canUpdate)
            {
                SoundManager.instance.BuiltTower();
                GameManager.instance.AddGold(-upgradeCost);
                upgradeCost += upgradeCost;
                priceUpdateText.text = this.upgradeCost.ToString();
                canUpdate = true;
                level++;
                if (this.level >= maxlevel)
                {
                    this.ObjButtonUpdate.SetActive(false);
                }
                //GameManager.instance.ReduceGold(upgradeCost);
                anim.SetTrigger("Upgrade");
                shotPerSecond++;
                this.projectilepre.Damage++;
                Debug.Log("UPDATE");
                //SpawnEffect();
                EffectPlaying();
            }
        }
        public void EffectPlaying()
        {
            if (EffectPlay != null)
            {
                StopCoroutine(EffectPlay);
            }
            EffectPlay = StartCoroutine(WaitForEffectToEnd());
        }
        Coroutine EffectPlay;
        IEnumerator WaitForEffectToEnd()
        {
            //effectA.transform.position = transform.position;
            effectA.Play();
            yield return new WaitForSeconds(effectA.main.duration); // Chờ thời gian hiệu ứng kết thúc
            FunctionB();
        }

        void FunctionB()
        {
            canUpdate = false;
        }
        //private void SpawnEffect()
        //{
        //    GameObject a = Instantiate(this.effect);
        //    a.transform.SetParent(this.holderEffect, false);
        //    DOVirtual.DelayedCall(1f, delegate
        //    {
        //        Destroy(a);
        //        canUpdate = false;
        //    });
        //}


        public void CloseTower()
        {
            GameManager.instance.AddGold(this.towerPrice);
            Debug.Log("towerPrice" + this.towerPrice);
            Destroy(gameObject);
        }
        public void Shoot(Transform target)
        {
            if (nextShotTime < Time.time)
            {
                Projectile _bulet = Instantiate(projectilepre, firePoint.position, Quaternion.identity);
                _bulet.Move(target);
                // Debug.Log("abc");
                nextShotTime = Time.time + (1 / shotPerSecond);
            }
        }


        private void Update()
        {
            //closetEnemy = GetClosetEnemy();
            if (closetEnemy != null && isPlay && !GameManager.instance.isEndGame)
            {
                //LookAtEnemy();
                if (shouldShoot)
                {
                    Shoot(closetEnemy);
                }

            }

        }

        public void LookAtEnemy()
        {
            Vector2 LookDirection = closetEnemy.transform.position - transform.position;
            transform.up = new Vector2(LookDirection.x, LookDirection.y);
        }


        public Transform GetClosetEnemy()
        {
            //multiEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (multiEnemies == null || multiEnemies.Count == 0)
            {
                Debug.LogWarning("multiEnemies array is null or empty!");
                return null;
            }
            float closetDistance = Mathf.Infinity;
            Transform enemyPos = null;
            foreach (GameObject enemies in multiEnemies)
            {
                if (enemies == null) continue;
                if (enemies.GetComponent<Enemy>().isDead || enemies.GetComponent<Enemy>() == null) continue;
                float currentDistance;
                currentDistance = Vector3.Distance(transform.position, enemies.transform.position);
                if (currentDistance < closetDistance)
                {
                    closetDistance = currentDistance;
                    //enemyPos = enemies.transform;
                    enemyPos = enemies.GetComponent<Enemy>().takeDamagePos;
                }
            }
            return enemyPos;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                //this.shouldShoot = true;
                ////this.multiEnemies.AddRange()
                //GameObject a = other.gameObject;
                //this.multiEnemies.Add(a);
                //closetEnemy = GetClosetEnemy();
                if (other.GetComponent<Enemy>().dataEnemy.Hp <= 0) return;
                closetEnemy = GetClosetEnemy();
                this.shouldShoot = true;
                GameObject a = other.gameObject;
                if (!this.multiEnemies.Contains(a))
                {
                    this.multiEnemies.Add(a);

                }
                var enemy = other.GetComponent<Enemy>();
                if (this.multiEnemies != null && enemy != null && enemy.isDead)
                {
                    this.multiEnemies.Remove(a);
                    this.multiEnemies.RemoveAll(e => e == null);
                }


            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                this.shouldShoot = false;
                this.multiEnemies.Remove(other.gameObject);
            }
        }
    }
}