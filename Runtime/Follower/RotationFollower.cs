using UnityEngine;

namespace Moein.Trans.Follower
{
    public class RotationFollower : TransformFollower
    {
        public bool isLookRotation;
        private Quaternion smoothedQuaternion;

        protected override void Follow()
        {
            if (isLookRotation)
                smoothedQuaternion = Quaternion.LookRotation(target.transform.position - transform.position);

            Quaternion newR = Quaternion.Euler(target.rotation.eulerAngles);
            smoothedQuaternion =
            Quaternion.Lerp(transform.rotation, newR, smoothSpeed * Time.deltaTime);
        }

        protected override void FixedFollow()
        {
            transform.rotation = smoothedQuaternion;
        }

        public override void SetOffset()
        {
            offset = Quaternion.LookRotation(target.transform.position - transform.position).eulerAngles;
        }
    }
}