
using TMPro;
using UnityEngine;

namespace TamQuocDefender
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI waveText;
        [SerializeField] private TextMeshProUGUI EnemyPassText;
        [SerializeField] private TextMeshProUGUI goldText;
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
    }
}