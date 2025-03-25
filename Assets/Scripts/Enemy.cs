using DG.Tweening;
using UnityEngine;

namespace TamQuocDefender
{
    public class Enemy : MonoBehaviour
    {
        //public Transform[] waypoints;
        public Transform[] Waypoints;
        public float duration = 2f; // Thời gian di chuyển toàn bộ đường đi
        private PathType pathType = PathType.CatmullRom; // Kiểu đường đi
        private PathMode pathMode = PathMode.Sidescroller2D; // Kiểu di chuyển (3D hoặc 2D)
        private bool loop = false; // Có lặp lại hay không
        private bool closePath = false; // Đóng kín đường (quay về điểm đầu tiên)
        public Transform takeDamagePos;

        public Animator anim;
        private int currentHp;
        public bool isDead;
        Tweener pathTweener;
        private Vector3 lastPosition;
        public DataEnemy dataEnemy;
        public HpBar hpBar;
        private void Start()
        {
            anim = GetComponent<Animator>();
            //MoveObject();
            //this.currentHp = dataEnemy.Hp;
            //UpdateHpUI();
        }

        public void Init(Transform[] WaysPoint)
        {
            SetWayPoint(WaysPoint);
            transform.DOKill();
            this.currentHp = dataEnemy.Hp;
            UpdateHpUI();
            MoveObject();
            isDead = false;
        }

        public void SetWayPoint(Transform[] WaysPoint)
        {
            this.Waypoints = new Transform[WaysPoint.Length];
            for (int i = 0; i < this.Waypoints.Length; i++)
            {
                this.Waypoints[i] = WaysPoint[i].transform;
            }
        }
        private void UpdateHpUI()
        {
            this.hpBar.UpdateHpBar(this.dataEnemy.Hp, this.currentHp);
        }
        private void MoveObject()
        {
            if (Waypoints == null || Waypoints.Length == 0)
            {
                Debug.LogWarning("No waypoints assigned!");
                return;
            }

            // Chuyển đổi các Transform thành mảng Vector3
            Vector3[] pathPoints = new Vector3[Waypoints.Length];
            float a = UnityEngine.Random.Range(-0.4f, 0.4f);
            for (int i = 0; i < Waypoints.Length; i++)
            {

                //pathPoints[i] = waypoints[i].position;
                Vector3 waypointPosition = Waypoints[i].position;
                //pathPoints[i] = new Vector3(waypointPosition.x, waypointPosition.y + a, waypointPosition.z);

                if (i != 0 && i != Waypoints.Length - 1)
                {
                    pathPoints[i] = new Vector3(waypointPosition.x, waypointPosition.y + a, waypointPosition.z);
                }
                else
                {
                    // Giữ nguyên điểm đầu và điểm cuối mà không thay đổi
                    pathPoints[i] = waypointPosition;
                }
            }

            // Sử dụng DOPath để di chuyển
            pathTweener = transform.DOPath(
                pathPoints,
                duration,
                pathType,
                pathMode
            ).SetSpeedBased()
            .OnUpdate(delegate
            {
                if (isDead)
                {
                    pathTweener.Kill();
                }
                float currentx = transform.position.x;
                float velocityx = (currentx - lastPosition.x) / Time.deltaTime;

                // Kiểm tra vận tốc Y giảm và xoay vật thể
                if (velocityx < 0)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0); // Quay 180 độ
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0); // Quay về hướng ban đầu
                }

                // Cập nhật vị trí cho lần tính toán tiếp theo
                lastPosition = transform.position;

            })
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                AtkPlayer();
                Debug.Log("destroy");
                //Destroy(gameObject);
            });
            // Đóng kín đường đi nếu cần
            //if (closePath)
            //{
            //    pathTweener.SetOptions(closePath);
            //}

            // Cài đặt lặp lại
            //if (loop)
            //{
            //    pathTweener.SetLoops(-1, LoopType.Restart);
            //}
        }

        private void AtkPlayer()
        {
            SoundManager.instance.AudioPass();
            GameManager.instance.numberEnemyPass++;
            GameManager.instance.numberEnemy--;
            this.isDead = true;
            ObjectPooling.instance.ReturnEnemy(this);

            LevelDataCf levelDataCf = DataAsset.instance.GetLevelDataCfByLevel(UserManager.instance.dataPlayerController.levelPlay);
            GameManager.instance.InitUi();
            if (GameManager.instance.numberEnemyPass >= levelDataCf.MaxEnemyCanPass)
            {
                Debug.Log("defeat");
                GameManager.instance.Lose();
                return;
            }
            if (GameManager.instance.numberEnemy <= 0)
            {
                GameManager.instance.Win();

            }
        }

     
        public void TakeDamage(int dmg)
        {
            //Debug.Log("Die0");
            if (this.currentHp <= 0) return;
            this.currentHp -= dmg;
            anim.SetTrigger("Hurt");
            if (currentHp <= 0)
            {
                isDead = true;
                currentHp = 0;
                anim.SetBool("IsDead", true);
                SoundManager.instance.AudioEnemyDie();
                //GameManager.instance.RemoveEnemyDie(this);
                //GameManager.instance.CheckNextLevel();

                //Destroy(gameObject, 1f);
                //DOVirtual.DelayedCall(0.2f, delegate
                //{
                //    GameManager.instance.numberEnemy--;
                //    if (GameManager.instance.numberEnemy <= 0)
                //    {
                //        GameManager.instance.Win();

                //    }
                //    ObjectPooling.instance.ReturnEnemy(this);
                //    GameManager.instance.AddGold(this.dataEnemy.Gold);
                //});

            }
            UpdateHpUI();
        }

        public void Die()
        {
            GameManager.instance.numberEnemy--;
            if (GameManager.instance.numberEnemy <= 0)
            {
                GameManager.instance.Win();

            }
            ObjectPooling.instance.ReturnEnemy(this);
            GameManager.instance.AddGold(this.dataEnemy.Gold);

        }

        public void EndGame()
        {
            pathTweener.Kill();
        }
    }

}
