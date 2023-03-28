using UnityEngine;

namespace Moein.Core
{
    public class SecondOrderSystem_Vector3 : SecondOrderSystemBase<Vector3>
    {
        public SecondOrderSystem_Vector3(Vector3 initializeState, float f, float z, float r) : base(initializeState, f, z, r)
        {
        }

        public override Vector3 Update(float T, Vector3 x, Vector3 xd = default)
        {
            if (xd == default)
            {
                xd = (x - xp) / T;
                xp = x;
            }

            float kStable;
            kStable = Mathf.Max(k2, T * T / 2 + T * k1 / 2, T * k1);

            int iterations = (int)Mathf.Ceil(T / T_crit);
            T /= iterations;
            for (int i = 0; i < iterations; i++)
            {
                y += T * yd;
                yd += T * (x + k3 * xd - y - k1 * yd) / kStable;
            }

            return y;
        }
    }
}