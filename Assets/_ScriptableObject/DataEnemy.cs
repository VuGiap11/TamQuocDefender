
using UnityEngine;
namespace TamQuocDefender
{
    [CreateAssetMenu(fileName = "Data", menuName = "DataEnemy")]
    public class DataEnemy : ScriptableObject
    {
        public string Id;
        public int Hp;
        public float Speed;
        public int Gold;
    }

}