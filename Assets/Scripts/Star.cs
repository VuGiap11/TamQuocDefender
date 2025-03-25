using System.Collections.Generic;
using UnityEngine;

namespace TamQuocDefender
{
    public class Star : MonoBehaviour
    {
        public List<GameObject> Stars = new List<GameObject>();
        public void InitStar(int star)
        {
            for (int i = 0; i < Stars.Count; i++)
            {
                Stars[i].SetActive(i>= star);
            }
        }
    }
}