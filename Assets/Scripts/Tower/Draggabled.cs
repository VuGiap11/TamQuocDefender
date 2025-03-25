using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TamQuocDefender
{
    public class Draggabled : MonoBehaviour
    {
        [SerializeField] private bool isDragged = false;
        [SerializeField] private Vector2 mouseDragStartPosition;
        [SerializeField] private Vector2 startPos;
        public Tower tower;


        private void Start()
        {
            //isDragged = true;
            //mouseDragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //startPos = transform.position;
        }


        public void OnMouseDowns()
        {
            isDragged = true;
            mouseDragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPos = transform.position;
            //IncreaseOrderInlayer(5);
        }

        private void OnMouseDrag()
        {
            if (isDragged)
            {
                //transform.localPosition = spriteDragStartPosition + (Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseDragStartPosition);
                mouseDragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = mouseDragStartPosition;
            }
        }

        private void OnMouseUp()
        {
            isDragged = false;
            mouseDragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TowerPlace towerPlace = GameManager.instance.CheckNearPos(mouseDragStartPosition);
            if (towerPlace == null)
            {
                transform.position = startPos;
                Destroy(gameObject);
            }
            else
            {
                transform.position = towerPlace.transform.position;
            }
            //int pos = GameController.instance.CheckNearPos(mouseDragStartPosition);

            //Debug.Log(pos);
            //if (pos == -1)
            //{
            //    transform.position = startPos;
            //    Debug.Log(pos);

            //}
            //else
            //{

            //    //GameController.instance.swap(this.HeroModel, HeroModel.index, pos);
            //    //index = pos;
            //    //IncreaseOrderInlayer(1);
            //}

        }
    }
}