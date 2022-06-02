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

        #region Quad

        private static float EaseInQuad(float t)
        {
            return t * t;
        }
        private static float EaseOutQuad(float t)
        {
            return 1 - (1 - t) * (1 - t);
        }
        private static float EaseInOutQuad(float t)
        {
            return t < 0.5 ? 2 * t * t : 1 - Mathf.Pow(-2 * t + 2, 2) / 2;
        }

        #endregion

        #region Cubic
        
        private static float EaseInCubic(float t)
        {
            return t * t * t;
        }
        private static float EaseOutCubic(float t)
        {
            return 1 - Mathf.Pow(1 - t, 3);
        }
        private static float EaseInOutCubic(float t)
        {
            return t < 0.5 ? 4 * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 3) / 2;
        }

        #endregion
    }
}