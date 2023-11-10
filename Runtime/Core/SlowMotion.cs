﻿using System.Collections;
using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace Moein.Core
{
    public class SlowMotion : MonoBehaviour
    {
        [SerializeField] private float fixedTimeStep = .02f;

        private float slowMotionTimeScale = .1f;
        private float slowMotionTime = .5f;
        private float fadeInTime = .5f;
        private float fadeOutTime = .5f;

        private Coroutine coroutine;

#if UNITY_EDITOR && !ENABLE_INPUT_SYSTEM
        public KeyCode slowMotionKey = KeyCode.E;

        private void Update()
        {
            if (Input.GetKeyDown(slowMotionKey))
            {
                DoSlowMotion(slowMotionTimeScale, slowMotionTime, fadeInTime, fadeOutTime);
            }
        }
#endif

#if UNITY_EDITOR && ENABLE_INPUT_SYSTEM
        private void Update()
        {
            if (Keyboard.current.backslashKey.wasPressedThisFrame)
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
                Time.fixedDeltaTime = fixedTimeStep * Time.timeScale;
                yield return null;
            }

            Time.timeScale = timeScaleTarget;
            Time.fixedDeltaTime = fixedTimeStep * timeScaleTarget;
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