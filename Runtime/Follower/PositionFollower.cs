using System;
using UnityEngine;

namespace Moein.Trans.Follower
{
    public class PositionFollower : TransformFollower
    {
        [Tooltip("boolean Vector3 sync with target")] public Vector3 targetAxis = Vector3.one;
        [Tooltip("boolean Vector3 sync with target offset")] public Vector3 offsetAxis = Vector3.zero;
        [Tooltip("boolean Vector3 sync with init position")] public Vector3 maskAxis = Vector3.zero;

        Vector3 initPosition;
        Vector3 desiredPosition, smoothedPosition;

        new void Start()
        {
            base.Start();
            initPosition = transform.position;
        }

        protected override void Follow()
        {
            if (target == null)
            {
                return;
            }
            desiredPosition.x = (maskAxis.x * initPosition.x) + (targetAxis.x * target.position.x) + (offsetAxis.x * offset.x);
            desiredPosition.y = (maskAxis.y * initPosition.y) + (targetAxis.y * target.position.y) + (offsetAxis.y * offset.y);
            desiredPosition.z = (maskAxis.z * initPosition.z) + (targetAxis.z * target.position.z) + (offsetAxis.z * offset.z);

            smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }

        protected override void FixedFollow()
        {
            transform.position = smoothedPosition;
        }

        public override void SetOffset()
        {
            try
            {
                offset = transform.position - target.position;
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }

    }
}