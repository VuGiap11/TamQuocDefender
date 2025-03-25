using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TamQuocDefender
{
    public class DoorSelect : MonoBehaviour
    {
        public GameObject DoorClose;
        public TextMeshProUGUI levelText;
        //public int Level;
        public Level level;
        public Button buttonSelect;
        public GameObject Star;
        public void Init()
        {
            //levelText.text = Level.ToString();
            //if (Level > UserManager.instance.dataPlayerController.level)
            //{
            //   DoorClose.SetActive(true);
            //    buttonSelect.interactable = false;
            //}
            //else
            //{
            //    DoorClose.SetActive(false);
            //    buttonSelect.interactable = true;
            //}
            levelText.text = (this.level.level + 1).ToString();
            if (this.level.isOpen)
            {
                this.DoorClose.SetActive(false);
                this.buttonSelect.interactable = true;
                this.Star.SetActive(true);
                this.Star.GetComponent<Star>().InitStar(this.level.Star);
            }
            else
            {
                DoorClose.SetActive(true);
                buttonSelect.interactable = false;
                this.Star.SetActive(false);
            }
        }
        public void ChooseLevel()
        {
            //Debug.Log("level" + Level);
            MainMenuController.Instance.SelectLevel(this.level.level);
        }
    }
}