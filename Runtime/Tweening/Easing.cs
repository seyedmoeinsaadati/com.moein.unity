using UnityEngine;

namespace Moein
{
    public static class Easing
    {
        public enum Ease
        {
            Linear,
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

        private static readonly float PI = Mathf.PI;
        private static readonly float c1 = 1.70158f;
        private static readonly float c2 = c1 * 1.525f;
        private static readonly float c3 = c1 + 1;
        private static readonly float c4 = (2 * PI) / 3;
        private static readonly float c5 = (2 * PI) / 4.5f;
        private static readonly float n1 = 7.5625f;
        private static readonly float d1 = 2.75f;

        public static float Evaluate(this Ease self, float t)
        {
            switch (self)
            {
                case Ease.Linear:
                    return EaseLinear(t);
                case Ease.InSin:
                    return EaseInSin(t);
                case Ease.OutSin:
                    return EaseOutSin(t);
                case Ease.InOutSin:
                    return EaseInOutSin(t);
                case Ease.InQuad:
                    return EaseInQuad(t);
                case Ease.OutQuad:
                    return EaseOutQuad(t);
                case Ease.InOutQuad:
                    return EaseInOutQuad(t);
                case Ease.InCubic:
                    return EaseInCubic(t);
                case Ease.OutCubic:
                    return EaseOutCubic(t);
                case Ease.InOutCubic:
                    return EaseInOutCubic(t);
                case Ease.InQuart:
                    return EaseInQuart(t);
                case Ease.OutQuart:
                    return EaseOutQuart(t);
                case Ease.InOutQuart:
                    return EaseInOutQuart(t);
                case Ease.InQuint:
                    return EaseInQuint(t);
                case Ease.OutQuint:
                    return EaseOutQuint(t);
                case Ease.InOutQuint:
                    return EaseInOutQuint(t);
                case Ease.InExpo:
                    return EaseInExpo(t);
                case Ease.OutExpo:
                    return EaseOutExpo(t);
                case Ease.InOutExpo:
                    return EaseInOutExpo(t);
                case Ease.InCirc:
                    return EaseInCirc(t);
                case Ease.OutCirc:
                    return EaseOutCirc(t);
                case Ease.InOutCirc:
                    return EaseInOutCirc(t);
                case Ease.InBack:
                    return EaseInBack(t);
                case Ease.OutBack:
                    return EaseOutBack(t);
                case Ease.InOutBack:
                    return EaseInOutBack(t);
                case Ease.InElastic:
                    return EaseInElastic(t);
                case Ease.OutElastic:
                    return EaseOutElastic(t);
                case Ease.InOutElastic:
                    return EaseInOutElastic(t);
                case Ease.InBounce:
                    return EaseInBounce(t);
                case Ease.OutBounce:
                    return EaseOutBounce(t);
                case Ease.InOutBounce:
                    return EaseInOutBounce(t);
                default:
                    return 0;
            }

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

        #region Quart

        private static float EaseInQuart(float t)
        {
            return t * t * t * t;
        }
        private static float EaseOutQuart(float t)
        {
            return 1 - Mathf.Pow(1 - t, 4);
        }
        private static float EaseInOutQuart(float t)
        {
            return t < 0.5 ? 8 * t * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 4) / 2;
        }

        #endregion

        #region Quint

        private static float EaseInQuint(float t)
        {
            return t * t * t * t * t;
        }

        private static float EaseOutQuint(float t)
        {
            return 1 - Mathf.Pow(1 - t, 5);
        }

        private static float EaseInOutQuint(float t)
        {
            return t < 0.5 ? 16 * t * t * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 5) / 2;
        }

        #endregion

        #region Expo

        private static float EaseInExpo(float t)
        {
            return t == 0 ? 0 : Mathf.Pow(2, 10 * t - 10);
        }

        private static float EaseOutExpo(float t)
        {
            return t == 1 ? 1 : 1 - Mathf.Pow(2, -10 * t);
        }

        private static float EaseInOutExpo(float t)
        {
            return t == 0 ? 0 :
                t == 1 ? 1 :
                t < 0.5 ? Mathf.Pow(2, 20 * t - 10) / 2 :
                (2 - Mathf.Pow(2, -20 * t + 10)) / 2;
        }

        #endregion

        #region Circ

        private static float EaseInCirc(float t)
        {
            return 1 - Mathf.Sqrt(1 - Mathf.Pow(t, 2));
        }

        private static float EaseOutCirc(float t)
        {
            return Mathf.Sqrt(1 - Mathf.Pow(t - 1, 2));
        }

        private static float EaseInOutCirc(float t)
        {
            return t < 0.5 ? (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * t, 2))) / 2
            : (Mathf.Sqrt(1 - Mathf.Pow(-2 * t + 2, 2)) + 1) / 2;
        }

        #endregion

        #region Back

        private static float EaseInBack(float t)
        {
            return c3 * t * t * t - c1 * t * t;
        }

        private static float EaseOutBack(float t)
        {
            return 1 + c3 * Mathf.Pow(t - 1, 3) + c1 * Mathf.Pow(t - 1, 2);
        }

        private static float EaseInOutBack(float t)
        {
            return t < 0.5 ? (Mathf.Pow(2 * t, 2) * ((c2 + 1) * 2 * t - c2)) / 2
            : (Mathf.Pow(2 * t - 2, 2) * ((c2 + 1) * (t * 2 - 2) + c2) + 2) / 2;
        }

        #endregion

        #region Elastic

        private static float EaseInElastic(float t)
        {
            return t == 0 ? 0
            : t == 1 ? 1
            : -Mathf.Pow(2, 10 * t - 10) * Mathf.Sin(t * 10 - 10.75f) * c4;
        }

        private static float EaseOutElastic(float t)
        {
            return t == 0 ? 0
            : t == 1 ? 1
            : Mathf.Pow(2, -10 * t) * Mathf.Sin((t * 10 - 0.75f) * c4) + 1;
        }

        private static float EaseInOutElastic(float t)
        {
            return t == 0 ? 0
              : t == 1 ? 1
              : t < 0.5 ? -(Mathf.Pow(2, 20 * t - 10) * Mathf.Sin((20 * t - 11.125f) * c5)) / 2
              : (Mathf.Pow(2, -20 * t + 10) * Mathf.Sin((20 * t - 11.125f) * c5)) / 2 + 1;
        }

        #endregion

        #region Bounce

        private static float EaseInBounce(float t)
        {
            return 1 - EaseOutBounce(1 - t);
        }

        private static float EaseOutBounce(float t)
        {
            if (t < 1 / d1) return n1 * t * t;
            else if (t < 2 / d1) return n1 * (t -= 1.5f / d1) * t + 0.75f;
            else if (t < 2.5f / d1) return n1 * (t -= 2.25f / d1) * t + 0.9375f;
            else return n1 * (t -= 2.625f / d1) * t + 0.984375f;
        }

        private static float EaseInOutBounce(float t)
        {
            return t < 0.5 ? (1 - EaseOutBounce(1 - 2 * t)) / 2
            : (1 + EaseOutBounce(2 * t - 1)) / 2;
        }

        #endregion

        private static float EaseLinear(float t)
        {
            return t;
        }

    }

}