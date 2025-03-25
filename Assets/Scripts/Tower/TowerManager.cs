using System.Collections.Generic;
using UnityEngine;

namespace TamQuocDefender
{
    public class TowerManager : MonoBehaviour
    {
        public static TowerManager instance;
        public TowerButton TowerButtonPressed { get; set; }
        public SpriteRenderer spriteRenderer;
        private Collider2D buildTile;
        public List<GameObject> TowerList = new List<GameObject>();
        public List<Collider2D> BuildList = new List<Collider2D>();
        [SerializeField] private Transform holder;
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            buildTile = GetComponent<Collider2D>();
        }

        private void Update()
        {
            //if (Input.GetMouseButtonDown(0))
            //{
            //    Vector2 worldPointPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //    RaycastHit2D hit = Physics2D.Raycast(worldPointPosition, Vector2.zero);
            //    if (hit)
            //    {
            //        if (hit.collider.tag == "BuildPlace")
            //        {
            //            buildTile = hit.collider;
            //            buildTile.tag = "BuildPlaceFull";
            //            RegisterBuildPlace(buildTile);
            //            placeTower(hit);
            //        } 
            //    }

            //}
            //if (spriteRenderer.enabled)
            //{
            //    followMouse();
            //}
        }
        private void followMouse()
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(transform.position.x, transform.position.y);
        }
        public void RegisterBuildPlace(Collider2D other)
        {
            this.BuildList.Add(other);
        }

        public void RenameTagsBuildPlace()
        {
            foreach (Collider2D buildTag in BuildList)
            {
                buildTag.tag = "BuildSite";
            }
            BuildList.Clear();
        }
        public void RegisterTower(GameObject tower)
        {
            TowerList.Add(tower);
        }
        public void DestroyAllTower()
        {
            foreach (GameObject tower in TowerList)
            {
                Destroy(tower.gameObject);
            }
            TowerList.Clear();
        }
        //public void placeTower(RaycastHit2D hit)
        //{
        //    if (!EventSystem.current.IsPointerOverGameObject() && TowerButtonPressed != null)
        //    {
        //        GameObject newTower = Instantiate(TowerButtonPressed.TowerObject);
        //        newTower.transform.position = hit.transform.position;
        //        //newTower.transform.SetParent(this.holder, true);
        //        RegisterTower(newTower);
        //        BuyTower(TowerButtonPressed.TowerPrice);
        //        DisableDragSprite();
        //        Debug.Log("aaaaaaa");
        //    }
        //}

        //public void BuyTower(int price)
        //{
        //    GameManager.instance.ReduceGold(price);
        //}
        //public void SelectTower(TowerButton towerButton)
        ////{
        ////    if (towerButton.TowerPrice <= DataAsset.instance.levelData.LevelDatas[UserManager.instance.dataPlayerController.levelPlay].Gold)
        ////    {
        ////        this.TowerButtonPressed = towerButton;
        ////        EnableDragSprite(towerButton.DragSprite);
        ////    }

        //}

        //public void SelectTower(TowerButton towerButton)
        //{

        //    ////Vector2 worldPointPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    //Vector2 worldPointPosition = towerButton.transform.position;
        //    //RaycastHit2D hit = Physics2D.Raycast(worldPointPosition, Vector2.zero);
        //    //if (hit)
        //    //{
        //    //    if (hit.collider.tag == "BuildPlace")
        //    //    {
        //    //        buildTile = hit.collider;
        //    //        buildTile.tag = "BuildPlaceFull";
        //    //        RegisterBuildPlace(buildTile);
        //    //        placeTower(hit);
        //    //    }
        //    //}
        //    if (towerButton.TowerPrice <= UserManager.instance.dataPlayerController.gold)
        //    {
        //        GameObject newTower = Instantiate(towerButton.TowerObject);
        //        Draggabled draggable = newTower.GetComponent<Draggabled>();
        //        if (draggable == null)
        //        {
        //            draggable = newTower.AddComponent<Draggabled>();
        //        }

        //        // Bắt đầu kéo thả ngay sau khi spawn
        //        draggable.OnMouseDowns();
        //        //newTower.GetComponent<Draggabled>().OnMouseDowns();
        //        //newTower.transform.position = hit.transform.position;
        //        //newTower.transform.SetParent(this.holder, true);
        //        RegisterTower(newTower);
        //        BuyTower(TowerButtonPressed.TowerPrice);
        //        //DisableDragSprite();
        //    }

        //}

        public void EnableDragSprite(Sprite sprite)
        {
            this.spriteRenderer.enabled = true;
            this.spriteRenderer.sprite = sprite;
        }

        public void DisableDragSprite()
        {
            this.spriteRenderer.enabled = false;
            this.spriteRenderer.sprite = null;
        }
    }
}
