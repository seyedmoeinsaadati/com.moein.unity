using UnityEngine;

namespace Moein
{
    public class Easing
    {
        public enum Ease
        {
            InSin,
            OutSin,
            InOutSin,
            InQuad,
            OutQuad,
            InOutQuad,
            InCubic,
            OutCubic,
            InOutCubic,
            InQuart,
            OutQuart,
            InOutQuart,
            InQuint,
            OutQuint,
            InOutQuint,
            InExpo,
            OutExpo,
            InOutExpo,
            InCirc,
            OutCirc,
            InOutCirc,
            InBack,
            OutBack,
            InOutBack,
            InElastic,
            OutElastic,
            InOutElastic,
            InBounce,
            OutBounce,
            InOutBounce
        }

        public static float PI = Mathf.PI;
        public static float Evaluate(float t)
        {
            return 0;
        }

        #region Sin

        private static float EaseInSin(float t)
        {
            return 1 - Mathf.Cos((t * PI) / 2);
        }
        private static float EaseOutSin(float t)
        {
            return Mathf.Sin((t * PI) / 2); ;
        }
        private static float EaseInOutSin(float t)
        {
            return -(Mathf.Cos(t * PI) - 1) / 2;
        }

        #endregion
    }
}