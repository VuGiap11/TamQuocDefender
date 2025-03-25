using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TamQuocDefender
{
    public class BackGround : MonoBehaviour
    { 
        public Transform[] WaysPoint;
        public int id;
        private SpriteRenderer spriteRenderer;
        public List<TowerPlace> towerPlaces = new List<TowerPlace>();
        private void Start()
        {
            //ScaleBackGround();
        }

        [ContextMenu("ScaleBackGround")]
        public void ScaleBackGround()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            float cameraHeight = 2f * Camera.main.orthographicSize;
            float cameraWidth = cameraHeight * (float)Screen.width / (float)Screen.height;

            float bgHeight = spriteRenderer.bounds.size.y;
            float bgWidth = spriteRenderer.bounds.size.x;

            float scaleX = cameraWidth / bgWidth;
            float scaleY = cameraHeight / bgHeight;

            transform.localScale = new Vector3(scaleX, scaleY, 1);
            //spriteRenderer = GetComponent<SpriteRenderer>();

            //// Lấy chiều rộng và chiều cao của màn hình (tính theo đơn vị world space)
            //float screenHeight = Screen.height;
            //float screenWidth = Screen.width;

            //// Chuyển đổi chiều rộng và chiều cao màn hình từ pixel sang world space
            //float screenHeightWorld = screenHeight / Screen.dpi; // DPI là dots per inch (điểm mỗi inch)
            //float screenWidthWorld = screenWidth / Screen.dpi;

            //// Lấy kích thước của sprite
            //float bgHeight = spriteRenderer.bounds.size.y;
            //float bgWidth = spriteRenderer.bounds.size.x;

            //// Tính tỷ lệ cần thiết để sprite vừa vặn với màn hình
            //float scaleX = screenWidthWorld / bgWidth;
            //float scaleY = screenHeightWorld / bgHeight;

            //// Điều chỉnh tỷ lệ của đối tượng
            //transform.localScale = new Vector3(scaleX, scaleY, 1);
        }
    }
}