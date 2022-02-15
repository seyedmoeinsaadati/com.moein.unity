using UnityEngine;

namespace Moein.Core
{
    [System.Serializable]
    public class Curve3
    {
        public AnimationCurve x, y, z;
        public Vector3 Evaluate(float t)
        {
            return new Vector3(x.Evaluate(t), y.Evaluate(t), z.Evaluate(t));
        }
    }
}