using System.Collections;
using UnityEngine;

namespace Moein.Core
{
    public class SlowMotion : MonoBehaviour
    {
        private float slowMotionTimeScale = .1f;
        private float slowMotionTime = .5f;
        private float fadeInTime = .5f;
        private float fadeOutTime = .5f;

        public KeyCode slowMotionKey = KeyCode.E;

        private Coroutine coroutine;

#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(slowMotionKey))
            {
                DoSlowMotion(slowMotionTimeScale, slowMotionTime, fadeInTime, fadeOutTime);
            }
        }
#endif

        public void DoSlowMotion()
        {
            if (coroutine != null) StopCoroutine(coroutine);
            coroutine = StartCoroutine(SlowMotionRoutine());
        }

        private IEnumerator SlowMotionRoutine()
        {
            yield return ToTimeScale(slowMotionTimeScale, fadeInTime);
            yield return new WaitForSecondsRealtime(slowMotionTime);
            yield return ToTimeScale(1, fadeOutTime);
        }

        private IEnumerator ToTimeScale(float timeScaleTarget, float fadeTime)
        {
            float startScale = Time.timeScale;
            float t = 0;

            while (t < 1)
            {
                t += (1.0f / fadeTime) * Time.unscaledDeltaTime;
                Time.timeScale = Mathf.Lerp(startScale, timeScaleTarget, t);
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
                yield return null;
            }

            Time.timeScale = timeScaleTarget;
            Time.fixedDeltaTime = 0.02f * timeScaleTarget;
        }

        //////////////////////////////////////////////////////
        /// STATIC MEMBERS
        //////////////////////////////////////////////////////
        public static void DoSlowMotion(float timeScale, float slowMotionTime, float fadeInTime, float fadeOutTime)
        {
            Instance.slowMotionTimeScale = timeScale;
            Instance.slowMotionTime = slowMotionTime;
            Instance.fadeOutTime = fadeOutTime;
            Instance.fadeInTime = fadeInTime;
            Instance.DoSlowMotion();
        }

        // remove here if you do not need singletone SlowMotion
        private static SlowMotion instance = null;

        public static SlowMotion Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<SlowMotion>();
                    if (instance == null)
                    {
                        instance = new GameObject().AddComponent<SlowMotion>();
                        instance.gameObject.name = instance.GetType().Name;
                    }
                }

                return instance;
            }
        }
    }
}