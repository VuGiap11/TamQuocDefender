using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

namespace TamQuocDefender
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Image _hpBarSprite;
        public void UpdateHpBar(int maxHp, int curHp)
        {
            if (maxHp <= 0) return;
            _hpBarSprite.fillAmount = (float)curHp / (float)maxHp;
            //Debug.Log((float)curHp / (float)maxHp);
        }

        public void UpdateTime(float maxHp, float curHp)
        {
            if (maxHp <= 0) return;
            _hpBarSprite.fillAmount = curHp / maxHp;
        }
    }
}