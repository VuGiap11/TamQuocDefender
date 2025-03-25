using TMPro;
using UnityEngine;
using System.Collections;

namespace TamQuocDefender
{
    public class FpsManager : MonoBehaviour
    {
        //[SerializeField] private TextMeshProUGUI fpsText;
        //public float deltaTime;

        //public void Update()
        //{
        //    deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        //    float fps = 1.0f / deltaTime;
        //    fpsText.text = Mathf.Ceil(fps).ToString();
        //}public int FramesPerSec { get; protected set; }
        public int FramesPerSec { get; protected set; }
        [SerializeField] private float frequency = 1f;


        public TextMeshProUGUI counter;

        private void Start()
        {
            StartCoroutine(FPS());
        }

        private IEnumerator FPS()
        {
            for (; ; )
            {
                int lastFrameCount = Time.frameCount;
                float lastTime = Time.realtimeSinceStartup;
                yield return new WaitForSeconds(frequency);

                float timeSpan = Time.realtimeSinceStartup - lastTime;
                int frameCount = Time.frameCount - lastFrameCount;

                FramesPerSec = Mathf.RoundToInt(frameCount / timeSpan);
                counter.text = "FPS: " + FramesPerSec.ToString();
            }
        }
    }
}