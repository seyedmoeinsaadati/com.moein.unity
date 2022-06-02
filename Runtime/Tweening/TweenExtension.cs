using System;
using System.Collections;
using UnityEngine;

namespace Moein.Tweening
{
    public static class TweenExtension
    {

        #region Position

        public static void DOPosition(this Transform self, Vector3 endValue, float duration, Ease ease, bool isLocal = true, Action OnComplete = null)
        {
        }

        public static void DOPosition(this Transform self, Vector3 endValue, float duration, AnimationCurve ease, bool isLocal = true, Action OnComplete = null)
        {
        }

        private static IEnumerator DOPositionRoutine(Transform target, Vector3 endValue, float duration,
            Ease ease, bool isLocal = true, Action OnComplete = null)
        {
            float epsilon = 0;
            Vector3 startValue = isLocal ? target.localPosition : target.position;
            if (isLocal)
            {
                while (epsilon < duration)
                {
                    epsilon += Time.deltaTime;
                    float t = epsilon / duration;
                    target.localPosition = Vector3.Lerp(startValue, endValue, ease.Evaluate(t));
                    yield return null;
                }

                target.localPosition = endValue;
            }
            else
            {
                while (epsilon < duration)
                {
                    epsilon += Time.deltaTime;
                    float t = epsilon / duration;
                    target.position = Vector3.Lerp(startValue, endValue, ease.Evaluate(t));
                    yield return null;
                }

                target.position = endValue;
            }

            OnComplete?.Invoke();
        }

        private static IEnumerator DOPositionRoutine(Transform target, Vector3 endValue, float duration,
            AnimationCurve ease, bool isLocal = true, Action OnComplete = null)
        {
            float epsilon = 0;
            Vector3 startValue = isLocal ? target.localPosition : target.position;
            if (isLocal)
            {
                while (epsilon < duration)
                {
                    epsilon += Time.deltaTime;
                    float t = epsilon / duration;
                    target.localPosition = Vector3.Lerp(startValue, endValue, ease.Evaluate(t));
                    yield return null;
                }

                target.localPosition = endValue;
            }
            else
            {
                while (epsilon < duration)
                {
                    epsilon += Time.deltaTime;
                    float t = epsilon / duration;
                    target.position = Vector3.Lerp(startValue, endValue, ease.Evaluate(t));
                    yield return null;
                }

                target.position = endValue;
            }
            OnComplete?.Invoke();
        }

        #endregion

        #region Rotation

        public static void DORotation(this Transform self, Vector3 endValue, float duration, Ease ease, bool isLocal = true, Action OnComplete = null)
        {
        }

        public static void DORotation(this Transform self, Vector3 endValue, float duration, AnimationCurve ease, bool isLocal = true, Action OnComplete = null)
        {
        }

        private static IEnumerator DORotationRoutine(Transform target, Quaternion endValue, float duration,
            Ease ease, bool isLocal = true, Action OnComplete = null)
        {
            float epsilon = 0;
            Quaternion startValue = isLocal ? target.localRotation : target.rotation;
            if (isLocal)
            {
                while (epsilon < duration)
                {
                    epsilon += Time.deltaTime;

                    float t = epsilon / duration;
                    target.localRotation = Quaternion.Slerp(startValue, endValue, ease.Evaluate(t));
                    yield return null;
                }

                target.localRotation = endValue;
            }
            else
            {
                while (epsilon < duration)
                {
                    epsilon += Time.deltaTime;

                    float t = epsilon / duration;
                    target.rotation = Quaternion.Slerp(startValue, endValue, ease.Evaluate(t));
                    yield return null;
                }

                target.rotation = endValue;
            }
            OnComplete?.Invoke();
        }

        private static IEnumerator DORotationRoutine(Transform target, Quaternion endValue, float duration,
            AnimationCurve ease, bool isLocal = true, Action OnComplete = null)
        {
            float epsilon = 0;
            Quaternion startValue = isLocal ? target.localRotation : target.rotation;
            if (isLocal)
            {
                while (epsilon < duration)
                {
                    epsilon += Time.deltaTime;
                    float t = epsilon / duration;
                    target.localRotation = Quaternion.Slerp(startValue, endValue, ease.Evaluate(t));
                    yield return null;
                }

                target.localRotation = endValue;
            }
            else
            {
                while (epsilon < duration)
                {
                    epsilon += Time.deltaTime;
                    float t = epsilon / duration;
                    target.rotation = Quaternion.Slerp(startValue, endValue, ease.Evaluate(t));
                    yield return null;
                }

                target.rotation = endValue;
            }
            OnComplete?.Invoke();
        }

        #endregion

        #region Sacle
        public static void DOSacle(this Transform self, Vector3 endValue, float duration, Ease ease, Action OnComplete = null)
        {

        }

        public static void DOSacle(this Transform self, Vector3 endValue, float duration, AnimationCurve ease, Action OnComplete = null)
        {

        }

        private static IEnumerator DOScaleRoutine(Transform target, Vector3 endValue, float duration, Ease ease, Action OnComplete = null)
        {
            float epsilon = 0;
            Vector3 startValue = target.localScale;
            while (epsilon < duration)
            {
                epsilon += Time.deltaTime;
                float t = epsilon / duration;
                target.localScale = Vector3.Slerp(startValue, endValue, ease.Evaluate(t));
                yield return null;
            }

            target.localScale = endValue;
            OnComplete?.Invoke();
        }

        private static IEnumerator DOScaleRoutine(Transform target, Vector3 endValue, float duration, AnimationCurve ease, Action OnComplete = null)
        {
            float epsilon = 0;
            Vector3 startValue = target.localScale;
            while (epsilon < duration)
            {
                epsilon += Time.deltaTime;
                float t = epsilon / duration;
                target.localScale = Vector3.Slerp(startValue, endValue, ease.Evaluate(t));
                yield return null;
            }

            target.localScale = endValue;
            OnComplete?.Invoke();
        }

        #endregion


    }
}