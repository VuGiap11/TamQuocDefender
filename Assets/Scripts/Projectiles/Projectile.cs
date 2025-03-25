using DG.Tweening;
using UnityEngine;

namespace TamQuocDefender
{
    public class Projectile : MonoBehaviour
    {
        public int Damage;
        public float speed = 20f;
        public void Move(Transform target)
        {
            Tweener pathTweener = transform.DOMove(target.position, speed)
            .SetSpeedBased()
            .SetEase(Ease.Linear)
            .OnUpdate(delegate
            {
                if (target == null)
                {
                    Destroy(gameObject);
                }
            })
            .OnComplete(delegate
            {
                Destroy(gameObject);
            });
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //    if (collision.gameObject.GetComponent<Enemy>() != null) 
            //    {
            //        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            //        enemy.TakeDamage(this.Damage);
            //        //Destroy(gameObject);
            //    }
            try
            {
                if (collision.CompareTag("Enemy"))
                {
                    Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(this.Damage);
                        //Debug.Log("lôi dan");
                        Destroy(gameObject);
                    }
                    //enemy.TakeDamage(this.Damage);
                }
            }
            catch
            {   
            }
        }

    }
}