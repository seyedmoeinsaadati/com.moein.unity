using System.Collections;
using UnityEngine;

namespace Moein.Core
{
    public class SlowMotion : MonoBehaviour
    {
        public float slowMotionFactor;

        [Tooltip("after time(s) starts to back normal")]
        public float slowMotionLenght;

        [Tooltip("back to normal duration")] public float bulletTimeLenght;

        public KeyCode slowMotionKey = KeyCode.E;

        public void DoSlowMotion()
        {
            Time.timeScale = slowMotionFactor;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            Invoke("BackToNormal", slowMotionLenght * Time.timeScale);
        }

        public void BackToNormal()
        {
            StartCoroutine(BackToNormalCoroutine());
        }

        void Update()
        {
            if (Input.GetKeyDown(slowMotionKey))
            {
                DoSlowMotion();
            }
        }

        private IEnumerator BackToNormalCoroutine()
        {
            while (true)
            {
                Time.timeScale += (1.0f / bulletTimeLenght) * Time.unscaledDeltaTime;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
                // check state
                if (Time.timeScale >= 1)
                {
                    // normal state
                    Time.timeScale = 1;
                    Time.fixedDeltaTime = 0.02f;
                    break;
                }

                yield return null;
            }
        }
    }
}