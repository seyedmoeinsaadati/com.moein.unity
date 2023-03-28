using UnityEngine;

namespace Moein.Core
{
    public abstract class SecondOrderSystemBase<T>
    {
        public const float PI = Mathf.PI;

        protected T xp;
        protected T y, yd;

        protected float k1, k2, k3;
        protected float T_crit;
        protected float w;

        protected SecondOrderSystemBase(T initializeState, float f, float z, float r)
        {
            w = (2 * PI * f);
            k1 = z / (PI * f);
            k2 = 1 / (w * w);
            k3 = r * z / w;

            T_crit = .8f * (Mathf.Sqrt(4 * k2 + k1 * k1) - k1);

            xp = initializeState;
            y = initializeState;
            yd = default;
        }

        /// <param name="x">currentState</param>
        /// <param name="xd">derivation of currentState </param>
        public abstract T Update(float deltaTime, T x, T xd = default);
    }
}