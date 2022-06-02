using UnityEngine;

namespace Moein.Tweening
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

        public static float Evaluate(float t)
        {
            return 0;
        }
    }
}