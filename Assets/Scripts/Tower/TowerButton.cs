
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


namespace TamQuocDefender
{
    public class TowerButton : MonoBehaviour
    {
        [SerializeField] GameObject towerObject;
        [SerializeField] private Sprite dragSprite;
        [SerializeField] int towerPrice;


        [SerializeField] private bool isDragged = false;
        [SerializeField] private Vector2 mouseDragStartPosition;
        [SerializeField] private Vector2 startPos;
        //public int TowerPrice
        //{
        //    get
        //    {
        //        return towerPrice;
        //    }
        //    set
        //    {
        //        towerPrice = value;
        //    }
        //}

        //public GameObject TowerObject
        //{
        //    get
        //    {
        //        return towerObject;
        //    }
        //}

        //public Sprite DragSprite
        //{
        //    get
        //    {
        //        return dragSprite;
        //    }
        //}

        GameObject newTower;
        public void StartMove()
        {;
            if (towerPrice <= GameManager.instance.gold && !GameManager.instance.isSpawnTower)
            {
                 newTower = Instantiate(towerObject);
                Vector2 worldPointPosition = Camera.main.ScreenToWorldPoint(transform.position);
                Debug.Log("worldPointPosition" + worldPointPosition);
                newTower.transform.position = worldPointPosition;
                newTower.GetComponent<Tower>().isPlay = false;
                newTower.GetComponent<Tower>().towerPrice = towerPrice;
                Debug.Log(towerPrice);
                GameManager.instance.AddGold(-towerPrice);
                GameManager.instance.isSpawnTower = true;

                isDragged = true;
                mouseDragStartPosition = transform.position;
                startPos = transform.position;
            }
            else
            {
                return;
            }
          
            //IncreaseOrderInlayer(5);
        }
        public void Drag()
        {
            if (isDragged)
            {
                //mouseDragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //newTower.transform.position = mouseDragStartPosition;
                // Lấy tọa độ chuột trên màn hình và chuyển đổi sang tọa độ thế giới
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = Camera.main.WorldToScreenPoint(newTower.transform.position).z; // Đặt giá trị Z phù hợp
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                // Gán vị trí mới cho đối tượng
                newTower.transform.position = worldPosition;
            }
        }

        public void MouseUp()
        {
            if (isDragged)
            {
                GameManager.instance.isSpawnTower = false;
                isDragged = false;
                mouseDragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                TowerPlace towerPlace = GameManager.instance.CheckNearPos(mouseDragStartPosition);
                if (towerPlace == null)
                {
                    transform.position = startPos;
                    Destroy(newTower);
                    GameManager.instance.AddGold(towerPrice);
                }
                else
                {
                    SoundManager.instance.BuiltTower();
                    newTower.transform.position = towerPlace.transform.position;
                    newTower.transform.SetParent(towerPlace.transform, true);
                    towerPlace.Tower = newTower;
                    newTower.GetComponent<Tower>().isPlay = true;
                    newTower.GetComponent<Tower>().effectA.Play();
                }

            }else
            {
                return;
            }

        }
    }
}