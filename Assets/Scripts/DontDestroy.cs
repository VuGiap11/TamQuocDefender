using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TamQuocDefender
{
    public class DontDestroy : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}