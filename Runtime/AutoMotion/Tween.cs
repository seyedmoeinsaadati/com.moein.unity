using System.Collections;
using UnityEngine;

namespace Moein.Trans.AutoMotion
{
    public static class Tween
    {
        private static IEnumerator DOPosition(Transform target, Vector3 endValue, float duration,
            bool isLocal = true)
        {
            float epsilon = 0;
            Vector3 startValue = isLocal ? target.localPosition : target.position;
            if (isLocal)
            {
                while (epsilon < duration)
                {
                    epsilon += Time.deltaTime;

                    float t = epsilon / duration;
                    target.localPosition = Vector3.Lerp(startValue, endValue, t);
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
                    target.position = Vector3.Lerp(startValue, endValue, t);
                    yield return null;
                }

                target.position = endValue;
            }
        }

        private static IEnumerator DOPosition(Transform target, Vector3 endValue, float duration, AnimationCurve ease,
            bool isLocal = true)
        {
            float epsilon = 0;
            Vector3 startValue = isLocal ? target.localPosition : target.position;
            if (isLocal)
            {
                while (epsilon < duration)
                {
                    epsilon += Time.deltaTime;

                    float t = epsilon / duration;
                    t = ease.Evaluate(t);
                    target.localPosition = Vector3.Lerp(startValue, endValue, t);
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
                    t = ease.Evaluate(t);
                    target.position = Vector3.Lerp(startValue, endValue, t);
                    yield return null;
                }

                target.position = endValue;
            }
        }

        private static IEnumerator DORotation(Transform target, Quaternion endValue, float duration,
            bool isLocal = true)
        {
            float epsilon = 0;
            Quaternion startValue = isLocal ? target.localRotation : target.rotation;
            if (isLocal)
            {
                while (epsilon < duration)
                {
                    epsilon += Time.deltaTime;

                    float t = epsilon / duration;
                    target.localRotation = Quaternion.Slerp(startValue, endValue, t);
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
                    target.rotation = Quaternion.Slerp(startValue, endValue, t);
                    yield return null;
                }

                target.rotation = endValue;
            }
        }

        private static IEnumerator DORotation(Transform target, Quaternion endValue, float duration,
            AnimationCurve ease,
            bool isLocal = true)
        {
            float epsilon = 0;
            Quaternion startValue = isLocal ? target.localRotation : target.rotation;
            if (isLocal)
            {
                while (epsilon < duration)
                {
                    epsilon += Time.deltaTime;

                    float t = epsilon / duration;
                    t = ease.Evaluate(t);
                    target.localRotation = Quaternion.Slerp(startValue, endValue, t);
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
                    t = ease.Evaluate(t);
                    target.rotation = Quaternion.Slerp(startValue, endValue, t);
                    yield return null;
                }

                target.rotation = endValue;
            }
        }
    }
}