using UnityEngine;
using Plane = Moein.Trans.Drawing.Plane;
using UnityPlane = UnityEngine.Plane;

namespace Moein.Mirror
{
    public class PlaneMirror : Plane, IMirrorTransform
    {
        private UnityPlane mirrorPlane;
        private Vector3 closestPoint;
        private Vector3 targetPos;
        private float distanceToMirror;

        public Vector3 GetMirrorPosition(Vector3 target)
        {
            targetPos = target;
            UpdatePlane();

            //Update the transform:
            closestPoint = mirrorPlane.ClosestPointOnPlane(targetPos);
            distanceToMirror = mirrorPlane.GetDistanceToPoint(targetPos);
            return closestPoint - mirrorPlane.normal * distanceToMirror;
        }

        public Quaternion GetMirrorRotation(Quaternion target)
        {
            UpdatePlane();
            return Quaternion.LookRotation(Vector3.Reflect(target * Vector3.forward, mirrorPlane.normal),
                Vector3.Reflect(target * Vector3.up, mirrorPlane.normal));
        }

        public Vector3 GetMirrorScale(Vector3 target)
        {
            return new Vector3(transform.localScale.x * -1,
                transform.localScale.y, transform.localScale.z);
        }

        private void UpdatePlane()
        {
            mirrorPlane = new UnityPlane(transform.forward, transform.position);
        }
    }
}